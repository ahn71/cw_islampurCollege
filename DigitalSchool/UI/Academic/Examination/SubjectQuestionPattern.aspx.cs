using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using System.Data;
using System.Data.SqlClient;
using DS.BLL.Examinition;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.PropertyEntities.Model.ManagedClass;
using DS.PropertyEntities.Model.ManagedSubject;
using DS.BLL.ManagedSubject;
using DS.PropertyEntities.Model.Examinition;
using DS.PropertyEntities.Model.ManagedBatch;
using DS.BLL.ControlPanel;
using DS.DAL;
using System.Drawing;

namespace DS.UI.Academic.Examination
{
    public partial class SubjectQuestionPattern : System.Web.UI.Page
    {      
        DataTable dt;      
        ClassGroupEntry clsgrpEntry;
        SubQuestionPatternEntry subQPatternEntry;
        List<SubQuestionPatternEntities> AllsubQPatterList;
        List<SubQuestionPatternEntities> PreQpatternList;
        ExamInfoEntry examEntry;
        List<ExamInfoEntity> examinfoList;
        float m = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                    //---url bind---
                    aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                    aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                    aExamHome.HRef = "~/" + Classes.Routing.ExaminationHomeRouteUrl;
                    //---url bind end---
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "SubjectQuestionPattern.aspx", btnSave,btnEdit,btnDelete,btnPrint)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        Hide_Print_Edit();
                        ExamTypeEntry.GetExamType(ddlExamType);
                        BatchEntry.GetDropdownlist(ddlBatch,"True");
                        QuestionPatternEntry.GetDropdownlist(ddlQPattern);
                        AddColumns();
                        ExamTypeEntry.GetDropDownList(ddlExamTypeList);
                        BatchEntry.GetSubQPatternDropdownlist(ddlBatchList);
                        return;
                    }
                lblMessage.InnerText = "";
               
            }
            catch { }
        }
        private void Hide_Print_Edit() 
        {
            btnPrint.Enabled = false;
            btnPrint.CssClass = "";
            btnEdit.Enabled = false;
            btnEdit.CssClass = "";
            btnDelete.Enabled = false;
            btnDelete.CssClass = "";
        }
        private void Show_Print_Edit()
        {
            if(Session["__View__"].ToString().Equals("true"))
            { 
            btnPrint.Enabled = true;
            btnPrint.CssClass = "btn btn-primary";
            }
            if (Session["__Update__"].ToString().Equals("true")) { 
            btnEdit.Enabled = true;
            btnEdit.CssClass = "btn btn-success";
            }
            if (Session["__Delete__"].ToString().Equals("true"))
            {
                btnDelete.Enabled = true;
                btnDelete.CssClass = "btn btn-danger";
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
        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] batchClassId = ddlBatch.SelectedValue.Split('_');
            List<ClassSubject> SubjectList = new List<ClassSubject>();
            SubjectList = ClassSubjectEntry.GetClassSubjectListByFiltering(int.Parse(batchClassId[1])).FindAll(c => c.GroupId == 0);
            List<SubjectEntities> AllList = new List<SubjectEntities>();           
            if (SubjectList != null)
            {
                for (int i = 0; i < SubjectList.Count; i++)
                {
                    SubjectEntities subentry = new SubjectEntities();
                    subentry.SubjectId = SubjectList[i].Subject.SubjectId;
                    subentry.SubjectName = SubjectList[i].Subject.SubjectName;
                    AllList.Add(subentry);
                }
                var DistinctItems = AllList.GroupBy(x => x.SubjectId).Select(y => y.First());

                ddlsubjectName.DataSource = DistinctItems.ToList();
                ddlsubjectName.DataValueField = "SubjectId";
                ddlsubjectName.DataTextField = "SubjectName";
                ddlsubjectName.DataBind();
                ddlsubjectName.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(batchClassId[1]), ddlGroup);
        }

        protected void ddlsubjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] batchClassId = ddlBatch.SelectedValue.Split('_');
            List<ClassSubject> SubjectList = new List<ClassSubject>();
            SubjectList = ClassSubjectEntry.GetClassSubjectListByFiltering(int.Parse(batchClassId[1])).
                FindAll(c=>c.Subject.SubjectId==int.Parse(ddlsubjectName.SelectedValue));
            List<CourseEntity> AllList = new List<CourseEntity>();
            int count = 0;
            if (SubjectList != null)
            {
                ddlCourse.Enabled = true;
                
                for (int i = 0; i < SubjectList.Count; i++)
                {
                    if (SubjectList[i].Course.CourseId != 0)
                    {
                        CourseEntity subentry = new CourseEntity();
                        subentry.CourseId = SubjectList[i].Course.CourseId;
                        subentry.CourseName = SubjectList[i].Course.CourseName;
                        AllList.Add(subentry);
                        count++;
                    }
                }
                var DistinctItems = AllList.GroupBy(x => x.CourseId).Select(y => y.First());
                ddlCourse.DataSource = DistinctItems;
                ddlCourse.DataValueField = "CourseId";
                ddlCourse.DataTextField = "CourseName";
                ddlCourse.DataBind();
              //  ddlCourse.Items.Insert(0, new ListItem("...Select...", "0"));
                ddlCourse.Items.Insert(0, new ListItem("...No Course...", "0"));
            }
            if (count == 0) 
           { ddlCourse.Items.Insert(0, new ListItem("...No Course...", "0")); ddlCourse.Enabled = false; }
            float result = SubjectList.Sum(x =>x.SubMarks);
            txtsubMarks.Text = result.ToString("#");
            tr.Visible = true;
            tdsubMarks.Visible = true;
            tdcourseMarks.Visible = true;
            tdisoptional.Visible = true;
            txtcourseMarks.Text = "";
            if (SubjectList[0].IsOptional == true)
            {
                chkOptional.Checked = true;
                chkIsoptional.Checked = true;
            }
            else
            {
                chkOptional.Checked = false;
                chkIsoptional.Checked = false;
            }
            
        }
        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] batchClassId = ddlBatch.SelectedValue.Split('_');
            List<ClassSubject> SubjectList = new List<ClassSubject>();
            SubjectList = ClassSubjectEntry.GetClassSubjectListByFiltering(int.Parse(batchClassId[1])).
                FindAll(c => c.Subject.SubjectId == int.Parse(ddlsubjectName.SelectedValue)
                && c.Course.CourseId == int.Parse(ddlCourse.SelectedValue));
            float result = SubjectList[0].SubMarks;
            txtcourseMarks.Text = result.ToString("#");
            tdcourseMarks.Visible = true;
        }
        protected void chkIsoptional_CheckedChanged(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "isoptional();", true);
        }   
        protected void btnAdd_Click(object sender, EventArgs e)
        {
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
        private Boolean AddSubQPatternValidation()
        {
            try
            {
                
                string[] BatchId = ddlBatch.SelectedValue.Split('_');
                string Optional = chkOptional.Checked ? "Yes" : "No";
                dt = new DataTable();
                dt = (DataTable)ViewState["__tableInfo__"];
                if (btnSave.Text == "Save")
                {
                    if (subQPatternEntry == null)
                    {
                        subQPatternEntry = new SubQuestionPatternEntry();
                    }
                    bool result = chkIsoptional.Checked ? true : false;
                    AllsubQPatterList = subQPatternEntry.GetEntitiesData(BatchId[0], ddlExamType.SelectedValue, ddlGroup.SelectedValue);
                    if (AllsubQPatterList!=null)
                    {
                        AllsubQPatterList = AllsubQPatterList.FindAll
                            (c => c.ExamType.ExId == int.Parse(ddlExamType.SelectedValue)
                                && c.Batch.BatchId == int.Parse(BatchId[0]) && c.Group.ClsGrpID ==
                                int.Parse(ddlGroup.SelectedValue) && c.Subject.SubjectId ==
                                int.Parse(ddlsubjectName.SelectedValue) && c.Course.CourseId ==
                                int.Parse(ddlCourse.SelectedValue) && c.QPattern.QPId ==
                                int.Parse(ddlQPattern.SelectedValue));
                        if (AllsubQPatterList.Count > 0)
                        {
                            lblMessage.InnerText = "warning->Already set " + ddlQPattern.SelectedItem.Text + " Question Pattern";
                            return false;
                        }
                    }                  
                    
                }                
                if (dt == null) return true;               
                for (byte i = 0; i < dt.Rows.Count; i++)
                {
                    if ((dt.Rows[i]["SubId"].ToString() == ddlsubjectName.SelectedValue)
                        && (dt.Rows[i]["QPId"].ToString() == ddlQPattern.SelectedValue)
                        && (dt.Rows[i]["CourseId"].ToString() == ddlCourse.SelectedValue))
                    {
                        lblMessage.InnerText = "warning->" + ddlsubjectName.SelectedItem.Text + " Question Pattern " + ddlQPattern.SelectedItem.Text + " Already Added";
                        return false;
                    }
                }
                if (ddlCourse.SelectedValue == "0")
                {
                    object sumObject;
                    sumObject = dt.Compute("Sum(QMarks)", "SubId='" + ddlsubjectName.SelectedValue + "' AND IsOptional='"+Optional+"'");
                    string getsum = sumObject.ToString() == "" ? "0" : sumObject.ToString();
                    float total = float.Parse(getsum) + float.Parse(txtQMarks.Text);
                    if (total > float.Parse(txtsubMarks.Text))
                    {
                        lblMessage.InnerText = "warning->Subject Marks "+txtsubMarks.Text
                            + ": You Can Create " + (float.Parse(txtsubMarks.Text) - float.Parse(getsum)) + " Marks";
                        txtQMarks.Focus();
                        return false;
                    }
                }
                else if(ddlsubjectName.SelectedValue!="0"&&ddlCourse.SelectedValue!="0")
                {
                    object sumObject;
                    sumObject = dt.Compute("Sum(QMarks)", "SubId='" + ddlsubjectName.SelectedValue
                        + "' AND CourseId='" + ddlCourse.SelectedValue + "' AND IsOptional='"+Optional+"'");
                    string getsum = sumObject.ToString() == "" ? "0" : sumObject.ToString();
                    float total = float.Parse(getsum) + float.Parse(txtQMarks.Text);
                    if (total > float.Parse(txtcourseMarks.Text))
                    {
                        lblMessage.InnerText = "warning->Course Marks " + txtcourseMarks.Text
                            + ": You Can Create " + (float.Parse(txtcourseMarks.Text) - float.Parse(getsum)) + " Marks";
                        txtQMarks.Focus();
                        return false;
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
        private void addRowsAndColumns()
        {
            try
            {
                string[] value = new string[11];
                value[0] = ddlsubjectName.SelectedValue;
                value[1] = ddlsubjectName.SelectedItem.Text;
                value[2] = ddlCourse.SelectedValue;
                if (ddlCourse.SelectedValue == "0")
                {
                    value[3] = "";
                }
                else
                {
                    value[3] = ddlCourse.SelectedItem.Text;
                }
                value[4] = ddlQPattern.SelectedValue;
                value[5] = ddlQPattern.SelectedItem.Text;
                value[6] = txtQMarks.Text;
                value[7] = txtPassMark.Text;                
                if (chkOptional.Checked)
                {
                    value[8] = "Yes";
                }
                else
                {
                    value[8] = "No";
                }
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
                    dt.Columns.Add("SubId", typeof(long));
                    dt.Columns.Add("SubName", typeof(string));
                    dt.Columns.Add("CourseId", typeof(long));
                    dt.Columns.Add("CourseName", typeof(string));
                    dt.Columns.Add("QPId", typeof(int));
                    dt.Columns.Add("QPName", typeof(string));
                    dt.Columns.Add("QMarks", typeof(float));
                    dt.Columns.Add("PassMarks", typeof(float));                   
                    dt.Columns.Add("IsOptional", typeof(string));
                    dt.Columns.Add("SL", typeof(int));
                    dt.Columns.Add("ConvertTo", typeof(float)); // add by nayem ,15-12-2017. purpose: Question Pattern wise convert.
                }
                int count = dt.Rows.Count;
                count++;
                value[9] = count.ToString();
                value[10] = txtConvertTo.Text.Trim();
                dt.Rows.Add(value);
                dt.DefaultView.Sort = "SL DESC";

                DataView dView = dt.DefaultView;
                dt = dView.ToTable();
                ViewState["__tableInfo__"] = dt;
                gvSubQPattern.DataSource = dt;
                gvSubQPattern.Columns[0].Visible = true;                
                gvSubQPattern.DataBind();
            }
            catch (Exception ex) { }
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
                    dt.DefaultView.Sort = "SL DESC";
                    DataView dView = dt.DefaultView;
                    dt = dView.ToTable();
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

            if (btnSave.Text == "Save")
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; return; }
                if (SubPatternValidation() == false) return;
                if (saveSubjectQuestionPattern() == true)
                {                
                    ExamTypeEntry.GetDropDownList(ddlExamTypeList);
                    BatchEntry.GetSubQPatternDropdownlist(ddlBatchList);
                    LoadSubQPatternList("");
                    lblMessage.InnerText = "success->Successfully saved";
                }
            }
            else if (btnSave.Text == "Update")
            {               
                SubQuestionPatternEntities entities = new SubQuestionPatternEntities();
                entities.ExamType = new ExamTypeEntities()
                {
                    ExId = int.Parse(ddlExamType.SelectedValue)
                };
                string[] batchId = ddlBatch.SelectedValue.Split('_');
                entities.Batch = new BatchEntities()
                {
                    BatchId = int.Parse(batchId[0])
                };
                entities.Group = new ClassGroupEntities()
                {
                    ClsGrpID = int.Parse(ddlGroup.SelectedValue)
                };
                if (subQPatternEntry == null)
                {
                    subQPatternEntry = new SubQuestionPatternEntry();
                }
                subQPatternEntry.AddEntities = entities; 
                //....................validation................
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["__tableInfo__"];
                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, "SubId");
                if ((ddlsubjectName.Items.Count - 1) != distinctValues.Rows.Count)
                {
                    lblMessage.InnerText = "warning->Add All Subject For Question Pattern";
                    return;
                }
                //..............................................
            
                bool result=subQPatternEntry.Delete();
                if (result==true)
                {
                    if (SubPatternValidation() == false) return;
                    if (saveSubjectQuestionPattern() == true)
                    {
                        ExamTypeEntry.GetDropDownList(ddlExamTypeList);
                        BatchEntry.GetSubQPatternDropdownlist(ddlBatchList);
                        LoadSubQPatternList("");
                        btnSave.Text = "Save";
                        lblMessage.InnerText = "success->Successfully Updated";
                    }
                }
            }
        }
        private Boolean saveSubjectQuestionPattern()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["__tableInfo__"];
                DataView view = new DataView(dt);
                DataTable distinctValues = view.ToTable(true, "SubId");
                if ((ddlsubjectName.Items.Count - 1) != distinctValues.Rows.Count)
                {
                    lblMessage.InnerText = "warning->Add All Subject For Question Pattern";
                    return false;
                }
                /*--Start----
                 date: 25-11-2019
                 purpose: course validation has been withdrawn for combined subject.
                 by Nayem.
                 -------

                //--------
                //modifyed by nayem date: 10-12-2017
                //purpose: when find group then it will compare with group
                string sql="";
                string[] batchClassId = ddlBatch.SelectedValue.Split('_');
                if (ddlGroup.Enabled)
                    sql = "select Distinct COUNT(CourseId) as CourseId from ClassSubject cs left join Tbl_Class_Group cg "+
                        " on  cs.classid=cg.classid and  cs.GroupId=cg.GroupId "+
                        "where cs.ClassId='" + batchClassId[1] + "'AND cs.CourseId!=0 and (cg.ClsGrpID="+ddlGroup.SelectedValue+" or cs.GroupId=0 )";               
                else
                    sql = "SELECT Distinct COUNT(CourseId) as "
                 + "CourseId FROM ClassSubject where ClassID='" + batchClassId[1] + "' AND CourseId!=0";

                DataTable dtCourse = CRUD.ReturnTableNull(sql);
                //-------end by nayem--------
                view = new DataView(dt);
                distinctValues = new DataTable();
                distinctValues = view.ToTable(true, "CourseId");
                int count = distinctValues.Select("CourseId not in (0)").Count();
                if (dtCourse.Rows[0]["CourseId"].ToString() != count.ToString())
                {

                    lblMessage.InnerText = "warning->Add All Course For Question Pattern";
                    return false;
                }
                 --End*/
                bool result = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    SubQuestionPatternEntities entities = new SubQuestionPatternEntities();
                    entities.ExamType = new ExamTypeEntities()
                    {
                        ExId = int.Parse(ddlExamType.SelectedValue)
                    };
                    string[] batchId = ddlBatch.SelectedValue.Split('_');
                    entities.Batch = new BatchEntities()
                    {
                        BatchId = int.Parse(batchId[0])
                    };
                    entities.Group = new ClassGroupEntities()
                    {
                        ClsGrpID = int.Parse(ddlGroup.SelectedValue)
                    };
                    entities.Subject = new SubjectEntities()
                    {
                        SubjectId = int.Parse(dt.Rows[i]["SubId"].ToString())
                    };
                    entities.Course = new CourseEntity()
                    {
                        CourseId = int.Parse(dt.Rows[i]["CourseId"].ToString())
                    };
                    entities.QPattern = new QuestionPatternEntities()
                    {
                        QPId = int.Parse(dt.Rows[i]["QPId"].ToString())
                    };
                    entities.QMarks = float.Parse(dt.Rows[i]["QMarks"].ToString());
                    entities.PassMarks = float.Parse(dt.Rows[i]["PassMarks"].ToString());
                    object sumObject;
                    sumObject = dt.Compute("Sum(QMarks)", "SubId='" + dt.Rows[i]["SubId"].ToString()
                        + "' AND CourseId='" + dt.Rows[i]["CourseId"].ToString() + "' ");
                    entities.SubQPMarks = float.Parse(sumObject.ToString());
                    entities.ConvertTo = float.Parse(dt.Rows[i]["ConvertTo"].ToString());
                    if (dt.Rows[i]["IsOptional"].ToString() == "Yes")
                    {
                        entities.IsOptional = true;
                    }
                    else
                    {
                        entities.IsOptional = false;
                    }

                    if (subQPatternEntry == null)
                    {
                        subQPatternEntry = new SubQuestionPatternEntry();
                    }
                    subQPatternEntry.AddEntities = entities;
                    result = subQPatternEntry.Insert();
                }
                if (result == true)
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

        private Boolean SubPatternValidation()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = (DataTable)ViewState["__tableInfo__"];
                if (dt == null)
                {
                    lblMessage.InnerText = "warning->First Time Add Question Pattern";
                    AddColumns();
                    return false;
                }

                if (subQPatternEntry == null)
                {
                    subQPatternEntry = new SubQuestionPatternEntry();
                }
                //...........Condition For Convert To
                string[] batchId = ddlBatch.SelectedValue.Split('_');
                //float getconMarks = subQPatternEntry.GetConvertMarks(batchId[0], ddlGroup.SelectedValue);

                //string val = txtConvertTo.Text == "" ? "0" : txtConvertTo.Text;
                //float total = getconMarks + float.Parse(val);
                //if (total > 100)
                //{
                //    lblMessage.InnerText = "warning->You Can Set " + (100 - getconMarks) + " % For Convert";
                //    txtConvertTo.Focus();
                //    return false;
                //}
                //............End Convert To....................
                //---------- add by nayem date: 22-12-2017-----------
                //string cmd = "";
                //if (ViewState["__IsSemesterExam__"].ToString().Equals(""))
                //    cmd = "SELECT DISTINCT sqp.ExId FROM SubjectQuestionPattern sqp inner join ExamType et on sqp.ExId=et.ExId "
                //+ "WHERE BatchId='" + batchId[0] + "' AND ClsGrpID='" + ddlGroup.SelectedValue + "' and SemesterExam is null ";
                //else
                //    cmd = "SELECT DISTINCT sqp.ExId FROM SubjectQuestionPattern sqp inner join ExamType et on sqp.ExId=et.ExId "
                //+ "WHERE BatchId='" + batchId[0] + "' AND ClsGrpID='" + ddlGroup.SelectedValue + "' and SemesterExam ='" + ViewState["__IsSemesterExam__"].ToString() + "'";
                //DataTable dtExamType = new DataTable();
                //dtExamType = CRUD.ReturnTableNull(cmd);
                //if (dtExamType.Rows.Count == 0) return true;
                //-------End -------------------
             //   AllsubQPatterList = subQPatternEntry.GetEntitiesData(batchId[0], dtExamType.Rows[0]["ExId"].ToString(), ddlGroup.SelectedValue);
                AllsubQPatterList = subQPatternEntry.GetEntitiesData(batchId[0], ddlExamType.SelectedValue, ddlGroup.SelectedValue);
                if (AllsubQPatterList != null)
                {
                   
                    AllsubQPatterList = AllsubQPatterList.FindAll(c => c.Batch.BatchId == int.Parse(batchId[0])
                       && c.Group.ClsGrpID == int.Parse(ddlGroup.SelectedValue) && c.ExamType.ExId == int.Parse(ddlExamType.SelectedValue));

                    if (AllsubQPatterList.Count == 0) return true;
                    else
                    {
                        if (AllsubQPatterList.Count!=dt.Rows.Count)
                        {
                            lblMessage.InnerText = "warning->Privious Exam Question Pattern And Present Exam Question Pattern Does not Match";
                            return false;
                        }
                        for (byte x = 0; x < dt.Rows.Count; x++)
                        {
                            PreQpatternList = AllsubQPatterList.FindAll(c => c.Subject.SubjectId == int.Parse(dt.Rows[x]["SubId"].ToString())
                                && c.Course.CourseId == int.Parse(dt.Rows[x]["CourseId"].ToString()) && c.QPattern.QPId == int.Parse(dt.Rows[x]["QPId"].ToString()));
                            if (PreQpatternList.Count == 0)
                            {
                                lblMessage.InnerText = "warning->" + dt.Rows[x]["SubName"].ToString() + " "
                                    + dt.Rows[x]["QPName"].ToString() + " Miss Match." + " Previous Subject Pattern ";
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
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //--------------
            ddlExamType.Enabled = true;
            ddlBatch.Enabled = true;
            //--------------
            btnSave.Text = "Save";            
            ViewState["__tableInfo__"] = null;
            AddColumns();

        }  
        protected void ddlBatchList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] batchClassId = ddlBatchList.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(batchClassId[1]), ddlGroupList);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {           
            LoadSubQPatternList("");
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "loadpatterninfo();", true);  //Open New Tab for Sever side code
        }
        private void LoadSubQPatternList(string sqlcmd)
        {
            try
            {
                string[] batchId=ddlBatchList.SelectedValue.Split('_');
                if (subQPatternEntry == null)
                {
                    subQPatternEntry = new SubQuestionPatternEntry();
                }
                AllsubQPatterList = subQPatternEntry.GetEntitiesData(batchId[0], ddlExamTypeList.SelectedValue, ddlGroupList.SelectedValue);
                int totalRows = AllsubQPatterList.Count;
//...........................................................
                if (totalRows < 1) Hide_Print_Edit();
                else 
                    Show_Print_Edit();
//................................................
                string divInfo = "";
                divInfo = " <table id='tblsubquepattern' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Subject Name</th>";
                divInfo += "<th>Course Name</th>";
                divInfo += "<th>Question Pattern</th>";
                divInfo += "<th style='width:70px;text-align:center;'>Q.Marks</th>";
                divInfo += "<th style='width:89px;text-align:center;'>Pass Marks</th>";
                divInfo += "<th style='width:87px;text-align:center;'>Convert To</th>";
                divInfo += "<th style='width:87px;text-align:center;'>Is Optional</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Pattern List available</div>";                    
                    divSQpattarn.Controls.Add(new LiteralControl(divInfo));
                    msgForPatterStatus.Visible = false;
                    return;
                }               

                divInfo += "<tbody>";
                int id = 0;
                for (int x = 0; x < AllsubQPatterList.Count; x++)
                {
                    id++;
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + AllsubQPatterList[x].Subject.SubjectName + "</td>";
                    divInfo += "<td >" + AllsubQPatterList[x].Course.CourseName + "</td>";

                    divInfo += "<td >" + AllsubQPatterList[x].QPattern.QPName + "</td>";
                    divInfo += "<td style='width:70px;text-align:center;'>" + AllsubQPatterList[x].QMarks + "</td>";
                    divInfo += "<td style='width:70px;text-align:center;'>" + AllsubQPatterList[x].PassMarks + "</td>";
                    divInfo += "<td style='width:70px;text-align:center;'>" + AllsubQPatterList[x].ConvertTo + "</td>";
                    if (AllsubQPatterList[x].IsOptional == true)
                    {
                        divInfo += "<td style='width:70px;text-align:center;'>Yes</td>";
                    }
                    else
                    {
                        divInfo += "<td style='width:70px;text-align:center;'>No</td>";
                    }
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";

                divSQpattarn.Controls.Add(new LiteralControl(divInfo));
                if (examEntry == null)
                {
                    examEntry = new ExamInfoEntry();
                }
                examinfoList = examEntry.GetEntitiesData(batchId[0],ddlExamTypeList.SelectedItem.Text);
                //if (examinfoList!=null)
                //{
                //    msgForPatterStatus.Visible = true;
                //    btnEdit.Enabled=false;
                //    btnEdit.CssClass = "";
                //    btnDelete.Enabled = false;
                //    btnDelete.CssClass = "";
                //}
            }
            catch { }
        }       

        protected void btnEdit_Click(object sender, EventArgs e)
        {

            string[] batchId = ddlBatchList.SelectedValue.Split('_');
            if (subQPatternEntry == null)
            {
                subQPatternEntry = new SubQuestionPatternEntry();
            }
            AllsubQPatterList = subQPatternEntry.GetEntitiesData(batchId[0], ddlExamTypeList.SelectedValue, ddlGroupList.SelectedValue);
            if (AllsubQPatterList.Count == 0) return;
            editaddcolumn();
            int SL=0;
            for (int x = 0; x < AllsubQPatterList.Count; x++)
            {
                SL=SL+1;
                dt.Rows.Add(AllsubQPatterList[x].Subject.SubjectId,
                            AllsubQPatterList[x].Subject.SubjectName,
                            AllsubQPatterList[x].Course.CourseId,
                            AllsubQPatterList[x].Course.CourseName,
                            AllsubQPatterList[x].QPattern.QPId,
                            AllsubQPatterList[x].QPattern.QPName,
                            AllsubQPatterList[x].QMarks,
                            AllsubQPatterList[x].PassMarks,
                            AllsubQPatterList[x].IsOptional,
                            SL, AllsubQPatterList[x].ConvertTo);
            }
            ViewState["__tableInfo__"] = dt;
            gvSubQPattern.DataSource = dt;
            gvSubQPattern.Columns[0].Visible = true;
            gvSubQPattern.Columns[2].Visible = true;
            gvSubQPattern.DataBind();            
            btnSave.Text = "Update";
            ddlExamType.SelectedValue = ddlExamTypeList.SelectedValue;
            ddlBatch.SelectedValue = ddlBatchList.SelectedValue;
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(batchId[1]), ddlGroup);
            ddlGroup.SelectedValue = ddlGroupList.SelectedValue;
            //--------------------
            ddlExamType.Enabled = false;
            ddlBatch.Enabled = false;
            ddlGroup.Enabled = false;
            //--------------------
            List<ClassSubject> SubjectList = new List<ClassSubject>();
            SubjectList = ClassSubjectEntry.GetClassSubjectListByFiltering(int.Parse(batchId[1])).FindAll(c => c.GroupId == 0 || c.GroupId == GetGroupId());
            List<SubjectEntities> AllList = new List<SubjectEntities>();
            if (SubjectList != null)
            {
                for (int i = 0; i < SubjectList.Count; i++)
                {
                    SubjectEntities subentry = new SubjectEntities();
                    subentry.SubjectId = SubjectList[i].Subject.SubjectId;
                    subentry.SubjectName = SubjectList[i].Subject.SubjectName;
                    AllList.Add(subentry);
                }
                var DistinctItems = AllList.GroupBy(x => x.SubjectId).Select(y => y.First());

                ddlsubjectName.DataSource = DistinctItems.ToList();
                ddlsubjectName.DataValueField = "SubjectId";
                ddlsubjectName.DataTextField = "SubjectName";
                ddlsubjectName.DataBind();
                ddlsubjectName.Items.Insert(0, new ListItem("...Select...", "0"));
            }
                
        }
        private void editaddcolumn()
        {
            dt = new DataTable();
            dt.Columns.Add("SubId", typeof(long));
            dt.Columns.Add("SubName", typeof(string));
            dt.Columns.Add("CourseId", typeof(long));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("QPId", typeof(int));
            dt.Columns.Add("QPName", typeof(string));
            dt.Columns.Add("QMarks", typeof(float));
            dt.Columns.Add("PassMarks", typeof(float));            
            dt.Columns.Add("IsOptional", typeof(string));
            dt.Columns.Add("SL", typeof(string));
            dt.Columns.Add("ConvertTo", typeof(float));
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SubQuestionPatternEntities entities = new SubQuestionPatternEntities();
                entities.ExamType = new ExamTypeEntities()
                {
                    ExId = int.Parse(ddlExamTypeList.SelectedValue)
                };
                string[] batchId = ddlBatchList.SelectedValue.Split('_');
                entities.Batch = new BatchEntities()
                {
                    BatchId = int.Parse(batchId[0])
                };
                entities.Group = new ClassGroupEntities()
                {
                    ClsGrpID = int.Parse(ddlGroupList.SelectedValue)
                };
                if (subQPatternEntry == null)
                {
                    subQPatternEntry = new SubQuestionPatternEntry();
                }
                subQPatternEntry.AddEntities = entities;
                bool result = subQPatternEntry.Delete();
                
                if (result==true)
                {
                    LoadSubQPatternList("");
                    lblMessage.InnerText = "success->Successfully Deleted.";
                }
            }
            catch { }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            if (subQPatternEntry == null)
            {
                subQPatternEntry = new SubQuestionPatternEntry();
            }
            string ddlGroup = "";
            //if (ddlGroupList.SelectedItem.Text.Trim() == "...No Group...")
            //    ddlGroup = "0";
           // else 
           
            string[] BatchId = ddlBatchList.SelectedValue.Split('_');
            dt = subQPatternEntry.GetDataTable(ddlExamTypeList.SelectedValue, BatchId[0].ToString(), ddlGroupList.SelectedValue.ToString());
            if(dt.Rows.Count==0)
            {
                lblMessage.InnerText = "warning-> No Pattern List available"; return;
            }
         Session["_SubjectQuestionPattern_"] = dt;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=SubjectQuestionPattern');", true);  //Open New Tab for Sever side code
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] batchClassId = ddlBatch.SelectedValue.Split('_');
            List<ClassSubject> SubjectList = new List<ClassSubject>();
            SubjectList = ClassSubjectEntry.GetClassSubjectListByFiltering(int.Parse(batchClassId[1])).FindAll(c => c.GroupId == 0 || c.GroupId == GetGroupId());
            List<SubjectEntities> AllList = new List<SubjectEntities>();
            if (SubjectList != null)
            {
                for (int i = 0; i < SubjectList.Count; i++)
                {
                    SubjectEntities subentry = new SubjectEntities();
                    subentry.SubjectId = SubjectList[i].Subject.SubjectId;
                    subentry.SubjectName = SubjectList[i].Subject.SubjectName;
                    AllList.Add(subentry);
                }
                var DistinctItems = AllList.GroupBy(x => x.SubjectId).Select(y => y.First());

                ddlsubjectName.DataSource = DistinctItems.ToList();
                ddlsubjectName.DataValueField = "SubjectId";
                ddlsubjectName.DataTextField = "SubjectName";
                ddlsubjectName.DataBind();
                ddlsubjectName.Items.Insert(0, new ListItem("...Select...", "0"));
            }           
        }
        private int GetGroupId() 
        {
            List<ClassGroupEntities> ClassGroupList = new List<ClassGroupEntities>();
            if (clsgrpEntry == null) { clsgrpEntry = new ClassGroupEntry(); }
            ClassGroupList = clsgrpEntry.GetEntitiesData().FindAll(d => d.ClsGrpID == int.Parse(ddlGroup.SelectedValue));
            int GroupId = ClassGroupList[0].GroupID;
            return GroupId;
        }

        protected void btnDependency_Click(object sender, EventArgs e)
        {
            ShowDependencyMarks();
            ShowDependencyModal.Show();
        }
        private void ShowDependencyMarks()
        {
            try
            {
                if (subQPatternEntry == null)
                {
                    subQPatternEntry = new SubQuestionPatternEntry();
                }
                dt = new DataTable();
                string[] batchId = ddlBatchList.SelectedValue.Split('_');
                dt = subQPatternEntry.GetConvertTotalMarks(batchId[0], ddlGroupList.SelectedValue);
                gvDependency.DataSource = dt;
                gvDependency.DataBind();
                lblBatch.Text = ddlBatchList.SelectedItem.Text;
                lblGroup.Text = ddlGroupList.SelectedItem.Text;
               
            }
            catch { }
        }

        protected void gvDependency_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label ConvertTo = (Label)e.Row.FindControl("lblConvertTo");
                //Label lblUnitsInStock = (Label)e.Row.FindControl("lblUnitsInStock");
                m = m + float.Parse(ConvertTo.Text);
                //Table tb = new Table();
                if (ConvertTo.Text != "0")
                {
                    e.Row.BackColor = Color.FromName("#A1DCF2");  
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lblTotalPrice = (Label)e.Row.FindControl("ConvertTo");
                lblTotalPrice.Text = m.ToString();
                total.Text = m.ToString();
            }
        }
        
        protected void ddlExamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ExamType = "";
            ExamType = ExamTypeEntry.GetIsSemesterExam(ddlExamType.SelectedValue);
            ViewState["__IsSemesterExam__"] = ExamType;
            //if (ExamType == "")
            //{
            //    txtsubMarks.Enabled = true;
            //    txtcourseMarks.Enabled = true;
            //}
            //else
            //{
            //    txtsubMarks.Enabled = false;
            //    txtcourseMarks.Enabled = false;
            //}
        }
    }
}