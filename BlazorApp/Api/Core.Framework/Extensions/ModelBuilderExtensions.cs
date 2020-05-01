using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Framework.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Framework.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder, TypeConfiguration<TEntity> configuration, List<PropertyInfo> dbsets, string schema)
            where TEntity : class
        {
            var modelType = typeof(TEntity);
            var isInDbSet = dbsets.Any(x => x.PropertyType.GenericTypeArguments.First() == modelType);
            var baseType = modelType.GetTypeInfo().BaseType;
            if (!isInDbSet )
            {
                var isTablePerHierarchy = dbsets.Any(x =>x.PropertyType.GenericTypeArguments.First() == baseType);
                var entityTypeConfiguration = new EntityTypeConfiguration();
                entityTypeConfiguration.Map(modelBuilder.Entity<TEntity>(), true, schema, isTablePerHierarchy);
               
            }
            configuration.Map(modelBuilder.Entity<TEntity>());
        }

        public static PropertyBuilder HasColumnName(this PropertyBuilder property)
        {
            
            property.HasColumnName(property.Metadata.Name.ToPostgreConvention());
            
            return property;
        }

        public static string ToPostgreConvention(this string value, string splitter = "_")
        {
            var result = string.Empty;

            for (int index = 0; index < value.Length; index++)
            {
                var ch = value[index];
                if (index == 0)
                {
                    result += ch.ToString().ToLower();
                    continue;
                }

                if (char.IsLower(ch))
                {
                    result += ch.ToString();
                }
                else if (string.IsNullOrEmpty(result))
                    result += ch.ToString();
                else
                    result += splitter + ch.ToString().ToLower();
            }

            return result;
        }

        public static string ToPlural(this string value)
        {
            string result = value;
            var ysNotToBeReplaced = new string[] {"ay", "ey", "iy", "oy", "uy"};

            if (result.EndsWith("s") || result.EndsWith("x") || result.EndsWith("z") || result.EndsWith("ch") || result.EndsWith("sh"))
            {
                result += "es";
            }
            else
            {
                if (result.EndsWith("y") && !ysNotToBeReplaced.Contains(result.Substring(result.Length - 2)))
                {
                    result = result.Remove(result.Length - 1);
                    result += "ies";
                }
                else
                {
                    result += "s";
                }
            }
            return result;
        }

        /// <summary>
        /// Apply default model binding for all DbSet<> in contexts
        /// All other tables must have custom call to EntityTypeConfiguration.Map method
        /// or create Table mapping class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="modelBuilder"></param>
        /// <param name="schema"></param>
        public static void DefaultModelBinding(this DbContext context, ModelBuilder modelBuilder, string schema = "public", bool usePlural = true)
        {
            var entityTypeConfiguration = new EntityTypeConfiguration();

            var properties = context.GetContextDbSetPropertyInfos();
            foreach (var property in properties)
            {
                var entityType = Enumerable.First<Type>(property.PropertyType.GenericTypeArguments);
                //var isTablePerHierarchy = Enumerable.Any<PropertyInfo>(properties, 
                //    x => Enumerable.First<Type>(x.PropertyType.GenericTypeArguments) 
                //    == entityType.GetTypeInfo().BaseType);
                
                entityTypeConfiguration.Map(modelBuilder.Entity(entityType), usePlural, schema);
               
            }
        }

        public  static List<PropertyInfo> GetContextDbSetPropertyInfos(this DbContext context)
        {
            return context.GetType().GetProperties().Where(x => x.PropertyType.IsConstructedGenericType)
                .Where(x=> (typeof(DbSet<>).IsAssignableFrom(x.PropertyType.GetGenericTypeDefinition())))
                .ToList();
        }

    }
}