using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Attendance
{
    public class AttendanceFineEntities:IDisposable
    {
        public int AFId { get; set; }
        public double AbsentFineAmount { get; set; }
        public DateTime Date { get; set; }
        public Boolean IsActive { get; set; }

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
