using DS.PropertyEntities.Model.Admission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.ManagedClass
{
   public  class SectionChangeEntities : IDisposable
    {
        public int SL{get;set;}
        public CurrentStdEntities Student { get; set; }       
        public int? PreClsSecID { get; set; }
        public int? NewClsSecID { get; set; }
        public DateTime ChangeDate { get; set; }
      

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