using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Attendance
{
    public class StudentAbsentDetails
    {
        public int AbsenId { get; set; }
        public int BatchId { get; set; }
        public int StudentId { get; set; }
        public DateTime AbsentDate { get; set; }
        public double AbsentFine { get; set; }
        public bool IsPaid { get; set; }

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
