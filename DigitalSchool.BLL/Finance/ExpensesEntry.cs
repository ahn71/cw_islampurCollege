using DS.DAL;
using DS.PropertyEntities.Model.Finance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Finance
{
    public class ExpensesEntry:IDisposable
    {
         private ExpensesEntities _Entities;
        string sql = string.Empty;
        public ExpensesEntry() { }
        public ExpensesEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Accounts_Expenses] " +
                "([TitleID],[Amount],[Date],[Particular]) VALUES (" +
                "'" + _Entities.Ac_Title.ID + "','" + _Entities.Amount + "','" + _Entities.Date + "','"+_Entities.Particular+"')");
               bool result = CRUD.ExecuteQuery(sql);
               return result;
        }
        public bool Update()
        {
            sql = string.Format("UPDATE  [dbo].[Accounts_Expenses]  set " +
                "[TitleID]='" + _Entities.Ac_Title.ID + "',[Amount]='" + _Entities.Amount
                + "',[Date]='" + _Entities.Date + "',[Particular]='"+_Entities.Particular+"' WHERE ExpensesID='" + _Entities.ExpensesID + "' ");
            bool result = CRUD.ExecuteQuery(sql);
            return result;
        }
        public bool Delete(string ExpensesID)
        {
            sql = string.Format("Delete From [dbo].[Accounts_Expenses]   WHERE ExpensesID='" + ExpensesID + "' ");
            bool result = CRUD.ExecuteQuery(sql);
            return result;
        }
        public List<ExpensesEntities> GetEntitiesData()
        {
            List<ExpensesEntities> ListEntities = new List<ExpensesEntities>();
            sql = string.Format("SELECT ExpensesID,Accounts_Title.TitleID,Accounts_Title.Title,Amount,"
            + "Date,Particular FROM Accounts_Expenses INNER JOIN Accounts_Title ON Accounts_Expenses.TitleID=Accounts_Title.TitleID");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new ExpensesEntities
                                    {
                                        ExpensesID = int.Parse(row["ExpensesID"].ToString()),
                                        Ac_Title = new TitleEntities()
                                        {
                                            ID = int.Parse(row["TitleID"].ToString()),
                                            Title = row["Title"].ToString()
                                        },
                                        Amount = float.Parse(row["Amount"].ToString()),
                                        Date = Convert.ToDateTime(row["Date"].ToString()),
                                        Particular = row["Particular"].ToString()
                                                                             
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
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
