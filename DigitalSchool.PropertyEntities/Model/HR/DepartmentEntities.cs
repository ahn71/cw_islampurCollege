using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.HR
{
    public class DepartmentEntities : IDisposable
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsTeacher { get; set; }
        public bool Status { get; set; }

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
