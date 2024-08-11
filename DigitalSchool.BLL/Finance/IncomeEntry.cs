using DS.DAL;
using DS.PropertyEntities.Model.Finance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Finance
{
    public class IncomeEntry:IDisposable
    {
         private IncomeEntities _Entities;
        string sql = string.Empty;
        public IncomeEntry() { }
        public IncomeEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Accounts_Income] " +
                "([TitleID],[Amount],[Date],[Particular]) VALUES (" +
                "'" + _Entities.Ac_Title.ID + "','" + _Entities.Amount + "','" + _Entities.Date + "','" + _Entities.Particular + "' )");
               bool result = CRUD.ExecuteQuery(sql);
               return result;
        }
        public bool Update()
        {
            sql = string.Format("UPDATE  [dbo].[Accounts_Income]  set " +
                "[TitleID]='" + _Entities.Ac_Title.ID + "',[Amount]='" + _Entities.Amount
                + "',[Date]='" + _Entities.Date + "',[Particular]='" + _Entities.Particular + "' WHERE IncomeID='" + _Entities.IncomeID + "' ");
            bool result = CRUD.ExecuteQuery(sql);
            return result;
        }
        public bool Delete(string IncomeID)
        {
            sql = string.Format("Delete from [dbo].[Accounts_Income]   WHERE IncomeID='" + IncomeID + "' ");
            bool result = CRUD.ExecuteQuery(sql);
            return result;
        }
        public List<IncomeEntities> GetEntitiesData()
        {
            List<IncomeEntities> ListEntities = new List<IncomeEntities>();
            sql = string.Format("SELECT IncomeID,Accounts_Title.TitleID,Accounts_Title.Title,Amount,"
            + "Date,Particular FROM Accounts_Income INNER JOIN Accounts_Title ON Accounts_Income.TitleID=Accounts_Title.TitleID");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new IncomeEntities
                                    {
                                        IncomeID = int.Parse(row["IncomeID"].ToString()),
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
