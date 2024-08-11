using DS.BLL.Admission;
using DS.BLL.Finance;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.StudentManage
{
    public partial class StudentFinance : System.Web.UI.Page
    {
        FeesCollectionEntry fc;
        StudentFine stdFine;
        CurrentStdEntry crntStd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblName.Text = Session["__FullName__"].ToString() + "'s DashBoard";
                FeesCollectionEntry fc = new FeesCollectionEntry();
                fc.LoadStudentFessCategory(ddlFeesCat, Session["__StudentId__"].ToString());
            }
        }

        protected void A1_ServerClick(object sender, EventArgs e)
        {
            LoadFeesCategory();
        }
        private void LoadFeesCategory()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("SELECT FeeCatName,PName,Amount FROM v_FeesCatDetails WHERE FeeCatId='"+ddlFeesCat.SelectedValue+"'");
                Session["__CategorywiseParticulars__"] = dt;
                dt = new DataTable();
                CurrentStdEntry crntStd = new CurrentStdEntry();
                dt = crntStd.GetLoginStudentInfo(Session["__StudentId__"].ToString());
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=CategorywiseParticulars-" + dt.Rows[0]["ShiftName"].ToString() + "-" + dt.Rows[0]["BatchName"].ToString() + "-" + dt.Rows[0]["GroupName"].ToString() + "-" + dt.Rows[0]["SectionName"].ToString() + "');", true);
            }
            catch
            {

            }
        }

        protected void A2_ServerClick(object sender, EventArgs e)
        {   
            DataTable dt = new DataTable();
            if (fc == null)
            {
                fc = new FeesCollectionEntry();
            }
            dt = fc.LoadDueList(" WHERE BatchID!='0' AND PayStatus='False' AND StudentId='" + Session["__StudentId__"].ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                Session["__DueList__"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=DueList');", true);  //Open New Tab for Sever side code
            }
            else lblMessage.InnerText = "warning->No Due List";
        }

        protected void A3_ServerClick(object sender, EventArgs e)
        {
            DataTable dtFineCollection = new DataTable();
            DataTable dtStd = new DataTable();
            if (crntStd == null)
            {
                crntStd = new CurrentStdEntry();
            }
            dtStd = crntStd.GetLoginStudentInfo(Session["__StudentId__"].ToString());
            DataTable dt = new DataTable();
            if (stdFine == null)
            {
                stdFine = new StudentFine();
            }
            dt = stdFine.GetStudentFine(Session["__StudentId__"].ToString());        //....Load Student Fine From Student Payment Table
            
            dtFineCollection.Columns.Add("Id", typeof(int));
            dtFineCollection.Columns.Add("FinePurpose", typeof(string));
            dtFineCollection.Columns.Add("FineAmount", typeof(float));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dtFineCollection.Rows.Add(dt.Rows[i]["FineId"].ToString(), dt.Rows[i]["FinePurpose"].ToString(), dt.Rows[i]["Fineamount"].ToString());
                }
            }
            DataTable dtAbsent = new DataTable();
            dtAbsent = stdFine.GetAbsentFine(Session["__StudentId__"].ToString());     //....Load Absent Fine From Student Absent Details Table 
            if (dtAbsent.Rows[0]["Fineamount"].ToString() != "")
            {
                for(int i=0;i<dtAbsent.Rows.Count;i++)
                {
                    dtFineCollection.Rows.Add(0, "absent", dtAbsent.Rows[i]["Fineamount"].ToString());
                }
            }
            if(dtFineCollection.Rows.Count>0)
            {
                DataTable dtFine = new DataTable();
                dtFine.Columns.Add("FullName", typeof(string));
                dtFine.Columns.Add("ShiftName", typeof(string));
                dtFine.Columns.Add("BatchName", typeof(string));
                dtFine.Columns.Add("GroupName", typeof(string));
                dtFine.Columns.Add("SectionName", typeof(string));
                dtFine.Columns.Add("RollNo", typeof(int));
                dtFine.Columns.Add("FinePurpose", typeof(string));
                dtFine.Columns.Add("FineAmount", typeof(decimal));
                dtFine.Columns.Add("PaymentDate", typeof(string));
                for (int x = 0; x < dtFineCollection.Rows.Count; x++)
                {
                    dtFine.Rows.Add(dtStd.Rows[0]["FullName"].ToString(), dtStd.Rows[0]["ShiftName"].ToString(),
                        dtStd.Rows[0]["BatchName"].ToString(), dtStd.Rows[0]["GroupName"].ToString(),
                        dtStd.Rows[0]["SectionName"].ToString(), dtStd.Rows[0]["RollNo"].ToString(),
                        dtFineCollection.Rows[x]["FinePurpose"].ToString(), dtFineCollection.Rows[x]["FineAmount"].ToString(),
                        DateTime.Now.ToString("dd-MM-yyyy"));
                }
                Session["__FineList__"] = dtFine;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=FineList');", true);  //Open New Tab for Sever side code
            }
            else
            {
                lblMessage.InnerText = "warning->No Fine List";
            }
        }
    }
}