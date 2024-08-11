using DS.PropertyEntities.Model.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.DSWS
{
    public class HRAttendanceEntities:IDisposable
    {
        public Employee EmpInfo { get; set; }
        public int EID { get; set; }
        public DateTime? AttDate { get; set; }
        public string AttStatus { get; set; }
        
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
