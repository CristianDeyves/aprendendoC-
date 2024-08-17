using CrudApi.Estudantes;
using Microsoft.EntityFrameworkCore;

namespace CrudApi.Data
{
    public class AppDbContext : DbContext
    {
        DbSet<Estudante> Estudantes { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
    }
}