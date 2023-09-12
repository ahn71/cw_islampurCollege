using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Timetable
{
    public class SessionEntities : IDisposable
    {
        public int SessionEntitiesId { get; set; }
        public string SessionName { get; set; }
        public DateTime StartMonth { get; set; }
        public DateTime EndMonth { get; set; }
        public string Details { get; set; }

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
