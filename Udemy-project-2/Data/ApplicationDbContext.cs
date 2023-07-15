﻿using Microsoft.EntityFrameworkCore;
using Udemy_project_2.Models;

namespace Udemy_project_2.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options):base(options)
        {
            

        }
        public DbSet<category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<category>().HasData(
                new category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new category { Id = 3, Name = "History", DisplayOrder = 3 }
                );
        }

    }
}
