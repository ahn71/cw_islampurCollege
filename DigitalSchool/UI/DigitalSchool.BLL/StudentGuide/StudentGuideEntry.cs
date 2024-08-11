using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.StudentGuide;
using System.Web.UI.WebControls;

namespace DS.BLL.StudentGuide
{
    public class StudentGuideEntry: IDisposable
    {
        private StudentGuideEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        DataTable dt;
        public StudentGuideEntry()
        {
            
        }

        public StudentGuideEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }     

        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[tbl_Guide_Teacher] " +
                "([StudentId],[EID],[GuideStatus]) VALUES ( '" + _Entities
                .StudentId + "','" + _Entities.EID + "','" + _Entities.GuideStatus + "')");
            return result = CRUD.ExecuteQuery(sql);             
        }     

        public bool Update()
        {
            //sql = string.Format("UPDATE [dbo].[Tbl_Bu‎ilding_Name] SET " +
            //    "[BuildingName] = '" + _Entities.BuildingName + "' WHERE [BuildingId] = '" + _Entities.BuildingId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public DataTable LoadAdviserList(string DId)
        {
            try
            {
                if (DId == "0")
                {
                    sql = string.Format("SELECT Distinct EID,TCodeNo+'_'+EName as EName FROM v_Guide_Teacher WHERE GuideStatus='True'");
                }
                else
                {
                    sql = string.Format("SELECT Distinct EID,TCodeNo+'_'+EName as EName FROM v_Guide_Teacher WHERE GuideStatus='True' AND DId='"+DId+"'");
                }
               dt = CRUD.ReturnTableNull(sql);
               return dt;
            }
            catch { return dt; }
        }
        public DataTable LoadStudentList(string EID)
        {
            try
            {
                sql = string.Format("SELECT StudentId,FullName,ShiftName,BatchName,Case  WHEN  GroupName IS NULL then "
                + "'No Group' ELSE GroupName END as GroupName,SectionName,RollNo FROM v_Guide_Teacher WHERE EID='" + EID + "' AND GuideStatus='True'");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public DataTable LoadAdviserInfo(string EID)
        {
            try
            {
                sql = string.Format("SELECT EID,EName,ECardNo,TCodeNo,DName,DesName,EPictureName FROM v_EmployeeInfo WHERE EID='"+EID+"'");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public  void LoadDepartment(DropDownList dl)
        {
            try
            {
                sql = string.Format("SELECT DISTINCT DId, DName FROM v_Guide_Teacher WHERE  DId  is Not Null ");
                dt = CRUD.ReturnTableNull(sql);
                dl.DataSource = dt;
                dl.DataTextField = "DName";
                dl.DataValueField = "DId";
                dl.DataBind();
                dl.Items.Insert(0,new ListItem("All","0"));
            }
            catch {}
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
