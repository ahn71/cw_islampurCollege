using DS.DAL.ComplexScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.BLL.Examinition;
using DS.BLL.ControlPanel;

namespace DS.UI.Reports.Examination
{
    public partial class AcademicTranscript : System.Web.UI.Page
    {
        Class_ClasswiseMarksheet_TotalResultProcess_Entry clsTotalResultEntry;
        Exam_Final_Result_Stock_Of_All_Batch_Entry FinalResultEntry;
        DataTable dt;
        ClassGroupEntry clsgrpEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AcademicTranscript.aspx", "")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        btnPrintPreview.Enabled = false;
                        btnPrintPreview.CssClass = "";
                        ShiftEntry.GetDropDownList(dlShift);
                        BatchEntry.GetDropdownlist(dlBatch, "True");                        
                    }
                lblMessage.InnerText = "";
            }
            catch { }
        }
         
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
                string[] BatchClsID = dlBatch.SelectedValue.Split('_');
                ExamInfoEntry.GetExamIdList(dlExamId, BatchClsID[0],true);
                if (clsgrpEntry == null)
                {
                    clsgrpEntry = new ClassGroupEntry();
                }
                clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), ddlGroup);
                ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
            }
            catch { }
        }
        protected void dlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            if (clsTotalResultEntry == null)
            {
                clsTotalResultEntry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
            }
            dt = new DataTable();
            string className = new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
            dt = clsTotalResultEntry.LoadRoll(className, dlShift.SelectedValue, BatchClsID[0],
                ddlGroup.SelectedValue, dlSection.SelectedValue, dlExamId.SelectedItem.Text);
            ddlRoll.DataSource = dt;
            if (dt == null)
            {
                ddlRoll.Items.Clear();
                return;
            }
            ddlRoll.DataTextField = "RollNo";
            ddlRoll.DataValueField = "StudentId";
            ddlRoll.DataBind();
            ddlRoll.Items.Insert(0, new ListItem("...Select..", "0"));
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            loadAcademicTranscript("");
        }
        private void loadAcademicTranscript(string sqlCmd)   // generate studentfine information if his already fined
        {
            try
            {
                //--------Validation------------
                if (ddlGroup.Enabled == true && ddlGroup.SelectedValue == "0") { lblMessage.InnerText = "warning-> Select Group Name"; ddlGroup.Focus(); return; }
                if (dlSection.SelectedValue == "0") { lblMessage.InnerText = "warning-> select Section Name"; dlSection.Focus(); return; }
                if (ddlRoll.SelectedValue == "0") { lblMessage.InnerText = "warning-> Select Roll No"; ddlRoll.Focus(); return; }
                //------------------------------
                string[] BatchClsID = dlBatch.SelectedValue.Split('_');
                DataTable dtgpa = new DataTable();
                sqlDB.fillDataTable("select GName,GMarkMin,GMarkMax,GPointMin,GPointMax from Grading Order By GPointMin DESC ", dtgpa);

                DataTable dtMarkSheet = new DataTable();

                if (FinalResultEntry == null)
                {
                    FinalResultEntry = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                }
                string className = new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dtMarkSheet = FinalResultEntry.GetAcademicTranscript(className, BatchClsID[0],
                    dlShift.SelectedValue,dlSection.SelectedValue,ddlGroup.SelectedValue,
                    dlExamId.SelectedValue,ddlRoll.SelectedItem.Text);
                if (dtMarkSheet == null)
                {
                    lblMessage.InnerText = "warning->Academic Transcript Not Found";
                    btnPrintPreview.Enabled = false;
                    btnPrintPreview.CssClass = "";
                    return;
                }
                else if (dtMarkSheet.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning->Academic Transcript Not Found";
                    btnPrintPreview.Enabled = false;
                    btnPrintPreview.CssClass = "";
                    return;
                }
                btnPrintPreview.Enabled = true;
                btnPrintPreview.CssClass = "btn btn-success";
                string divInfo = "";
                DataTable schoolInfo = new DataTable();
                sqlDB.fillDataTable("Select SchoolName,Address From School_Setup", schoolInfo);                
                divInfo += "<div style='text-align:center;'>";
                divInfo += "<h1 style='font-weight:bold;font-size: 12px; '>" + schoolInfo.Rows[0]["SchoolName"].ToString().ToUpper() + "," 
                + schoolInfo.Rows[0]["Address"].ToString().ToUpper() + " </h1>";
                divInfo += "<h2 style='font-weight: bold; padding: 0px; font-size: 10px;'>BANGLADESH <br> " + dtMarkSheet.Rows[0]["ExName"].ToString() + "-"
                + dtMarkSheet.Rows[0]["ExInDate"].ToString() + " </h2>";

                divInfo += "</div>";
                divInfo += "<div style='height:auto; width:99%; margin-top:0px;'>";
                divInfo += "<div style='height:140px; width:69%; float:left;'>"; //div1 st
                divInfo += "<table id='tblLogo' style='height:100px; font-size:10px; width:100%; float:left ' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                divInfo += "<tr>";
                divInfo += "<td> </td>";
                divInfo += "<td> </td>";
                divInfo += "<td style='text-align:center'> " + "<img src='/Images/logo.png' style='height:100px; width:100px ; margin: 0 0 0 170px;' '  </td>";
                divInfo += "<tr>";
                divInfo += "<td> </td>";
                divInfo += "<td> </td>";
                divInfo += "<td style='text-align:center; '> <h4 style='margin: 0 0 0 170px;font-size:10px;'>ACADEMIC TRANSCRIPT </h4></td>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "</div>"; //div1 end
                divInfo += "<div style='height:auto; width:30%; float:right; border:1px solid gray; '>";
                divInfo += " <table id='tblFine' class='display ac_transcript'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric' style='width:50px;height: 15px;'>Grade</th>";
                divInfo += "<th class='numeric' style='height: 15px;'>Marks</th>";
                divInfo += "<th class='numeric' style='width:100px;height: 15px;'>Point</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                for (byte b = 0; b < dtgpa.Rows.Count; b++)
                {
                    divInfo += "<tr>";
                    divInfo += "<td class='numeric' style='padding:0px;;  font-size: 10px;'>" + dtgpa.Rows[b]["GName"].ToString() + "</td>";
                    divInfo += "<td class='numeric' style='padding:0px; font-size: 10px;'>" + dtgpa.Rows[b]["GMarkMin"].ToString() + "-" 
                    + dtgpa.Rows[b]["GMarkMax"].ToString() + "</td>";
                    divInfo += "<td class='numeric' style='padding:0px; font-size: 10px;'>" + dtgpa.Rows[b]["GPointMin"].ToString() + "</td>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";

                divInfo += "</div>";
                divInfo += "</div>";

                divInfo += "<div>"; //div main start
                divInfo += " <table id='tblStinfo' class='ac_transcript'  style='height:auto; width:99%; float: left;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                divInfo += "<tr>";
                divInfo += "<td style='width:150px;font-size: 12px'> Name of Student </td>";
                divInfo += "<td style='width:10px;'> : </td>";
                divInfo += "<td style='width:220px; font-size: 12px; font-family: Lucida Console;'> " + dtMarkSheet.Rows[0]["FullName"].ToString() + " </td>";
                divInfo += "<td style='width:120px;font-size: 12px;'>  </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td style='width:80px;'>  </td>";

                divInfo += "<tr>";
                divInfo += "<td style='font-size: 12px;'> Father's Name </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td  style='font-size: 12px;'> " + dtMarkSheet.Rows[0]["FathersName"].ToString() + " </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";

                divInfo += "<tr>";
                divInfo += "<td style='font-size: 12px;'> Mother's Name </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td  style='font-size: 12px;'> " + dtMarkSheet.Rows[0]["MothersName"].ToString() + " </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";

                divInfo += "<tr>";
                divInfo += "<td style='font-size: 12px;'> Roll No </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td  style='font-size: 12px;'>  " + ddlRoll.SelectedItem.Text.Trim() + "  </td>";
                string classN = new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                //if (classN.Equals("Nine") || classN.Equals("Ten"))
                //{
                //    divInfo += "<td style='width:160px;'>  Registration No </td>";
                //    divInfo += "<td> : </td>";
                //    divInfo += "<td> </td>";
                //}
                //else
                //{
                //    divInfo += "<td style='width:160px;'>  </td>";
                //    divInfo += "<td>  </td>";
                //    divInfo += "<td> </td>";
                //}

                divInfo += "<tr>";
                
                divInfo += "<td> Group </td>";
                divInfo += "<td> : </td>";
                if (ddlGroup.SelectedValue == "0")
                {
                    divInfo += "<td ></td>";
                }
                else
                {
                    divInfo += "<td >"+ddlGroup.SelectedItem.Text+"</td>";
                }
              
                divInfo += "<td> Section </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td > " + dlSection.SelectedItem.Text + " </td>";
               
                //divInfo += "<td style='width:160px'> Type of Student </td>";
                //divInfo += "<td> : </td>";
                //divInfo += "<td>  </td>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "</div><br/><br/>"; //div main end

                divInfo += "<div>"; //div marks sheet start
                divInfo += "<table id='tblMarksList' class='display ac_transcript'  style='height:auto; width: 595px;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric' style='background-color:black;color:white;'>SL</th>";
                divInfo += "<th style='background-color:black;color:white;'>Name of Subjects</th>";
                divInfo += "<th class='numeric' style='background-color:black;color:white;'>Letter Grade</th>";
                divInfo += "<th class='numeric' style='background-color:black;color:white;'>Grade Point</th>";
                divInfo += "<th class='numeric' style='background-color:black;color:white;'>Grade Point Average (GPA)</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                DataTable dtSub = new DataTable();
                DataTable dtOpSub = new DataTable();
                try
                {
                     dtSub = dtMarkSheet.Select("IsOptional='False'").CopyToDataTable();
                }
                catch { }
                try
                {
                     dtOpSub = dtMarkSheet.Select("IsOptional='True'").CopyToDataTable();
                }
                catch { }

                int sl = 0;
                for (int i = 0; i < dtSub.Rows.Count; i++)
                {
                    
                    sl = i + 1;

                    divInfo += "<tr>";
                    divInfo += "<td style='width:30px;' class='numeric'> " + sl + " </td>";
                    divInfo += "<td style='width:200px;' >" + dtSub.Rows[i]["SubName"].ToString() + " </td>";
                    divInfo += "<td style='width:80px;' class='numeric'> " + dtSub.Rows[i]["GradeOfSubject_WithAllDependencySub"].ToString() + " </td>";
                    divInfo += "<td style='width:100px; ' class='numeric'> " + dtSub.Rows[i]["PointOfSubject_WithAllDependencySub"].ToString() + " </td>";
                    if (i == 0) divInfo += "<td style='width:100px; ' rowspan='" + (dtSub.Rows.Count) + "'> <h4 style='text-align:center'> "
                    + dtSub.Rows[0]["FinalGPA_OfExam"].ToString() + " </h4>  </td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "</div>"; //div marks sheet end

                if (dtOpSub.Rows.Count>0)
                {              

                    divInfo += "<h5>Additional Subject</h5>";
                    divInfo += "<table id='tblAdditionalSub' class='display ac_transcript'  style='height:auto; border:1px solid #D5D5D5; margin-left: 0; width: 478px;margin-bottom:15px' > ";
                    divInfo += "<thead>";
                    divInfo += "</thead>";
                    divInfo += "<td style='width:30px;' class='numeric' > " + (sl + 1) + " </td>";
                    divInfo += "<td style='width:200px;'> " + dtOpSub.Rows[0]["SubName"] + " </td>";
                    divInfo += "<td style='width:80px;'> " + dtOpSub.Rows[0]["GradeOfSubject_WithAllDependencySub"] + " </td>";
                    divInfo += "<td style='width:100px;'> " + dtOpSub.Rows[0]["FinalGPA_OfExam_WithOptionalSub"] + " </td>";
                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";
                    divInfo += "</table>";
                }                
                divInfo += "<h5 style='width:330px; float:left; font-family: sans-serif; font-size: 11px;'>Date of Publication of Result : "
                + dtSub.Rows[0]["PublishDate"] + "</h5> <h5 style='float:right; width:200px; font-family: sans-serif; font-size: 11px;'>Controller of "
                +"Examinations </h5><br/><br/>";

                divAcademicTranscript.Controls.Add(new LiteralControl(divInfo));
                Session["__AcademicTranscript__"] = divInfo;
            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "print report", "goToNewTab('/Report/AcademicTranscriptPrint.aspx');", true);
            }
            catch { }
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), ddlGroup.SelectedValue);
        }       
    }
}