using Microsoft.EntityFrameworkCore;
using System;
using WAD.WebApp._7947.DAL.DTO;

namespace WAD.WebApp._7947.DAL
{
    public class PizzaStoreDbContext : DbContext
    {
        public PizzaStoreDbContext(DbContextOptions<PizzaStoreDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }

    }
}
