using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace DS.Report
{
    public partial class SubjectWiseMarksList : System.Web.UI.Page
    {
        DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["__Status__"].ToString() == "MarkList")
                {
                    divTitleDetailsMarkList.Visible = false;
                    divTitelCertainMarkList.Visible = true;
                    getReportElementForMarkList();
                }
                else if (Session["__Status__"].ToString() == "FinalResult")
                {
                    DataTable dtFR = (DataTable)Session["__FinalResult__"];
                    gvDisplayTotalFinalResult.DataSource = dtFR;
                    gvDisplayTotalFinalResult.DataBind();
                    divTitelCertainMarkList.Visible = false;
                    lblTitelExam.Text = Session["__BatchInfo__"].ToString();
                }
                else
                {
                    divTitleDetailsMarkList.Visible = true;
                    divTitelCertainMarkList.Visible = false;
                    getReportElementForDetails();

                }
            }
            catch { }
        }

        private void getReportElementForMarkList()
        {
            try
            {
                string[] getExamTitle = Session["__getExamTitle__"].ToString().Split('_');
                examTitle.Text = getExamTitle[0].ToUpper() + "-" + DateTime.Now.Year.ToString();
                subjectTitle.Text = Session["__getSubjectName__"].ToString()+"(Mark list)";
                dt = (DataTable)Session["__getSubjectMarkList__"];

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Fee Fine</div>";
                    divMarkListReport.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table  class='display'  style='width:100%; margin:0px auto;' class='datatables_wrapper'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    divInfo += "<th class='numeric' style='width:50px;'>"+dt.Columns[i].ToString()+"</th>";
                }
             
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
               
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    divInfo += "<tr>";
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        divInfo += "<td class='numeric'>" + dt.Rows[x].ItemArray[j].ToString() + "</td>";
                    }

                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";

                divMarkListReport.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }


        private void getReportElementForDetails()
        {
            try
            {
                gvMarkList.Visible = false;
               // Session["__getSubjectMarkList__"] = dt;
                string[] getExamTitle = Session["__getExamTitle__"].ToString().Split('_');
                lblTitelExam.Text = getExamTitle[0] + DateTime.Now.Year.ToString();
                
                dt = (DataTable)Session["__getSubjectMarkList__"];
                DataTable dtSub = (DataTable)Session["__getSubjectDetails__"];
                DataTable dtDetails = (DataTable)Session["__getDetails__"];
                string tblInfo = "";
                string mainSubTitel = "<table class='datatables_wrapper'  class='display' >";  //sub =subject 
                mainSubTitel += "<thead>";
                mainSubTitel += "<tr>";
                mainSubTitel += "<th style='width:195px'></th>";
                int getCharLangth=0;
                int totalWidth = 0;

                tblInfo += "<table class='display'  >";
                tblInfo += "<thead>";

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (i == 0)
                    {
                        tblInfo += "<th style='width:60px; margin-left:120px'>Roll No</th> <th style='width:60px' >Name</th>";
                        for (byte s = 0; s < dtSub.Rows.Count; s++)
                        {
                            getCharLangth = 0;
                            DataTable dtGetQP = dtDetails.Select("SubId=" + dtSub.Rows[s]["SubId"].ToString() + "").CopyToDataTable();
                            dtGetQP = dtGetQP.DefaultView.ToTable(true, "QPName");
                            //tblInfo += "<th style='width:150px'></th>";
                            for (byte b = 0; b < dtGetQP.Rows.Count; b++)
                            {
                                tblInfo += "<th>" + dtGetQP.Rows[b]["QPName"].ToString() + "</th>";
                                getCharLangth += dtGetQP.Rows[b]["QPName"].ToString().Length * 10;
                                if (b == dtGetQP.Rows.Count - 1)
                                {
                                    totalWidth += getCharLangth+17;
                                    if (s == 0) mainSubTitel += "<th style='width:" + (getCharLangth + 17) + "px; text-align:center;' >" + dtSub.Rows[s]["SubName"].ToString() + "</th>";
                                    else mainSubTitel += "<th style='width:" + (getCharLangth + 17) + "px; text-align:center;'>" + dtSub.Rows[s]["SubName"].ToString() + "</th>";
                                    
                                }
                            }

                            if (s == dtSub.Rows.Count - 1)
                            {
                                mainSubTitel += "</tr>";
                                mainSubTitel += "</thead>";
                                mainSubTitel += "</table>";
                            }
                        }
                    }

                   
                        tblInfo += "</tr>";
                        for (byte b = 0; b < dt.Columns.Count; b++)
                        {
                            tblInfo += "<td>"+dt.Rows[i].ItemArray[b].ToString()+"</td>";
                        }

                        tblInfo += "</thead>";
                        tblInfo += "</tr>";
               

                }
                divMarkListReport.Style.Add("width", totalWidth.ToString()+"px");
                tblInfo += "</table>";
                divMarkListReport.Controls.Add(new LiteralControl(mainSubTitel));
                divMarkListReport.Controls.Add(new LiteralControl(tblInfo));
            }
            catch { }
        }
    }
}