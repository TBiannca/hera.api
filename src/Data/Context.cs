using Microsoft.EntityFrameworkCore;

namespace Data;
public class Context : DbContext
{
    public DbSet<EPerson> Persons { get; set; }
    
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
}