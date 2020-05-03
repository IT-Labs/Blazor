using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Core.Shared;
using Core.Shared.Interfaces;
using Core.Framework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Framework.Repository
{
    public class EntityTypeConfiguration
    {
        public void Map(EntityTypeBuilder builder, bool usePlural, string schema = "public", bool isTablePerHierarchy = false)
        {
            var tableName = builder.Metadata.ClrType.Name.ToPostgreConvention();
            builder.ToTable(usePlural ? tableName.ToPlural() : tableName, schema);
            var proper = new List<IMutableProperty>(builder.Metadata.GetProperties());
            List<PropertyInfo> properties = new List<PropertyInfo>();
            if (isTablePerHierarchy)
            {
                properties = builder.Metadata.ClrType.GetProperties(BindingFlags.Public
              |
              BindingFlags.DeclaredOnly |
              BindingFlags.Instance).ToList();
            }

            foreach (var mutableProperty in proper)
            {
                var name = mutableProperty.Name;
                if (isTablePerHierarchy && properties.All(x => x.Name != name))
                {
                    //exclude all columns from parent table because EF add all child tables properties in parent table  at this moment
                    continue;
                }
                builder.Property(name).HasColumnName(builder.Property(name).Metadata.Name.ToPostgreConvention());
            }
        }
    }
}