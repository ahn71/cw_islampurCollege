using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ManagedClass
{
    public class ClassGroupEntities:IDisposable
    {
        public int ClsGrpID { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int NumofMandatorySub { get; set; }

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
