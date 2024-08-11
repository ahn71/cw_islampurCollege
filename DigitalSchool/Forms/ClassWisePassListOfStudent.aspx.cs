using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class ClassWisePassListOfStudent : System.Web.UI.Page
    {
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
                        Classes.commonTask.loadBatch(ddlBatch);
                        sqlDB.loadDropDownList("Select  SectionName from Sections  Order by SectionName", dlSection);
                    }
                }
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadStudentInfo("");
            Session["__FaillSubject__"] = null;
        }
        private void loadStudentInfo(string sqlCmd) // for load pass list of Student
        {
            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet" + "_TotalResultProcess";

                DataTable dtPass = new DataTable();
                DataTable dtFail = new DataTable();
                if (dlResultStatus.SelectedItem.Text == "Pass")
                {
                    sqlCmd = "SELECT dbo.StudentProfile.FullName, dbo.StudentProfile.RollNo , " + getTable + ".GPA,  " + getTable + ".Grade,  " + getTable + ".TotalMarks"
                    +"FROM " + getTable + " INNER JOIN  dbo.StudentProfile ON " + getTable + ".StudentId = dbo.StudentProfile.StudentId where  " + getTable + "."
                    +"BatchName='" + ddlBatch.SelectedItem.Text + "' and " + getTable + ".Shift='" + dlShift.SelectedItem.Text + "' and " + getTable + ".SectionName='" 
                    + dlSection.SelectedItem.Text + "' and " + getTable + ".Grade !='F' and " + getTable + ".Grade !='' Order By " + getTable + ".TotalMarks  Desc ";
                    sqlDB.fillDataTable(sqlCmd, dtPass);

                    gvPassListOfStudent.DataSource = dtPass;
                    gvPassListOfStudent.DataBind();
                    gvPassListOfStudent.Caption = "Pass Student List";

                    gvFailListOfStudent.DataSource = null;
                    gvFailListOfStudent.DataBind();
                }
                else if (dlResultStatus.SelectedItem.Text == "Fail")
                {
                    sqlCmd = "SELECT dbo.StudentProfile.FullName, dbo.StudentProfile.RollNo , " + getTable + ".GPA,  " + getTable + ".Grade,  " + getTable + ".TotalMarks"
                    +" FROM " + getTable + " INNER JOIN  dbo.StudentProfile ON " + getTable + ".StudentId = dbo.StudentProfile.StudentId where  " + getTable + ".BatchName='"
                    + ddlBatch.SelectedItem.Text + "' and " + getTable + ".Shift='" + dlShift.SelectedItem.Text + "' and " + getTable + ".SectionName='"
                    + dlSection.SelectedItem.Text + "' and  (" + getTable + ".Grade='F' Or " + getTable + ".Grade='') Order By " + getTable + ".TotalMarks Desc ";
                    sqlDB.fillDataTable(sqlCmd, dtFail);

                    gvFailListOfStudent.DataSource = dtFail;
                    gvFailListOfStudent.DataBind();
                    gvFailListOfStudent.Caption = "Fail Student List";

                    gvPassListOfStudent.DataSource = null;
                    gvPassListOfStudent.DataBind();
                }
                else if (dlResultStatus.SelectedItem.Text == "All")
                {
                    sqlCmd = "SELECT dbo.StudentProfile.FullName, dbo.CurrentStudentInfo.RollNo , " + getTable + ".GPA,  " + getTable + ".Grade,  " + getTable 
                        + ".TotalMarks FROM " + getTable + " INNER JOIN  dbo.StudentProfile  ON " + getTable + ".StudentId = dbo.StudentProfile.StudentId INNER" 
                    +" JOIN dbo.CurrentStudentInfo ON dbo.CurrentStudentInfo.StudentId=" + getTable + ".StudentId  where  " + getTable + ".BatchName='" 
                    + ddlBatch.SelectedItem.Text + "' and " + getTable + ".Shift='" + dlShift.SelectedItem.Text + "' and " + getTable + ".SectionName='" 
                    + dlSection.SelectedItem.Text + "' and " + getTable + ".Grade!='F' and " + getTable + ".Grade!=''  Order By " + getTable + ".TotalMarks Desc ";
                   
                   sqlDB.fillDataTable(sqlCmd, dtPass);

                   sqlCmd = "SELECT dbo.StudentProfile.FullName, dbo.CurrentStudentInfo.RollNo , " + getTable + ".GPA,  " + getTable + ".Grade,  " + getTable + ".TotalMarks"
                    +" FROM " + getTable + " INNER JOIN  dbo.StudentProfile  ON " + getTable + ".StudentId = dbo.StudentProfile.StudentId INNER JOIN dbo.CurrentStudentInfo"
                    +" ON dbo.CurrentStudentInfo.StudentId =" + getTable + ".StudentId where  " + getTable + ".BatchName='" + ddlBatch.SelectedItem.Text + "' and " 
                    + getTable + ".Shift='" + dlShift.SelectedItem.Text + "' and " + getTable + ".SectionName='" + dlSection.SelectedItem.Text + "' and  (" 
                    + getTable + ".Grade ='F' Or " + getTable + ".Grade ='') Order By " + getTable + ".TotalMarks  Desc ";
                   sqlDB.fillDataTable(sqlCmd, dtFail);

                   gvPassListOfStudent.DataSource = dtPass;
                   gvPassListOfStudent.DataBind();
                   gvPassListOfStudent.Caption = "Pass Student List";

                   gvFailListOfStudent.DataSource = dtFail;
                   gvFailListOfStudent.DataBind();
                   gvFailListOfStudent.Caption = "Fail Student List";
                }

                Session["__PassList__"] = dtPass;
                Session["__FailList__"] = dtFail;
                Session["__ClassInfo__"] = "Batch : " + ddlBatch.SelectedItem.Text + "(" + dlSection.SelectedItem.Text + ")";
            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/StudentResultReport.aspx');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnFailSubjectWise_Click(object sender, EventArgs e)
        {
            failListSubjectWise();
            failStudentCount = "false";
        }

        DataSet ds;
        DataTable dt;
        string totalReport = "";
        private void failListSubjectWise()
        {
            try
            {
                gvFailListOfStudent.DataSource = null;
                gvFailListOfStudent.DataBind();
                gvPassListOfStudent.DataSource = null;
                gvPassListOfStudent.DataBind();

                Session["__PassList__"] = null;
                Session["__FailList__"] = null;
                Session["__ClassInfo__"] = "Batch : " + ddlBatch.SelectedItem.Text + "(" + dlSection.SelectedItem.Text + ")";

                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet" + "_TotalResultProcess";

                DataTable dtFailOneSubjectList = new DataTable();
                sqlDB.fillDataTable("Select * From " + getTable + " where  " + getTable + ".BatchName='" + ddlBatch.SelectedItem.Text + "' and " + getTable 
                    + ".Shift='" + dlShift.SelectedItem.Text + "' and " + getTable + ".SectionName='" + dlSection.SelectedItem.Text + "' and  (" + getTable 
                    + ".Grade='F' Or " + getTable + ".Grade='')  ", dtFailOneSubjectList);
                
                ds = new DataSet();

                for (int i = 0; i < dtFailOneSubjectList.Rows.Count; i++)
                {
                    dt = new DataTable();
                    dt.Columns.Add("RollNo");
                    dt.Columns.Add("SubName");
                    dt.Columns.Add("Grade");
                    dt.Columns.Add("Marks");

                    for (int j = 0; j < dtFailOneSubjectList.Columns.Count; j++)
                    {
                        string subNames = dtFailOneSubjectList.Columns[j].ColumnName;
                        string[] subN = subNames.Split('_');
                        string val = dtFailOneSubjectList.Rows[i][subNames].ToString();

                        if (val == "F" &&  subNames!="Grade")
                        {
                            dt.Rows.Add(dtFailOneSubjectList.Rows[i]["RollNo"].ToString(), subNames, dtFailOneSubjectList.Rows[i]["Grade"].ToString(), 
                                dtFailOneSubjectList.Rows[i][subN[0] + "_TMarks"].ToString());                         
                        }
                    }

                    ds.Tables.Add(dt);
                }

                int totalRows = dt.Rows.Count;
                string divInfo = "";


                if (failStudentCount == "true")
                {
                    int oneSub = 0;
                    int twoSub = 0;
                    int threeSub = 0;
                    int moreSub = 0;
                    for (int l = 0; l < ds.Tables.Count; l++)
                    {
                        if (ds.Tables[l].Rows.Count == 1) oneSub += 1;
                        if (ds.Tables[l].Rows.Count == 2) twoSub += 1;
                        if (ds.Tables[l].Rows.Count == 3) threeSub += 1;
                        if (ds.Tables[l].Rows.Count > 3) moreSub += 1;
                    }
                    int totalSt = (oneSub + twoSub + threeSub + moreSub);

                    divInfo = " <table id='tblClassList' class='display' style='border:1px solid black'> ";
                    divInfo += "<thead>";
                    divInfo += "<tr>";
                    divInfo += "<th style='width:80px' class='numeric'>Total Student</th>";
                    divInfo += "<th style='width:80px' class='numeric'>Fail One Subject</th>";
                    divInfo += "<th style='width:80px' class='numeric'>Fail Tow Subject</th>";
                    divInfo += "<th style='width:80px' class='numeric'>Fail Three Subject</th>";
                    divInfo += "<th style='width:80px' class='numeric'>Fail More Subject</th>";
                    divInfo += "</tr>";
                    divInfo += "</thead>";

                    divInfo += "<tbody>";
                    divInfo += "<tr>";
                    divInfo += "<td style='width:80px' class='numeric'>" + totalSt + "</td>";
                    divInfo += "<td style='width:80px' class='numeric'>" + oneSub + "</td>";
                    divInfo += "<td style='width:80px' class='numeric'> " + twoSub + "</td>";
                    divInfo += "<td style='width:80px' class='numeric'>" + threeSub + "</td>";
                    divInfo += "<td style='width:80px' class='numeric'>" + moreSub + "</td>";
                    divInfo += "</tr>";
                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                    divLoadFailSubject.Controls.Add(new LiteralControl(divInfo));
                    Session["__FaillSubject__"] = divInfo;
                    return;
                }

                for (int j = 0; j < ds.Tables.Count; j++)
                {
                    if (ds.Tables[j].Rows.Count >0)
                    {
                        

                        if (totalRows == 0)
                        {
                            divInfo = "<div class='noData'>No Subject available</div>";
                            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                            divLoadFailSubject.Controls.Add(new LiteralControl(divInfo));
                          //  return;
                        }

                        divInfo = " <table id='tblClassList' class='display' style='border:1px solid black'> ";
                        divInfo += "<thead>";
                        divInfo += "<tr>";
                        divInfo += "<th style='width:80px' class='numeric'>RollNo</th>";
                        divInfo += "<th style='width:200px'>SubName</th>";
                        divInfo += "<th style='width:80px' class='numeric'>Grade</th>";
                        divInfo += "<th style='width:80px' class='numeric'>Marks</th>";

                        divInfo += "</tr>";

                        divInfo += "</thead>";

                        divInfo += "<tbody>";

                        for (int x = 0; x < ds.Tables[j].Rows.Count; x++)
                        {
                            divInfo += "<tr>";
                            divInfo += "<td class='numeric'>" + ds.Tables[j].Rows[x]["RollNo"].ToString() + "</td>";
                            divInfo += "<td >" + ds.Tables[j].Rows[x]["SubName"].ToString() + "</td>";
                            divInfo += "<td class='numeric'>" + ds.Tables[j].Rows[x]["Grade"].ToString() + "</td>";
                            divInfo += "<td class='numeric'>" + ds.Tables[j].Rows[x]["Marks"].ToString() + "</td>";
                            divInfo += "</tr>";
                        }

                        divInfo += "</tbody>";
                        divInfo += "<tfoot>";

                        divInfo += "</table>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                        divLoadFailSubject.Controls.Add(new LiteralControl(divInfo));
                        totalReport += divInfo;
                    }
                }
                Session["__FaillSubject__"] = totalReport;
            }
            catch { }
        }

        string failStudentCount = "false";
        protected void btnNumberOfFailStudent_Click(object sender, EventArgs e)
        {
            failStudentCount = "true";
            failListSubjectWise();
        }


        int totalAPlus =0;
        int totalA = 0;
        int toatalAminus = 0;
        int totalB = 0;
        int totalC = 0;
        int totalD = 0;
        int totalF = 0;
        protected void btnGrateInfo_Click(object sender, EventArgs e)
        {
            try
            {
                gvFailListOfStudent.DataSource = null;
                gvFailListOfStudent.DataBind();
                gvPassListOfStudent.DataSource = null;
                gvPassListOfStudent.DataBind();

                Session["__PassList__"] = null;
                Session["__FailList__"] = null;
                Session["__ClassInfo__"] = "Batch : " + ddlBatch.SelectedItem.Text + "(" + dlSection.SelectedItem.Text + ")";

                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet" + "_TotalResultProcess";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Grade From " + getTable + " where  " + getTable + ".BatchName='" + ddlBatch.SelectedItem.Text + "' and " + getTable 
                    + ".Shift='" + dlShift.SelectedItem.Text + "' and " + getTable + ".SectionName='" + dlSection.SelectedItem.Text + "'  ", dt);

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["Grade"].ToString() == "A+") totalAPlus += 1;
                    if (dt.Rows[j]["Grade"].ToString() == "A") totalA += 1;
                    if (dt.Rows[j]["Grade"].ToString() == "A-") toatalAminus += 1;
                    if (dt.Rows[j]["Grade"].ToString() == "B") totalB += 1;
                    if (dt.Rows[j]["Grade"].ToString() == "C") totalC += 1;
                    if (dt.Rows[j]["Grade"].ToString() == "D") totalD += 1;
                    if (dt.Rows[j]["Grade"].ToString() == "F") totalF += 1;
                }
                string divInfo = "";
                divInfo = " <table id='tblClassList' class='display' style='border:1px solid black'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th style='width:80px' class='numeric'>Total Student</th>";
                divInfo += "<th style='width:80px' class='numeric'>Total Pass Student</th>";
                divInfo += "<th style='width:80px' class='numeric'>Total A+</th>";
                divInfo += "<th style='width:80px' class='numeric'>Total A</th>";
                divInfo += "<th style='width:80px' class='numeric'>Total A-</th>";
                divInfo += "<th style='width:80px' class='numeric'>Total B</th>";
                divInfo += "<th style='width:80px' class='numeric'>Total C</th>";
                divInfo += "<th style='width:80px' class='numeric'>Total D</th>";
                divInfo += "<th style='width:80px' class='numeric'>Total F</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";

                divInfo += "<tbody>";
                divInfo += "<tr>";
                divInfo += "<td style='width:80px' class='numeric'>" + dt.Rows.Count + "</td>";
                divInfo += "<td style='width:80px' class='numeric'>" + (totalAPlus + totalA + toatalAminus + totalB + totalC + totalD) + "</td>";
                divInfo += "<td style='width:80px' class='numeric'>" + totalAPlus + "</td>";
                divInfo += "<td style='width:80px' class='numeric'>" + totalA + "</td>";
                divInfo += "<td style='width:80px' class='numeric'>" + toatalAminus + "</td>";
                divInfo += "<td style='width:80px' class='numeric'>" + totalB + "</td>";
                divInfo += "<td style='width:80px' class='numeric'>" + totalC + "</td>";
                divInfo += "<td style='width:80px' class='numeric'>" + totalD + "</td>";
                divInfo += "<td style='width:80px' class='numeric'>" + totalF + "</td>";
                divInfo += "</tr>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divLoadFailSubject.Controls.Add(new LiteralControl(divInfo));
                Session["__FaillSubject__"] = divInfo;

            }
            catch { }
        }


        int maleResult = 0;
        int femaleResult = 0;
        int totalMale = 0;
        int totalFemale = 0;
        protected void btnResultGenderWise_Click(object sender, EventArgs e)
        {
            try
            {
                gvFailListOfStudent.DataSource = null;
                gvFailListOfStudent.DataBind();
                gvPassListOfStudent.DataSource = null;
                gvPassListOfStudent.DataBind();

                Session["__PassList__"] = null;
                Session["__FailList__"] = null;
                Session["__ClassInfo__"] = "Batch : " + ddlBatch.SelectedItem.Text + "(" + dlSection.SelectedItem.Text + ")";

                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet" + "_TotalResultProcess";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select " + getTable + ".StudentId, " + getTable + ".Grade, StudentProfile.Gender From " + getTable + "  INNER JOIN StudentProfile ON " 
                    + getTable + ".StudentId=StudentProfile.StudentId where  " + getTable + ".BatchName='" + ddlBatch.SelectedItem.Text + "' and " + getTable + ".Shift='" 
                    + dlShift.SelectedItem.Text + "' and " + getTable + ".SectionName='" + dlSection.SelectedItem.Text + "'  ", dt);

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["Gender"].ToString() == "Male")
                    {
                        totalMale += 1;
                        if (dt.Rows[j]["Grade"].ToString() == "A") maleResult += 1;
                    }
                    else if (dt.Rows[j]["Gender"].ToString() == "Female")
                    {
                        totalFemale += 1;
                        if (dt.Rows[j]["Grade"].ToString() == "A") femaleResult += 1;
                    }
                }

                string divInfo = "";
                divInfo = " <table id='tblClassList' class='display' style='border:1px solid black'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th style='width:80px' class='numeric'>Total Student</th>";
                divInfo += "<th style='width:80px' class='numeric'>Total A (Male : " + totalMale + ")</th>";
                divInfo += "<th style='width:80px' class='numeric'>Total A (Female : " + totalFemale + ")</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";

                divInfo += "<tbody>";
                divInfo += "<tr>";
                divInfo += "<td style='width:80px' class='numeric'>" + dt.Rows.Count + "</td>";
                divInfo += "<td style='width:80px' class='numeric'>" + maleResult + "</td>";
                divInfo += "<td style='width:80px' class='numeric'>" + femaleResult + "</td>";
                divInfo += "</tr>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                divLoadFailSubject.Controls.Add(new LiteralControl(divInfo));
                Session["__FaillSubject__"] = divInfo;

            }
            catch { }
        }

    }
}