using Core.Shared.Interfaces;
using Core.Shared.Repository;
using Lamar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Core.Framework
{
    public abstract class StartupConfiguration : IStartupConfiguration
    {
        public string CorsPolicy { get; }
        public IConfigurationRoot Configuration { get; protected set; }
        protected ILoggerFactory LoggerFactory { get; private set; }


        protected StartupConfiguration(string corsPolicy, IWebHostEnvironment env)
        {
            CorsPolicy = corsPolicy;

            var builder = new ConfigurationBuilder();

            //builder.AddApplicationInsightsSettings(env.IsDevelopment());
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureContainer(ServiceRegistry services)
        {
            services = ConfigureAdditionalServices(services);
            services.AddCors(options => options.AddPolicy(CorsPolicy,
                builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("X-Authorization")
            ));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();

            services.AddSingleton(provider => Configuration);

            /*services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var authority = $"https://cognito-idp.us-east-1.amazonaws.com/{AWSCognitoSettings.UserPoolId}";
                    var audience = AWSCognitoSettings.UserPoolClientId;

                    options.Audience = audience;
                    options.Authority = authority;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authority,
                        ValidAudience = audience,
                        IssuerSigningKey = new CognitoSigningKey(AWSCognitoSettings.UserPoolClientSecret).ComputeKey()
                    };
                });

            services.AddAuthorizationCore(options =>
            {
                foreach (var value in EnumExtensions.GetValues<Permissions>())
                {
                    options.AddPolicy(value.ToString(), policy => policy.Requirements.Add(value.ToRequirement()));
                }

            });*/

            services.AddControllers(options =>
            {
                options.Filters.Add(new RequestFilter());
                options.Filters.Add(new ResponseFilter());
                options.Filters.Add(new CustomExceptionFilter(services.BuildServiceProvider().GetService<ILogger<CustomExceptionFilter>>()));
                options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
            })
            .AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                x.SerializerSettings.Converters.Add(new CustomStringToEnumConverter());
                x.SerializerSettings.Converters.Add(new DateTimeConverter());
            });

            services.AddSwaggerGen(c =>
            {
                CreateSwaggerGenOptions(c, Assembly.GetEntryAssembly());
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (xmlPath != null && File.Exists(xmlPath)) { c.IncludeXmlComments(xmlPath); }
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        public virtual ServiceRegistry ConfigureAdditionalServices(ServiceRegistry services)
        {
            return services;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            LoggerFactory = loggerFactory;
            app.UseCors(CorsPolicy);
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            var container = (Lamar.IContainer)app.ApplicationServices;
            IoC.Initialize(container);

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseSwagger();

            var entryAssembly = Assembly.GetEntryAssembly();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{entryAssembly.GetName().Name} Api V1");
            });

            app.UseEndpoints(routes =>
                routes.MapControllerRoute(CorsPolicy, "{controller}/{Id?}")
            );

            if (!env.IsProduction())
            {
                app.Run(async x =>
                {
                    var text = $"{entryAssembly.GetName().Name} Under Construction!";
                    Console.WriteLine(text);
                    await x.Response.WriteAsync(text);
                });
            }

            var context = container.GetInstance<IContext>();
            SeedInMemoryDatabase(context);
        }

        private void SeedInMemoryDatabase(IContext context)
        {
            var movies = TestData.MoviesTestData();
            var actors = TestData.ActorsTestData();
            context.Insert(movies);
            context.Insert(actors);
            context.Insert(TestData.ActorMoviesTestData(movies, actors));
        }

        public virtual ServiceRegistry AdditionalContainerConfiguration(ServiceRegistry services)
        {
            
            return services;
        }

        public SwaggerGenOptions CreateSwaggerGenOptions(SwaggerGenOptions c, Assembly entryAssembly)
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = $"{entryAssembly.GetName().Name} Service Api", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "Authorization"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });

            c.DescribeAllParametersInCamelCase();
            return c;
        }
    }
}