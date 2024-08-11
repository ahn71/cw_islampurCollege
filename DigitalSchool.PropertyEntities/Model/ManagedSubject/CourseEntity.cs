using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ManagedSubject
{
    public class CourseEntity :IDisposable
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int Ordering { get; set; }
        public int SubId { get; set; }
        public string subName { get; set; }
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
