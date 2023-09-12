using DS.DAL;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class AdmCollectionReport : System.Web.UI.Page
    {
        float total = 0;
        protected void Page_Load(object sender, EventArgs e)
        {           
            try
            {
                if (total < 1) { };
                lblYear.Text = System.DateTime.Now.Year.ToString(); 
                divFeesCollection.Controls.Add(new LiteralControl(Session["__FeeCollectionReport__"].ToString()));
                lblStName.Text = Session["__FullName__"].ToString();
                lblStRoll.Text = Session["__Roll__"].ToString();
                if (lblStRoll.Text == "Roll: 0") lblStRoll.Text = "Roll: Undefined";
                lblClass.Text = Session["__Class__"].ToString();
                lblCategory.Text = Session["__FeeCategory__"].ToString();
                lblTransactionNo.Text = Session["__TransactionNO__"].ToString();
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("SELECT SchoolName FROM School_Setup");
                if (dt.Rows.Count > 0)
                    lblSchoolName.Text = dt.Rows[0]["SchoolName"].ToString();
            }
            catch { }
        }       
    }
}