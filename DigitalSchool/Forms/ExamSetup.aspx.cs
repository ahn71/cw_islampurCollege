using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Admin
{
    public partial class ExamSettings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    getExamList();
                    getClassesList();
                    AddColumns();
                    Classes.Exam.LoadClassExamSetup(ddlClassList);
                    Classes.Exam.LoadExamTypeExamSetup(ddlExamType);
                    string divInfo = "";
                    divInfo = "<div class='noData'>No Exam Setup available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divExamSetup.Controls.Add(new LiteralControl(divInfo));
                }
            }
        }

        private void AddColumns()
        {
            try
            {
                gvAddExamSetup.DataSource = new object[] { null };
                gvAddExamSetup.Columns[0].Visible = false;
                gvAddExamSetup.RowStyle.HorizontalAlign = HorizontalAlign.Center;
                gvAddExamSetup.DataBind();              
            }
            catch { }
        }
        private void addRowsAndColumns()
        {
            try
            {
                string[] value = new string[6];
                value[0] = ddlSubjectList.SelectedValue;
                value[1] = ddlSubjectList.SelectedItem.Text;
                value[2] = ddlParrern.SelectedValue;
                value[3] = ddlParrern.SelectedItem.Text;
                value[4] = txtMarks.Text;
                value[5] = txtEx_Marks_Convert_To.Text;
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
                    dt.Columns.Add("SubId");
                    dt.Columns.Add("SubName");
                    dt.Columns.Add("QPId");
                    dt.Columns.Add("QPName");
                    dt.Columns.Add("Marks");
                    dt.Columns.Add("ConvertTo");
                }
                dt.Rows.Add(value);
                ViewState["__tableInfo__"] = dt;
                gvAddExamSetup.DataSource = dt;
                gvAddExamSetup.Columns[0].Visible = true;
                gvAddExamSetup.DataBind();
            }
            catch { }
        }


        private void getClassesList()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ClassID,ClassName from Classes  Order by ClassOrder ", dt);
                dlClasses.DataSource = dt;
                dlClasses.DataTextField = "ClassName";
                dlClasses.DataValueField = "ClassID";
                dlClasses.DataBind();
                dlClasses.Items.Add("...Select Class...");
                dlClasses.SelectedIndex = dlClasses.Items.Count - 1;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        private void getExamList()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ExId,ExName from ExamType   Order by ExName ", dt);
                dlExam.DataSource = dt;
                dlExam.DataTextField = "ExName";
                dlExam.DataValueField = "ExId";
                dlExam.DataBind();
                dlExam.Items.Add("...Select Exam Type...");
                dlExam.SelectedIndex = dlExam.Items.Count - 1;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }
        private Boolean saveExamSetup()
        {
            try
            {
                DataTable dt=new DataTable();
               dt=(DataTable)ViewState["__tableInfo__"];
                int result=0;
                for(int i=0;i<dt.Rows.Count;i++)
                {
                SqlCommand cmd = new SqlCommand("Insert into  ExamSetup (ClassID, ExId, SubId, QPId, Marks, ConvertTo)  values (@ClassID, @ExId, @SubId, @QPId, @Marks, @ConvertTo) ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@ClassID", dlClasses.SelectedValue);
                cmd.Parameters.AddWithValue("@ExId", dlExam.SelectedValue);
                cmd.Parameters.AddWithValue("@SubId", dt.Rows[i]["SubId"].ToString());
                cmd.Parameters.AddWithValue("@QPId", dt.Rows[i]["QPId"].ToString());
                cmd.Parameters.AddWithValue("@Marks", dt.Rows[i]["Marks"].ToString());
                cmd.Parameters.AddWithValue("@ConvertTo", dt.Rows[i]["ConvertTo"].ToString());

                result = (int)cmd.ExecuteNonQuery();
                }               
                return true;

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                lblMessage.InnerText = "warning->Please Add Marks Convert To";
                return;
            }
            if (btnSave.Text == "Save")
            {
                if (saveExamSetup() == true)
                {
                    ViewState["__tableInfo__"] = null;
                    AddColumns();
                    Classes.Exam.LoadClassExamSetup(ddlClassList);
                    Classes.Exam.LoadExamTypeExamSetup(ddlExamType);
                    lblMessage.InnerText = "success->Successfully saved";
                }
            }
            else
            {
                int result = 0;
                SqlCommand cmd = new SqlCommand("Delete From ExamSetup where ClassID=" + dlClasses.SelectedValue + " and ExId="+dlExam.SelectedValue+"", sqlDB.connection);
                result=(int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    if (saveExamSetup() == true)
                    {
                        ViewState["__tableInfo__"] = null;
                        AddColumns();
                        Classes.Exam.LoadClassExamSetup(ddlClassList);
                        Classes.Exam.LoadExamTypeExamSetup(ddlExamType);
                        loadExamSetup("");
                        btnSave.Text = "Save";
                        lblMessage.InnerText = "success->Successfully Updated";
                    }
                }
            }
        }



        protected void btnClear_Click(object sender, EventArgs e)
        {
            btnSave.Text = "Save";
            ViewState["__tableInfo__"] = null;
            AddColumns();
            txtEx_Marks_Convert_To.Text = "";

        }

        private void loadExamSetup(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select SubId, SubName, QPId,QPName,Marks,ConvertTo  from V_ExamSetup where ClassID=" + ddlClassList.SelectedValue + " and ExId="+ddlExamType.SelectedValue+"";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";
            divExamSetup.Controls.Clear();
            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Exam Setup available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divExamSetup.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblExamSettingsList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Subject Name</th>";
            divInfo += "<th>Ques.Pattern</th>";
            divInfo += "<th>Marks</th>";
            divInfo += "<th>ConvertTo</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + dt.Rows[x]["SubName"].ToString() + "</td>";
                divInfo += "<td >" + dt.Rows[x]["QPName"].ToString() + "</td>";
                divInfo += "<td >" + dt.Rows[x]["Marks"].ToString() + "</td>";
                divInfo += "<td >" + dt.Rows[x]["ConvertTo"].ToString() + "</td>";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";
            divExamSetup.Controls.Add(new LiteralControl(divInfo));
            
        }

        protected void dlClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            Classes.Exam.LoadSubjectList(ddlSubjectList,dlClasses.SelectedValue);
            DataTable dt=new DataTable();
            dt=(DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
            }
        }

        protected void ddlSubjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Classes.Exam.LoadQuestionPattern(ddlParrern,dlClasses.SelectedValue,ddlSubjectList.SelectedValue);
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
            }
        }

        protected void ddlParrern_SelectedIndexChanged(object sender, EventArgs e)
        {
           txtMarks.Text= (Classes.Exam.LoadMarks(dlClasses.SelectedValue,ddlSubjectList.SelectedValue,ddlParrern.SelectedValue)).ToString();
           DataTable dt = new DataTable();
           dt = (DataTable)ViewState["__tableInfo__"];
           if (dt == null)
           {
               AddColumns();
           }
           txtEx_Marks_Convert_To.Focus();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (dlExam.SelectedItem.Text == "...Select Exam Type...")
            {
                lblMessage.InnerText = "warning->Select Exam Type";
                return;
            }
            else if (dlClasses.SelectedItem.Text == "...Select Class...")
            {
                lblMessage.InnerText = "warning->Select Class";
                return;
            }
            else if (ddlSubjectList.SelectedItem.Text == "...Select Subject...")
            {
                lblMessage.InnerText = "warning->Select Subject";
                return;
            }
            else if (ddlParrern.SelectedItem.Text == "...Selet Pattern...")
            {
                lblMessage.InnerText = "warning->Select Pattern";
                return;
            }
            else if (txtMarks.Text == "")
            {
                lblMessage.InnerText = "warning->Select Pattern";
                return;
            }
            else if (txtEx_Marks_Convert_To.Text.ToString().Length == 0)
            {
                lblMessage.InnerText = "warning->Type Marks Convert To";
                return;
            }
            addRowsAndColumns();
            txtEx_Marks_Convert_To.Text = "";
            txtMarks.Text = "";
        }

        protected void gvAddExamSetup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            loadExamSetup("");
            if (e.CommandName == "Remove")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["__tableInfo__"];
                if (dt==null) return;
                List<DataRow> rowsToDelete = new List<DataRow>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i == index)
                    {
                        DataRow row = dt.Rows[i];
                        rowsToDelete.Add(row);
                    }
                }
                //Deleting the rows 
                foreach (DataRow row in rowsToDelete)
                {
                    dt.Rows.Remove(row);
                }
                dt.AcceptChanges();
                ViewState["__tableInfo__"] = dt;
                gvAddExamSetup.DataSource = dt;
                gvAddExamSetup.DataBind();
                if (gvAddExamSetup.Rows.Count == 0)
                {
                    ViewState["__tableInfo__"] = null;

                    AddColumns();
                }
            }
        }

        protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadExamSetup("");
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (ddlClassList.SelectedItem.Text == "---Select Class---")
            {
                lblMessage.InnerText = "warning->Please Select Class";
                return;
            }
            if (ddlExamType.SelectedItem.Text == "---Select Exam Type---")
            {
                lblMessage.InnerText = "warning->Please Select Exam Type";
                return;
            }
            DataTable dt = new DataTable();
            sqlDB.fillDataTable("Select SubId,SubName,QPId,QPName,Marks,ConvertTo From V_ExamSetup where ExId=" + ddlExamType.SelectedValue + " and ClassID="+ddlClassList.SelectedValue+"", dt);
            if (dt.Rows.Count == 0)
            {
                lblMessage.InnerText = "warning->No Data available";
                return;
            }
            ViewState["__tableInfo__"] = dt;
            gvAddExamSetup.DataSource = dt;
            gvAddExamSetup.Columns[0].Visible = true;
            gvAddExamSetup.DataBind();
            loadExamSetup("");
            btnSave.Text = "Update";
            string ClassName = ddlClassList.SelectedItem.Text;
            for (int i = 0; i < dlClasses.Items.Count; i++)
            {
                if (dlClasses.Items[i].Text == ClassName)
                {
                    dlClasses.SelectedIndex = i;
                }
            }
            string ExamType = ddlExamType.SelectedItem.Text;
            for (int x = 0; x < dlExam.Items.Count; x++)
            {
                if (dlExam.Items[x].Text == ExamType)
                {
                    dlExam.SelectedIndex = x;
                }
            }
                Classes.Exam.LoadSubjectList(ddlSubjectList, dlClasses.SelectedValue);
        }

        protected void ddlClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadExamSetup("");
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["__tableInfo__"];
            if (dt == null)
            {
                AddColumns();
            }
        }

    }
}