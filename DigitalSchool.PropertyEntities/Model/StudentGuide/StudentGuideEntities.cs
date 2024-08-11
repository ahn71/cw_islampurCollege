using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.StudentGuide
{
    public class StudentGuideEntities : IDisposable
    {
        public int StudentId { get; set; }
        public int EID { get; set; }
        public Boolean GuideStatus { get; set; }

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
