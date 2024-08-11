using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Report
{
    public partial class AdmCollectionReprint : System.Web.UI.Page
    {
        float total = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblYear.Text = System.DateTime.Now.Year.ToString();
                DataTable dtPL = (DataTable)Session["__dtPL__"];
                divFeesCollection.Controls.Add(new LiteralControl(Session["__FeeCollectionRePrint__"].ToString()));
                lblStName.Text = "Name: " + dtPL.Rows[0]["FullName"].ToString();
                lblStRoll.Text = "Roll: " + dtPL.Rows[0]["RollNo"].ToString();
                if (lblStRoll.Text == "Roll: 0") lblStRoll.Text = "Roll: Undefined";
                lblClass.Text ="Class: "+ dtPL.Rows[0]["ClassName"].ToString();
                lblCategory.Text = "Category: " + dtPL.Rows[0]["FeeCatName"].ToString();
                lblTransactionNo.Text = "Transaction No: " + dtPL.Rows[0]["TransactionNo"].ToString();
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("SELECT SchoolName FROM School_Setup");
                if (dt.Rows.Count > 0)
                    lblSchoolName.Text = dt.Rows[0]["SchoolName"].ToString();
            }
            catch { }
        }      
    }
}