using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;
namespace DS.Report
{
    public partial class StudentFinalResultReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            loadFinalResultDetailsReport();
            //loadFinalResultListReport();
        }

        string divInfo;
        private void loadFinalResultListReport() 
        {
            try
            {
                DataTable dtResultList = (DataTable)Session["__FinalResult__"];
                DataTable dtClsId = new DataTable();
                DataTable dt;
                
                int sl = 0;

                divInfo += "<div style='text-align:center;'>";
                divInfo += "<h2 style='font-weight: bold; padding: 0px; font-size: 18px;'>DIGITAL SCHOOL <br/> " + Session["__ClassTitle__"] + " <br/> " + Session["__ShiftTitle__"] + " </h2>";
                divInfo += "</div>";

                divInfo += "<div style='width: 595px; margin:0 auto; border:1px solid black'>"; //div marks sheet start
                divInfo += "<table id='tblFine' class='display'  style='height:auto; width: 595px;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric'>SL</th>";
                divInfo += "<th class='numeric'>Roll</th>";
                divInfo += "<th class='numeric'>Grade</th>";
                divInfo += "<th class='numeric'>Grade Point</th>";
                divInfo += "<th class='numeric'>GPA</th>";
                divInfo += "<th class='numeric'>Total Marks</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";

                for (int i = 0; i < dtResultList.Rows.Count; i++)
                {
                    sqlDB.fillDataTable("select ClassId from v_ClasswiseSubject where  ClassName='" + Session["__ClassName__"].ToString() + "' ", dtClsId);

                    sqlDB.fillDataTable("select v_ClasswiseSubject.DependencySubId from v_ClasswiseSubject   where ClassID=" + dtClsId.Rows[i]["ClassID"].ToString() + " and DependencySubId=" + dtResultList.Rows[i]["SubId"].ToString() + "", dt = new DataTable());
                   
                    sl = i + 1;

                    divInfo += "<tr>";
                    divInfo += "<td style='width:30px;' class='numeric'> " + sl + " </td>";
                    divInfo += "<td style='width:80px;' class='numeric'> " + dtResultList.Rows[i]["RollNo"].ToString() + " </td>";
                    divInfo += "<td style='width:80px;' class='numeric'> " + dtResultList.Rows[i]["Grade"].ToString() + " </td>";
                    divInfo += "<td style='width:100px; ' class='numeric'> " + dtResultList.Rows[i]["SPoint"].ToString() + " </td>";
                    divInfo += "<td style='width:100px; ' class='numeric'> " + dtResultList.Rows[i]["GPA"].ToString() + " </td>";
                    divInfo += "<td style='width:100px; ' class='numeric'> " + dtResultList.Rows[i]["TotalMarks"].ToString() + " </td>";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "</div><br/><br/>"; //div marks sheet end

                divFinalResult.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        } //list of Final Result 


        private void loadFinalResultDetailsReport()
        {
            try
            {
                DataTable dt = (DataTable)Session["__FainalResult__"]; //Class_SevenMarksSheet_TotalResultProcess
                DataTable dtPublishDate = new DataTable();
                sqlDB.fillDataTable("select distinct convert(varchar(11),PublistDate,106) as PublistDate  From Class_" + Session["__ClassName__"] + "MarksSheet_TotalResultProcess where ExInId='" + Session["__ExamId__"].ToString() + "' ", dtPublishDate);

                divInfo += "<div style='text-align:center;'>";
                divInfo += "<h2 style='font-weight: bold; padding: 0px; font-size: 18px;'>DIGITAL SCHOOL <br/> " + Session["__ClassTitle__"] + " <br/> " + Session["__ShiftTitle__"] + " </h2>";             
                divInfo += "</div>";
                divInfo += "<p> Publish Date : " + dtPublishDate.Rows[0]["PublistDate"] + " </p>";

                divInfo += "<div style='width: 100%; margin:0 auto; border:1px solid black'>"; //div marks sheet start
                divInfo += "<table id='tblFine' class='display'  style='height:auto; width: 100%;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric'>SL</th>";
                divInfo += "<th class='numeric'>Roll</th>";
                divInfo += "<th style='width:214px'>Name</th>";

                DataTable dtSetSub = dt.DefaultView.ToTable(true, "SubName", "SubId");

                DataTable dtDependencySubId = (DataTable)Session["__DependencySubId__"];
                string getBookName = "";
                for (int x = 0; x < dtSetSub.Rows.Count; x++)
                {
                    if (dtDependencySubId.Rows.Count > 0)
                    {
                        try
                        {
                            DataRow[] dr = dtDependencySubId.Select("DependencySubId=" + dtSetSub.Rows[x]["SubId"].ToString() + "");
                            string[] getBook = dr[0]["SubName"].ToString().Split(' '); getBookName = getBook[0];
                            dtDependencySubId.Rows.Remove(dr[0]);
                        }
                        catch { }
                    }
                    else getBookName = dtSetSub.Rows[x]["SubName"].ToString();

                    divInfo += "<th class='numeric' style='width:100px'>" + getBookName + "</th>";
                }
                divInfo += "<th class='numeric'>GPA</th>";
                divInfo += "<th class='numeric'>Grade</th>";
                divInfo += "<th class='numeric'>T.Marks</th>";
                

                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";

                DataTable dtTotalStudent = dt.DefaultView.ToTable(true,"RollNo");

                for (int i = 0; i < dtTotalStudent.Rows.Count; i++)
                {

                    DataTable dtResults = dt.Select("RollNo=" + dtTotalStudent.Rows[i]["RollNo"].ToString() + "").CopyToDataTable();
                    for (byte b = 0; b < dtResults.Rows.Count; b++)
                    {
                        if (b == 0)
                        {
                            divInfo += "<tr>";
                            divInfo += "<td style='width:30px;' class='numeric'> " + (i + 1) + " </td>";                                                // sl
                            divInfo += "<td style='width:80px;' class='numeric'> " + dtResults.Rows[i]["RollNo"].ToString() + " </td>";                  // roll no
                            divInfo += "<td > " + dtResults.Rows[i]["FullName"].ToString() + " </td>";                                                   // student name
                        }
                        divInfo += "<td class='numeric' style='width:100px;' > " + dtResults.Rows[b]["Marks"].ToString() + " </td>";                     //Subject Marks
                        
                        if (b == dtResults.Rows.Count - 1)
                        {
                            divInfo += "<td style='width:100px; ' class='numeric'> " + dtResults.Rows[b]["GPA"].ToString() + " </td>";
                            divInfo += "<td style='width:100px;' class='numeric'> " + dtResults.Rows[b]["Grade"].ToString() + " </td>";
                            divInfo += "<td style='width:100px; ' class='numeric'> " + dtResults.Rows[b]["TotalMarks"].ToString() + " </td>";
                            divInfo += "</tr>";
                        }
                        
                    }
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "</div><br/><br/>"; //div marks sheet end

                divFinalResult.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        } 
    }
}