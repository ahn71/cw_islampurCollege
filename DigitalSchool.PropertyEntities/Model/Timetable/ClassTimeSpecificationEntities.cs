using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Timetable
{
    public class ClassTimeSpecificationEntities : IDisposable
    {
        public int ClsTimeID { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string period { get; set; }
        public int OrderBy { get; set; }
        public bool IsbreakTime { get; set; }
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }

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
