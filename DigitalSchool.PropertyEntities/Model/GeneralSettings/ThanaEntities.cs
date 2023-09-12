using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.GeneralSettings
{
    public class ThanaEntities : IDisposable
    {
        public int ThanaId { get; set; }
        public int DistrictId { get; set; }
        public string ThanaName { get; set; }

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
