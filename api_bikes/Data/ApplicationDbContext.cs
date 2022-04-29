using System;
using api_bikes.Models;
using Microsoft.EntityFrameworkCore;

namespace api_bikes.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {//añadir lo de dentro de ApplicationDbContext
        }

        
        public DbSet<Bike> Bike { get; set; }
        public DbSet<BikeType> BikeType { get; set; }
        public DbSet<PreSale> PreSale  { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlServer("Server=localhost,1433; Database=BikesDb; User=sa; Password=Bootcamp18");
    }
}