using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.DSWS
{
    public class StdAttEntities:IDisposable
    {
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public int ClsGrpID { get; set; }
        public string GroupName { get; set; }
        public int ClsSecID { get; set; }
        public string SectionName { get; set; }
        public int TotalStudent { get; set; }
        public int TotalPresent { get; set; }
        public int TotalAbsent { get; set; }
        public DateTime? AttDate { get; set; }       

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
