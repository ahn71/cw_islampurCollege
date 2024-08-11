using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Finance
{
    public class DiscountEntry:IDisposable
    {
        public DataTable LoadDiscountList(string condition)
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("Select * From v_Discount "+condition+"");
            return dt;
        }

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
