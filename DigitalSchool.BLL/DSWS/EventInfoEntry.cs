using DS.DAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.DSWS
{
   public  class EventInfoEntry
    {
       EventInfoEntities _entities;
        string sql = "";
        bool result;
        DataTable dt;
        int sl = 1;
        public EventInfoEntities AddEntities
        {
            set
            {
                _entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[WSEventInfo] " +
                     "([EventName],[IsActive],[Notes]) VALUES ( " +
                     "N'" + _entities.EventName + "'," +
                     "'" + _entities.IsActive + "',N'" + _entities.Notes + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[WSEventInfo] SET " +
                 "[EventName] = N'" + _entities.EventName + "', " +
                 "[IsActive] = '" + _entities.IsActive + "',[Notes]=N'" + _entities.Notes + "' " +
                 " WHERE [ESL] = '" + _entities.ESL + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Delete(string ESL)
        {
            sql = "delete from WSEventInfo where ESL=" + ESL + "";
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<EventInfoEntities> getEntitiesData()
        {
            sql = "select * from WSEventInfo order by ESL desc";
            dt = new DataTable();
            List<EventInfoEntities> ListEntities = new List<EventInfoEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new EventInfoEntities
                                    {
                                        slNo = sl++,
                                        ESL = int.Parse(row["ESL"].ToString()),
                                        EventName = row["EventName"].ToString(),
                                        Notes = row["Notes"].ToString(),
                                        IsActive = bool.Parse(row["IsActive"].ToString())
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
    }
}
