using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DS.DAL;

namespace DS.BLL.DSWS
{
   public static  class EBooksBLL
    {
       public static DataTable getEbooksData(string ClassId) 
       {
           string sql = "select BookTiltle,BookImage,BookReadUrl,BookDownUrl from WSEbooks where BookCatagory='"+ClassId+"'";
        DataTable dt=new DataTable();
        dt = CRUD.ReturnTableNull(sql);
          return dt;
       }
    }
}
