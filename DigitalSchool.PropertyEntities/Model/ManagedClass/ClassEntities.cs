using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ManagedClass
{
    public class ClassEntities : IDisposable
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int ClassOrder { get; set; }

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
