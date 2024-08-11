using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.DSWS;
using DS.DAL;
using System.Data;

namespace DS.BLL.DSWS
{
  public  class AlbumInfoEntry
    {
      AlbumInfoEntities _entities;
      string sql = "";
      bool result;
      DataTable dt;
      int sl = 1;
      public AlbumInfoEntities AddEntities 
      {
          set 
          {
              _entities = value;
          }
      }
      public bool Insert()
      {
          sql = string.Format("INSERT INTO [dbo].[WSGalleryInfo] " +
                   "([AlbumName],[IsActive],[Notes]) VALUES ( " +
                   "N'" + _entities.AlbumName + "'," +
                   "'" + _entities.IsActive + "',N'" + _entities.Notes + "')");
          return result = CRUD.ExecuteQuery(sql);
      }
      public bool Update()
      {
          sql = string.Format("UPDATE [dbo].[WSGalleryInfo] SET " +
               "[AlbumName] = N'" + _entities.AlbumName + "', " +
               "[IsActive] = '" + _entities.IsActive + "',[Notes]=N'" + _entities.Notes + "' " +
               " WHERE [PASL] = '" + _entities.PASL + "'");          
          return result = CRUD.ExecuteQuery(sql);
      }
      public bool Delete(string PSL)
      {
          sql = "delete from WSGalleryInfo where PASL=" + PSL + "";
          return result = CRUD.ExecuteQuery(sql);
      }
      public List<AlbumInfoEntities> getEntitiesData()
      {
          sql = "select * from WSGalleryInfo order by PASL desc";
          dt = new DataTable();
          List<AlbumInfoEntities> ListEntities = new List<AlbumInfoEntities>();
          dt = CRUD.ReturnTableNull(sql);
          if (dt != null)
          {
              if (dt.Rows.Count > 0)
              {
                  ListEntities = (from DataRow row in dt.Rows
                                  select new AlbumInfoEntities
                                  {
                                      slNo=sl++,
                                      PASL = int.Parse(row["PASL"].ToString()),
                                      AlbumName = row["AlbumName"].ToString(),
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
