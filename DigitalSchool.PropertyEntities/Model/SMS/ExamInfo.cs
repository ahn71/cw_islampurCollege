using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.ManagedBatch;

namespace DS.PropertyEntities.Model.SMS
{
    public class ExamInfo : IDisposable
    {
        public int ExInSI { get; set; }
        public string ExInId { get; set; }
        public DateTime ExInDate { get; set; }
        public BatchEntities Batch { get; set; }
        public string ExInDependency { get; set; }

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
