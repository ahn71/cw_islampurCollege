using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.PropertyEntities.Model.Attendance;
using System.Data;
using DS.DAL;
using System.Web.UI.WebControls;

namespace DS.BLL.Attendace
{
    public class AbsentStudents : IDisposable
    {
        private AbsentStudentsEntities _Entities;
        string sql = string.Empty;
        bool result = true;
        public AbsentStudents() { }       
        public List<AbsentStudentsEntities> GetEntitiesData(string condtion)
        {
            List<AbsentStudentsEntities> ListEntities = new List<AbsentStudentsEntities>();
            sql = string.Format("SELECT Distinct [StudentId],[FullName],[RollNo],[ClassName],[GroupName],[SectionName],"
                                + "[ShiftName],[GuardianMobileNo],[ClassOrder] " +
                                "FROM v_DailyAbsentRecord "+condtion+"");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows
                                    select new AbsentStudentsEntities
                                    {
                                        AbsentStdID = int.Parse(row["StudentId"].ToString()),
                                        StudentName = row["FullName"].ToString(),
                                        Roll = int.Parse(row["RollNo"].ToString()),
                                        ClassName = row["ClassName"].ToString(),
                                        Section = row["SectionName"].ToString(),
                                        Shift = row["ShiftName"].ToString(),
                                        GuardiantMobile = row["GuardianMobileNo"].ToString()
                                    }).ToList();
                    return ListEntities;
                }

            }
            return ListEntities = null;
        }
        public void LoadBatchWaysRollNo(DropDownList dl, string BatchId, string ClsGrpId, string ClsSecId, string ShiftId)
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("Select RollNo,StudentId From CurrentStudentInfo where ConfigId='" 
                + ShiftId + "' and BatchId='" + BatchId + "' and ClsGrpId='" + ClsGrpId + "' and ClsSecId='" 
                + ClsSecId + "' ORDER BY RollNo");
            dl.DataSource = dt;
            dl.DataTextField = "RollNo";
            dl.DataValueField = "StudentId";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public void LoadCardNo(DropDownList dl, string DepartmentList, string DesignationList, string ShiftId, string empType)  // For Load Staff & Faculty Card No.
        {
            empType = (empType == "2") ? "" : "and IsFaculty=" + empType + "";
            ShiftId = (ShiftId == "0")? "":" and ShiftId='" + ShiftId + "'";
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull(" select EID,CONVERT(varchar(30),ECardNo)+'_'+EName as ECardNo from EmployeeInfo where DId in(" + DepartmentList + ") and DesId in(" + DesignationList + ") " + ShiftId + " " + empType + "");
            dl.DataSource = dt;
            dl.DataTextField = "ECardNo";
            dl.DataValueField = "EID";
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
