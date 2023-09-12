using DS.DAL;
using DS.PropertyEntities.Model.Finance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Finance
{
    public class TransactionEntry:IDisposable
    {
         private TransactionEntities _Entities;
        string sql = string.Empty;       
        public TransactionEntry() { }
        public TransactionEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public static int GetMaxID()
        {
            string sql = string.Format("SELECT MAX(TransactionNo) as TransactionNo FROM Tbl_Transaction");
            int MaxId = CRUD.GetMaxID(sql);
            if (MaxId == null)
            {
                return MaxId = 1;
            }
            {
                return MaxId + 1;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Transaction] " +
                "([TransactionNo],[Purpose],[StudentId],[OthersID],"
                + "[ReferenceID],[TransactionDate]) VALUES (" +
                "'" + _Entities.TransactionNo + "', " +
                "'" + _Entities.Purpose + "' ," +
                "'" + _Entities.StudentID + "', " +
                "'" + _Entities.OthersID + "', " +
                "'" + _Entities.ReferenceID + "', " +
                "'" + _Entities.TransactionDate + "')");
               bool result = CRUD.ExecuteQuery(sql);
               return result;
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
