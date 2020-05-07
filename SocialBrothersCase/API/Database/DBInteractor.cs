using Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Database
{
    public class DBInteractor: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = SocialBrothers.db;");
        }


        public DbSet<Address> Address { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().HasData(
            new Address() { Id = 1, HouseNumber = 11, Country = "Netherlands", City = "Woerden", Street = "Forintdreef", PostalCode = "3446XP" },
            new Address() { Id = 2, HouseNumber = 5, Country = "Netherlands", City = "Woerden", Street = "Eendrachtstraat", PostalCode = "3441AP" },
            new Address() { Id = 3, HouseNumber = 86, Country = "Netherlands", City = "Woerden", Street = "Kallameer", PostalCode = "3446JG" },
            new Address() { Id = 4, HouseNumber = 2, Country = "Netherlands", City = "Woerden", Street = "Tornemeer", PostalCode = "3446JL" }
            );
        }
    }
}
