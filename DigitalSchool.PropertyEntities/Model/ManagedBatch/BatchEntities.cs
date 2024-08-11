using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ManagedBatch
{
    public class BatchEntities : IDisposable
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public bool IsUsed { get; set; }
        public int Year { get; set; }
        public int ClassId { get; set; }

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
