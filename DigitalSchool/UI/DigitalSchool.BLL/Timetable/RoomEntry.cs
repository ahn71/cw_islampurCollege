using DS.DAL;
using DS.PropertyEntities.Model.Timetable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Timetable
{
    public class RoomEntry : IDisposable
    {
        private RoomEntities _Entities;
        string sql = string.Empty;
        bool result = true;

        public RoomEntry()
        {

        }

        public RoomEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_BuildingWith_Room] " +
                "([RoomName],[RoomCapacity],[BuildingId]) VALUES ( '" + _Entities.RoomName + "', " +
                " '" + _Entities.RoomCapacity + "', '" + _Entities.BuildingId + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[Tbl_BuildingWith_Room] SET" +
                    " [RoomName] = '" + _Entities.RoomName + "'," +
                    " [RoomCapacity] = '" + _Entities.RoomCapacity + "'," +
                    " [BuildingId] = '" + _Entities.BuildingId + "'" +
                    " WHERE [RoomId] = '" + _Entities.RoomId + "'");                
            return result = CRUD.ExecuteQuery(sql);
        }

        public List<RoomEntities> GetEntitiesData(int? BuildingId)
        {
            List<RoomEntities> ListEntities = new List<RoomEntities>();
            if(BuildingId != null)
            {
                sql = string.Format("SELECT r.[RoomId], r.[RoomCode], r.[RoomName], r.[RoomCapacity], r.[BuildingId], b.[BuildingName] " +
                      " FROM [dbo].[Tbl_BuildingWith_Room] r JOIN [dbo].[Tbl_Bu‎ilding_Name] b ON (r.[BuildingId] = b.[BuildingId]) " +
                      " WHERE r.[BuildingId] = '" + BuildingId + "'");
            }
            else
            {
                sql = string.Format("SELECT r.[RoomId], r.[RoomCode], r.[RoomName], r.[RoomCapacity], r.[BuildingId], b.[BuildingName] " +
                                    " FROM [dbo].[Tbl_BuildingWith_Room] r JOIN [dbo].[Tbl_Bu‎ilding_Name] b ON (r.[BuildingId] = b.[BuildingId])");
            }
            
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new RoomEntities
                                    {
                                        RoomId = int.Parse(row["RoomId"].ToString()),
                                        RoomCode = row["RoomCode"].ToString(),
                                        RoomName = row["RoomName"].ToString(),
                                        RoomCapacity = int.Parse(row["RoomCapacity"].ToString()),
                                        BuildingId = int.Parse(row["BuildingId"].ToString()),
                                        BuildingName = row["BuildingName"].ToString()
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
