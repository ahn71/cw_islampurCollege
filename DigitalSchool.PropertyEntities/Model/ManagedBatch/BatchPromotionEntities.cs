using DS.PropertyEntities.Model.Admission;
using DS.PropertyEntities.Model.ManagedClass;
using DS.PropertyEntities.Model.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ManagedBatch
{
    public class BatchPromotionEntities : IDisposable
    {       
        public int ID { get; set; }
        public CurrentStdEntities Student { get; set; }
        public GroupEntities Group { get; set; }
        public decimal GPA { get; set; }
        public int? NewClassID { get; set; }
        public string NewClassName { get; set; }        
        public int? NewRoll { get; set; }
        public int? NewClsgrpID { get; set; }
        public int? NewClsSecID { get; set; }
        public string NewSectionName { get; set; }       
        public int?  NewBatchID { get; set; }
        public string NewBatchName { get; set; }

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
