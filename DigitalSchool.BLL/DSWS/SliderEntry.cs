using DS.DAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.DSWS
{
  public  class SliderEntry
    {
      SliderEntities _Entities;
       WSSlider _Slider;
      bool result;
      string sql = "";
      int sL = 1;
      DataTable dt;
      List<WSSlider> _list;
      public SliderEntities SetEntities 
      {
          set
          {
              _Entities = value;
          }

      }
        public WSSlider SetSlider
        {
          set
          {
                _Slider = value;
          }

      }
      public bool Insert()
      {
          try
          {
              sql = string.Format("INSERT INTO [dbo].[WSSlider] " +
             "([Location],[Chosen],[Ordering]) VALUES ( " +
             "'" + _Entities.Location + "'," +
             "'" + _Entities.Chosen + "'," + _Entities.Ordering + ")");
              return result = CRUD.ExecuteQuery(sql);
          }
          catch { return false; }
      }
      public bool Update()
      {
           try
          {
          sql = string.Format("UPDATE [dbo].[WSSlider] SET " +
              "[Location] = '" + _Entities.Location + "',[Chosen] = '" + _Entities.Chosen + "'," +
              "[Ordering]=" + _Entities.Ordering + " " +
              "WHERE [SL] = '" + _Entities.SSL + "'");
          return result = CRUD.ExecuteQuery(sql);
          }
           catch { return false; }
      }
      public bool Delete(string SSL)
      {
          sql = "delete from WSSlider where SL=" + SSL + "";
          return result = CRUD.ExecuteQuery(sql);
      }
        public bool save()
        {
            try
            {
                using (cw_islampurCollegeDB db = new cw_islampurCollegeDB())
                {
                    db.WSSliders.Add(_Slider);
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception ex) { return false; }
        }
        public bool update()
        {
            try
            {
                using (cw_islampurCollegeDB db = new cw_islampurCollegeDB())
                {
                    db.Entry(_Slider).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public bool delete(int sl)
        {
            try
            {
                using (cw_islampurCollegeDB db = new cw_islampurCollegeDB())
                {

                    WSSlider slider = new WSSlider()
                    {
                        SL = sl
                    };
                    db.WSSliders.Attach(slider);
                    db.WSSliders.Remove(slider);
                    db.SaveChanges();
                }
                return true;
            }
            catch (Exception ex) { return false; }
        }
        public List<WSSlider> list()
        {
            try
            {

                using (cw_islampurCollegeDB db = new cw_islampurCollegeDB())
                {
                    _list = new List<WSSlider>();
                    _list = db.WSSliders.ToList<WSSlider>().OrderBy(p => p.Ordering).ToList();
                    return _list;
                }
            }
            catch (Exception ex) { return null; }
        }
        public List<SliderEntities> getEntitiesData()
      {
          sql = " select SL, Location,Chosen,Ordering" +
                " from WSSlider order by SL desc";
          dt = new DataTable();
          List<SliderEntities> ListEntities = new List<SliderEntities>();
          dt = CRUD.ReturnTableNull(sql);
          if (dt != null)
          {
              if (dt.Rows.Count > 0)
              {
                  ListEntities = (from DataRow row in dt.Rows
                                  select new SliderEntities
                                  {
                                      sl = sL++,
                                      SSL = int.Parse(row["SL"].ToString()),
                                      Location = row["Location"].ToString(),                                      
                                      Ordering = int.Parse(row["Ordering"].ToString()),
                                      Chosen = bool.Parse(row["Chosen"].ToString())
                                  }
                                  ).ToList();

                  return ListEntities;
              }

          }
          return ListEntities = null;

      }

      public  List<SliderEntities> getSliderData()
      {
          sql = " select Location from WSSlider where Chosen=1 order by Ordering";
          dt = new DataTable();
          List<SliderEntities> ListEntities = new List<SliderEntities>();
          dt = CRUD.ReturnTableNull(sql);
          if (dt != null)
          {
              if (dt.Rows.Count > 0)
              {
                  ListEntities = (from DataRow row in dt.Rows
                                  select new SliderEntities
                                  {
                                      Location = row["Location"].ToString()                                     
                                  }
                                  ).ToList();

                  return ListEntities;
              }

          }
          return ListEntities = null;

      }
    }
}
