using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.PropertyEntities.Model.DSWS
{
    public class GroupSubject
    {

        public static string getSubsName(string manSubIds)
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("SELECT STRING_AGG(subname ,',') as subname FROM NewSubject WHERE SubId IN(" + manSubIds + ")");

            return dt.Rows[0]["subname"].ToString();
        }
         
    }
    
}
