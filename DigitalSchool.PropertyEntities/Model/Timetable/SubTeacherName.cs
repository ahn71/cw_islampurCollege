using DS.PropertyEntities.Model.HR;
using DS.PropertyEntities.Model.ManagedSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Timetable
{
    public class SubTeacherNameList : IDisposable
    {
        public int SubTecherId { get; set; }
        public string Subject { get; set; }
        public SubjectEntities SubjectInfo { get; set; }
        public string Teacher { get; set; }
        public Employee TeacherInfo { get; set; }

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
