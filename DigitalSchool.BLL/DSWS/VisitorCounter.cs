using DS.DAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.DSWS
{
   public  class VisitorCounter
    {
        private VisitorCounterEntities _Entities;
        bool result;
        string sql = "";
        int sL = 1;
        DataTable dt;
        public VisitorCounterEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("IF (NOT EXISTS(SELECT hitNumber FROM VisitorCounter WHERE lastUpdate =  '"+_Entities.lastUpdate+"' )) "+
      "BEGIN " +
           "INSERT INTO VisitorCounter(hitNumber, lastUpdate) VALUES(" + _Entities.hitNumber + ", '" + _Entities.lastUpdate + "') " +
      "END " +
      "ELSE " +
      "BEGIN " +
          "UPDATE VisitorCounter  SET hitNumber = " + _Entities.hitNumber + "  WHERE lastUpdate = '" + _Entities.lastUpdate + "' " +
      "END");
            return result = CRUD.ExecuteQuery(sql);
        }
        public int visitorNumber(string date)
        {
            sql = "SELECT hitNumber FROM VisitorCounter WHERE lastUpdate =  '" + date + "'";
            dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt == null || dt.Rows.Count == 0)
                return 0;
            else
                return int.Parse( dt.Rows[0]["hitNumber"].ToString());
             
        }
    }
}
