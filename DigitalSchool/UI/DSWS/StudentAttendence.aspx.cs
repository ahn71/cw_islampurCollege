using ComplexScriptingSystem;
using DS.BLL.Attendace;
using DS.BLL.DSWS;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.DAL;
using DS.DAL.AdviitDAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class StudentAttendence : System.Web.UI.Page
    {
        List<StdAttEntities> StdAttEntities = new List<StdAttEntities>();
        StdAttEntry StdAttEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtStdDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                StudentAttendance();
            }
            lblMessage.InnerText = "";
        }
        private void StudentAttendance()
        {
            try
            {
                if (StdAttEntry == null)
                {
                    StdAttEntry = new StdAttEntry();
                }
                StdAttEntities = StdAttEntry.getStdInfo();
                gvStudentAtt.DataSource = StdAttEntities;
                gvStudentAtt.DataBind();
                if (StdAttEntities == null)
                {
                    lblMessage.InnerText = "warning-> ইনফরমেশন নাই";
                }
                StdAttEntities = StdAttEntry.getStdAttInfo(convertDateTime.getCertainCulture(txtStdDate.Text).ToString("yyyy-MM-dd"));
                if (StdAttEntities == null) return;
                List<StdAttEntities> StdAtt = new List<StdAttEntities>();
                foreach (GridViewRow row in gvStudentAtt.Rows)
                {
                    TextBox txtP = (TextBox)row.Cells[4].FindControl("txtPresent");
                    TextBox txtA = (TextBox)row.Cells[4].FindControl("txtAbsent");
                    int batchID = Convert.ToInt32(gvStudentAtt.DataKeys[row.RowIndex].Values[0].ToString());
                    int clsGrpID = Convert.ToInt32(gvStudentAtt.DataKeys[row.RowIndex].Values[1].ToString());
                    int clsSecID = Convert.ToInt32(gvStudentAtt.DataKeys[row.RowIndex].Values[2].ToString());
                    StdAtt = StdAttEntities.FindAll(c => c.BatchId == batchID && c.ClsGrpID == clsGrpID && c.ClsSecID == clsSecID);
                    txtP.Text = StdAtt[0].TotalPresent.ToString();
                    txtA.Text = StdAtt[0].TotalAbsent.ToString();
                }

            }
            catch { }
        }
        protected void gvStudentAtt_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            StudentAttendance();
            gvStudentAtt.PageIndex = e.NewPageIndex;
            gvStudentAtt.DataBind();
            StdAttEntities = StdAttEntry.getStdAttInfo(convertDateTime.getCertainCulture(txtStdDate.Text).ToString("yyyy-MM-dd"));
            if (StdAttEntities == null) return;
            List<StdAttEntities> StdAtt = new List<StdAttEntities>();
            foreach (GridViewRow row in gvStudentAtt.Rows)
            {
                TextBox txtP = (TextBox)row.Cells[4].FindControl("txtPresent");
                TextBox txtA = (TextBox)row.Cells[4].FindControl("txtAbsent");
                int batchID = Convert.ToInt32(gvStudentAtt.DataKeys[row.RowIndex].Values[0].ToString());
                int clsGrpID = Convert.ToInt32(gvStudentAtt.DataKeys[row.RowIndex].Values[1].ToString());
                int clsSecID = Convert.ToInt32(gvStudentAtt.DataKeys[row.RowIndex].Values[2].ToString());
                StdAtt = StdAttEntities.FindAll(c => c.BatchId == batchID && c.ClsGrpID == clsGrpID && c.ClsSecID == clsSecID);
                if (StdAtt.Count > 0)
                {
                    txtP.Text = StdAtt[0].TotalPresent.ToString();
                    txtA.Text = StdAtt[0].TotalAbsent.ToString();
                }
            }
        }
        protected void btnStdSearch_Click(object sender, EventArgs e)
        {
            StudentAttendance();
        }
    }
}