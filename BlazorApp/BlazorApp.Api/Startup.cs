using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace BlazorApp.Api
{
	public class Startup
	{
		private string _cors = "cors";
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(_cors,
				builder =>
				{
					builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
				});
			});

			services.AddControllersWithViews();

			services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
				.AddAzureAD(options => Configuration.Bind("AzureAd", options));

			services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme,
			options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						// Instead of using the default validation (validating against a 
						// single issuer value, as we do in
						// line of business apps), we inject our own multitenant 
						// validation logic
						ValidateIssuer = false,
						// If the app is meant to be accessed by entire organizations, 
						// add your issuer validation logic here.
						// IssuerValidator = 
						// (issuer, securityToken, validationParameters) => {
						//    if (myIssuerValidationLogic(issuer)) return issuer;
						//}
					};
					options.Events = new OpenIdConnectEvents
					{
						OnTicketReceived = context =>
						{
							// This is called on successful authentication
							// This is an opportunity to write to the database 
							// or alter internal roles ect.
							return Task.CompletedTask;
						},
						OnAuthenticationFailed = context =>
						{
							context.Response.Redirect("/Error");
							context.HandleResponse(); // Suppress the exception
							return Task.CompletedTask;
						}
					};
				});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseCors(_cors);

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();


			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
