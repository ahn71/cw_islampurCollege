using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DS.PropertyEntities.Model.AccessControls;
using DS.PropertyEntities.Model.ManagedClass;

namespace DS.DAL
{
    public class EduDbContext:DbContext
    {
        public EduDbContext() : base("name=EduDbConnection")
        {
        }

        public DbSet<Module> UserModules { get; set; }
        public DbSet<ClassEntities> Classes { get; set; }
    }
}
