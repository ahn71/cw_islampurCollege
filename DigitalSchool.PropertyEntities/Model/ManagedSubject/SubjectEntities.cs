using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ManagedSubject
{
    public class SubjectEntities : IDisposable
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int OrderBy { get; set; }
        public bool IsActive { get; set; }

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
