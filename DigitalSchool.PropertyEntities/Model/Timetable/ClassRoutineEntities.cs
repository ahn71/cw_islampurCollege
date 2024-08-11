using DS.PropertyEntities.Model.HR;
using DS.PropertyEntities.Model.ManagedSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Timetable
{
    public class ClassRoutineEntities : IDisposable
    {
        public int ClassRoutineID { get; set; }
        public Employee EmpInfo { get; set; }
        public SubjectEntities SubInfo { get; set; }
        public  CourseEntity CourseInfo { get; set; } 
        public WeeklyDaysEntities Day { get; set; }
        public ClassTimeSpecificationEntities ClassTime { get; set; }
        public string Batch { get; set; }
        public string BatchId { get; set; }
        public string ClsGroup { get; set; }
        public string ClasGrpId { get; set; }      
        public string Section { get; set; }
        public string ClsSecId { get; set; }
        public string Shift { get; set; }
        public string ShiftId { get; set; }
        public RoomEntities Room { get; set; }
        public string BatchYear { get; set; }
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
