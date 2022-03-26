using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;
public class Context : IdentityDbContext
{
    public DbSet<EPerson> Persons { get; set; }
    
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }
}