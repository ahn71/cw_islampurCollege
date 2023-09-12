using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.GeneralSettings
{
    public class OffdayEntities:IDisposable
    {
        public int OffDateId { get; set; }
        public DateTime? OffDate { get; set; }
        public string Purpose { get; set; }
        public string DayName { get; set; }
        public string Month { get; set; }        

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

