using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Finance
{
    public class ExpensesEntities:IDisposable
    {
        public int ExpensesID { get; set; }
        public TitleEntities Ac_Title { get; set; }
        public float Amount { get; set; }
        public DateTime Date { get; set; }
        public string Particular { get; set; }

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
