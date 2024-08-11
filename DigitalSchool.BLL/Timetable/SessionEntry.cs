using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Timetable;
using DS.DAL;
using System.Data;

namespace DS.BLL.Timetable
{
    public class SessionEntry : IDisposable
    {
        private SessionEntities _SEntities;
        string sql = string.Empty;
        bool result = true;

        public SessionEntry()
        {
            
        }

        public SessionEntities AddSessionEntities
        {
            set
            {
                _SEntities = value;
            }
        }
        

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_Session] " +
                "([SessionName],[StartMonth],[EndMonth],[Details]) " +
                " VALUES ( '" + _SEntities.SessionName + "', " +
                " '" + _SEntities.StartMonth + "', '" + _SEntities.EndMonth + "','" + _SEntities.Details + "')");
            return result = CRUD.ExecuteQuery(sql);             
        }

        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_Session] SET " +
                "[SessionName] = '" + _SEntities.SessionName + "', " +
                "[StartMonth] = '" + _SEntities.StartMonth + "', " +
                "[EndMonth] = '" + _SEntities.EndMonth + "', " +
                "[Details] = '" + _SEntities.Details + "' " +
                "WHERE [SessionId] = '" + _SEntities.SessionEntitiesId + "'");
            return result = CRUD.ExecuteQuery(sql);  
        }

        public IList<SessionEntities> GetSessionEntities()
        {
            List<SessionEntities> ListSEntities = new List<SessionEntities>();
            sql = string.Format("SELECT [SessionId],[SessionName],[StartMonth],[EndMonth],[Details] FROM [dbo].[Tbl_Session]");
            DataTable dt = new DataTable();
 
            dt = CRUD.ReturnTableNull(sql);
            if(dt != null)
            {
                if(dt.Rows.Count > 0)
                {
                    ListSEntities = (from DataRow row in dt.Rows

                                     select new SessionEntities
                                     {
                                         SessionEntitiesId = int.Parse(row["SessionId"].ToString()),
                                         SessionName = row["SessionName"].ToString(),
                                         StartMonth = Convert.ToDateTime(row["StartMonth"].ToString()),
                                         EndMonth = Convert.ToDateTime(row["EndMonth"].ToString()),
                                         Details = row["Details"].ToString()

                                     }).ToList();
                    return ListSEntities;
                
                }

            }
            return ListSEntities = null;            
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
