using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL;

namespace DS.Forms
{
    public partial class IndividualBatchAssign : System.Web.UI.Page
    {
        SqlCommand cmd;
        DataTable dt;
        SqlDataAdapter da;

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
                        Classes.commonTask.loadClass(dlClass);
                        Classes.commonTask.loadSection(dlSectionAssign);
                        Classes.commonTask.loadBatch(dlBatch);
                    }
                }
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadCurrentStudentInfo("");
        }

        private void loadCurrentStudentInfo(string sqlCmd)
        {
            try
            {
                dlBatch.Text = dlClass.SelectedItem.Text + TimeZoneBD.getCurrentTimeBD().Year.ToString();
                dlSectionAssign.Text = dlSection.SelectedItem.Text;

                sqlCmd = "select StudentId,ClassName,SectionName,FullName,RollNo,Gender,Mobile from v_CurrentStudentInfo where ClassName='" + dlClass.SelectedItem.Text + "' and SectionName='" + dlSection.SelectedItem.Text + "' and Shift='" + dlShift.SelectedItem.Text + "' and BatchName is null ";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (dlClass.SelectedItem.Text == "---Select---" || dlSection.SelectedItem.Text == "---Select---")
                {
                    divInfo = "<div class='noData'>Select class and section</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divStudentDetails.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No student available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divStudentDetails.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th>Class</th>";
                divInfo += "<th>Section</th>";
                divInfo += "<th>Name</th>";
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

        protected void btnSsign_Click(object sender, EventArgs e)
        {
            if(txtStName.Text!="" || txtStRoll.Text!="") assignStudent();
        }

        private void assignStudent()
        {
            try
            {
                if (dlClass.SelectedItem.Text.ToLower() != new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()).ToLower())
                {
                    lblMessage.InnerText = "warning->Do you want change Batch Name?";
                    return;
                }


                string getClass = dlClass.SelectedItem.Text;

                da = new SqlDataAdapter("select RollNo from CurrentStudentInfo where BatchName='" + dlBatch.SelectedItem.Text + "' AND SectionName = (select SectionName from CurrentStudentInfo where StudentId=" + lblStudentId.Value.ToString() + ") AND RollNo="+txtStRoll.Text.Trim()+" ", sqlDB.connection);
                da.Fill(dt = new DataTable());
                if (dt.Rows.Count > 0)
                {
                    lblMessage.InnerText = "warning-> Already this roll number enterd !";
                    return;
                }

                cmd = new SqlCommand("update CurrentStudentInfo set BatchName='" + dlBatch.SelectedItem.Text + "', RollNo=" + txtStRoll.Text.Trim() + ", SectionName='"+dlSectionAssign.SelectedItem.Text+"' where StudentId=" + lblStudentId.Value.ToString() + "", sqlDB.connection);
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

        protected void dlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSectionClayseWise();
        }

        private void loadSectionClayseWise()
        {
            try
            {
                DataTable dt;
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='" + dlClass.SelectedItem.Text.Trim() + "'", dt = new DataTable(), sqlDB.connection);
                if (byte.Parse(dt.Rows[0]["ClassOrder"].ToString()) >= 9)
                {

                    dlSection.Items.Clear();
                    dlSection.Items.Add("...Select...");
                    dlSection.Items.Add("Science");
                    dlSection.Items.Add("Commerce");
                    dlSection.Items.Add("Arts");
                    dlSection.SelectedIndex = dlSection.Items.Count - dlSection.Items.Count;

                    dlSectionAssign.Items.Clear();
                    dlSectionAssign.Items.Add("...Select...");
                    dlSectionAssign.Items.Add("Science");
                    dlSectionAssign.Items.Add("Commerce");
                    dlSectionAssign.Items.Add("Arts");
                    dlSectionAssign.SelectedIndex = dlSectionAssign.Items.Count - dlSectionAssign.Items.Count;
                }
                else
                {
                    dlSection.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' AND SectionName<>'Arts' Order by SectionName", dlSection);
                    dlSection.Items.Add("...Select...");
                    dlSection.SelectedIndex = dlSection.Items.Count - 1;

                    dlSectionAssign.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' AND SectionName<>'Arts' Order by SectionName", dlSectionAssign);
                    dlSectionAssign.Items.Add("...Select...");
                    dlSectionAssign.SelectedIndex = dlSectionAssign.Items.Count - 1;
                }
            }
            catch { }
        }
    }
}