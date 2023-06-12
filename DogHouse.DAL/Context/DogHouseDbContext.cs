using DogHouse.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DogHouse.DAL.Context;

public class DogHouseDbContext : DbContext
{
    public DbSet<Dog> Dogs { get; set; }
    
    public DogHouseDbContext(DbContextOptions options) : base(options) { }
}