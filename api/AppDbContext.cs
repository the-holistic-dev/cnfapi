using System;
using System.Data.Common;
using api.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace api;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public DbSet<Food> Foods { get; set; }
    public DbSet<FoodGroup> FoodGroups { get; set; }
    public DbSet<Measure> Measures { get; set; }
    public DbSet<ConversionFactor> Factors { get; set; }
    public DbSet<Nutrient> Nutrients { get; set; }
    public DbSet<FoodNutrient> FoodNutrients { get; set; }
}
