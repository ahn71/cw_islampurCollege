using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ComplexScriptingSystem;
namespace DS.Forms
{
    public partial class AcademicTranscript : System.Web.UI.Page
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
                        Classes.commonTask.loadBatch(dlBatch);
                        Classes.commonTask.loadSection(dlSection);
                        checkDependencyExam();
                    }
                }

            }
            catch { }
        }

        private void checkDependencyExam()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select ExInId from ExamInfo where ExInDependency <>'0' Order By ExInSl DESC", dt = new DataTable());
                dlExamId.DataSource = dt;
                dlExamId.DataTextField = "ExInId"; 
                dlExamId.DataBind();
            }
            catch { }
        }

        private void loadExamIdClassAndBatchWise()
        {
            try
            {
                DataTable dt = new DataTable();
                SQLOperation.selectBySetCommandInDatatable("select ExInId from ExamInfo where ExInId Like '%" + dlBatch.SelectedItem.Text.Trim() + "%' order by ExInSl desc", dt, sqlDB.connection);
                dlExamId.Items.Clear();
                dlExamId.Items.Add("Select Exam Id");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dlExamId.Items.Add(dt.Rows[i]["ExInId"].ToString());
                }
                dlExamId.SelectedIndex = dlExamId.Items.Count - dlExamId.Items.Count;
            }
            catch { }
        }

        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSectionClayseWise();

            loadExamIdClassAndBatchWise();
        }

        private void loadSectionClayseWise()
        {
            try
            {
                string className = new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                DataTable dt;
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='" + className + "' ", dt = new DataTable(), sqlDB.connection);
                if ((dt.Rows[0]["ClassOrder"].ToString().Equals("9") || (dt.Rows[0]["ClassOrder"].ToString().Equals("10"))))
                {
                    dlSection.Items.Clear();
                    dlSection.Items.Add("...Select...");
                    dlSection.Items.Add("Science");
                    dlSection.Items.Add("Commerce");
                    dlSection.Items.Add("Arts");
                    dlSection.SelectedIndex = dlSection.Items.Count - dlSection.Items.Count;
                }
                else
                {
                    dlSection.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' AND SectionName<>'Arts' Order by SectionName", dlSection);
                    dlSection.Items.Add("...Select...");
                    dlSection.SelectedIndex = dlSection.Items.Count - 1;
                }
            }
            catch { }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtRoll.Text.Equals(""))
            {
                lblMessage.InnerText = "warning->Input Roll Number";
                return;
            }
            loadAcademicTranscript("");
        }

        private void loadAcademicTranscript(string sqlCmd)   // generate studentfine information if his already fined
        {
            try
            {
                DataTable dtst = new DataTable();
                sqlDB.fillDataTable("select Distinct(StudentId) ,FullName,SectionName from v_FinlaResultLog where BatchName='" + dlBatch.SelectedItem.Text + "' and RollNo =" + txtRoll.Text.Trim() + "  ", dtst);
                
                DataTable dtstPInfo = new DataTable();
                sqlDB.fillDataTable("select RollNo,FathersName,MothersName from StudentProfile where StudentId=" + dtst.Rows[0]["StudentId"] + "  ", dtstPInfo);

                DataTable dtgpa = new DataTable();
                sqlDB.fillDataTable("select GName,GMarkMin,GMarkMax,GPointMin,GPointMax from Grading Order By GPointMin DESC ", dtgpa);

                DataTable dtMarkSheet = new DataTable();
                sqlDB.fillDataTable("select SubId,SubName,Marks,SGrade,SPoint,GPA,Grade,TotalMarks,RollNo from v_FinlaResultLog where BatchName='" + dlBatch.SelectedItem.Text + "' and Shift='" + dlShift.SelectedItem.Text + "' and SectionName='" + dlSection.SelectedItem.Text + "' and ExInId='" + dlExamId.SelectedItem.Text + "' and RollNo=" + txtRoll.Text.Trim() + " ", dtMarkSheet);


                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                string[] getExamTitle = dlExamId.SelectedItem.Text.Split('_');
                divInfo += "<div style='text-align:center;'>";
                divInfo += "<h1 style='font-weight:bold;font-size: 22px; '>HARIHAR PARA HIGH SCHOOL, ENAYETNAGAR, NARAYANGANJ. </h1>";
                divInfo += "<h2 style='font-weight: bold; padding: 0px; font-size: 18px;'>BANGLADESH <br> " + getExamTitle[0] + "-" + new String(getExamTitle[2].Where(Char.IsNumber).ToArray()) + " </h2>";
              
                divInfo += "</div>";

                divInfo += "<div style='height:auto; width:99%; margin-top:0px;'>";

                divInfo += "<div style='height:170px; width:70%; float:left;'>"; //div1 st

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
                divInfo += "<td style='text-align:center; '> <h4 style='margin: 0 0 0 170px;'>ACADEMIC TRANSCRIPT </h4></td>";
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>"; 
                divInfo += "</div>"; //div1 end

                divInfo += "<div style='height:auto; width:28%; float:right; border:1px solid gray; '>";

                divInfo += " <table id='tblFine' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric' style='width:50px;'>Grade</th>";
                divInfo += "<th class='numeric'>Marks</th>";
                divInfo += "<th class='numeric' style='width:100px;'>Point</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                for (byte b = 0; b < dtgpa.Rows.Count; b++)
                {
                    divInfo += "<tr>";
                    divInfo += "<td class='numeric' style='padding:0px;;  font-size: 10px;'>" + dtgpa.Rows[b]["GName"].ToString() + "</td>";
                    divInfo += "<td class='numeric' style='padding:0px; font-size: 10px;'>" + dtgpa.Rows[b]["GMarkMin"].ToString() + "-" + dtgpa.Rows[b]["GMarkMax"].ToString() + "</td>";
                    divInfo += "<td class='numeric' style='padding:0px; font-size: 10px;'>" + dtgpa.Rows[b]["GPointMin"].ToString() + "</td>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>"; 

                divInfo += "</div>";
                divInfo += "</div>";

                divInfo += "<div>"; //div main start
                divInfo += " <table id='tblStinfo'  style='height:auto; width:99%; float: left;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                divInfo += "<tr>";
                divInfo += "<td style='width:150px;'> Name of Student </td>";
                divInfo += "<td style='width:10px;'> : </td>";
                divInfo += "<td style='width:220px;  font-family: Lucida Console;'> "+ dtst.Rows[0]["FullName"].ToString()+" </td>";
                divInfo += "<td style='width:120px;'>  </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td style='width:80px;'>  </td>";

                divInfo += "<tr>";
                divInfo += "<td> Father's Name </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td > " + dtstPInfo.Rows[0]["FathersName"].ToString() + " </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";

                divInfo += "<tr>";
                divInfo += "<td> Mother's Name </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td > " + dtstPInfo.Rows[0]["MothersName"].ToString() + " </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";
                divInfo += "<td>  </td>";

                divInfo += "<tr>";
                divInfo += "<td> Roll No </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td >  " + txtRoll.Text.Trim() + "  </td>";
                string classN = new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                if (classN.Equals("Nine") || classN.Equals("Ten"))
                {
                    divInfo += "<td style='width:160px;'>  Registration No </td>";
                    divInfo += "<td> : </td>";
                    divInfo += "<td> </td>";
                }
                else
                {
                    divInfo += "<td style='width:160px;'>  </td>";
                    divInfo += "<td>  </td>";
                    divInfo += "<td> </td>";
                }

                divInfo += "<tr>";
                if (classN.Equals("Nine") || classN.Equals("Ten"))
                {
                    divInfo += "<td> Group </td>";
                    divInfo += "<td> : </td>";
                    divInfo += "<td ></td>";
                }
                else
                {
                    divInfo += "<td> Section </td>";
                    divInfo += "<td> : </td>";
                    divInfo += "<td > " + dtst.Rows[0]["SectionName"] + " </td>";
                }               
                divInfo += "<td style='width:160px'> Type of Student </td>";
                divInfo += "<td> : </td>";
                divInfo += "<td>  </td>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>"; 
                divInfo += "</div><br/><br/>"; //div main end

                divInfo += "<div>"; //div marks sheet start
                divInfo += "<table id='tblMarksList' class='display'  style='height:auto; width: 595px;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric'>SL</th>";
                divInfo += "<th>Name of Subjects</th>";
                divInfo += "<th class='numeric'>Letter Grade</th>";
                divInfo += "<th class='numeric'>Grade Point</th>";
                divInfo += "<th class='numeric'>Grade Point Average (GPA)</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";

                DataTable dtClsId = new DataTable();

                int sl = 0;
                for (int i = 0; i < dtMarkSheet.Rows.Count; i++)
                {
                    sqlDB.fillDataTable("select ClassId from v_ClasswiseSubject where  ClassName='" + new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "' ", dtClsId);

                    sqlDB.fillDataTable("select v_ClasswiseSubject.DependencySubId from v_ClasswiseSubject   where ClassID=" + dtClsId.Rows[i]["ClassID"].ToString() + " and DependencySubId=" + dtMarkSheet.Rows[i]["SubId"].ToString() + "", dt = new DataTable());
                    string getBookName="";
                    if (dt.Rows.Count > 0)
                    {
                        string[] getBook = dtMarkSheet.Rows[i]["SubName"].ToString().Split(' '); getBookName = getBook[0];
                    }
                    else getBookName = dtMarkSheet.Rows[i]["SubName"].ToString();
                    sl = i + 1;

                    divInfo += "<tr>";
                    divInfo += "<td style='width:30px;' class='numeric'> " + sl + " </td>";
                    divInfo += "<td style='width:200px;' > " + getBookName+ " </td>";
                    divInfo += "<td style='width:80px;' class='numeric'> " + dtMarkSheet.Rows[i]["SGrade"].ToString() + " </td>";
                    divInfo += "<td style='width:100px; ' class='numeric'> " + dtMarkSheet.Rows[i]["SPoint"].ToString() + " </td>";
                    if (i == 0) divInfo += "<td style='width:100px; ' rowspan='" + (dtMarkSheet.Rows.Count) + "'> <h4 style='text-align:center'> " + dtMarkSheet.Rows[dtMarkSheet.Rows.Count - 1]["GPA"].ToString() + " </h4>  </td>";
                }
               
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "</div><br/><br/>"; //div marks sheet end
              
                if (classN.Equals("Nine") || classN.Equals("Ten"))
                {
                    DataTable dtOpS = new DataTable();
                    sqlDB.fillDataTable("select SubId,StudentId from OptionalSubjectInfo where StudentId=" + dtst.Rows[0]["StudentId"] + " ", dtOpS);
                  
                    DataTable dtOpSubName = new DataTable();
                    sqlDB.fillDataTable("select SubName,Marks,SGrade,SPoint,GPA,Grade from v_FinlaResultLog where SubId=" + dtOpS.Rows[0]["SubId"] + " ", dtOpSubName);

                    divInfo = "<h5>Additional Subject</h5>";
                    divInfo += "<table id='tblAdditionalSub' class='display'  style='height:auto; border:1px solid #D5D5D5; margin-left: 0; width: 478px;' > ";
                    divInfo += "<thead>";
                    divInfo += "</thead>";
                    divInfo += "<td style='width:30px;' class='numeric' > " + (sl + 1) + " </td>";
                    divInfo += "<td style='width:200px;'> " + dtOpSubName.Rows[0]["SubName"] + " </td>";
                    divInfo += "<td style='width:80px;'> " + dtOpSubName.Rows[0]["SGrade"] + " </td>";
                    divInfo += "<td style='width:100px;'> " + dtOpSubName.Rows[0]["SPoint"] + " </td>";
                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";
                    divInfo += "</table>";                  
                }

                DataTable dtPublishDate = new DataTable();
                sqlDB.fillDataTable("select convert(varchar(11),PublistDate,106) as PublistDate from Class_" + classN + "MarksSheet_TotalResultProcess ", dtPublishDate);
                divInfo += "<br/><br/><br/><h5 style='width:330px; float:left; font-family: sans-serif; font-size: 11px;'>Date of Publication of Result : " + dtPublishDate.Rows[0]["PublistDate"] + "</h5> <h5 style='float:right; width:200px; font-family: sans-serif; font-size: 11px;'>Controller of Examinations </h5><br/><br/>";

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

    }
}