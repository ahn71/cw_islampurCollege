using DS.DAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.DSWS
{
   public  class AddNoticeEntry
    {
       private AddNoticeEntities _Entities;
       bool result;
       string sql = "";
       int sL=1;
       DataTable dt;
    public   AddNoticeEntities AddEntities 
       {
           set 
           {
               _Entities = value;
           }
       }
       public bool Insert()
       {
           sql = string.Format("INSERT INTO [dbo].[WSNotice] " +
               "([NSubject],[NDetails],[NEntryDate],[NPublishedDate],[NOrdering],[IsActive]) VALUES ( " +
               "N'" + _Entities.NSubject + "'," +
               "N'" + _Entities.NDetails + "','" + _Entities.NEntryDate + "','" + _Entities.NPublishedDate + "',"+
               "" + _Entities.NOrdering + ",'" + _Entities.IsActive + "')");
           return result = CRUD.ExecuteQuery(sql);
       }
       public bool Update()
       {
           sql = string.Format("UPDATE [dbo].[WSNotice] SET " +
               "[NSubject] = N'" + _Entities.NSubject + "',[NDetails] = N'" + _Entities.NDetails + "',"+
               "[NPublishedDate]='" + _Entities.NPublishedDate + "',"+
               "[NOrdering]="+_Entities.NOrdering+",[IsActive]='" + _Entities.IsActive + "' " +
               " WHERE [NSL] = '" + _Entities.NSL + "'");
           return result = CRUD.ExecuteQuery(sql);
       }
       public bool Delete(string NSL)
       {
           sql = "delete from WSNotice where NSL=" + NSL + "";
           return result = CRUD.ExecuteQuery(sql);
       }
       public List<AddNoticeEntities> getEntitiesData()
       {
           sql = " select NSL, NSubject,NDetails,"+
                 "NPublishedDate,NOrdering,IsActive" +
                 " from WSNotice order by NSL desc";
           dt = new DataTable();
           List<AddNoticeEntities> ListEntities = new List<AddNoticeEntities>();
           dt = CRUD.ReturnTableNull(sql);
           if (dt != null)
           {
               if (dt.Rows.Count > 0)
               {
                   ListEntities = (from DataRow row in dt.Rows
                                   select new AddNoticeEntities
                                   {
                                       SL=sL++,
                                       NSL = int.Parse(row["NSL"].ToString()),
                                       NSubject = row["NSubject"].ToString(),
                                       NDetails = row["NDetails"].ToString(),
                                      // NEntryDate = DateTime.Parse(row["NEntryDate"].ToString()),
                                       NPublishedDate = DateTime.Parse(row["NPublishedDate"].ToString()),
                                       NOrdering = int.Parse(row["NOrdering"].ToString()),
                                       IsActive = bool.Parse(row["IsActive"].ToString())
                                   }
                                   ).ToList();

                   return ListEntities;
               }

           }
           return ListEntities = null;

       }

       public List<AddNoticeEntities> getNoticeData()
       {
           sql = " select NSubject,NDetails,NPublishedDate"+
                 " from WSNotice where IsActive=1 order by NOrdering";
           dt = new DataTable();
           List<AddNoticeEntities> ListEntities = new List<AddNoticeEntities>();
           dt = CRUD.ReturnTableNull(sql);
           if (dt != null)
           {
               if (dt.Rows.Count > 0)
               {
                   ListEntities = (from DataRow row in dt.Rows
                                   select new AddNoticeEntities
                                   {                                       
                                       NSubject = row["NSubject"].ToString(),
                                       NDetails = row["NDetails"].ToString(),                                       
                                       NPublishedDate = DateTime.Parse(row["NPublishedDate"].ToString())                                                                           
                                   }
                                   ).ToList();

                   return ListEntities;
               }

           }
           return ListEntities = null;

       }

    }
}
