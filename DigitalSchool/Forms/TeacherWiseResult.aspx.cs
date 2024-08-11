using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;

namespace DS.Forms
{
    public partial class TeacherWiseResult : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Classes.commonTask.loadBatch(ddlBatch);
                        sqlDB.loadDropDownList("Select  SectionName from Sections  Order by SectionName", dlSection);
                        LoadDepartment(dlDepartment);
                    }
                }
            }
            catch { }
        }

        public void LoadDepartment(DropDownList dl)
        {
            try
            {
                dl.Items.Clear();
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select DId,DName From Departments_HR where DStatus='True' and DName!='MLS'", dt);
                dl.DataSource = dt;
                dl.DataTextField = "DName";
                dl.DataValueField = "DId";
                dl.DataBind();
                dl.Items.Add("--Select--");
                dl.Text = "--Select--";
            }
            catch { }
        }

        protected void dlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sqlDB.bindDropDownList("Select TCodeNo,EName From v_EmployeeInfo Where DName='" + dlDepartment.SelectedItem.Text + "' ", "TCodeNo", "EName", dlTeacher);
                dlTeacher.Items.Add("--Select--");
                dlTeacher.Text = "--Select--";
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadResult();
        }

        private void loadResult()
        {
            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet" + "_TotalResultProcess";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select SubName From v_ClassRoutine Where EName='"+dlTeacher.SelectedItem.Text+"' ", dt);
                DataTable dtSubGate = new DataTable();
                sqlDB.fillDataTable("Select BanglaGrammer_Grade From " + getTable + " Where SubName='" + dt.Rows[0]["SubName"] + "' ", dtSubGate);
            }
            catch { }
        }

    }
}