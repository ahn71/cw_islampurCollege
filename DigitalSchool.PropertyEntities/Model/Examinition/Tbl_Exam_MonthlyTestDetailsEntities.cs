using DS.PropertyEntities.Model.Admission;
using DS.PropertyEntities.Model.GeneralSettings;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.PropertyEntities.Model.ManagedClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Examinition
{
    public class Tbl_Exam_MonthlyTestDetailsEntities : IDisposable
    {
        public Tbl_Exam_MontlyTestEntities tbl_exam_monthlytest { get; set; }
        public ShiftEntities Shift { get; set; }
        public ExamTypeEntities ExamType { get; set; }
        public  ExamInfoEntity Examinfo { get; set; }
        public BatchEntities Batch { get; set; }        
        public ClassGroupEntities Group { get; set; }
        public SectionEntities Section { get; set; }
        public CurrentStdEntities Student { get; set; }
        public int MonthlyTest_Id { get; set; }
        public int StudentId { get; set; }
        public int RollNo { get; set; }       
        public float Obtainmarks { get; set; }       
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
