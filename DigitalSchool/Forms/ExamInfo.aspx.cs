using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using ComplexScriptingSystem;

namespace DS.Forms
{
    public partial class ExamInfo : System.Web.UI.Page
    {
        DataTable dt;
        SqlCommand cmd;
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
                        trDependency.Visible = false;
                        dlBatch.Items.Add("---Select---");
                        Classes.commonTask.loadBatch(dlBatch);
                        dlExamType.Items.Add("---Select---");
                        Classes.Exam.LoadExamType(dlExamType);
                        Classes.Exam.loadDependencyExam(dlDependency);
                        LoadExamInfoId("");
                    }
                }
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveExamInfo();   // for enterd exam information in exam info           
        }

        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            setExamInfoId();
            loadSectionClayseWise();
        }

        private void setExamInfoId()
        {
            try
            {
                if (dlBatch.Text == "---Select--" || dlExamType.Text == "---Select---" || txtDate.Text.Trim().Length < 4) return;
                else
                {
                    if (trGroup.Visible)
                    {
                        lblExamId.Text = dlExamType.SelectedItem.Text + "_" + txtDate.Text.Trim() + "_" + dlBatch.Text+"_"+ddlGroup.SelectedItem.Text.Trim();
                    }
                    else  lblExamId.Text =dlExamType.SelectedItem.Text + "_" + txtDate.Text.Trim()+"_"+ dlBatch.Text;
                }
            }
            catch { }
        }

        protected void dlExamType_SelectedIndexChanged(object sender, EventArgs e)
        {
           setExamInfoId();
           checkDependencyExam();          
        }

        private void loadSectionClayseWise()
        {
            try
            {
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='" + new String(dlBatch.SelectedItem.Text.Trim().Where(Char.IsLetter).ToArray()) + "'", dt = new DataTable(), sqlDB.connection);
                if ((dt.Rows[0]["ClassOrder"].ToString().Equals("9") || (dt.Rows[0]["ClassOrder"].ToString().Equals("10"))))
                {
                    trGroup.Visible = true;
                    ddlGroup.Items.Clear();
                    ddlGroup.Items.Add("...Select...");
                    ddlGroup.Items.Add("Science");
                    ddlGroup.Items.Add("Commerce");
                    ddlGroup.Items.Add("Arts");

                    ddlGroup.SelectedIndex = ddlGroup.Items.Count - ddlGroup.Items.Count;
                }

                else
                {
                    trGroup.Visible = false;
                }
            }
            catch { }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
           setExamInfoId();
        }

        private void saveExamInfo()   // for enterd exam information in exam info table 
        {
            try
            {
                string getDependencyExay=(!trDependency.Visible)?"0":dlDependency.SelectedItem.Text;
                string[] getColumns = { "ExInDate", "BatchName", "ExInDependency","ExInId" };
                string[] getValues = {txtDate.Text.Trim(),dlBatch.Text.Trim(),getDependencyExay,lblExamId.Text.Trim()};
                if (SQLOperation.forSaveValue("ExamInfo", getColumns, getValues,sqlDB.connection) == true)
                {
                    EnterdEssentialStudentInfo(); // for enterd data in marksheet table  of this class
                    LoadExamInfoId("");
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "warning->Already this exam id generated !";
            }
        }
        private void checkDependencyExam()
        {
            try
            {
                sqlDB.fillDataTable("select ExDependency from ExamType where ExId='" + dlExamType.Text.Trim() + "' AND ExDependency="+1+"", dt = new DataTable());
                if (dt.Rows.Count > 0) trDependency.Visible = true;
                else trDependency.Visible = false;
            }
            catch { }
        }

        private void EnterdEssentialStudentInfo()   // for enterd data in marksheet table  of this class
        {
            try
            {
                string getClass = new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getMarkSheetName = "Class_" + getClass + "MarksSheet";

                /* load total student of this class by batch */
                DataTable dtCS = new DataTable();  //CS =current student
                if (!trGroup.Visible) SQLOperation.selectBySetCommandInDatatable("select StudentId,RollNo,SectionName,Shift from CurrentStudentInfo where BatchName='" + dlBatch.SelectedItem.Text + "' order by StudentId", dtCS, sqlDB.connection);
                else SQLOperation.selectBySetCommandInDatatable("select StudentId,RollNo,SectionName,Shift from CurrentStudentInfo where BatchName='" + dlBatch.SelectedItem.Text + "' AND SectionName='"+ddlGroup.SelectedItem.Text.Trim()+"' order by StudentId", dtCS, sqlDB.connection);

                /* load total subject question pattern id of this class */
                DataTable dtSQPId = new DataTable();  // SQP=Subject 
                if (!trGroup.Visible) SQLOperation.selectBySetCommandInDatatable("select SubQPId,ConvertTo from v_SubjectQuestionPattern where ExId=" + dlExamType.SelectedValue.ToString() + " AND ClassName='" + getClass + "' order by SubQPId", dtSQPId, sqlDB.connection);
                else SQLOperation.selectBySetCommandInDatatable("select SubQPId,ConvertTo from v_SubjectQuestionPattern where ExId=" + dlExamType.SelectedValue.ToString() + " AND ClassName='" + getClass + "' AND GroupName='"+ddlGroup.SelectedItem.Text.Trim()+"' order by SubQPId", dtSQPId, sqlDB.connection);



                for (int i = 0; i < dtCS.Rows.Count; i++)
                {
                    for (byte b = 0; b < dtSQPId.Rows.Count; b++)
                    {
                        string getGroup = (trGroup.Visible) ? ddlGroup.SelectedItem.Text : "NoGroup";
                        string[] getColumns = { "ExId", "ExInId", "StudentId", "RollNo", "BatchName", "SectionName", "Shift", "SubQPId", "ConvertTo", "GroupName" };
                        string[] getValues = { dlExamType.SelectedValue.ToString(), lblExamId.Text.Trim(), dtCS.Rows[i]["StudentId"].ToString(), dtCS.Rows[i]["RollNo"].ToString(), dlBatch.SelectedItem.Text, dtCS.Rows[i]["SectionName"].ToString(), dtCS.Rows[i]["Shift"].ToString(), dtSQPId.Rows[b]["SubQPId"].ToString(), dtSQPId.Rows[b]["ConvertTo"].ToString(),getGroup};
                        SQLOperation.forSaveValue(getMarkSheetName, getColumns, getValues, sqlDB.connection);                       
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void LoadExamInfoId(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select ExInId  from ExamInfo order by ExInSl Desc ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";

            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Exam ID available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divExamInfoId.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblClassList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Exam Info ID</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["ExInId"].ToString() + "</td>";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";
            divExamInfoId.Controls.Add(new LiteralControl(divInfo));
        }

        private void setStudentIdAndSection()   // set student id and section name in marks sheet 
        {
            
        }
    }
}