using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Attendance
{
    public class AbsentStudentsEntities : IDisposable
    {
        public int AbsentStdID { get; set; }
        public string StudentName { get; set; }
        public int Roll { get; set; }
        public string ClassName { get; set; }
        public string Section { get; set; }
        public string Shift { get; set; }
        public string GuardiantMobile { get; set; }

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
