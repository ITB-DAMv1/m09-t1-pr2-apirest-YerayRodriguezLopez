﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using T1PR2_APIREST.Models;

namespace T1PR2_APIREST.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<Game> Games { get; set; }
        public DbSet<FavGames> FavGames { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Game>()
                .HasMany(g => g.Votes)
                .WithOne(v => v.Game)
                .HasForeignKey(v => v.GameId);

            builder.Entity<User>()
                .HasMany(u => u.Votes)
                .WithOne(v => v.User)
                .HasForeignKey(v => v.UserId);

            builder.Entity<FavGames>()
                .HasIndex(v => new { v.UserId, v.GameId })
                .IsUnique();
        }
    }
}
