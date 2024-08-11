using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Admission
{
    public class AdmStdInfoEntities : IDisposable
    {
        public int AdmissionID { get; set; }
        public long AdmissionNo { get; set; }
        public DateTime AdmissionDate { get; set; }
        public CurrentStdEntities Student { get; set; }
        public int ClassID { get; set; }
        public int ClsSecID { get; set; }
        public int RollNo { get; set; }
        public int StartBatchID { get; set; }
        public int EndBatchID { get; set; }
        public bool? StdStatus { get; set; }
        public string Session { get; set; }
        public int? SpendYear { get; set; }

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
