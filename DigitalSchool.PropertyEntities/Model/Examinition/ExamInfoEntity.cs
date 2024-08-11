using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Examinition
{
    public class ExamInfoEntity : IDisposable
    {
        public int ExInSl { get; set; }
        public string ExInId { get; set; }
        public DateTime ExStartDate { get; set; }
        public DateTime ExEndDate { get; set; }
        public int BatchId { get; set; }
        public int ClsGrpID { get; set; }
        public string ExName { get; set; }

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
