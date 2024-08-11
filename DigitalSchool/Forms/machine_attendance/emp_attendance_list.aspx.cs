using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms.machine_attendance
{
    public partial class emp_attendance_list : System.Web.UI.Page
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
                    Classes.commonTask.LoadDeprtmentAtttedence(dlDepartment);
                    Classes.commonTask.LoadDesignation(dlDesignation);
                    loadEmployeeAttendanceList();
                }
            }
        }
        private void loadEmployeeAttendanceList()
        {
            try
            {
                sqlDB.fillDataTable("select EId,AttDate,ECardNo,EName,EGender,LoginTime,LogoutTime,AttStatus,AttManual,AttMonth from v_DailyEmployeeAttendanceRecord order by attDate,ECardNo", dt = new DataTable());
                gvEmpAttendanceList.DataSource = dt;
                gvEmpAttendanceList.DataBind();
            }
            catch { }
        }

        protected void gvStudentAttendanceList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            loadEmployeeAttendanceList();
        }

        protected void gvEmpAttendanceList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("delete"))
                {
                    //int getIndex = Convert.ToInt32(e.CommandArgument.ToString());
                    string[] getValues = e.CommandArgument.ToString().Split(',');
                    string attDate = getValues[1].Substring(6, 4) + "-" + getValues[1].Substring(3, 2) + "-" + getValues[1].Substring(0, 2);
                    cmd = new SqlCommand("delete from DailyAttendanceRecord where AttDate='" + attDate + "' And EID=" + getValues[0] + "", sqlDB.connection);
                    int IsDelete = cmd.ExecuteNonQuery();              
                    loadEmployeeAttendanceList();
               


                }
            }
            catch { }
        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch { }
        }

    }
}