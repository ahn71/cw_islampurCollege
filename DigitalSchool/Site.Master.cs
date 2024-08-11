using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adviitRuntimeScripting;
using System.Data;

namespace DS
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
               // if (Session["__username__"] == null) Response.Redirect("/Login.aspx"); //Check valid login
                //lblMsg.Text = Session["formName"].ToString();
               // lblUsername.Text = Session["__username__"].ToString();
               // lblName.Text = Session["__username__"].ToString();

             //   setMenu();
            }
            catch {
                // lblMsg.Text = "Home"; 
            }            
        }      

        public void setMenu()
        {
            try
            {
                DataTable dt=new DataTable ();
                sqlDB.fillDataTable("select CountType from AttendanceCountType", dt);
                if (dt.Rows[0]["CountType"].ToString().Equals("Machine System"))
                {
                    liStudentManuallyAttendance.Visible = false;
                    liTeacherManuallyAttendance.Visible = false; 
                }
                else
                {
                    liStudentMachineAttendance.Visible = false;
                    liTeacherMachineAttendance.Visible = false;
                }
            }
            catch { }
        }
    }
}
