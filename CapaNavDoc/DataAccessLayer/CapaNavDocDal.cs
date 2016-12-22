using System.Data.Entity;
using CapaNavDoc.Models;


namespace CapaNavDoc.DataAccessLayer
{
    public class CapaNavDocDal:DbContext
    {
        public DbSet<Center> Centers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<EquipmentMonitoring> EquipmentMonitorings { get; set; }
        public DbSet<EquipmentCenter> EquipmentCenters { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Action> Actions { get; set; }  
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Center>().ToTable("Centers");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<EquipmentMonitoring>().ToTable("EquipmentMonitorings");
            modelBuilder.Entity<EquipmentCenter>().ToTable("EquipmentCenters");
            modelBuilder.Entity<Equipment>().ToTable("Equipments");
            modelBuilder.Entity<Action>().ToTable("Actions");

            base.OnModelCreating(modelBuilder);
        }
    }
}
