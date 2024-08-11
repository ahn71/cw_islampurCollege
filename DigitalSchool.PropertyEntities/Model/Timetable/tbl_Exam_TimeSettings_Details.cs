using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Timetable
{
    public class tbl_Exam_TimeSettings_Details:IDisposable
    {
        public int ExScSl { get; set; }
        public int ExScId { get; set; }
        public DateTime ExamDate { get; set; }
        public string ExamTimeDuration1 { get; set; }
        public string Details1 { get; set; }
        public string ExamTimeDuration2 { get; set; }
        public string Details2 { get; set; }
        public string ExamTimeDuration3 { get; set; }
        public string Details3 { get; set; }
        public string ExamTimeDuration4 { get; set; }
        public string Details4 { get; set; }
        public string ExamTimeDuration5 { get; set; }
        public string Details5 { get; set; }

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
