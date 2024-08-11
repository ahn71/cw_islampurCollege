using DS.PropertyEntities.Model.Admission;
using DS.PropertyEntities.Model.GeneralSettings;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.PropertyEntities.Model.ManagedClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ManagedSubject
{
    public class OptionalSubjectEntities:IDisposable
    {
        public int OpSubId { get; set; }
        public CurrentStdEntities Student { get; set; }
        public ShiftEntities Shift { get; set; }
        public GroupEntities Group { get; set; }
        public SectionEntities Section { get; set; }
        public BatchEntities Batch { get; set; }       
        public SubjectEntities Subject { get; set; }
        public int OpBatchId { get; set; }
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
