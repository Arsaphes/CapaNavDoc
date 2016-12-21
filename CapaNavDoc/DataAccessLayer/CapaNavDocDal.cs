using System.Data.Entity;
using CapaNavDoc.Models;


namespace CapaNavDoc.DataAccessLayer
{
    public class CapaNavDocDal:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Center> Centers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Center>().ToTable("Centers");

            base.OnModelCreating(modelBuilder);
        }



    }
}
