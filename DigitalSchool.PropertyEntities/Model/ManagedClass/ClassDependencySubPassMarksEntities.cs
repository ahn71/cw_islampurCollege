using DS.PropertyEntities.Model.ManagedSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ManagedClass
{
    public class ClassDependencySubPassMarksEntities:IDisposable
    {
        public int ID { get; set; }
        public ClassEntities Class { get; set; }      
        public SubjectEntities Subject { get; set; }
        public float PassMarks { get; set; }
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
