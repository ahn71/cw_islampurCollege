using DS.BLL.Examinition;
using DS.BLL.ManagedClass;
using DS.Classes;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class admit_card : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                commonTask.loadYearFromBatch(ddlYear);
                ClassEntry.GetEntitiesData(ddlClass);                
            }
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            //if (paymentVerification())
            //{
                PreviewAdmitCard();
            //}
            
        }
        private void PreviewAdmitCard()
        {
           
            DataTable dt = new DataTable();
            dt = ExamCommon.getAdmitCard(ViewState["__BatchID__"].ToString(), ddlClass.SelectedValue, ddlExamList.SelectedValue, "0",txtAdmNo.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                hMessage.Visible = false;
                Session["__AdmitCard__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=AdmitCard');", true);  //Open New Tab for Sever side code
            }
            else
                hMessage.Visible = true;
        }
        private void loadExam()
        {
            try {
                if (ddlYear.SelectedValue != "0" || ddlClass.SelectedValue != "0")
                {                
                    string BatchID = commonTask.getBatchID(ddlClass.SelectedItem.Text+ddlYear.SelectedValue); ;
                    ViewState["__BatchID__"] = BatchID;
                    ExamInfoEntry.GetExamIdListWithExInSl(ddlExamList, BatchID);
                }
            }
            catch (Exception ex ) { }
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadExam();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadExam();
        }
        private bool paymentVerification()
        {
            try
            {
                DataTable dt = new DataTable();
                dt=CRUD.ReturnTableNull("select OrderNo from PaymentInfo p left join FeesCategoryInfo ct on p.FeeCatId=ct.FeeCatId left join ExamInfo ex on ex.ExInSl=ct.ExInSl where p.OrderNo='"+txtInvoice.Text.Trim()+ "' and p.StudentId in (select StudentId from v_CurrentStudentInfo where IsActive=1 and BatchName='"+ddlClass.SelectedItem.Text+ ddlYear.SelectedValue+"' and RollNo="+txtAdmNo.Text.Trim()+") and ct.ExInSl=" + ddlExamList.SelectedValue+" and p.status='success' and p.IsPaid=1");
                if (dt == null || dt.Rows.Count == 0)
                {
                    hMessage.InnerText = "Invalid Payment Invoice No!";
                    hMessage.Visible = true;
                    return false;
                }
                hMessage.Visible = false;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ViewAdmitCard();

        }
        private void ViewAdmitCard()
        {
            DataTable dt = new DataTable();
            dt = ExamCommon.getAdmitCard(ViewState["__BatchID__"].ToString(), ddlClass.SelectedValue, ddlExamList.SelectedValue, "0", txtAdmNo.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                hMessage.Visible = false;
                divStudentAdmit.Visible = true;
                divPrintButton.Visible = true;
                hExamName.InnerText = dt.Rows[0]["ExamName"].ToString();

                lblName.Text = dt.Rows[0]["FullName"].ToString();
                lblClassRoll.Text = dt.Rows[0]["RollNo"].ToString();
                lblClass.Text = dt.Rows[0]["ClassName"].ToString();
                lblGroup.Text = dt.Rows[0]["GroupName"].ToString();
                lblSection.Text = dt.Rows[0]["SectionName"].ToString();
                lblGuardianMobileNo.Text = dt.Rows[0]["GuardianMobileNo"].ToString();
                string divInfo = "";
                if (!dt.Rows[0]["ClassName"].ToString().Contains("Twelve"))
                {
                    divInfo = @"<h5 style='font-weight:600'>Exam Routine</h5>
<table class='table table-bordered'>
                    <thead>
                        <tr>
                            <th>Date(Day)</th>
                            <th>Start Time-End Time</th>
                            <th>Subject</th>                           
                        </tr>
                    </thead>
                  <tbody>";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        divInfo += "<tr><td>" + dt.Rows[i]["ExamDate"].ToString()
                        + "(" + dt.Rows[i]["ExamDay"].ToString() + ")</td><td>" + dt.Rows[i]["ExamStartTime"].ToString()
                        + "-" + dt.Rows[i]["ExamEndTime"].ToString() + "</td><td>" + dt.Rows[i]["Subject"].ToString()
                        + "</td></tr>";
                    }
                    divInfo += "</tbody></table>";

                }
                divExamRoutine.Controls.Add(new LiteralControl(divInfo));
            }
            else
            {
                divStudentAdmit.Visible = false;
                divPrintButton.Visible = false;
                hMessage.Visible = true;
            }
                
        }

    }
}