using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Timetable
{
    public class ClsTimeSetNameEntities : IDisposable
    {
        public int ClsTimeSetNameId { get; set; }
        public string Name { get; set; }       

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
