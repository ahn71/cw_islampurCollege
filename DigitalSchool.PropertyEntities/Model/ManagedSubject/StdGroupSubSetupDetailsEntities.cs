using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ManagedSubject
{
    public class StdGroupSubSetupDetailsEntities:IDisposable
    {
        public int SGSubId { get; set; }
        public int SubId { get; set; }
        public Boolean MSStatus { get; set; }

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
