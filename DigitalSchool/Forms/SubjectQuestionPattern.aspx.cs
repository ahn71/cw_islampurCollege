using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using ComplexScriptingSystem;

namespace DS.Forms
{

    public partial class SubjectQuestionPattern : System.Web.UI.Page
    {
        public event GridViewDeletedEventHandler RowDeleting;
        public event GridViewDeletedEventHandler RowSawing;
        DataTable dt;
        SqlCommand cmd;
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
                        Classes.Exam.LoadExamType(ddlExamType);
                        Classes.commonTask.LoadSubClass(ddlClassName);
                        ddlClassName.Items.Add("...Select...");
                        ddlClassName.SelectedIndex = ddlClassName.Items.Count - 1;
                        AddColumns();
                        Classes.Exam.LoadExamTypeList(ddlExamTypeList);
                        LoadClassName();
                        LoadQuestionPattarn();
                        return;
                    }
                }
            }
            catch { }
        }
        private void LoadSubject()
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select SubId,SubName From V_ClasswiseSubject where ClassID=" + ddlClassName.SelectedValue + " order by Ordering", dt);
                ddlsubjectName.DataSource = dt;
                ddlsubjectName.DataTextField = "SubName";
                ddlsubjectName.DataValueField = "SubId";
                ddlsubjectName.DataBind();
                ddlsubjectName.Items.Add("...Select Subject...");
                ddlsubjectName.SelectedIndex = ddlsubjectName.Items.Count - 1;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }



        private void LoadQuestionPattarn()
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select QPId,QPName From QuestionPattern", dt);
                ddlQPattern.DataSource = dt;
                ddlQPattern.DataTextField = "QPName";
                ddlQPattern.DataValueField = "QPId";
                ddlQPattern.DataBind();
                ddlQPattern.Items.Add("...Select...");
                ddlQPattern.SelectedIndex = ddlQPattern.Items.Count - 1;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }
        private void AddColumns()
        {
            try
            {
                gvSubQPattern.DataSource = new object[] { null };
                gvSubQPattern.Columns[0].Visible = false;
                
                gvSubQPattern.RowStyle.HorizontalAlign = HorizontalAlign.Center;

                gvSubQPattern.DataBind();
            }
            catch { }
        }
        private Boolean saveSubjectQuestionPattern()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["__tableInfo__"];
                int result = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    SqlCommand cmd = new SqlCommand("Insert into  SubjectQuestionPattern values (@ExId,@ClassID, @SubId, @QPId, @QMarks,@PassMarks,@ConvertTo,@GroupName) ", sqlDB.connection);

                    cmd.Parameters.AddWithValue("@ExId",ddlExamType.SelectedValue);
                    cmd.Parameters.AddWithValue("@ClassID", ddlClassName.SelectedValue);                   
                    cmd.Parameters.AddWithValue("@SubId", dt.Rows[i]["SubId"].ToString());
                    cmd.Parameters.AddWithValue("@QPId", dt.Rows[i]["QPId"].ToString());
                    cmd.Parameters.AddWithValue("@QMarks", dt.Rows[i]["QMarks"].ToString());
                    cmd.Parameters.AddWithValue("@PassMarks",dt.Rows[i]["PassMarks"].ToString());
                    if (dt.Rows[i]["ConvertTo"].ToString().Length == 0)
                    {
                        cmd.Parameters.AddWithValue("@ConvertTo", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@ConvertTo", dt.Rows[i]["ConvertTo"].ToString());
                    }

                    if (!trGroup1.Visible) cmd.Parameters.AddWithValue("@GroupName", "NoGroup");
                    else
                    {
                        if (DropGrp.SelectedValue == "1") cmd.Parameters.AddWithValue("@GroupName", "Science");
                        else if (DropGrp.SelectedValue == "2") cmd.Parameters.AddWithValue("@GroupName", "Commerce");
                        else cmd.Parameters.AddWithValue("@GroupName", "Arts");
                    }

                    result = (int)cmd.ExecuteNonQuery();
                    
                }

                groupHide();
                if (result > 0)
                {
                    ViewState["__tableInfo__"] = null;
                    AddColumns();
                    return true;
                }
                else
                {
                    lblMessage.InnerText = "error->Unable to save";
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        private Boolean UpdateSubjectQuestionPattern()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["__tableInfo__"];
                int result = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    SqlCommand cmd = new SqlCommand("Insert into  SubjectQuestionPattern values (@ClassID, @SubId, @QPId, @QMarks) ", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@ClassID", ddlClassName.SelectedValue);
                    cmd.Parameters.AddWithValue("@SubId", dt.Rows[i]["SubId"].ToString());
                    cmd.Parameters.AddWithValue("@QPId", dt.Rows[i]["QPId"].ToString());
                    cmd.Parameters.AddWithValue("@QMarks", dt.Rows[i]["QMarks"].ToString());

                    result = (int)cmd.ExecuteNonQuery();
                }

                if (result > 0)
                {
                    ViewState["__tableInfo__"] = null;
                    AddColumns();
                    lblMessage.InnerText = "success->Successfully saved";
                    return true;
                }
                else
                {
                    lblMessage.InnerText = "error->Unable to save";
                    return false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        protected void ddlClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            LoadSubject(); loadSectionClayseWise();
           
        }
        private void loadSectionClayseWise()
        {
            try
            {
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='" +ddlClassName.SelectedItem.Text + "'", dt = new DataTable(), sqlDB.connection);
                if ((dt.Rows[0]["ClassOrder"].ToString().Equals("9") || (dt.Rows[0]["ClassOrder"].ToString().Equals("10")))) 
                {
                    trGroup1.Visible = true;
                    trGroup2.Visible = true;
                }
                else groupHide();
                
            }
            catch { }
        }

        private void groupHide()
        {
            try
            {
                trGroup1.Visible = false;
                trGroup2.Visible = false;
                //chkArts.Checked = false;
                //chkScience.Checked = false;
                //chkCommerce.Checked = false;
            }
            catch { }
        }
        private void LoadSectionClasswiseList()
        {
            if (ddlClassList.SelectedItem.Text == "...Select...") return;
            SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='" + ddlClassList.SelectedItem.Text + "'", dt = new DataTable(), sqlDB.connection);
            if ((dt.Rows[0]["ClassOrder"].ToString().Equals("9") || (dt.Rows[0]["ClassOrder"].ToString().Equals("10")))) trGrouplist.Visible = true;
            else grouplistHide();
        }
        private void grouplistHide()
        {
            try
            {
                trGrouplist.Visible = false;
                chkArtslist.Checked = false;
                chkSciencelist.Checked = false;
                chkCommercelist.Checked = false;
            }
            catch { }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (AddSubQPatternValidation() == false)
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["__tableInfo__"];
                if (dt == null)
                {
                    AddColumns();
                }
                else
                {
                    gvSubQPattern.DataSource = dt;
                    gvSubQPattern.DataBind();
                }
                return;
            }           
            addRowsAndColumns();             
        }
       
        private void addRowsAndColumns()
        {
            try
            {
                string[] value = new string[7];
                value[0] = ddlsubjectName.SelectedValue;
                value[1] = ddlsubjectName.SelectedItem.Text;
                value[2] = ddlQPattern.SelectedValue;
                value[3] = ddlQPattern.SelectedItem.Text;
                value[4] = txtQMarks.Text;
                value[5] = txtPassMark.Text;
                value[6] = txtConvertTo.Text;                          
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
                    dt.Columns.Add("SubId",typeof(long));
                    dt.Columns.Add("SubName",typeof(string));
                    dt.Columns.Add("QPId",typeof(int));
                    dt.Columns.Add("QPName", typeof(string));
                    dt.Columns.Add("QMarks", typeof(float));
                    dt.Columns.Add("PassMarks", typeof(float));
                    dt.Columns.Add("ConvertTo", typeof(float));
                }

                dt.Rows.Add(value);               
                ViewState["__tableInfo__"] = dt;
                gvSubQPattern.DataSource = dt;
                gvSubQPattern.Columns[0].Visible = true;
                gvSubQPattern.DataBind();
            }
            catch { }
        }
        private Boolean AddSubQPatternValidation()
        {
            try
            {
                if (txtPassMark.Text.ToString().Length == 0)
                {
                    lblMessage.InnerText = "warning->Please Type Pattern Marks";
                    return false;
                }
               else if (txtPassMark.Text.ToString().Length == 0)
                {
                    lblMessage.InnerText = "warning->Please Type Pass Mark";
                    return false;
                }
                sqlDB.fillDataTable("Select SubQPId from SubjectQuestionPattern where ExId=" + ddlExamType.SelectedValue + " and ClassID=" + ddlClassName.SelectedValue + " and SubId=" + ddlsubjectName.SelectedValue + " and QPId="+ddlQPattern.SelectedValue+"", dt = new DataTable());
                 if (dt.Rows.Count > 0)
                {
                    lblMessage.InnerText = "warning->Already set " + ddlQPattern.SelectedItem.Text + " Question Pattern";
                    return false;
                }
                dt = new DataTable();
                dt = (DataTable)ViewState["__tableInfo__"];
                if (dt == null) return true;
                for (byte i = 0; i < dt.Rows.Count; i++)
                {
                    if ((dt.Rows[i]["SubId"].ToString() == ddlsubjectName.SelectedValue) && (dt.Rows[i]["QPId"].ToString() == ddlQPattern.SelectedValue))
                    {
                        lblMessage.InnerText = "warning->" + ddlsubjectName.SelectedItem.Text + " Question Pattern " + ddlQPattern.SelectedItem.Text + " Already Added";
                        return false;
                    }
                }

                    return true;
            }
            catch(Exception ex)
            {
                lblMessage.InnerText = ex.Message;
                return false;
            }
        }

        protected void gvSubQPattern_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LoadSubQPatternList("");
                if (e.CommandName == "Remove")
                {
                    int index = Convert.ToInt32(e.CommandArgument);

                    DataTable dt = new DataTable();
                    dt = (DataTable)ViewState["__tableInfo__"];
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
                    gvSubQPattern.DataSource = dt;
                    gvSubQPattern.DataBind();                    
                    if (gvSubQPattern.Rows.Count == 0)
                    {
                        ViewState["__tableInfo__"] = null;
                        AddColumns();
                    }
                }
               
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           if (SubPatternValidation() == false) return;
            
            if (btnSave.Text == "Save")
            {
                if (saveSubjectQuestionPattern() == true)
                {
                    LoadClassName();
                    Classes.Exam.LoadExamTypeList(ddlExamTypeList);
                    LoadSubQPatternList("");
                    lblMessage.InnerText = "success->Successfully saved";
                }
            }
            else if(btnSave.Text=="Update")
            {
                SqlCommand cmd = new SqlCommand("Delete From SubjectQuestionPattern where ClassID=" + ddlClassList.SelectedValue + " and ExId="+ddlExamType.SelectedValue+"", sqlDB.connection);
                int result=(int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    if (saveSubjectQuestionPattern() == true)
                    {
                        LoadClassName();
                        Classes.Exam.LoadExamTypeList(ddlExamTypeList);
                        LoadSubQPatternList("");
                        btnSave.Text = "Save";
                        lblMessage.InnerText = "success->Successfully Updated";
                    }
                }
            }
        }

        private Boolean SubPatternValidation()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["__tableInfo__"];
                DataTable ClsOrd = new DataTable();
                sqlDB.fillDataTable("Select ClassOrder From Classes where ClassID=" + ddlClassName.SelectedValue + "", ClsOrd);
                if (int.Parse(ClsOrd.Rows[0]["ClassOrder"].ToString()) < 9)
                {
                     DataTable dtsubQ=new DataTable();
                     sqlDB.fillDataTable("Select SubId,QPId,QPName from v_SubjectQuestionPattern where ClassID=" + ddlClassName.SelectedValue + " order by SubId", dtsubQ);
                    if(dtsubQ.Rows.Count==0) return true;
                    else
                    {
                      
                        for (byte x = 0; x < dt.Rows.Count; x++)
                        {
                            string expression = "SubId=" + dt.Rows[x]["SubId"].ToString() + " and QPId=" + dt.Rows[x]["QPId"].ToString() + "";  
                            DataRow[] d;
                            d = dtsubQ.Select(expression);                   
                            if (d.Length==0)
                                {
                                    lblMessage.InnerText = "warning->" + dt.Rows[x]["SubName"].ToString() + " " + dt.Rows[x]["QPName"].ToString() + " Miss Match." + " Previous Subject Pattern " + dtsubQ.Rows[x]["QPName"].ToString();
                                    return false;
                                }
                        }
                      }
                    }
                 else
                  {
                    string GroupName = "";
                    if (DropGrp.SelectedValue == "1") GroupName = DropGrp.SelectedItem.Text;
                    else if (DropGrp.SelectedValue == "2") GroupName = DropGrp.SelectedItem.Text;
                    else if (DropGrp.SelectedValue == "3") GroupName = DropGrp.SelectedItem.Text;      
                    DataTable dtsubQ = new DataTable();
                    sqlDB.fillDataTable("Select SubId,QPId,QPName from v_SubjectQuestionPattern where ClassID=" + ddlClassName.SelectedValue + " and GroupName='"+GroupName+"' order by SubId", dtsubQ);
                    if (dtsubQ.Rows.Count == 0) return true;
                    else
                    {
                        for (byte x = 0; x < dt.Rows.Count; x++)
                        {
                            string expression = "SubId=" + dt.Rows[x]["SubId"].ToString() + " and QPId=" + dt.Rows[x]["QPId"].ToString() + "";
                            DataRow[] d;
                            d = dtsubQ.Select(expression);
                            if (d.Length == 0)
                            {
                                lblMessage.InnerText = "warning->" + dt.Rows[x]["SubName"].ToString() + " " + dt.Rows[x]["QPName"].ToString() + " Miss Match." + " Previous Subject Pattern " + dtsubQ.Rows[x]["QPName"].ToString();
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = ex.Message;
                return false;
            }
        }


        private void create_o_alterMarkSheet()
        {
            try
            {
                byte b = 0;
                string getFields="";

                SQLOperation.selectBySetCommandInDatatable("select SubId,SubName,QPName from v_SubjectQuestionPattern where classId=" + ddlClassName.SelectedValue.ToString() + " group by SubId,SubName,QPName order by subid", dt = new DataTable(), sqlDB.connection);
               // SQLOperation.selectBySetCommandInDatatable("select SubQPId from SubjectQuestionPattern where classId=" + ddlClassName.SelectedValue.ToString() + " order by SubQPId", dt = new DataTable(), sqlDB.connection);
                string[] getSubjectId = new string[dt.Rows.Count];
                while (b < dt.Rows.Count)
                {
                    if (b == dt.Rows.Count - 1) getFields += dt.Rows[b]["SubName"].ToString().Replace(" ", "") + "_" + dt.Rows[b]["QPName"].ToString().Replace(" ", "") + "_" + dt.Rows[b]["SubId"].ToString() + " float";
                   
                    else getFields += dt.Rows[b]["SubName"].ToString().Replace(" ", "") + "_" + dt.Rows[b]["QPName"].ToString().Replace(" ", "") +"_"+dt.Rows[b]["SubId"].ToString()+ " float,";
                   
                    DataTable dtSId ;   // SID=Subject Id
                    b++;
                    
                }
                cmd = new SqlCommand("CREATE TABLE Class_" + ddlClassName.SelectedItem.Text + "_MarkSheet " + " (ExInSl Bigint identity,ExInId Varchar(50),StudentId Bigint, RollNo bigint,FullName varchar(50),SectionName varchar(50)," + getFields + ", Foreign Key (StudentId) References StudentProfile(StudentId) On Update Cascade On Delete Cascade,Foreign key (ExInId) References ExamInfo (ExInId) on update cascade on delete cascade )", sqlDB.connection);
                cmd.ExecuteNonQuery();
                saveSubjectId_o_StudentID(getSubjectId);
            }
            catch { }
        }


        private void saveSubjectId_o_StudentID(string [] setSubId)
        {
            try
            {

               // string getClass = new String(dlBatch.Text.Where(Char.IsLetter).ToArray());
                string tableName = "Class_" +ddlClassName.SelectedItem.Text+ "_MarkSheet";
                //SQLOperation.selectBySetCommandInDatatable("select StudentId from CurrentStudentInfo where BatchName='" + dlBatch.Text + "'", dt = new DataTable(), sqlDB.connection);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] getColumns = { "SubId", "StudentId", };
                    string[] getValues = { setSubId[i], dt.Rows[i]["StudentId"].ToString() };
                    SQLOperation.forSaveValue(tableName, getColumns, getValues, sqlDB.connection);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void LoadSubQPatternList(string sqlcmd)
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ClassOrder from Classes where ClassID=" + ddlClassList.SelectedValue + "", dt);
                if (byte.Parse(dt.Rows[0]["ClassOrder"].ToString()) < 9)
                {
                    sqlcmd = "Select SubName,QPName,QMarks,PassMarks,ConvertTo from V_SubjectQuestionPattern where ClassID=" + ddlClassList.SelectedValue + " and ExId=" + ddlExamTypeList.SelectedValue + " order by Ordering ";
                }
                else
                {
                    string GroupName = "";
                    if (chkSciencelist.Checked)
                        GroupName = "Science";
                    else if (chkCommercelist.Checked)
                        GroupName = "Commerce";
                    else if (chkArtslist.Checked)
                        GroupName = "Arts";
                    sqlcmd = "Select SubName,QPName,QMarks,PassMarks,ConvertTo from V_SubjectQuestionPattern where ClassID=" + ddlClassList.SelectedValue + " and GroupName='"+GroupName+"' and ExId=" + ddlExamTypeList.SelectedValue + " order by Ordering ";
                }
               
                sqlDB.fillDataTable(sqlcmd, dt=new DataTable());

                int totalRows = dt.Rows.Count;
                divSQpattarn.Controls.Clear();
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Pattern List available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divSQpattarn.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblBatch' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Subject Name</th>";
                divInfo += "<th>Question Pattern</th>";
                divInfo += "<th>Q.Marks</th>";
                divInfo += "<th>Pass Marks</th>";
                divInfo += "<th>Convert To</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";
                int id = 0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id++;
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["SubName"].ToString() + "</td>";

                    divInfo += "<td >" + dt.Rows[x]["QPName"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["QMarks"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["PassMarks"].ToString() + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["ConvertTo"].ToString() + "</td>";
                    
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";

                divSQpattarn.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        private void LoadClassName()
        {
            DataTable dt = new DataTable();
            sqlDB.fillDataTable("Select Distinct ClassID,ClassName From V_SubjectQuestionPattern", dt);
            ddlClassList.DataTextField = "ClassName";
            ddlClassList.DataValueField = "ClassID";
            ddlClassList.DataSource = dt;
            ddlClassList.DataBind();
            ddlClassList.Items.Add("...Select Class...");
            ddlClassList.SelectedIndex = ddlClassList.Items.Count - 1;
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
           

        }

        protected void ddlClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            LoadSectionClasswiseList();         
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            btnSave.Text = "Save";
            txtQMarks.Text = "";
            ViewState["__tableInfo__"] = null;
            AddColumns();
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (SearchDeleteValidation() == false)
            {
                string divInfo = "";
                divInfo = "<div class='noData'>No Pattern List available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divSQpattarn.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            LoadSubQPatternList("");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (SearchDeleteValidation() == false)
                {
                    string divInfo = "";
                    divInfo = "<div class='noData'>No Pattern List available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divSQpattarn.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                string sqlcmd = "";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ClassOrder from Classes where ClassID=" + ddlClassList.SelectedValue + "", dt);
                if (byte.Parse(dt.Rows[0]["ClassOrder"].ToString()) < 9)
                {
                    sqlcmd = "Delete from SubjectQuestionPattern where ClassID=" + ddlClassList.SelectedValue + " and ExId=" + ddlExamTypeList.SelectedValue + "  ";
                }
                else
                {
                    string GroupName = "";
                    if (chkSciencelist.Checked)
                        GroupName = "Science";
                    else if (chkCommercelist.Checked)
                        GroupName = "Commerce";
                    else if (chkArtslist.Checked)
                        GroupName = "Arts";
                    sqlcmd = "Delete from SubjectQuestionPattern where ClassID=" + ddlClassList.SelectedValue + " and GroupName='" + GroupName + "' and ExId=" + ddlExamTypeList.SelectedValue + " ";
                }
                int result = 0;
                SqlCommand cmd = new SqlCommand(sqlcmd, sqlDB.connection);
                result=(int)cmd.ExecuteNonQuery();
                if (result > 0)
                {
                    LoadSubQPatternList("");
                    lblMessage.InnerText = "success->Successfully Deleted.";
                }
            }
            catch { }
        }

        private Boolean SearchDeleteValidation()
        {
            try
            {
                lblMessage.InnerText = "";
                if (ddlExamTypeList.SelectedItem.Text == "...Select Exam Type...")
                {
                    lblMessage.InnerText = "warning->Please Select Exam Type";
                    return false;
                }
                else if (ddlClassList.Text == "...Select Class...")
                {
                    lblMessage.InnerText = "warning->Please Select Class";
                    return false;
                }
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ClassOrder from Classes where ClassID=" + ddlClassList.SelectedValue + "", dt);
                if (byte.Parse(dt.Rows[0]["ClassOrder"].ToString()) >= 9)
                {
                    string GroupName = "";
                    if (chkSciencelist.Checked)
                        GroupName = "Science";
                    else if (chkCommercelist.Checked)
                        GroupName = "Commerce";
                    else if (chkArtslist.Checked)
                        GroupName = "Arts";
                    if (GroupName.Length == 0)
                    {
                        lblMessage.InnerText = "warning->Please Select Group Name";
                        return false;
                    }
                }
                return true;
            }
            catch { return false; }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (SearchDeleteValidation() == false)
            {
                ViewState["__tableInfo__"] = null;
                AddColumns();
                return;
            }
            dt = new DataTable();
            sqlDB.fillDataTable("Select SubId,SubName,QPId,QPName,QMarks,PassMarks,ConvertTo from V_SubjectQuestionPattern where ClassID=" + ddlClassList.SelectedValue + " and ExId=" + ddlExamTypeList.SelectedValue + "", dt);
            if (dt.Rows.Count == 0) return;
            ViewState["__tableInfo__"] = dt;
            gvSubQPattern.DataSource = dt;
            gvSubQPattern.Columns[0].Visible = true;
            gvSubQPattern.DataBind();
            LoadSubQPatternList("");
            btnSave.Text = "Update";
            string ExamType = ddlExamTypeList.SelectedItem.Text;
            for (int i = 0; i < ddlExamType.Items.Count; i++)
            {
                if (ddlExamType.Items[i].Text == ExamType)
                {
                    ddlExamType.SelectedIndex = i;
                }
            }
            string ClassName = ddlClassList.SelectedItem.Text;
            for (int i = 0; i < ddlClassName.Items.Count; i++)
            {
                if (ddlClassName.Items[i].Text == ClassName)
                {
                    ddlClassName.SelectedIndex = i;
                }
            }
            LoadSubject();
        }
    }
}