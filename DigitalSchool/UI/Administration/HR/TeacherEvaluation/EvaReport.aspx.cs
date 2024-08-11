using DS.BLL.TeacherEvaluation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.HR.TeacherEvaluation
{
    public partial class EvaReport : System.Web.UI.Page
    {
        StringBuilder StrHtmlGenerate = new StringBuilder();
        StringBuilder StrExport = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    //  Button btnSave;
                    //         if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddDepartment.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    SessionEntry.GetDropdownlist(ddlEvaSession);
                   
                }
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            btnExport.Visible = false;
            if (ddlEvaSession.SelectedIndex < 1)
            {
                lblMessage.InnerText = "warning-> Please select session !";
                ddlEvaSession.Focus();
                return;

            }
            switch (rblReport.SelectedValue)
            {
                case "0":
                    FinalGradeSheet();
                    break;
                case "4":
                    FinalGradeSheet();
                    break;
                case "1":
                    CollegeRankReport();
                    break;
                case "2":
                    DepartmentRankReport();
                    break;
                case "3":
                    IndividualPerformanceReport();
                    break;
                case "6":
                    DepartmentPerformanceReport();
                    break;
                case "7":
                    SubIndicatorBasedPerformanceReport();
                    break;
            }
               
                  
        }
        private void FinalGradeSheet() 
        {
            if (EvaReports.FinalGradeSheet(ddlEvaSession.SelectedValue))
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EvaFinalGradeSheet');", true);
          
        }
        private void CollegeRankReport() 
        {
            if (EvaReports.CollegeRankReport(ddlEvaSession.SelectedValue))
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EvaTeachersPerformanceRanking');", true);

        }
        private void DepartmentRankReport()
        {
            if (EvaReports.DepartmentRankReport(ddlEvaSession.SelectedValue))
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=EvaDepartmentRank');", true);

        }
        private void IndividualPerformanceReport()
        {
            if (EvaReports.IndividualPerformanceReport(ddlEvaSession.SelectedValue))
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=IndividualPerformanceReport');", true);

        }
        private void DepartmentPerformanceReport()
        {
            if (EvaReports.DepartmentPerformanceReport(ddlEvaSession.SelectedValue))
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DepartmentPerformanceReport');", true);

        }
        private void SubIndicatorBasedPerformanceReport ()
        {
            if (EvaReports.SubIndicatorBasedPerformanceReport(ddlEvaSession.SelectedValue))
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=SubIndicatorBasedPerformanceReport');", true);

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            btnExport.Visible = false;
            if (ddlEvaSession.SelectedIndex < 1)
            {
                lblMessage.InnerText = "warning-> Please select session !";
                ddlEvaSession.Focus();
                return;

            }
            if (rblReport.SelectedValue=="0")
            {
                
            DataTable dt = new DataTable();
            dt = EvaReports.returnDataTabelFinalGradeSheet(ddlEvaSession.SelectedValue);
            if (dt == null || dt.Rows.Count == 0)
            {
                lblMessage.InnerText = "warning-> Any data not found"; return;
            }
            generateFinalGradeTable(dt);
            }
           else if (rblReport.SelectedValue == "4")
            {
                
                DataTable dt = new DataTable();
                string UserId = Session["__UserId__"].ToString();
                dt = EvaReports.returnDataTabelFinalGradeSheet(ddlEvaSession.SelectedValue, UserId);
                if (dt == null || dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning-> Any data not found"; return;
                }
                generateFinalGradeTableEvaluatorWise(dt);
            }
            else if (rblReport.SelectedValue == "5")
            {

                DataTable dt = new DataTable();
                string UserId = Session["__UserId__"].ToString();
                dt = EvaReports.returnDataTabelEvalutaroComparativeReport(ddlEvaSession.SelectedValue);
                if (dt == null || dt.Rows.Count == 0)
                {
                    lblMessage.InnerText = "warning-> Any data not found"; return;
                }
                generateEvaluatorComparativeReport(dt);
            }
        }
        private void generateFinalGradeTable(DataTable dt) 
        {
            DataTable dtSubCategory = new DataTable();
            dtSubCategory = EvaReports.returnSubCatergory(ddlEvaSession.SelectedValue);
            DataTable dtCommitteeMember = new DataTable();
            dtCommitteeMember = EvaReports.returnCommitteeMember(ddlEvaSession.SelectedValue);
            dtCommitteeMember.Rows.Add(new Object[] { "Ave." });
            int MemberCount = dtCommitteeMember.Rows.Count;
            string tblInfo = "<table id='tblFinalGradeSheet'class='FinalGradeSheet'>";
            bool isHeader = true;
            string thtr1 = "<tr> <th rowspan='2' class='text-center'>Department</th><th rowspan='2' class='text-center'>Name</th>";
            string thtr2 = "<tr>";
            string tr = "";
            for (byte i = 0; i < dtSubCategory.Rows.Count; i++) 
            {
              
                thtr1 += "<th colspan='" + MemberCount + "' class='text-center'>" + dtSubCategory.Rows[i]["SubCategory"].ToString() + " <br /> [" + dtSubCategory.Rows[i]["FullNumber"].ToString() + "]</th> ";

                for (byte j = 0; j < dtCommitteeMember.Rows.Count; j++)
                {
                    thtr2 += "<th>" + dtCommitteeMember.Rows[j]["FirstName"].ToString() + "</th>";
                }
            }
            thtr1 += "</tr>";
            thtr2 += "</tr>";
            //for (int k = 0; k < dt.Rows.Count; k=k+2)
            for (int k = 0; k < dt.Rows.Count; k=k+ dtCommitteeMember.Rows.Count - 1)
            {
                int t_k = k; // Temporary value for k.
                tr += "<tr><td>" + dt.Rows[k]["DName"].ToString() + "</td><td>" + dt.Rows[k]["EName"].ToString() + "</td>";
                for (byte l = 0; l < dtSubCategory.Rows.Count; l++)
                {
                    int cm =  dtCommitteeMember.Rows.Count-1;// cm means Committee member
                    int t_cm = cm; // Temporary variabl for Committee member
                    float ton = 0;// Total Obtain Number

                    for (byte m = 0; m <cm; m++)
                    {
                        
                        int t =k + m;
                        if (m > 0)
                        {
                            if (dt.Rows[t]["EID"].ToString() == dt.Rows[t - 1]["EID"].ToString())
                            {
                                string onN = dt.Rows[t]["" + dtSubCategory.Rows[l]["SubCategory"] + ""].ToString();// onN means Next obtain number 
                                try { ton += float.Parse(onN); tr += "<td class='text-center'>" + onN + "</td>"; }
                                catch { ton += 0; tr += "<td></td>"; }
                                
                                
                            }
                            else
                            {
                               // for (byte n = 0; n <= t_cm; t++)
                                for (byte n = 0; n <= t_cm; n++)
                                {
                                    tr += "<td></td>";
                                }
                                break;
                            }
                        }
                        else 
                        {
                            string on = dt.Rows[t]["" + dtSubCategory.Rows[l]["SubCategory"] + ""].ToString();// on means obtain number
                            try { ton += float.Parse(on); tr += "<td class='text-center'>" + on + "</td>"; }
                            catch { ton += 0; tr += "<td></td>"; }
                        }                        
                        t_cm--;
                           if(t_cm==0)
                               if (ton<1)
                                   tr += "<td></td>";
                        else
                                   tr += "<td class='text-center'>" + Math.Round( ton / cm,1) + "</td>";
                           t_k++;
                    }
                }

                tr += "</tr>";
               // k = t_k;
            }
            
            tblInfo +=thtr1 + thtr2+tr;            
            tblInfo += "</table>";
            divMarksheet.Controls.Add(new LiteralControl(tblInfo));
            btnExport.Visible = true;
        }

        private void generateFinalGradeTableEvaluatorWise(DataTable dt)
        {
            DataTable dtSubCategory = new DataTable();
            dtSubCategory = EvaReports.returnSubCatergory(ddlEvaSession.SelectedValue);
            //DataTable dtCommitteeMember = new DataTable();
            //dtCommitteeMember = EvaReports.returnCommitteeMember(ddlEvaSession.SelectedValue);
            //dtCommitteeMember.Rows.Add(new Object[] { "Ave." });
            //int MemberCount = dtCommitteeMember.Rows.Count;
            string tblInfo = "<table id='tblFinalGradeSheet'class='FinalGradeSheet'>";
            bool isHeader = true;
            int colspan = dtSubCategory.Rows.Count + 2;
            string thtr1 = "<tr><td colspan='"+ colspan + "'><h4 class='pull-left'>Final Grade Sheet_("+dt.Rows[0]["FirstName"].ToString()+")</h4><h4 class='text-right'>   </h4></td></tr><tr> <th rowspan='2' class='text-center'>Department</th><th rowspan='2' class='text-center'>Name</th>";
            string thtr2 = "<tr>";
            string tr = "";
            for (byte i = 0; i < dtSubCategory.Rows.Count; i++)
            {

                thtr1 += "<th  class='text-center'>" + dtSubCategory.Rows[i]["SubCategory"].ToString() + " <br /> [" + dtSubCategory.Rows[i]["FullNumber"].ToString() + "]</th> ";

                //for (byte j = 0; j < dtCommitteeMember.Rows.Count; j++)
                //{
                //    thtr2 += "<th>" + dtCommitteeMember.Rows[j]["FirstName"].ToString() + "</th>";
                //}
            }
            thtr1 += "</tr>";
            thtr2 += "</tr>";
            //for (int k = 0; k < dt.Rows.Count; k=k+2)
            for (int k = 0; k < dt.Rows.Count; k++)            {
               
                tr += "<tr><td>" + dt.Rows[k]["DName"].ToString() + "</td><td>" + dt.Rows[k]["EName"].ToString() + "</td>";
                for (byte l = 0; l < dtSubCategory.Rows.Count; l++)
                {
                   
                  tr+="<td>" + dt.Rows[k]["" + dtSubCategory.Rows[l]["SubCategory"] + ""].ToString() + "</td>";

                }

                tr += "</tr>";
                // k = t_k;
            }

            tblInfo += thtr1 + thtr2 + tr;
            tblInfo += "</table>";
            divMarksheet.Controls.Add(new LiteralControl(tblInfo));
            btnExport.Visible = true;
        }
        private void generateEvaluatorComparativeReport(DataTable dt)
        {
           

            DataTable dtCommitteeMember = new DataTable();
            dtCommitteeMember = EvaReports.returnCommitteeMember(ddlEvaSession.SelectedValue);
            dtCommitteeMember.Rows.Add(new Object[] { "Ave." });
            int MemberCountRow = dtCommitteeMember.Rows.Count;
            int MemberCount = dtCommitteeMember.Rows.Count-1;
            string tblInfo = "<table id='tblFinalGradeSheet'class='FinalGradeSheet'>";
            bool isHeader = true;
            int colspan = (MemberCountRow* 3) + 2;
            string thtr1 = "<tr><td colspan='"+colspan+ "'><h4 class='pull-left'>EVALUATORS' COMPARATIVE REPORT</h4><h4 class='text-right'>   </h4></td></tr><tr> <th rowspan='2' class='text-center'>Department</th><th rowspan='2' class='text-center'>Name</th> ";
            string thtr2 = "<tr>";
          
            string tr = "";
            string[] header = { "Full Mark 100", "Grade", "College" };
            for (byte i = 0; i < header.Length; i++)
            {

                thtr1 += "<th colspan='" + MemberCountRow + "' class='text-center'>"+ header [i]+ "</th> ";

                for (byte j = 0; j < dtCommitteeMember.Rows.Count; j++)
                {
                    thtr2 += "<th>" + dtCommitteeMember.Rows[j]["FirstName"].ToString() + "</th>";
                }
            }
            thtr1 += "</tr>";
            thtr2 += "</tr>";
          
                for (int k = 0; k < dt.Rows.Count; k=k+ MemberCount)
                {

                    tr += "<tr><td>" + dt.Rows[k]["DName"].ToString() + "</td><td>" + dt.Rows[k]["EName"].ToString() + "</td>";
                string tr_temp1 = "";
                string tr_temp2 = "";
                string tr_temp3 = "";
                float tMark = 0;
                string avgRank = "";
                int j = k;
                    for (byte l = 0; l < MemberCount; l++)
                    {
                    tMark += float.Parse(dt.Rows[j]["ObtainNumber"].ToString());
                    tr_temp1 += "<td>" + dt.Rows[j]["ObtainNumber"].ToString() + "</td>";
                    tr_temp2 += "<td>" + returnGrade(dt.Rows[j]["ObtainNumber"].ToString()) + "</td>";
                    tr_temp3 += "<td>" + returnRank(dt.Rows[j]["RankByInd"].ToString()) + "</td>";                   
                    avgRank = dt.Rows[j]["RankByAll"].ToString();
                    j++;
                    }
                tr_temp1 += "<td>" + Math.Round(tMark / MemberCount, 1) + "</td>";
                tr_temp2 += "<td>" + returnGrade(Math.Round(tMark / MemberCount, 1).ToString()) + "</td>";
               
                tr_temp3 += "<td>" + returnRank(avgRank) + "</td>";
                tr += tr_temp1 + tr_temp2 + tr_temp3;
                    tr += "</tr>";
                    // k = t_k;
                }

            tblInfo += thtr1 + thtr2 + tr;
            tblInfo += "</table>";
            divMarksheet.Controls.Add(new LiteralControl(tblInfo));
            btnExport.Visible = true;
        }
        private string returnRank(string rank)
        {
            if (int.Parse(rank) == 1)
                rank = "1st";
            else if (int.Parse(rank) == 2)
                rank = "2nd";
            else
                rank +="th";
            return rank;
        }
        private string returnGrade(string ObtainMark)
        {
            if (float.Parse(ObtainMark) >= 80)
                ObtainMark = "A+";
            else if (float.Parse(ObtainMark) >=70)
                ObtainMark = "A";
            else if(float.Parse(ObtainMark) >= 60)
                ObtainMark = "B";
            else if (float.Parse(ObtainMark) >= 50)
                ObtainMark = "C";
            else 
                ObtainMark = "D";
          
            return ObtainMark;
        }
        protected void rblReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnExport.Visible = false;
            if (rblReport.SelectedValue == "0" || rblReport.SelectedValue == "4" || rblReport.SelectedValue == "5")
            {
                btnSearch.Visible = true;
                btnPreview.Visible = false;
            }
            else
            {
                btnSearch.Visible = false;
                btnPreview.Visible = true;
            }
               
        }

      


    }
}