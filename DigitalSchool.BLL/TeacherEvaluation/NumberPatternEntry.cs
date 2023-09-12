using DS.DAL;
using DS.PropertyEntities.Model.TeacherEvaluation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.TeacherEvaluation
{
    public class NumberPatternEntry : IDisposable
    {
        private NumberPatternDetailsEntities _EntitiesD;
        private NumberPatternEntities _Entities;
        string sql = string.Empty;
        bool result = false;
        DataTable dt;
        public NumberPatternEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public NumberPatternDetailsEntities AddEntitiesD
        { 
            set
            {
                _EntitiesD=value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[TE_NumberPattern] " +
                 "([NumPattern])"
                 + " VALUES ( '" + _Entities.NumPattern + "'); SELECT CAST(scope_identity() AS int)");
            //CRUD.ExecuteQuery(sql);
            //sql = "SELECT NumPattern FROM TE_NumberPattern";
            _Entities.NumPatternID = CRUD.GetMaxID(sql);
            return true;
        }
        public bool Update()
        {
            sql = string.Format("UPDATE  [dbo].[TE_NumberPattern] SET [NumPattern]='" + _Entities.NumPattern + "' " +
                 " WHERE [NumPatternID]=" + _Entities.NumPatternID + "; delete TE_NumberPatternDetails where NumPatternID="+_Entities.NumPatternID+"");
            //CRUD.ExecuteQuery(sql);
            //sql = "SELECT NumPattern FROM TE_NumberPattern";
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool InsertD()
        {
            sql = string.Format("INSERT INTO [dbo].[TE_NumberPatternDetails] " +
                 "([NumPatternID],[SubCategoryID],[FullNumber],[Excellent],[Good],[Medium],[Weak],[SoWeak])"
                 + " VALUES ( " + _Entities.NumPatternID + "," + _EntitiesD.SubCategoryID + "," + _EntitiesD.FullNumber +
                 "," + _EntitiesD.Excellent + "," + _EntitiesD.Good + "," + _EntitiesD.Medium + "," +
                 _EntitiesD.Weak + "," + _EntitiesD.SoWeak + ")");
          
            return result = CRUD.ExecuteQuery(sql);
        }
        public void loadNumberPatternInGridview(GridView gv,string NumberPatternId) 
        {
            string chk = (NumberPatternId == "0") ? ",convert(bit,'true') as chk" : ",ISNULL(chk,'false') chk";
            sql = string.Format("with npd as (select *,convert(bit,'true') as chk from TE_NumberPatternDetails where NumPatternID=" + NumberPatternId + ") " +
                "SELECT sc.[SubCategoryID],[SubCategory],sc.[Ordering],c.[CategoryID],[Category],c.[Ordering],npd.FullNumber,npd.Excellent,npd.Good,npd.Medium,npd.Weak,npd.SoWeak" + chk + " FROM [dbo].[TE_SubCategory] sc " +                
                " left join npd on sc.SubCategoryID=npd.SubCategoryID inner join TE_Category c on c.CategoryID=sc.CategoryID where sc.Status=1  ORDER BY c.[Ordering],sc.[Ordering]");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            gv.DataSource = dt;
            gv.DataBind();
        }
        public List<NumberPatternEntities> GetEntitiesData()
        {
            List<NumberPatternEntities> ListEntities = new List<NumberPatternEntities>();
            sql = string.Format("select NumPatternID,NumPattern from TE_NumberPattern");
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new NumberPatternEntities
                                    {
                                        NumPattern = row["NumPattern"].ToString(),
                                        NumPatternID = int.Parse(row["NumPatternID"].ToString())
                                        

                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
     
        public static void GetDropdownlist(DropDownList dl)
        {
            
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("select NumPatternID,NumPattern from TE_NumberPattern");
            dl.DataSource = dt;
            dl.DataValueField = "NumPatternID";
            dl.DataTextField = "NumPattern";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        
        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            disposed = true;
        }
    }
}
