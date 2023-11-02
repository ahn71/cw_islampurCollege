using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations.Schema;
using DS.PropertyEntities.Model.AccessControls;
using DS.PropertyEntities.Model.ManagedClass;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DS.DAL
{
    public class EduDbContext : DbContext
    {
        public EduDbContext() : base("name=EduDbConnection")
        {
        }        
        public DbSet<Module> UserModules { get; set; }        
        public DbSet<ClassEntities> Classes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Module>().ToTable("UserModules");
            modelBuilder.Entity<ClassEntities>().ToTable("Classes");
        }
    }
}

