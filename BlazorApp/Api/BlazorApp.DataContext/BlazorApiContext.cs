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
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }

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
                //entity.HasMany(x => x.ActorMovies).WithOne(x => x.Movie);
            });

            builder.Entity<Actor>(entity =>
            {
                entity.HasKey(x => x.Id);
                //entity.HasMany(x => x.ActorMovies).WithOne(x => x.Actor);
            });

            builder.Entity<ActorMovie>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Movie).WithMany(x => x.ActorMovies).HasForeignKey(x => x.MovieId);
                entity.HasOne(x => x.Actor).WithMany(x => x.ActorMovies).HasForeignKey(x => x.ActorId);
            });
        }
    }
}
