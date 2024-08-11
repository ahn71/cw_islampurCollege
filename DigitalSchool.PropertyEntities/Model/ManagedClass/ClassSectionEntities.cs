using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ManagedClass
{
    public class ClassSectionEntities:IDisposable
    {
        public int ClsSecID { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int? GroupID { get; set; }
        public int? ClsGrpID { get; set; }
        public string GroupName { get; set; }
        public int SectionID { get; set; }
        public string SectionName { get; set; }

        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            disposed = true;
        }
    }
}
