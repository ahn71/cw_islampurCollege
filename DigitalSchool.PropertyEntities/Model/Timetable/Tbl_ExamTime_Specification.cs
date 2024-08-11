using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Timetable
{
    public class Tbl_ExamTime_Specification:IDisposable
    {
        public int ExamTimeId { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }       
        public string Period { get; set; }
        public int OrderBy { get; set; }
        public bool IsBreakTime { get; set; }
        public int ExamTimeSetNameId { get; set; }

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
