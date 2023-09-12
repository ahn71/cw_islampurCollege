using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Finance
{
    public class TransactionEntities:IDisposable
    {
        public int ID { get; set; }
        public int TransactionNo { get; set; }
        public string Purpose { get; set; }      
        public int StudentID { get; set; }
        public string OthersID { get; set; }
        public string ReferenceID { get; set; }
        public DateTime TransactionDate { get; set; }

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
