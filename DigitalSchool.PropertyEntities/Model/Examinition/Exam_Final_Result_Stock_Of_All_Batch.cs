using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Examinition
{
    public class Exam_Final_Result_Stock_Of_All_Batch:IDisposable
    {
        public int SL { get; set; }
        public string ExInId { get; set; }
        public string ExamId { get; set; }
        public int StudentId { get; set; }
        public int BatchId { get; set; }
        public int ShiftId { get; set; }
        public int ClsSecId { get; set; }
        public int ClsGrpID { get; set; }
        public float FinalGPA_OfExam_WithOptionalSub { get; set; }
        public string FinalGrade_OfExam_WithOptionalSub { get; set; }
        public float FinalTotalMarks_OfExam_WithOptionalSub { get; set; }
        public float FinalGPA_OfExam { get; set; }
        public string FinalGrad_OfExam { get; set; }
        public float FinalTotalMarks_OfExam { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsFinalExam { get; set; }

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
