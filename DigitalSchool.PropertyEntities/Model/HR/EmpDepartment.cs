using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.HR
{
    public class EmpDepartment : IDisposable
    {
        public DepartmentEntities Department { get; set; }
        public Employee Employee { get; set; }
        public int? WorkLoad { get; set; }

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
