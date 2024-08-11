using DS.DAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.DSWS
{
   public class AddPageContentEntry
    {
        private AddPageContentEntities _Entities;
        bool result;
        string sql = "";
        int sL = 1;
        DataTable dt;
        public AddPageContentEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[WSPageContent] " +
                "([PageID],[Title],[Image],[TextContent],[Status],[EntryTime]) VALUES ( " +
                "N'" + _Entities.PageID + "'," +
                "N'" + _Entities.Title + "',N'" + _Entities.Image + "',N'" + _Entities.TextContent + "'," +
                "'" + _Entities.Status + "','" + _Entities.EntryTime.ToString("yyyy-MM-dd HH:mm:ss")+ "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[WSPageContent] SET " +
                "[Title] = N'" + _Entities.Title + "'," +
                "[Image]=N'" + _Entities.Image + "'," +
                "[TextContent]=N'" + _Entities.TextContent + "',[Status]='" + _Entities.Status + "',[EntryTime]='" + _Entities.EntryTime.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                " WHERE [PageID] = '" + _Entities.PageID + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        //public bool UpdatewithFileName()
        //{
        //    sql = string.Format("UPDATE [dbo].[WSNotice] SET " +
        //        "[NSubject] = N'" + _Entities.NSubject + "',[NDetails] = N'" + _Entities.NDetails + "'," +
        //        "[NPublishedDate]='" + _Entities.NPublishedDate + "'," +
        //        "[NOrdering]=" + _Entities.NOrdering + ",[IsActive]='"
        //        + _Entities.IsActive + "',[Type]='" + _Entities.Type + "',[FileName]='" + _Entities.FileName + "' " +
        //        " WHERE [NSL] = '" + _Entities.NSL + "'");
        //    return result = CRUD.ExecuteQuery(sql);
        //}
        public bool Delete(string PageId)
        {
            sql = "delete from WSPageContent where PageId='" + PageId + "'";
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool IsItExist()
        {
            sql = " select PageId from WSPageContent where PageId= N'" + _Entities.PageID + "'";
            dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt.Rows.Count > 0) return true;
            else return false;
        }
        public List<AddPageContentEntities> getEntitiesData()
        {
            sql = " select pc.PageID,p.Page,pc.Title,pc.TextContent,pc.Status,pc.EntryTime,pc.Image from WSPageContent pc inner join WSPages p on pc.PageID=p.PageId order by p.ordering ";
            dt = new DataTable();
            List<AddPageContentEntities> ListEntities = new List<AddPageContentEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AddPageContentEntities
                                    {
                                        SL = sL++,
                                        PageID = row["PageID"].ToString(),
                                        Page = row["Page"].ToString(),
                                        Image = row["Image"].ToString(),
                                        Title = row["Title"].ToString(),
                                        EntryTime = DateTime.Parse(row["EntryTime"].ToString()),
                                        Status = bool.Parse(row["Status"].ToString()),
                                        TextContent = row["TextContent"].ToString()
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }

        //public List<AddNoticeEntities> getNoticeData()
        //{
        //    sql = " select NSL,NSubject,NDetails,NPublishedDate,SUBSTRING(NDetails,0,150) as NSummary,Type,FileName" +
        //          " from WSNotice where IsActive=1  order by NOrdering";
        //    dt = new DataTable();
        //    List<AddNoticeEntities> ListEntities = new List<AddNoticeEntities>();
        //    dt = CRUD.ReturnTableNull(sql);
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            ListEntities = (from DataRow row in dt.Rows
        //                            select new AddNoticeEntities
        //                            {
        //                                NSL = int.Parse(row["NSL"].ToString()),
        //                                NSubject = row["NSubject"].ToString(),
        //                                NDetails = row["NDetails"].ToString(),
        //                                NSummary = row["NSummary"].ToString(),
        //                                NPublishedDate = DateTime.Parse(row["NPublishedDate"].ToString()),
        //                                Type = row["Type"].ToString(),
        //                                FileName = row["FileName"].ToString()
        //                            }
        //                            ).ToList();

        //            return ListEntities;
        //        }

        //    }
        //    return ListEntities = null;

        //}
        //public List<AddNoticeEntities> getNewsUpdateData()
        //{
        //    sql = " select NSL,NSubject,NDetails,NPublishedDate,SUBSTRING(NDetails,0,150) as NSummary,FileName" +
        //          " from WSNotice where IsActive=1 and Type='News Updates' order by NOrdering";
        //    dt = new DataTable();
        //    List<AddNoticeEntities> ListEntities = new List<AddNoticeEntities>();
        //    dt = CRUD.ReturnTableNull(sql);
        //    if (dt != null)
        //    {
        //        if (dt.Rows.Count > 0)
        //        {
        //            ListEntities = (from DataRow row in dt.Rows
        //                            select new AddNoticeEntities
        //                            {
        //                                NSL = int.Parse(row["NSL"].ToString()),
        //                                NSubject = row["NSubject"].ToString(),
        //                                NSummary = row["NSummary"].ToString(),
        //                                NPublishedDate = DateTime.Parse(row["NPublishedDate"].ToString()),
        //                                NDetails = row["NDetails"].ToString(),
        //                                FileName = row["FileName"].ToString()
        //                            }
        //                            ).ToList();

        //            return ListEntities;
        //        }

        //    }
        //    return ListEntities = null;

        //}
        public List<AddPageContentEntities> getIndividualPageData(string PageId)
        {
            sql = " select Title,Image,TextContent from WSPageContent where PageID='"+ PageId + "'";
            dt = new DataTable();
            List<AddPageContentEntities> ListEntities = new List<AddPageContentEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AddPageContentEntities
                                    {                                        
                                        Title = row["Title"].ToString(),
                                        TextContent = row["TextContent"].ToString(),
                                        Image = row["Image"].ToString()
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
        //public DataTable getNoticeAttach()
        //{
        //    sql = " select FileName,Title,Status,format( PublishdDate,'dd-MMM-yyyy') PublishdDate from WSNoticeAttach " +
        //        " where Status=1 order by year(PublishdDate) desc, month(PublishdDate) desc,day(PublishdDate) desc";
        //    dt = new DataTable();
        //    List<AddNoticeEntities> ListEntities = new List<AddNoticeEntities>();
        //    dt = CRUD.ReturnTableNull(sql);
        //    return dt;

        //}

    }
}
