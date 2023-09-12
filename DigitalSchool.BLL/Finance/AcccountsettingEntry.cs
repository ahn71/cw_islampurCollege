using DS.DAL;
using DS.PropertyEntities.Model.Finance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Finance
{
    public class AcccountsettingEntry:IDisposable
    {
        private AccountsettingEntities _Entities;
        string sql = string.Empty;
        public AccountsettingEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }       
        public bool Update()
        {
            sql = string.Format("UPDATE  [dbo].[Accounts_Setting]  set " +
                "[FeesCount]='" + _Entities.feescount + "' ");
            bool result = CRUD.ExecuteQuery(sql);
            return result;
        }
        public bool CheckFeesCount()
        {
            List<AccountsettingEntities> ListEntities = new List<AccountsettingEntities>();
            ListEntities = GetEntitiesData();
            if (ListEntities[0].feescount == false)
                return false;
            return true;
        }
        public List<AccountsettingEntities> GetEntitiesData()
        {
            List<AccountsettingEntities> ListEntities = new List<AccountsettingEntities>();
            sql = string.Format("SELECT [Id],[FeesCount] FROM [dbo].[Accounts_Setting]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AccountsettingEntities
                                    {
                                        id = int.Parse(row["Id"].ToString()),
                                        feescount = row["FeesCount"].ToString() == "True" ? true : false
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
