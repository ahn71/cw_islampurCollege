using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Finance
{
    public class StudentFineEntities:IDisposable
    {
        public int FineId { get; set; }
        public double FineamountPaid { get; set; }
        public DateTime PayDate { get; set; }
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
