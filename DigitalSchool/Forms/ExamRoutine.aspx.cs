using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;
using DS.BLL;

namespace DS.Forms
{
    public partial class ExamRoutine : System.Web.UI.Page
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
                        Classes.commonTask.loadBatchWithId(dlBatch);
                        loadSection(dlSection);
                        Classes.commonTask.loadExamType(dlExamType);
                        Classes.Exam.loadPeriod(dlPeriod);
                        lblBatchName.Text = "- " + TimeZoneBD.getCurrentTimeBD().Year.ToString();
                    }
                }

                lblMessage.InnerText = "";
            }
            catch { }
        }


        public void loadSection(DropDownList dl)
        {
            try
            {
                sqlDB.loadDropDownList("Select  SectionName from Sections  Order by SectionName", dl);
                dl.Items.Add("All");
                dl.Items.Add("--Select--");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }
        }

        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {              
                string ClassName = new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());

                DataTable dtClsId = new DataTable();
                sqlDB.fillDataTable("Select ClassID From Classes Where ClassName='" + ClassName + "' ", dtClsId);

                DataTable dtSubject = new DataTable();
                sqlDB.fillDataTable("Select SubName from V_ClassSubject where ClassID=" + dtClsId.Rows[0]["ClassID"].ToString() + " Order by CSId ", dtSubject);
                dlSubject.DataSource = dtSubject;
                dlSubject.DataTextField = "SubName";
                dlSubject.DataBind();
            }
            catch { }
        }


        static DataTable dt;
        int OrderNo;
        static byte count = 0;

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
              
                if (count == 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("Day", typeof(string));
                    dt.Columns.Add("Date", typeof(string));
                    dt.Columns.Add("SubName", typeof(string));
                    dt.Columns.Add("Period", typeof(string));
                    dt.Columns.Add("Class", typeof(string));
                    count++;
                }

                dt.Rows.Add(dlDay.SelectedItem.Text, txtDate.Text.Trim(), dlSubject.SelectedValue, dlPeriod.SelectedItem.Text, new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()));

                gvExamRoutine.DataSource = dt;
                gvExamRoutine.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }



    }
}