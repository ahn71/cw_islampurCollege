using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace DS.Forms.machine_attendance
{
    
    public partial class student_attendance_list : System.Web.UI.Page
    {
        DataTable dt;
        SqlCommand cmd;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    Classes.commonTask.loadShift(ddlShift);
                    Classes.commonTask.loadBatch(ddlBatch);
                    Classes.commonTask.loadSection(ddlSectionName);
                    loadStudentAttendanceList();
                }
            }
        }

        private void loadStudentAttendanceList()
        {
            try
            {
                lblMessage.InnerText = "";
                if (ddlShift.SelectedItem.Text == "--Select Shift--" && ddlBatch.SelectedItem.Text == "...Select Batch..." && ddlSectionName.SelectedItem.Text == "--Select--")
                sqlDB.fillDataTable("select StudentId,AttDate,AdmissionNo,FullName,Gender,LoginTime,LogoutTime,AttStatus,AttManual,AttMonth from v_DailyStudentAttendanceRecord", dt = new DataTable());
                else sqlDB.fillDataTable("select StudentId,AttDate,AdmissionNo,FullName,Gender,LoginTime,LogoutTime,AttStatus,AttManual,AttMonth from v_DailyStudentAttendanceRecord where Shift='" + ddlShift.SelectedItem.Text + "'and BatchName='" + ddlBatch.SelectedItem.Text + "' and SectionName='"+ddlSectionName.SelectedItem.Text+"'", dt = new DataTable());
                gvStudentAttendanceList.DataSource = dt;
                gvStudentAttendanceList.DataBind();
                if (dt.Rows.Count == 0) lblMessage.InnerText = "warning->No Attendance Available";
            }
            catch { }
        }

        protected void gvStudentAttendanceList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("delete"))
                {
                    //int getIndex = Convert.ToInt32(e.CommandArgument.ToString());
                    string[] getValues = e.CommandArgument.ToString().Split(',');
                    string attDate=getValues[1].Substring(6, 4) + "-" + getValues[1].Substring(3, 2) + "-" + getValues[1].Substring(0, 2);
                    cmd = new SqlCommand("delete from DailyAttendanceRecord where AttDate='"+attDate+"' And StudentId="+getValues[0]+"",sqlDB.connection);
                    int  IsDelete=cmd.ExecuteNonQuery();                 
                    loadStudentAttendanceList();                  
                   
                    
                }
            }
            catch { }
        }

        protected void gvStudentAttendanceList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            loadStudentAttendanceList();
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            loadStudentAttendanceList();
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            ddlBatch.SelectedIndex = ddlBatch.Items.Count-1;
            ddlSectionName.SelectedIndex = ddlSectionName.Items.Count - 1 ;
            ddlShift.SelectedIndex = ddlShift.Items.Count - 1;
            loadStudentAttendanceList();
        }

        
    }
}