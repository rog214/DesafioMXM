using crudAPI.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace crudAPI.Domain;

public class AppDbContext : DbContext
{
    public DbSet<User> Users   { get; set; }
    
    public AppDbContext(DbContextOptions <AppDbContext> options)
     : base (options)
    {
    }
}