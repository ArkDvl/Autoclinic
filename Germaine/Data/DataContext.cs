using Microsoft.EntityFrameworkCore;
using Germaine.Models;

namespace Germaine.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}

        public DbSet<User> Users { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<Activity> Activities { get; set; }

        public DbSet<Modules> Modules { get; set; }
    }
}