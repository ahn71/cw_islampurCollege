using DS.PropertyEntities.Model.ManagedSubject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Examinition
{
    public class Class_ClasswiseMarksheet_TotalResultProcess:IDisposable
    {
        public int SL { get; set; }
        public string ExInId { get; set; }
        public string ExamId { get; set; }
        public int StudentId { get; set; }
        public int RollNo { get; set; }
        public int BatchId { get; set; }
        public int ClsSecId { get; set; }
        public int ShiftId { get; set; }
        public int SubId { get; set; }
        public string SubName { get; set; }
        public bool IsOptional { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int QPId { get; set; }
        public float Marks { get; set; }
        public SubjectEntities Subject { get; set; }
        public float MarksOfAllPatternBySCId { get; set; }
        public string GradeOfAllPatternBySCId { get; set; }
        public float PointOfAllPatternBySCId { get; set; }
        public float MarksOfSubject_WithAllDependencySub { get; set; }
        public string GradeOfSubject_WithAllDependencySub { get; set; }
        public float PointOfSubject_WithAllDependencySub { get; set; }
        public int ClsGrpID { get; set; }

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
