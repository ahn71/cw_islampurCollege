using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using adviitRuntimeScripting;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls.WebParts;

namespace DigitalSchool.Forms
{
    public partial class PromotionClassNine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                loadClassNineStudent();
                AddColumnsScience();
                AddColumnsCommerce();
                AddColumnsArts();
            }
        }
        private void loadClassNineStudent()
        {
            try
            {
                DataTable dt=new DataTable();
                sqlDB.fillDataTable("Select StudentId,FullName from V_ClassNineStudent where ClassName='Nine' and Session=(Select MAX(Session) From CurrentStudentInfo)", dt);
                chkbl.DataSource = dt;
                chkbl.DataTextField = "FullName";
                chkbl.DataValueField = "StudentId";
                chkbl.DataBind();
            }
            catch { }
        }
        private void AddColumnsScience()
        {
            try
            {
                string[] value = new string[1];
                DataTable dt = new DataTable();
                try
                {
                    dt = (DataTable)ViewState["__tableInfo__"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int x = 0; x < dt.Rows.Count; x++)
                            {
                                dt.Rows.Remove(dt.Rows[x]);
                            }
                            ViewState["__dt__"] = dt;
                            gvScience.DataSource = (DataTable)ViewState["__dt__"];
                            gvScience.DataBind();
                        }
                    }
                    dt = new DataTable();
                }
                catch { }

                if (dt.Columns.Count == 0)
                {                   
                    dt = new DataTable();
                    dt.Columns.Add("Student Name");
                    dt.Columns.Add("Roll");
                }
                dt.Rows.Add(value);
                ViewState["__tableInfo__"] = dt;
                gvScience.DataSource = dt;
                gvScience.RowStyle.HorizontalAlign = HorizontalAlign.Center;

                gvScience.DataBind();
            }
            catch { }
        }

        private void AddColumnsCommerce()
        {
            try
            {
                string[] value = new string[1];
                DataTable dt = new DataTable();
                try
                {
                    dt = (DataTable)ViewState["__tableInfo__"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int x = 0; x < dt.Rows.Count; x++)
                            {
                                dt.Rows.Remove(dt.Rows[x]);
                            }
                            ViewState["__dt__"] = dt;
                            gvCommerce.DataSource = (DataTable)ViewState["__dt__"];
                            gvCommerce.DataBind();
                        }
                    }
                    dt = new DataTable();
                }
                catch { }

                if (dt.Columns.Count == 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("Student Name");
                    dt.Columns.Add("Roll");
                }
                dt.Rows.Add(value);
                ViewState["__tableInfo__"] = dt;
                gvCommerce.DataSource = dt;
                gvCommerce.RowStyle.HorizontalAlign = HorizontalAlign.Center;

                gvCommerce.DataBind();
            }
            catch { }
        }
        private void AddColumnsArts()
        {
            try
            {
                string[] value = new string[0];
                DataTable dt = new DataTable();
                try
                {
                    dt = (DataTable)ViewState["__tableInfo__"];
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int x = 0; x < dt.Rows.Count; x++)
                            {
                                dt.Rows.Remove(dt.Rows[x]);
                            }
                            ViewState["__dt__"] = dt;
                            gvArts.DataSource = (DataTable)ViewState["__dt__"];
                            gvArts.DataBind();
                        }
                    }
                    dt = new DataTable();
                }
                catch { }

                if (dt.Columns.Count == 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("Student Name");
                }
                dt.Rows.Add(value);
                ViewState["__tableInfo__"] = dt;
                gvArts.DataSource = dt;
                gvArts.RowStyle.HorizontalAlign = HorizontalAlign.Center;

                gvArts.DataBind();
            }
            catch { }
        }

        protected void btnScience_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListItem li in chkbl.Items)
                {
                    if (li.Selected)
                    {
                        string[] value = new string[2];
                        string st = li.Text;
                        string st1 = li.Value;
                        string st3 = st1 + st;
                        //  value[0] = ddlParticulars.Text;
                        //  value[1] = txtDiscountAmount.Text;

                        DataTable dt = new DataTable();
                        try
                        {
                            dt = (DataTable)ViewState["__tableInfo__"];
                            if (dt == null) dt = new DataTable();
                        }
                        catch { }

                        if (dt.Columns.Count == 0)
                        {
                            dt = new DataTable();
                            dt = new DataTable();
                            dt.Columns.Add("Particulars Name");
                            dt.Columns.Add("Discount (%)");
                        }

                        dt.Rows.Add(value);
                        ViewState["__tableInfo__"] = dt;
                        
                        //  gvDiscount.DataSource = dt;
                        //  gvDiscount.DataBind();
                    }
                }
            }
            catch { }
        }
    }
}