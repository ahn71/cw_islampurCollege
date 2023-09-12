using DS.DAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;

namespace DS.BLL.DSWS
{
   public  class AddNoticeEntry
    {
       private AddNoticeEntities _Entities;
       
        private WSNoticeAttach wSNoticeAttach;
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
        public WSNoticeAttach AddwSNoticeAttach
        {
            set
            {
                wSNoticeAttach = value;
            }
        }
        public bool Insert()
       {
           sql = string.Format("INSERT INTO [dbo].[WSNotice] " +
               "([NSubject],[NDetails],[NEntryDate],[NPublishedDate],[NOrdering],[IsActive],[Type],[FileName]) VALUES ( " +
               "N'" + _Entities.NSubject + "'," +
               "N'" + _Entities.NDetails + "','" + _Entities.NEntryDate + "','" + _Entities.NPublishedDate + "',"+
               "" + _Entities.NOrdering + ",'" + _Entities.IsActive + "','" + _Entities.Type + "','" + _Entities.FileName + "')");
           return result = CRUD.ExecuteQuery(sql);
       }
        public int save()
        {
            try
            {
                using (cw_islampurCollegeDB db = new cw_islampurCollegeDB())
                {
                  
                   db.WSNoticeAttaches.Add(wSNoticeAttach);
                    db.SaveChanges();
                    return wSNoticeAttach.NSL;
                }
                
            }
            catch (DbEntityValidationException ex) {
                string a="", b="";
                foreach (var eve in ex.EntityValidationErrors)
                {
                   a += "Name: "+eve.Entry.Entity.GetType().Name+", State: "+ eve.Entry.State+" {";
                    foreach (var ve in eve.ValidationErrors)
                    {

                        a+= "- Property: "+ve.PropertyName+", Error: "+ ve.ErrorMessage;
                    }
                    a += "}";
                }
                return 0;
            }
        }
        public bool update()
        {
            try
            {
                using (cw_islampurCollegeDB db = new cw_islampurCollegeDB())
                {
                    db.Entry(wSNoticeAttach).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }

            }
            catch (DbEntityValidationException ex)
            {
                string a = "", b = "";
                foreach (var eve in ex.EntityValidationErrors)
                {
                    a += "Name: " + eve.Entry.Entity.GetType().Name + ", State: " + eve.Entry.State + " {";
                    foreach (var ve in eve.ValidationErrors)
                    {

                        a += "- Property: " + ve.PropertyName + ", Error: " + ve.ErrorMessage;
                    }
                    a += "}";
                }
                return false;
            }
        }
        public int InsertNoticeWithAttachment(string FileName,string Title,string Status,string PublishdDate,string NDetails,string NEntryDate,string pinTop,string IsImportantNews)
        {
            sql = string.Format("INSERT INTO [dbo].[WSNoticeAttach] " +
                "([FileName],[Title],[Status],[PublishdDate],[NDetails],[NEntryDate],[pinTop],[IsImportantNews]) VALUES (N'" + FileName
                +"',N'"+Title+"','"+Status+"','"+PublishdDate+"',N'"+NDetails+"','"+NEntryDate+"',"+pinTop+ ",'"+ IsImportantNews + "'); SELECT SCOPE_IDENTITY()");
            int result = CRUD.GetMaxID(sql);
            return result;
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[WSNotice] SET " +
                "[NSubject] = N'" + _Entities.NSubject + "',[NDetails] = N'" + _Entities.NDetails + "'," +
                "[NPublishedDate]='" + _Entities.NPublishedDate + "'," +
                "[NOrdering]=" + _Entities.NOrdering + ",[IsActive]='" + _Entities.IsActive + "',[Type]='" + _Entities.Type + "' " +
                " WHERE [NSL] = '" + _Entities.NSL + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool UpdateNoticeWithAttachment(string NSL, string FileName, string Title, string Status, string PublishdDate, string NDetails, string NEntryDate,string IsImportantNews)
        {
            sql = string.Format("UPDATE [dbo].[WSNoticeAttach] SET " +
                "[FileName] = N'" + FileName + "',[Title] = N'" + Title + "'," +
                "[Status]='" + Status + "'," +
                "[PublishdDate]='" + PublishdDate + "',[NDetails]=N'" + NDetails + "',[NEntryDate]='" + NEntryDate + "',[IsImportantNews]='"+ IsImportantNews + "' " +
                " WHERE [NSL] = '" + NSL + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool UpdatePinToTop(string NSL)
        {
            sql = string.Format(" update WSNoticeAttach set pinTop=0;  "+
                " update WSNoticeAttach set pinTop = 1 where NSL = "+NSL+"");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool UpdateToUnpin(string NSL)
        {
            sql = string.Format("update WSNoticeAttach set pinTop = 0 where NSL = " + NSL + "");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool UpdatewithFileName()
       {
           sql = string.Format("UPDATE [dbo].[WSNotice] SET " +
               "[NSubject] = N'" + _Entities.NSubject + "',[NDetails] = N'" + _Entities.NDetails + "'," +
               "[NPublishedDate]='" + _Entities.NPublishedDate + "'," +
               "[NOrdering]=" + _Entities.NOrdering + ",[IsActive]='"
               + _Entities.IsActive + "',[Type]='" + _Entities.Type + "',[FileName]='" + _Entities.FileName + "' " +
               " WHERE [NSL] = '" + _Entities.NSL + "'");
           return result = CRUD.ExecuteQuery(sql);
       }
       public bool Delete(string NSL)
       {
           sql = "delete from WSNotice where NSL=" + NSL + "";
           return result = CRUD.ExecuteQuery(sql);
       }
        public bool DeleteNoticeWithAttachment(string NSL)
        {
            sql = "delete from WSNoticeAttach where NSL=" + NSL + "";
            return result = CRUD.ExecuteQuery(sql);
        }
        public List<AddNoticeEntities> getEntitiesData()
       {
           sql = " select NSL, NSubject,NDetails,"+
                 "NPublishedDate,NOrdering,IsActive,SUBSTRING(NDetails,0,150) as NSummary,Type,FileName " +
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
                                       IsActive = bool.Parse(row["IsActive"].ToString()),
                                       NSummary = row["NSummary"].ToString(),
                                       Type=row["Type"].ToString(),
                                       FileName = row["FileName"].ToString()
                                   }
                                   ).ToList();

                   return ListEntities;
               }

           }
           return ListEntities = null;

       }

       public List<AddNoticeEntities> getNoticeData()
       {
           sql = " select NSL,NSubject,NDetails,NPublishedDate,SUBSTRING(NDetails,0,150) as NSummary,Type,FileName" +
                 " from WSNotice where IsActive=1  order by NOrdering";
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
                                       NSL=int.Parse(row["NSL"].ToString()),
                                       NSubject = row["NSubject"].ToString(),
                                       NDetails=row["NDetails"].ToString(),
                                       NSummary = row["NSummary"].ToString(),                                       
                                       NPublishedDate = DateTime.Parse(row["NPublishedDate"].ToString()),
                                       Type = row["Type"].ToString(),
                                       FileName = row["FileName"].ToString()                                  
                                   }
                                   ).ToList();

                   return ListEntities;
               }

           }
           return ListEntities = null;

       }
        public DataTable getNoticeWithAttachmentData()
        {
            sql = " select NSL, case when NDetails is null then  FileName else convert(varchar, NSL)+'_'+FileName end as FileName,FileName as onlyFileName,Title NSubject,Status,format( PublishdDate,'dd-MM-yyyy') PublishdDate,NDetails,pinTop,status IsActive,format(NEntryDate,'dd-MM-yyyy HH:mm:ss') NEntryDate,case when pinTop=1 then 'Pined' else 'Pin' end as pinText,IsNull(IsImportantNews,0) as IsImportantNews   from WSNoticeAttach  order by   year(PublishdDate) desc, month(PublishdDate) desc,day(PublishdDate) desc";
            dt = new DataTable();           
            dt = CRUD.ReturnTableNull(sql);           
            return dt;

        }
        public DataTable getNoticeWithAttachmentData(string NSL)
        {
            sql = " select NSL, case when NDetails is null then  FileName else convert(varchar, NSL)+'_'+FileName end as FileName,FileName as onlyFileName,Title NSubject,Status,format( PublishdDate,'dd-MM-yyyy') PublishdDate,NDetails,pinTop,status IsActive,format(NEntryDate,'dd-MM-yyyy HH:mm:ss') NEntryDate,case when pinTop=1 then 'Pined' else 'Pin' end as pinText,IsNull(IsImportantNews,0) as IsImportantNews   from WSNoticeAttach  Where NSL="+ NSL;
            dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            return dt;

        }
        public List<AddNoticeEntities> getNewsUpdateData()
       {
           sql = " select NSL,NSubject,NDetails,NPublishedDate,SUBSTRING(NDetails,0,150) as NSummary,FileName" +
                 " from WSNotice where IsActive=1 and Type='News Updates' order by NOrdering";
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
                                       NSL = int.Parse(row["NSL"].ToString()),
                                       NSubject = row["NSubject"].ToString(),
                                       NSummary = row["NSummary"].ToString(),
                                       NPublishedDate = DateTime.Parse(row["NPublishedDate"].ToString()),
                                       NDetails = row["NDetails"].ToString(),
                                       FileName = row["FileName"].ToString()
                                   }
                                   ).ToList();

                   return ListEntities;
               }

           }
           return ListEntities = null;

       }
       public List<AddNoticeEntities> getIndividualNoticeData(string NSL)
       {
           sql = " select NSubject,NDetails,NPublishedDate,FileName" +
                 " from WSNotice where NSL="+NSL+"";
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
                                       NPublishedDate = DateTime.Parse(row["NPublishedDate"].ToString()),
                                       FileName = row["FileName"].ToString()
                                   }
                                   ).ToList();

                   return ListEntities;
               }

           }
           return ListEntities = null;

       }
        public DataTable getIndividualNoticeWithAttachment(string NSL)
        {
            sql = " select NSL,Title,NDetails,PublishdDate,case when isnull(FileName,'')='' then  FileName else convert(varchar, NSL)+'_'+FileName end as FileName" +
                  " from WSNoticeAttach where NSL=" + NSL + "";
            dt = new DataTable();            
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return dt;
                }

            }
            return dt = null;

        }
        public DataTable getNoticeAttach()
       {
           sql = "select NSL, case when NDetails is null then  FileName else convert(varchar, NSL)+'_'+FileName end as FileName,Title,Status,format( PublishdDate,'dd-MMM-yyyy') PublishdDate,NDetails,pinTop,IsNull(IsImportantNews,0) as IsImportantNews from WSNoticeAttach " +
               " where Status=1 order by pinTop desc, year(PublishdDate) desc, month(PublishdDate) desc,day(PublishdDate) desc";
           dt = new DataTable();
           List<AddNoticeEntities> ListEntities = new List<AddNoticeEntities>();
           dt = CRUD.ReturnTableNull(sql);
           return dt;

       }

    }
}
