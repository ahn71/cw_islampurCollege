using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.BLL.Timetable
{
    public class Tbl_Exam_Time_SetName:IDisposable
    {
        public int ExamTimeSetNameId { get; set; }
        public string Name { get; set; }

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
