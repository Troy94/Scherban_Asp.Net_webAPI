using Fenrir_WebApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace Fenrir_WebApplication.DBcontext
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public PersonDbContext(DbContextOptions<PersonDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Person>().ToTable("Persons");
        }
    }
}
