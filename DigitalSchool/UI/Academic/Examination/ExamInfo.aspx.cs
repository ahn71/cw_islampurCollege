using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ManagedBatch;
using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.BLL.Examinition;
using DS.PropertyEntities.Model.Examinition;
using DS.BLL.ControlPanel;
using DS.DAL;

namespace DS.UI.Academics.Examination
{
    public partial class ExamInfo : System.Web.UI.Page
    {
        ExamInfoEntry examInfoEntry;
        bool result;
        DataTable dt;
        SqlCommand cmd;
        SqlDataAdapter da;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        //---url bind---
                        aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                        aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                        aExamHome.HRef = "~/" + Classes.Routing.ExaminationHomeRouteUrl;
                        //---url bind end---
                        //dlBatch.Items.Add("---Select---");
                        // Classes.commonTask.loadBatch(dlBatch);

                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ExamInfo.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");

                        BatchEntry.GetDropdownlist(dlBatch,true);
                        ExamTypeEntry.GetExamType(dlExamType);               
                        LoadExamInfoId("");
                    }
                }
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loaddatatable();", true);
            if (trGroup.Visible) if(!groupValidation())return;
            if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; LoadExamInfoId(""); return; }
            btnSave.Enabled = false;
            saveExamInfo();   // for enterd exam information in exam info           
        }

        private bool groupValidation()  // when group ddl is visible then mustb select a group
        {
            try
            {
               
               if (ddlGroup.SelectedValue.ToString() == "0")
                {
                    lblMessage.InnerText = "error->Please selecct a group";
                    LoadExamInfoId("");
                    return false;
                    
                }
                
                return true;
            }
            catch { return false; }
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
                if (dlBatch.Text == "---Select--" || dlExamType.Text == "---Select---" || txtStartDate.Text.Trim().Length < 10 || txtEndDate.Text.Trim().Length<10) return;
                else
                {
                    if (trGroup.Visible)
                    {
                        lblExamId.Text = dlExamType.SelectedItem.Text + "_" + txtStartDate.Text.Trim() +"_"+txtEndDate.Text+"_"+ dlBatch.SelectedItem.Text + "_" + ddlGroup.SelectedValue.ToString() + "_" + ddlGroup.SelectedItem.Text.Trim();
                    }
                    else lblExamId.Text = dlExamType.SelectedItem.Text + "_" + txtStartDate.Text.Trim() +"_"+txtEndDate.Text+"_"+"_" + dlBatch.SelectedItem.Text;
                }
            }
            catch { }
        }

        protected void dlExamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            setExamInfoId();
            
        }

        private void loadSectionClayseWise()
        {
            try
            {
                SQLOperation.selectBySetCommandInDatatable("Select distinct ClsGrpID,GroupName From v_Tbl_Class_Group where ClassId=(select ClassId from BatchInfo where BatchId=" + dlBatch.SelectedValue.ToString() + ")", dt = new DataTable(), sqlDB.connection);
                
                if (dt.Rows.Count>0)
                {
                    trGroup.Visible = true;
                    ddlGroup.Items.Clear();

                    ddlGroup.DataTextField = "GroupName";
                    ddlGroup.DataValueField = "ClsGrpID";
                    ddlGroup.DataSource = dt;
                    ddlGroup.DataBind();
                    ddlGroup.Items.Insert(0, new ListItem("Select", "0"));
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

        private ExamInfoEntity GetData
        {
           get
            {
                try
                {
                   
                    ExamInfoEntity eie = new ExamInfoEntity();
                    eie.ExInSl = 0;
                    eie.ExInId = lblExamId.Text.Trim();
                    eie.ExStartDate = DateTime.Parse(Classes.commonTask.ddMMyyyyToyyyyMMdd(txtStartDate.Text.Trim()));
                    eie.ExEndDate = DateTime.Parse(Classes.commonTask.ddMMyyyyToyyyyMMdd(txtEndDate.Text.Trim()));
                    eie.BatchId =int.Parse(dlBatch.SelectedValue.ToString());
                    eie.ExName = txtExamName.Text.Trim();
                    eie.ClsGrpID = int.Parse(ddlGroup.SelectedValue);
                    
                    return eie;
                }
                catch { return null; }
            }
            
        }
       

        private void saveExamInfo()   // for enterd exam information in exam info table 
        {
            try
            {
                using (ExamInfoEntity eie = GetData)
                {
                    if (examInfoEntry == null) examInfoEntry = new ExamInfoEntry();
                    examInfoEntry.SetExamInfoEntity = eie;

                    result=examInfoEntry.Insert(dlExamType.SelectedValue.ToString());
                    if (result)
                    {
                        //bellow method is comented. It's not need to generate here. It will be generated when search  marksheet on entry panel for entry marks (Date: 24-02-2020)
                       // EnterdEssentialStudentInfo(); // for enterd data in marksheet table  of this class
                        LoadExamInfoId("");
                        Response.Redirect("/UI/Academic/Examination/ExamInfo.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "warning->Already this exam id generated !";
                LoadExamInfoId("");
            }
        }
       

        private void EnterdEssentialStudentInfo()   // for enterd data in marksheet table  of this class
        {
            try
            {
                string getClass = new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getMarkSheetName = "Class_" + getClass + "MarksSheet";

                /* load total student of this class by batch */
                DataTable dtCS = new DataTable();  //CS =current student
                if (!trGroup.Visible) SQLOperation.selectBySetCommandInDatatable("select StudentId,RollNo,ClsSecId,ConfigId from CurrentStudentInfo where BatchId='" + dlBatch.SelectedItem.Value.ToString() + "' order by StudentId", dtCS, sqlDB.connection);
                else SQLOperation.selectBySetCommandInDatatable("select StudentId,RollNo,ClsSecId,ConfigId from CurrentStudentInfo where BatchId='" + dlBatch.SelectedItem.Value.ToString() + "' AND ClsGrpId='" + ddlGroup.SelectedItem.Value.Trim() + "' order by StudentId", dtCS, sqlDB.connection);

                /* load total subject question pattern id of this class */
                DataTable dtSQPId = new DataTable();  // SQP=Subject 
                if (!trGroup.Visible) SQLOperation.selectBySetCommandInDatatable("select distinct SubQPId,ConvertTo,SubId,CourseId from v_SubjectQuestionPattern where ExId=" + dlExamType.SelectedValue.ToString() + " AND BatchId='" + dlBatch.SelectedValue.ToString() + "' order by SubQPId", dtSQPId, sqlDB.connection);
                else SQLOperation.selectBySetCommandInDatatable("select distinct SubQPId,ConvertTo,SubId,CourseId from v_SubjectQuestionPattern where ExId=" + dlExamType.SelectedValue.ToString() + " AND BatchId='" + dlBatch.SelectedValue.ToString() + "' AND ClsGrpId='" + ddlGroup.SelectedValue.ToString() + "' order by SubQPId", dtSQPId, sqlDB.connection);



                for (int i = 0; i < dtCS.Rows.Count; i++)
                {
                    for (byte b = 0; b < dtSQPId.Rows.Count; b++)
                    {
                        string getGroup = (trGroup.Visible) ? ddlGroup.SelectedItem.Value : "0";
                        string[] getColumns = { "ExId", "ExInId", "StudentId", "RollNo", "BatchId", "ClsSecId", "ShiftId", "SubQPId", "ConvertToPercentage", "ClsGrpID", "SubId", "CourseId" };
                        string[] getValues = { dlExamType.SelectedValue.ToString(), lblExamId.Text.Trim(), dtCS.Rows[i]["StudentId"].ToString(), dtCS.Rows[i]["RollNo"].ToString(), dlBatch.SelectedItem.Value.ToString(), dtCS.Rows[i]["ClsSecId"].ToString(), dtCS.Rows[i]["ConfigId"].ToString(), 
                                                 dtSQPId.Rows[b]["SubQPId"].ToString(),dtSQPId.Rows[b]["ConvertTo"].ToString(), getGroup,dtSQPId.Rows[b]["SubId"].ToString(),dtSQPId.Rows[b]["CourseId"].ToString() };
                    //    SQLOperation.forSaveValue(getMarkSheetName, getColumns, getValues, sqlDB.connection);
                        //--------------
                        CRUD.ExecuteQuery("insert into " + getMarkSheetName + " (ExId, ExInId, StudentId, RollNo, BatchId, ClsSecId, ShiftId, SubQPId, ConvertToPercentage, ClsGrpID, SubId, CourseId)" + 
                            " values('" + dlExamType.SelectedValue.ToString() + "', '" + lblExamId.Text.Trim() + "','" + dtCS.Rows[i]["StudentId"].ToString() + "','" + dtCS.Rows[i]["RollNo"].ToString() +
                            "','" + dlBatch.SelectedItem.Value.ToString() + "','" + dtCS.Rows[i]["ClsSecId"].ToString() + "','" + dtCS.Rows[i]["ConfigId"].ToString() + "', '" + dtSQPId.Rows[b]["SubQPId"].ToString() +
                            "','" + dtSQPId.Rows[b]["ConvertTo"].ToString() + "', '" + getGroup + "','" + dtSQPId.Rows[b]["SubId"].ToString() + "','" + dtSQPId.Rows[b]["CourseId"].ToString() + "' )");
                        
                        //---------------
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void LoadExamInfoId(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select ExInId +' | ' + case when ExName is null then '' else ExName end ExInId from ExamInfo order by ExInSl Desc ";
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

        protected void dlDependency_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}