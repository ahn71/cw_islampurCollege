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
  public  class AlbumInfoDetails
    {
        AlbumDetailsEntities _entities;
        string sql = "";
        bool result;
        DataTable dt;
        int sl = 1;
        public AlbumDetailsEntities AddEntities
        {
            set
            {
                _entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[WSGalleryInfoDetails] " +
                     "([PASL],[Title],[Location],[ShortDes]) VALUES ( " +
                     "" + _entities.PASL + "," +
                     "N'" + _entities.Title + "','" + _entities.imgLocation + "',N'"+_entities.Description+"')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Update()
        {
            sql = string.Format("UPDATE [dbo].[WSGalleryInfoDetails] SET " +
                 "[PASL] = " + _entities.PASL + ", " +
                 "[Title] = N'" + _entities.Title+ "',[Location]='" + _entities.imgLocation+ "',[ShortDes]=N'" + _entities.Description + "' " +
                 " WHERE [SL] = '" + _entities.SL + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool Delete(string SL)
        {
            sql = "delete from WSGalleryInfoDetails where SL=" + SL + "";
            return result = CRUD.ExecuteQuery(sql);
        }
        public void bindAlbumlistInDropdownlist( DropDownList ddl) 
        {
            sql = "select PASL,AlbumName from WSGalleryinfo where IsActive=1";
            dt=new DataTable();
             dt = CRUD.ReturnTableNull(sql);
             if (dt.Rows.Count > 0) { 
             ddl.DataValueField = "PASL";
             ddl.DataTextField = "AlbumName";
             ddl.DataSource = dt;
             ddl.DataBind();
             }

        }
        public List<AlbumDetailsEntities> getEntitiesData()
        {
            sql = "select SL,WSGalleryInfoDetails.PASL,Title,Location,ShortDes,gi.AlbumName from WSGalleryInfoDetails "+
                " inner join WSGalleryInfo as GI on WSGalleryInfoDetails.PASL=GI.PASL order by SL desc";
            dt = new DataTable();
            List<AlbumDetailsEntities> ListEntities = new List<AlbumDetailsEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AlbumDetailsEntities
                                    {
                                        slNo=sl++,
                                        SL = int.Parse(row["SL"].ToString()),
                                        PASL = int.Parse(row["PASL"].ToString()),
                                        AlbumName = row["AlbumName"].ToString(),
                                        Title = row["Title"].ToString(),
                                        imgLocation = row["Location"].ToString(),
                                        Description = row["ShortDes"].ToString()
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
        public List<AlbumDetailsEntities> getGalleryData()
        {
            sql = "select Title,Location,ShortDes,gi.AlbumName from WSGalleryInfoDetails " +
                " inner join WSGalleryInfo as GI on WSGalleryInfoDetails.PASL=GI.PASL where GI.IsActive=1 order by SL desc";
            dt = new DataTable();
            List<AlbumDetailsEntities> ListEntities = new List<AlbumDetailsEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AlbumDetailsEntities
                                    {  
                                        AlbumName = row["AlbumName"].ToString(),
                                        Title = row["Title"].ToString(),
                                        imgLocation = row["Location"].ToString(),
                                        Description = row["ShortDes"].ToString()
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }

        public List<AlbumDetailsEntities> getAlbumSummary()
        {
            sql = "select a.Pasl,a.ALbumName,a.Location from v_photoAlbum a "+
                "inner join "+
                "("+
                "select distinct Pasl, max(Sl) as sl from v_photoAlbum "+
                " group by Pasl "+
                ") as b "+
                "on a.Pasl = b.Pasl "+
                "and a.sl = b.sl";
            dt = new DataTable();
            List<AlbumDetailsEntities> ListEntities = new List<AlbumDetailsEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AlbumDetailsEntities
                                    {
                                        PASL = int.Parse(row["Pasl"].ToString()),
                                        AlbumName = row["AlbumName"].ToString(),                                        
                                        imgLocation = row["Location"].ToString()                                     
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
        public List<AlbumDetailsEntities> getAlbumDetails(string PASL)
        {
            sql = "select Location from WSGalleryInfoDetails where PASL="+PASL+" order by SL desc";
            dt = new DataTable();
            List<AlbumDetailsEntities> ListEntities = new List<AlbumDetailsEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AlbumDetailsEntities
                                    {                                        
                                        imgLocation = row["Location"].ToString()
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
        public List<AlbumDetailsEntities> getAlbumDetails()
        {
            sql = "select Location from WSGalleryInfoDetails  order by SL desc";
            dt = new DataTable();
            List<AlbumDetailsEntities> ListEntities = new List<AlbumDetailsEntities>();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AlbumDetailsEntities
                                    {
                                        imgLocation = row["Location"].ToString()
                                    }
                                    ).ToList();

                    return ListEntities;
                }

            }
            return ListEntities = null;

        }
    }
}
