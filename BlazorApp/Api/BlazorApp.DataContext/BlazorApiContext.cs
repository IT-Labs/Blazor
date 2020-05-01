using BlazorApp.Shared.Entities;
using Core.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.DataContext
{
    public class BlazorApiContext : CoreDataContext
    {
        public DbSet<Movie> Movies { get; set; }

        public BlazorApiContext(DbContextOptions options, ILogger<BlazorApiContext> logger)
            : base(options, logger)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Movie>(entity =>
            {
                entity.HasKey(x => x.Id);
            });
        }
    }
}
