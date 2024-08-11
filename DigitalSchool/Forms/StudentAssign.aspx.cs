using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using DS.BLL;

namespace DS.Forms
{
    public partial class StudentAssign : System.Web.UI.Page
    {
        DataTable dt;
        SqlDataAdapter da;
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
                    Classes.commonTask.loadBatchLog(dlOldBatchs);
                    Classes.commonTask.loadSection(dlSection);
                    loadCurrentStudentInfo("");
                }
            }
        }

        private void loadCurrentStudentInfo(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "select StudentId,ClassName,SectionName,FullName,RollNo,Gender,Mobile from v_CurrentStudentInfo where BatchName='" + dlOldBatchs.Text + "'  and SectionName='" + dlSection.Text + "' ";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (dlOldBatchs.Text == "---Select---" || dlSection.Text == "---Select---")
                {
                    divInfo = "<div class='noData'>Select class and section</div>";
                    divInfo += "<div class='dataTables_wrapper'></div>";
                    divStudentDetails.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No student available</div>";
                    divInfo += "<div class='dataTables_wrapper'></div>";
                    divStudentDetails.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th>Class Name</th>";
                divInfo += "<th>Section Name</th>";
                divInfo += "<th>Full Name</th>";
                divInfo += "<th>Roll No</th>";
                divInfo += "<th>Gender</th>";
                divInfo += "<th>Mobile</th>";


                divInfo += "<th class='numeric control'>Select</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {

                    id = dt.Rows[x]["StudentId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["ClassName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["SectionName"].ToString() + "</td>";
                    divInfo += "<td >" + adviitScripting.getNameForFixedLength(dt.Rows[x]["FullName"].ToString(), 12) + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Gender"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["Mobile"].ToString() + "</td>";

                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/datatables/select.png'  onclick='studentAssign(" + id + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divStudentDetails.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dlOldBatchs.Text.ToString()))
                {
                    return;
                }

                string sqlCmd = "select StudentId,ClassName,SectionName,FullName,RollNo,Gender,Mobile from v_CurrentStudentInfo where BatchName='" + dlOldBatchs.Text + "'  and SectionName='" + dlSection.Text + "' ";
                loadCurrentStudentInfo(sqlCmd);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSsign_Click(object sender, EventArgs e)
        {
           assignStudent();
        }

        private void assignStudent()
        {
            try
            {
               string getClass = new String(dlOldBatchs.Text.Where(Char.IsLetter).ToArray());
               sqlDB.fillDataTable("select ClassName,ClassOrder from Classes",dt=new DataTable());

               var getOrder = dt.Select("className='"+getClass+"'");
               int getNewOrder = int.Parse(getOrder[0]["ClassOrder"].ToString()) + 1;
               var getNewClass = dt.Select("ClassOrder="+getNewOrder+"");

               da = new SqlDataAdapter("select RollNo from CurrentStudentInfo where batchName='" + getNewClass[0]["ClassName"].ToString() + TimeZoneBD.getCurrentTimeBD().Year.ToString() + "' AND SectionName = (select SectionName from CurrentStudentInfo where StudentId=" + lblStudentId.Value.ToString() + ")", sqlDB.connection);
               da.Fill(dt = new DataTable());
               if (dt.Rows.Count > 0)
               {
                   lblMessage.InnerText = "worning-> Already this roll number enterd !";
                   return;
               }

                cmd = new SqlCommand("update CurrentStudentInfo set BatchName='"+getNewClass[0]["ClassName"].ToString()+ TimeZoneBD.getCurrentTimeBD().Year.ToString()+"',RollNo=" + txtStRoll.Text.Trim() + " where StudentId=" + lblStudentId.Value.ToString() + "", sqlDB.connection);
                byte b = byte.Parse(cmd.ExecuteNonQuery().ToString());
                txtStName.Text = ""; txtStRoll.Text = "";
                if (b > 0)
                {
                    lblMessage.InnerText = "success->Successfully assign";
                    loadCurrentStudentInfo("");
                }
            }
            catch { }
        }
    }
}