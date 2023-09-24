using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.ManagedSubject;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using DS.BLL.ManagedClass;
using DS.PropertyEntities.Model.ManagedClass;
using DS.PropertyEntities.Model.ManagedSubject;
using DS.BLL.ControlPanel;
using DS.DAL;

namespace DS.UI.Academic.Examination.ManagedSubject
{
    public partial class ClassSubjectSetup : System.Web.UI.Page
    {
        ClassSubjectEntry class_subjectEntry;
        ClassDepedencySubPassMarksEntry subEntry;
        List<ClassDependencySubPassMarksEntities> DependencyList;
        ClassDepedencySubPassMarksEntry clsdeppassEntry;
        bool result;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "ClassSubjectSetup.aspx", btnSave, btnPrint)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                btnPrint.Enabled = false;
                btnPrint.CssClass = "";
                ClassEntry.GetEntitiesData(ddlClassName);
                SubjectEntry.GetSujectList(ddlSubject);


                ClassEntry.GetEntitiesData(ddlClassList);
                ClassEntry.GetEntitiesData(ddlMarksClass);
                // LoadClassList();
                DataBindForView();

            }
        }

        protected ClassSubject GetData()
        {
            try
            {
                ClassSubject cs = new ClassSubject();
                if (btnSave.Text == "Save") cs.ClassSubjectId = 0;
                else
                {
                    cs.ClassSubjectId = int.Parse(ViewState["__CSID__"].ToString());
                    cs.RelatedSubId_CSID_Old = ViewState["__relatedSubject_CSID__"].ToString();
                }
                    

                cs.Subject = new SubjectEntities()
                {
                    SubjectId = int.Parse(ddlSubject.SelectedValue.ToString())
                };

                cs.Course = new CourseEntity();
                {
                    CourseId = int.Parse(ddlCourse.SelectedItem.Value.ToString());
                };



                cs.CourseID = int.Parse(ddlCourse.SelectedItem.Value.ToString());

                cs.ClassId = int.Parse(ddlClassName.SelectedValue.ToString());

                cs.SubMarks = int.Parse(txtMarks.Text.Trim());
                cs.SubjectCode = txtSubCode.Text.Trim();
                cs.OrderBy = int.Parse(txtOrderBy.Text.Trim());
                if (chkSubjectType.SelectedValue == "0" || chkSubjectType.SelectedValue == "1")
                    cs.IsOptional = true;
                else cs.IsOptional = false;
                cs.BothType = (chkSubjectType.SelectedValue == "1") ? true : false;
                cs.IsCommon = (rblGroupList.SelectedValue == "0") ? true : false;
                cs.SubjectCode = txtSubCode.Text.Trim();
                cs.GroupId = int.Parse(rblGroupList.SelectedItem.Value);

                if (ddlRelatedSubject.SelectedIndex > 0)
                {
                    string[] s = ddlRelatedSubject.SelectedValue.ToString().Split('_');
                    cs.RelatedSubId_CSID = s[0];
                    cs.RelatedSubId = s[1];
                    
                }
                else
                {
                    cs.RelatedSubId = "0";
                    cs.RelatedSubId_CSID = "0";
                }
                

                return cs;
            }
            catch { return null; }
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);

            if (btnSave.Text == "Save")
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; return; }
                saveClassSubject();
            }

            else
            {
                updateClassSubject();

            }
        }
        private Boolean ClassSubValidation()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select ClassID,SubId From ClassSubject where ClassID=" + ddlClassName.SelectedValue + " and SubId=" + ddlSubject.SelectedValue + "", dt);
                if (dt.Rows.Count > 0)
                {
                    lblMessage.InnerText = "warning->Already Inserted " + ddlSubject.SelectedItem.Text;
                    return false;
                }
                return true;
            }
            catch { return false; }
        }
        private void LoadClassList()
        {
            DataTable dt = new DataTable();
            sqlDB.fillDataTable("Select ClassID,ClassName From Classes where (IsActive is null or IsActive=1) ORDER BY ClassOrder ASC", dt);
            ddlClassList.DataSource = dt;
            ddlClassList.DataTextField = "ClassName";
            ddlClassList.DataValueField = "ClassID";
            ddlClassList.DataBind();
        }
        private void saveClassSubject()
        {
            try
            {
                using (ClassSubject cs = GetData())
                {
                    if (class_subjectEntry == null) class_subjectEntry = new ClassSubjectEntry();
                    class_subjectEntry.AddEntities = cs;
                    if (class_subjectEntry.Insert())
                    {

                        ddlClassList.SelectedValue = ddlClassName.SelectedValue;
                        DataBindForView();
                        lblMessage.InnerText = "success->Successuflly save";
                        Clear();
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearText();", true);

                    }
                    else
                    {
                        DataBindForView();
                        lblMessage.InnerText = "error->Unable to save";

                    }

                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }
        private void updateClassSubject()
        {
            try
            {

                using (ClassSubject cs = GetData())
                {
                    if (class_subjectEntry == null) class_subjectEntry = new ClassSubjectEntry();
                    class_subjectEntry.AddEntities = cs;
                    result = class_subjectEntry.Update();
                    if (result)
                    {
                        ddlClassList.SelectedValue = ddlClassName.SelectedValue;
                        DataBindForView();
                        lblMessage.InnerText = "success->Successuflly Updated";
                        Clear();
                        //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearText();", true);
                        //btnSave.Text = "Save";

                    }
                    else
                    {
                        DataBindForView();
                        lblMessage.InnerText = "error->Unable to Update";

                    }
                }

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;

            }
        }

        private void DataBindForView()
        {
            try
            {
                List<ClassSubject> GetEntitiesData;
                if (class_subjectEntry == null) class_subjectEntry = new ClassSubjectEntry();
                if (ddlClassList.SelectedValue.ToString() == "0")
                {
                    GetEntitiesData = class_subjectEntry.GetEntitiesData;
                    if (GetEntitiesData == null || GetEntitiesData.Count < 1)
                    {
                        divSub.Visible = true;
                        divSub.InnerText = " Subject is not available in this Class";
                    }
                    else divSub.Visible = false;
                    btnPrint.Enabled = false;
                    btnPrint.CssClass = "";

                }
                else
                {
                    GetEntitiesData = ClassSubjectEntry.GetClassSubjectListByFiltering(int.Parse(ddlClassList.SelectedValue.ToString()));
                    if (GetEntitiesData.Count < 1)
                    {
                        divSub.Visible = true;
                        divSub.InnerText = "Subject is not available in this Class";
                        btnPrint.Enabled = false;
                        btnPrint.CssClass = "";

                    }
                    else
                    {
                        divSub.Visible = false;
                        if (Session["__View__"].ToString().Equals("true"))
                        {
                            btnPrint.Enabled = true;
                            btnPrint.CssClass = "btn btn-primary";
                        }
                    }
                }
                gvClassSubject.DataSource = GetEntitiesData.ToList();
                gvClassSubject.DataBind();
                if (Session["__Update__"].ToString().Equals("false"))
                    gvClassSubject.Columns[7].Visible = false;

            }
            catch { }
        }

        protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadStudentInfo();", true);
            lblMessage.InnerText = "";
            // LoadDependencySub();
            CourseEntry.GetCourseListBySubject(ddlCourse, ddlSubject.SelectedValue.ToString());
        }
        private void LoadDependencySub()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Dependency From NewSubject where SubId=" + ddlSubject.SelectedValue + "", dt);
                if (dt.Rows[0]["Dependency"].ToString() == "True")
                {
                    //  trDependencysub.Visible = true;
                    sqlDB.fillDataTable("Select SubId,SubName From NewSubject where Dependency='False'", dt = new DataTable());
                    // ddldependencysub.DataSource = dt;
                    // ddldependencysub.DataValueField = "SubId";
                    // ddldependencysub.DataTextField = "SubName";
                    // ddldependencysub.DataBind();

                }
                // else trDependencysub.Visible = false;
            }
            catch { }
        }

        protected void ddlClassList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            ClassSubjectEntry.GetClassSubjectListByFiltering(int.Parse(ddlClassList.SelectedValue.ToString()));
            DataBindForView();

        }

        protected void ddlClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadStudentInfo();", true);

            ddlClassList.SelectedValue = ddlClassName.SelectedValue.ToString();
            DataBindForView();
            GroupEntry.GetGroupByClass(rblGroupList, ddlClassList.SelectedValue.ToString());
            if (ddlClassName.SelectedValue.ToString() == "221")
            {
                SubjectEntry.GetSujectList(ddlRelatedSubject, ddlClassName.SelectedValue.ToString());
            }


        }

        public int CourseId { get; set; }

        protected void lnkbtnpassMarks_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            try
            {
                ddlMarksClass.SelectedValue = ddlClassName.SelectedValue;
                ddlMarksClass.Enabled = false;
                if (subEntry == null)
                {
                    subEntry = new ClassDepedencySubPassMarksEntry();
                }
                DataTable dt = CRUD.ReturnTableNull("SELECT ClassID FROM Class_DependencyPassMarks WHERE ClassID='" + ddlClassName.SelectedValue + "'");
                if (dt.Rows.Count == 0)
                {
                    DependencyList = subEntry.GetDependencySubEntitiesData(ddlClassName.SelectedValue);
                }
                else
                {
                    DependencyList = subEntry.DependencySubEntitiesData(ddlClassName.SelectedValue);
                }
                if (DependencyList == null)
                {
                    lblMessage.InnerText = "warning->No Dependency Subject in Class " + ddlClassName.SelectedItem.Text + "";
                    return;
                }
                gvSubjectList.DataSource = DependencyList.ToList();
                gvSubjectList.DataBind();
                showDependencyModal.Show();
            }
            catch { }
        }

        protected void btnSaveDepedencyMarks_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            try
            {
                if (gvSubjectList.Rows.Count > 0)
                {
                    var saveList = new List<ClassDependencySubPassMarksEntities>();
                    int count = 0;
                    foreach (GridViewRow row in gvSubjectList.Rows)
                    {
                        HiddenField subId = row.FindControl("hideSubId") as HiddenField;
                        Label SubName = row.FindControl("lblSubName") as Label;
                        TextBox passMarks = row.FindControl("txtPassMarks") as TextBox;
                        saveList.Add(new ClassDependencySubPassMarksEntities()
                        {
                            Class = new ClassEntities()
                            {
                                ClassID = int.Parse(ddlMarksClass.SelectedValue)
                            },
                            Subject = new SubjectEntities()
                            {
                                SubjectId = int.Parse(subId.Value)
                            },
                            PassMarks = passMarks.Text == "" ? 0 : int.Parse(passMarks.Text)

                        });
                        count++;
                    }
                    if (count > 0)
                    {
                        if (clsdeppassEntry == null)
                        {
                            clsdeppassEntry = new ClassDepedencySubPassMarksEntry();
                        }
                        bool result = clsdeppassEntry.Insert(saveList);
                        if (result)
                        {
                            lblMessage.InnerText = "success-> Save successfully";
                            return;
                        }
                        lblMessage.InnerText = "error-> Unable to save";
                        return;
                    }

                }
            }
            catch { }
        }

        protected void rblGroupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            try
            {
                //string [] subCode = txtSubCode.Text.Trim().Split('-');
                //string getsubcode = (subCode.Length == 1) ? subCode[0] : subCode[1];
                //if (rblGroupList.SelectedIndex != 0)
                //{

                //    txtSubCode.Text = rblGroupList.SelectedItem.Value + "-" + getsubcode;

                //}
                //else txtSubCode.Text = getsubcode;
            }
            catch { }
        }

        protected void gvClassSubject_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "load();", true);
            //List<ClassSubject> GetEntitiesData;
            //if (class_subjectEntry == null) class_subjectEntry = new ClassSubjectEntry();
            ////if (ddlClassList.SelectedValue.ToString() == "0")
            //GetEntitiesData = class_subjectEntry.GetEntitiesData;
            ////else GetEntitiesData = ClassSubjectEntry.GetClassSubjectListByFiltering(int.Parse(ddlClassList.SelectedValue.ToString()));

            //gvClassSubject.DataSource = GetEntitiesData.ToList();
            DataBindForView();

            gvClassSubject.PageIndex = e.NewPageIndex;
            gvClassSubject.DataBind();
        }

        protected void gvClassSubject_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadStudentInfo();", true);
            try
            {
                int rIndex = Convert.ToInt32(e.CommandArgument.ToString());



                ddlClassName.SelectedValue = gvClassSubject.DataKeys[rIndex].Values[1].ToString();
                DataTable dt = new DataTable();
                dt = ClassSubjectEntry.getSubjectIdByClassSubject(gvClassSubject.DataKeys[rIndex].Values[0].ToString());

                ddlSubject.SelectedValue = dt.Rows[0]["SubId"].ToString();
                if (dt.Rows[0]["RelatedSubId"].ToString() == "0")
                {
                    ddlRelatedSubject.SelectedValue = "0";
                    ViewState["__relatedSubject_CSID__"] = "0";
                }
                else
                {
                    string relatedSubject_CSID = ClassSubjectEntry.getCSIdByClassSubject(ddlClassName.SelectedValue,dt.Rows[0]["RelatedSubId"].ToString());
                    ViewState["__relatedSubject_CSID__"] = relatedSubject_CSID;
                    ddlRelatedSubject.SelectedValue = relatedSubject_CSID + "_" + dt.Rows[0]["RelatedSubId"].ToString();
                }
                





                CourseEntry.GetCourseListBySubject(ddlCourse, ddlSubject.SelectedValue.ToString());

                ddlCourse.SelectedValue = dt.Rows[0]["CourseId"].ToString();
                txtSubCode.Text = gvClassSubject.Rows[rIndex].Cells[3].Text;
                txtOrderBy.Text = gvClassSubject.Rows[rIndex].Cells[4].Text;
                txtMarks.Text = gvClassSubject.Rows[rIndex].Cells[5].Text;

                GroupEntry.GetGroupByClass(rblGroupList, ddlClassList.SelectedValue.ToString());

                rblGroupList.SelectedValue = gvClassSubject.DataKeys[rIndex].Values[2].ToString();
                // chkIsOptional.Checked=(gvClassSubject.Rows[rIndex].Cells[6].Text=="No")?true:false;
                chkSubjectType.ClearSelection();
                if (gvClassSubject.Rows[rIndex].Cells[6].Text == "No")
                    chkSubjectType.SelectedValue = "0";
                if (gvClassSubject.Rows[rIndex].Cells[7].Text == "Yes")
                    chkSubjectType.SelectedValue = "1";
                btnSave.Text = "Update";
                ViewState["__CSID__"] = gvClassSubject.DataKeys[rIndex].Values[0].ToString();
            }
            catch { }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //  ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadStudentInfo();", true);
            DataTable dt = new DataTable();
            if (class_subjectEntry == null) class_subjectEntry = new ClassSubjectEntry();
            dt = class_subjectEntry.GetDataTable(ddlClassList.SelectedValue);
            if (dt.Rows.Count == 0)
            {
                lblMessage.InnerText = "warning-> No Pattern List available"; return;
            }
            Session["_SubjectPattern_"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=SubjectPattern');", true);  //Open New Tab for Sever side code
        }
        private void Clear()
        {
            CSId.Value = "";
            txtOrderBy.Text = "";
            txtSubCode.Text = "";
            txtMarks.Text = "";
            ddlCourse.SelectedValue = "0";
            btnSave.Text = "Save";
            // chkIsOptional.Checked=false;

        }

        protected void chkSubjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkSubjectType.Items[0].Selected == true)
                chkSubjectType.Items[1].Selected = false;
            if (chkSubjectType.Items[1].Selected == true)
                chkSubjectType.Items[0].Selected = false;

        }

        //private void relatedSubSaveAndUpdate(int ClsSubId) 
        //{
        //if(ddlRelatedSubject.SelectedIndex > 0) 
        //  {
        //    string RelatedSubId= ddlRelatedSubject.SelectedValue.ToString();
        //        if (class_subjectEntry == null) class_subjectEntry = new ClassSubjectEntry();
        //        class_subjectEntry.RelatedSubIdUpdated(ClsSubId, RelatedSubId);

        //    }
        
        //}
    }
}