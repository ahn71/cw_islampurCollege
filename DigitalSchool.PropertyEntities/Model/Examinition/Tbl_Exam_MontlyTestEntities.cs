using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Examinition
{
    public class Tbl_Exam_MontlyTestEntities : IDisposable
    {
        public int MonthlyTest_Id { get; set; }
        public int ShiftId { get; set; }
        public int BatchId { get; set; }
        public int ClsGrpID { get; set; }
        public int ClsSecId { get; set; }
        public string ExInId { get; set; }
        public float Patternmarks { get; set; }
        public float Passmarks { get; set; }
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
