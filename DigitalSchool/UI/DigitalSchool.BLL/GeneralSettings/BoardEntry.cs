using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.DAL;
using DS.PropertyEntities.Model.GeneralSettings;
using System.Data;

namespace DS.BLL.GeneralSettings
{
    public class BoardEntry : IDisposable
    {
        private BoardEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public BoardEntry() { }
        public BoardEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Boards] " +
                "([BoardName]) VALUES ( '" + _Entities.BoardName + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Boards] SET " +
                "[BoardName] = '" + _Entities.BoardName + "' " +
                "WHERE [BoardId] = '" + _Entities.BoardId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<BoardEntities> GetEntitiesData()
        {
            List<BoardEntities> ListEntities = new List<BoardEntities>();
            sql = string.Format("SELECT [BoardId],[BoardName] FROM [dbo].[Boards]");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new BoardEntities
                                    {
                                        BoardId = int.Parse(row["BoardId"].ToString()),
                                        BoardName = row["BoardName"].ToString()                                     
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
