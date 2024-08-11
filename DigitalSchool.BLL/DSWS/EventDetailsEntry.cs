using DS.DAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.DSWS
{
   public class EventDetailsEntry
    {
       EventDetailsEntities _entities;
        string sql = "";
        bool result;
        DataTable dt;
        int sl = 1;
        public EventDetailsEntities AddEntities
        {
            set
            {
                _entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[WSEventInfoDetails] " +
                     "([ESL],[Title],[Location],[Description],[EventDate]) VALUES ( " +
                     "" + _entities.ESL + "," +
                     "N'" + _entities.Title + "','" + _entities.imgLocation + "',N'" + _entities.Description + "','"+_entities.EventDate+"')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[WSEventInfoDetails] SET " +
                 "[ESL] = " + _entities.ESL + ", " +
                 "[Title] = N'" + _entities.Title + "',[Location]='" + _entities.imgLocation + "',[Description]=N'" + _entities.Description + "',[EventDate]='" + _entities.EventDate + "' " +
                 " WHERE [SL] = '" + _entities.SL + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Delete(string SL)
        {
            sql = "delete from WSEventInfoDetails where SL=" + SL + "";
            return result = CRUD.ExecuteQuery(sql);
        }
        public void bindAlbumlistInDropdownlist(DropDownList ddl)
        {
            sql = "select ESL,EventName from WSEventInfo where IsActive=1";
            dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt.Rows.Count > 0)
            {
                ddl.DataValueField = "ESL";
                ddl.DataTextField = "EventName";
                ddl.DataSource = dt;
                ddl.DataBind();
            }

        }
        public List<EventDetailsEntities> getEntitiesData()
        {
            sql = "select SL,WSEventInfoDetails.ESL,Title,Location,Description,Ei.EventName,EventDate from WSEventInfoDetails " +
                " inner join WSEventInfo as Ei on WSEventInfoDetails.ESL=Ei.ESL order by SL desc";
            dt = new DataTable();
            List<EventDetailsEntities> ListEntities = new List<EventDetailsEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new EventDetailsEntities
                                    {
                                        slNo = sl++,
                                        SL = int.Parse(row["SL"].ToString()),
                                        ESL = int.Parse(row["ESL"].ToString()),
                                        EventName = row["EventName"].ToString(),
                                        Title = row["Title"].ToString(),
                                        imgLocation = row["Location"].ToString(),
                                        Description = row["Description"].ToString(),
                                        EventDate = returnDateTimeOrNull(row["EventDate"].ToString())
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
        public DateTime? returnDateTimeOrNull(string date) 
        {
            try {return DateTime.Parse(date); }
            catch { return null; }
        }
        public List<EventDetailsEntities> getEventData()
        {
            sql = "select SL,WSEventInfoDetails.ESL,Title,Location,Description,Ei.EventName,EventDate from WSEventInfoDetails " +
                " inner join WSEventInfo as Ei on WSEventInfoDetails.ESL=Ei.ESL order by SL desc";
            dt = new DataTable();
            List<EventDetailsEntities> ListEntities = new List<EventDetailsEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new EventDetailsEntities
                                    {
                                        slNo = sl++,
                                        SL = int.Parse(row["SL"].ToString()),
                                        ESL = int.Parse(row["ESL"].ToString()),
                                        EventName = row["EventName"].ToString(),
                                        Title = row["Title"].ToString(),
                                        imgLocation = row["Location"].ToString(),
                                        Description = row["Description"].ToString(),
                                        EventDate = returnDateTimeOrNull(row["EventDate"].ToString())
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
    }
}
