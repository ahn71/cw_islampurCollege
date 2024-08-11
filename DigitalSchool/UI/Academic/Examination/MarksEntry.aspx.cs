using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.PropertyEntities.Model.GeneralSettings;
using DS.PropertyEntities.Model.ManagedClass;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedClass;
using DS.BLL.ManagedBatch;
using DS.BLL.Examinition;
using DS.PropertyEntities.Model.Examinition;
using DS.UI.Academic.Examination;
using DS.DAL;
using DS.BLL.ControlPanel;
using System.Text;




namespace DS.UI.Academics.Examination
{
    public partial class MarksEntry : System.Web.UI.Page
    {
        DataTable dt;
        SqlCommand cmd;
        bool result;

        Class_ClasswiseMarksheet_TotalResultProcess_Entry class_classWiseMarkSheet_Entry;
        List<Class_ClasswiseMarksheet_TotalResultProcess> SubList = new List<Class_ClasswiseMarksheet_TotalResultProcess>();
        Exam_Final_Result_Stock_Of_All_Batch_Entry exam_Final_Result_Stock_Of_All_Batch_Entry;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.InnerText = "";
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(Session["__UserTypeId__"].ToString(), "MarksEntry.aspx", btnSearch, chkForCoutAsFinalResult, btnPreviewMarksheet, btnDetailsMarks,btnPrintPreview)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        ShiftEntry.GetDropDownList(ddlShift);                       
                        BatchEntry.GetDropdownlist(ddlBatch,true); 
                    }
                }
            }
            catch { }
        }
        string tblInfo = "";
        string tblHeader = "";
        private void loadMarksEntrySheet()
        {
            try
            {
                bool HasMultipleOptSub = false;
                bool HasSingleOptSub = false;
                string[] getExTypeId = ddlExamId.SelectedItem.Text.Split('_');
                DataTable dtExId = new DataTable();

                //-----------for title display---------------- 
                MarkSheetTitle.InnerText = "Marksheet entry point for batch " + ddlBatch.SelectedItem.Text + " section " + ddlSectionName.SelectedItem.Text.Trim() + " ("
                + ddlShift.SelectedItem.Text + ")";
                //--------------------------------------------

                //----------for get class name and marksheet name -------------------
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet";
                //---------------------------------------------------------------------

                 DataTable dt_GroupSubjectList = new DataTable();
                //----------for load all current student of this batch----------------- 
                if (ddlGroup.Enabled == false)
                {
                    SQLOperation.selectBySetCommandInDatatable("select StudentId from CurrentStudentInfo where BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId="
                    + ddlSectionName.SelectedItem.Value + " AND ConfigId=" + ddlShift.SelectedItem.Value + " order by RollNo", dt = new DataTable(), sqlDB.connection);
                  
                    //sqlDB.fillDataTable("select StudentId,SubId from OptionalSubjectInfo where StudentId in (select StudentId from CurrentStudentInfo where BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId="
                    //+ ddlSectionName.SelectedItem.Value + " AND ConfigId=" + ddlShift.SelectedItem.Value + " order by RollNo )", dt_GroupSubjectList);
                }
                else
                {
                    SQLOperation.selectBySetCommandInDatatable("select StudentId from CurrentStudentInfo where BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId="
                   + ddlSectionName.SelectedItem.Value + " AND ConfigId=" + ddlShift.SelectedItem.Value + " AND ClsGrpId=" + ddlGroup.SelectedItem.Value + " order by RollNo", dt = new DataTable(), sqlDB.connection);
                  
                   // sqlDB.fillDataTable("select StudentId,SubId from OptionalSubjectInfo where StudentId in (select StudentId from CurrentStudentInfo where BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId="
                   //     + ddlSectionName.SelectedItem.Value + " AND ConfigId=" + ddlShift.SelectedItem.Value + " AND ClsGrpId=" + ddlGroup.SelectedItem.Value + " order by RollNo )", dt_GroupSubjectList);
                }
                //---------------------------------------------------------------------

                //----------for load subject question pattern by batchId and exam type----------------------
                DataTable dtSQPInfo = new DataTable();   // SQP = subject question pattern information
               
                if (ddlGroup.Enabled == false)
                {
                    SQLOperation.selectBySetCommandInDatatable("select distinct SubQPId,SubId,CourseId,SubName,QPId,QPName,CourseName,IsOptional,QMarks from "
                + "v_SubjectQuestionPattern where BatchId='" + ddlBatch.SelectedItem.Value + "' AND ExId=(select ExId from ExamInfo where ExInId ='" + ddlExamId.SelectedItem.Text + "') order by QPId", dtSQPInfo, sqlDB.connection);                                           
                }
                else
                {
                    SQLOperation.selectBySetCommandInDatatable("select distinct SubQPId,SubId,SubName,QPId,QPName,CourseName,IsOptional,QMarks,CourseId from v_SubjectQuestionPattern where BatchId=" + ddlBatch.SelectedItem.Value + " "
                + "AND ExId=(select ExId from ExamInfo where ExInId ='" + ddlExamId.SelectedItem.Text + "') AND ClsGrpID='" + ddlGroup.SelectedItem.Value + "' order by QPId", dtSQPInfo, sqlDB.connection);    // specially for nine ten and upper from ten
                }
                 //-------------------------------------------------------------------------------------------

                //---------------------------class subject ordering synchronizing------------------------------------------
                System.Data.DataColumn Ordering = new DataColumn("Ordering", typeof(int));
                Ordering.DefaultValue = 0;
                dtSQPInfo.Columns.Add(Ordering);

                DataTable dtClassSubOrder = new DataTable();
                DataTable dtGroupId = new DataTable();
                sqlDB.fillDataTable("select GroupId from Tbl_Class_Group where ClsGrpId =" + ddlGroup.SelectedValue + "", dtGroupId);

                string GRPId = (ddlGroup.Enabled == false) ? "0" : dtGroupId.Rows[0]["GroupId"].ToString();
                
                sqlDB.fillDataTable("select SubId,CourseId,Ordering from ClassSubject where ClassID=(select ClassId from BatchInfo where BatchId=" + ddlBatch.SelectedValue + ") and GroupId in ('0','" + GRPId + "') order by Ordering  ", dtClassSubOrder = new DataTable());

                for (sbyte b = 0; b < dtSQPInfo.Rows.Count;b++)
                {
                    DataRow[] dr = dtClassSubOrder.Select("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString().Trim() + " AND CourseId=" + dtSQPInfo.Rows[b]["CourseId"].ToString().Trim() + " ", null);

                    dtSQPInfo.Select(string.Format("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString()) + "and CourseId=" + dtSQPInfo.Rows[b]["CourseId"].ToString() + "").ToList<DataRow>().ForEach(r => r["Ordering"] = int.Parse(dr[0]["Ordering"].ToString()));
                }

                DataView dv = dtSQPInfo.DefaultView;
                dv.Sort = "Ordering asc";
                dtSQPInfo = dv.ToTable();
                //----------------------------------------------End Synchronizing---------------------------------------------

                //-----------for create table of marks entry point ------------------------------------------
                tblInfo = "<Table id=tblMarkEntryPoint class=table table-striped table-bordered dt-responsive nowrap cellspacing='0' width='100%'>";                   

                tblHeader += "<th style='width:70px;text-align:center;'>Roll No</th>";

                //-------------------Bellow statements for header------------------------------------------------
                DataTable dt_TotalMarksheetForReport = new DataTable();
                DataTable dt_JustMarkSheetColumnsName = new DataTable();

                for (byte b = 0; b < dtSQPInfo.Rows.Count; b++)
                {
                    string subsName = "", subsName2 = "";
                    string[] SubJectName = dtSQPInfo.Rows[b]["SubName"].ToString().Trim().Split(' ');
                    for (sbyte s = 0; s < SubJectName.Length; s++)
                    {
                        subsName += SubJectName[s] + "</br>";
                        if (s == SubJectName.Length - 1) subsName2 += SubJectName[s];
                        else subsName2 += SubJectName[s] + Environment.NewLine;
                    }
                    subsName = subsName.Substring(0, subsName.LastIndexOf('<'));
                    
                    
                    string getCourseName=(dtSQPInfo.Rows[b]["CourseName"].ToString().Equals("Null"))?"":" "+dtSQPInfo.Rows[b]["CourseName"].ToString();
                    string IsOptional = (bool.Parse(dtSQPInfo.Rows[b]["IsOptional"].ToString()).Equals(true)) ? "" : "";
                    tblHeader += "<th style='border:2px solid gray;text-align: center;'>" + subsName + getCourseName + "</br>" + dtSQPInfo.Rows[b]["QPName"].ToString().Replace(" ", "") + "</br>" + IsOptional + " </th>";
                    if (b == dtSQPInfo.Rows.Count - 1) tblInfo += tblHeader;

                    if (b == 0)
                    {
                        dt_TotalMarksheetForReport.Columns.Add("RollNo", typeof(string));
                        dt_JustMarkSheetColumnsName.Columns.Add("RollNo", typeof(string));
                    }
                    dt_TotalMarksheetForReport.Columns.Add("DataColumn"+(b+1).ToString(), typeof(string));
                    dt_JustMarkSheetColumnsName.Columns.Add(subsName2 + getCourseName + Environment.NewLine + dtSQPInfo.Rows[b]["QPName"].ToString().Replace(" ", ""), typeof(string));
                   
                }

                Session["__JustColumnsName__"] = dt_JustMarkSheetColumnsName;
                //--------------------------------------------------------------------------------------------------

                tblInfo += "<tr>";
                DataTable dtCreateViewForClassMarksheet = new DataTable();

                string StudentIdList = "";
                foreach (DataRow row in dt.Rows)
                   StudentIdList+=","+ row["StudentId"].ToString();
                StudentIdList = StudentIdList.Remove(0, 1);

                sqlDB.fillDataTable("with ms as(select * from  " + getTable + " where exinid='" + ddlExamId.SelectedValue + "' and StudentId in(" + StudentIdList + ") ) "
                + "SELECT Distinct ms.ExInId,ms.StudentId,CurrentStudentInfo.FullName,ms.RollNo,ms.BatchId,ms.ClsSecId,ms.ShiftId,"
                +"ms.SubQPId,SubjectQuestionPattern.SubId,SubjectQuestionPattern.CourseId,SubjectQuestionPattern.IsOptional,v_ClassSujectListForResultProcess.SubName,"
                +" QuestionPattern.QPId,QuestionPattern.QPName,SubjectQuestionPattern.QMarks,ms.ConvertToPercentage,ms.Marks,"
                +"ms.ClsGrpID,SubjectQuestionPattern.SubQPMarks,SubjectQuestionPattern.PassMarks,v_ClassSujectListForResultProcess.IsOptional,v_ClassSujectListForResultProcess.BothType,v_ClassSujectListForResultProcess.IsCommon"
                +" FROM CurrentStudentInfo INNER JOIN ms ON CurrentStudentInfo.StudentId = ms.StudentId INNER JOIN ExamInfo ON "
                +"ms.ExInId = ExamInfo.ExInId INNER JOIN SubjectQuestionPattern ON ms.SubQPId = SubjectQuestionPattern.SubQPId INNER JOIN v_ClassSujectListForResultProcess "
                +"ON SubjectQuestionPattern.SubId = v_ClassSujectListForResultProcess.SubId INNER JOIN QuestionPattern ON SubjectQuestionPattern.QPId = QuestionPattern.QPId AND "
                +" v_ClassSujectListForResultProcess.ClassId=(select ClassId from BatchInfo where BatchId="+ddlBatch.SelectedItem.Value+") Order By "
                +"ms.RollNo ASC,QuestionPattern.QPId  ", dtCreateViewForClassMarksheet);

                //----------------------------For Ordering Synchronizing------------------------------------------------------------------
                Ordering = new DataColumn("Ordering", typeof(int));
                Ordering.DefaultValue = 0;
                dtCreateViewForClassMarksheet.Columns.Add(Ordering);


                for (int  b = 0; b < dtSQPInfo.Rows.Count; b++)
                {
                    DataRow[] dr = dtClassSubOrder.Select("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString().Trim() + " AND CourseId=" + dtSQPInfo.Rows[b]["CourseId"].ToString().Trim() + " ", null);

                    dtCreateViewForClassMarksheet.Select(string.Format("SubId=" + dtSQPInfo.Rows[b]["SubId"].ToString()) + "and CourseId=" + dtSQPInfo.Rows[b]["CourseId"].ToString() + "").ToList<DataRow>().ForEach(r => r["Ordering"] = int.Parse(dr[0]["Ordering"].ToString()));
                }

                dv = dtCreateViewForClassMarksheet.DefaultView;
                dv.Sort = "Ordering asc";
                dtCreateViewForClassMarksheet = dv.ToTable();

                ViewState["__getMarkSheetView__"] = dtCreateViewForClassMarksheet;
                //-------------------------------------------- End Ordering Synchronizing ---------------------------------------------------------------------


                
                for (int i = 0; i < dt.Rows.Count; i++)   // all student included in dt 
                {
                    int row = i + 1;

                    DataTable dtSubInfo = new DataTable();
                    dtSubInfo = dtCreateViewForClassMarksheet.Select("StudentId=" + dt.Rows[i]["StudentId"].ToString() + " AND ExInId='" + ddlExamId.Text.Trim() + "' ").CopyToDataTable();

                    dt_GroupSubjectList = new DataTable();
                    dt_GroupSubjectList = CRUD.ReturnTableNull("select SubId,MSStatus from v_StudentGroupSubSetupDetails where StudentId='" + dt.Rows[i]["StudentId"].ToString() + "' AND BatchId='" + ddlBatch.SelectedItem.Value + "'");
                    string SubIdRange = " in (";
                    for (int j = 0; j < dtSubInfo.Rows.Count; j++)
                    {
                        //------------------ if Any optional suject are exists then execute this block-----------------------------------------------
                        if (dt_GroupSubjectList.Rows.Count > 0)
                        {
                            //---------------------------For Just add common subject without others type of subject----------------------------------------------------------------------------------------------
                            if ((dtSubInfo.Rows[j]["IsCommon"].ToString().Equals("True") && dtSubInfo.Rows[j]["IsOptional"].ToString().Equals("False") && dtSubInfo.Rows[j]["BothType"].ToString().Equals("False")))
                            {

                                if (j == 0)
                                {
                                    tblInfo += "<td style='font-weight: bold; font-size: 15px;'>" + dtSubInfo.Rows[j]["RollNo"].ToString() + "</td> <td><input  autocomplete='off' onchange='saveData(this)' "
                                + "type='text' style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExInId"].ToString() + ":"
                                + dtSubInfo.Rows[j]["StudentId"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString()
                                + "></td>";

                                    dt_TotalMarksheetForReport.Rows.Add(dtSubInfo.Rows[j]["RollNo"].ToString(), dtSubInfo.Rows[j]["Marks"].ToString());
                                }
                                else
                                {
                                    tblInfo += "<td><input onKeyUp='$(this).val($(this).val().replace(/[^/d]/ig, ''))'  autocomplete='off'  onchange='saveData(this)' "
                                + "type=text style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExInId"].ToString() + ":" + dtSubInfo.Rows[j]["StudentId"].
                                ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString() + "></td>";
                                    dt_TotalMarksheetForReport.Rows[i][j + 1] = dtSubInfo.Rows[j]["Marks"].ToString();
                                }

                                row += dt.Rows.Count;


                            }
                            //---------------------------------------------------------------------------------------------------------------------
                            else
                            {
                                //---------------when any subject are not exists then it will be return null.that means this subject is not occopyed for this student


                                DataRow[] dr = dt_GroupSubjectList.Select("subId=" + dtSubInfo.Rows[j]["SubId"].ToString() + "");

                                if (dr.Length > 0)
                                {
                                    if (j == 0)
                                    {
                                        tblInfo += "<td style='font-weight: bold; font-size: 15px;'>" + dtSubInfo.Rows[j]["RollNo"].ToString() + "</td> <td><input autocomplete='off' onchange='saveData(this)' "
                                    + "type='text' style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExInId"].ToString() + ":"
                                    + dtSubInfo.Rows[j]["StudentId"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString()
                                    + "></td>";
                                        dt_TotalMarksheetForReport.Rows.Add(dtSubInfo.Rows[j]["RollNo"].ToString(), dtSubInfo.Rows[j]["Marks"].ToString());
                                    }
                                    else
                                    {
                                        tblInfo += "<td><input onKeyUp='$(this).val($(this).val().replace(/[^/d]/ig, ''))'  autocomplete='off'  onchange='saveData(this)' "
                                    + "type=text style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExInId"].ToString() + ":" + dtSubInfo.Rows[j]["StudentId"].
                                    ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString() + "></td>";
                                    
                                     dt_TotalMarksheetForReport.Rows[i][j + 1] = dtSubInfo.Rows[j]["Marks"].ToString();
                                    }

                                    row += dt.Rows.Count;
                                    HasSingleOptSub = true;
                                }
                                else
                                {
                                    if (j == 0)
                                    {
                                        tblInfo += "<td style='font-weight: bold; font-size: 15px;'>" + dtSubInfo.Rows[j]["RollNo"].ToString() + "</td> <td><input disabled='disabled' autocomplete='off' onchange='saveData(this)' "
                                    + "type='text' style='text-align: center; background-color:#A6A6A6; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExInId"].ToString() + ":"
                                    + dtSubInfo.Rows[j]["StudentId"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString()
                                    + "></td>";

                                        dt_TotalMarksheetForReport.Rows.Add(dtSubInfo.Rows[j]["RollNo"].ToString(), dtSubInfo.Rows[j]["Marks"].ToString());
                                    }
                                    else
                                    {
                                        tblInfo += "<td><input disabled='disabled' onKeyUp='$(this).val($(this).val().replace(/[^/d]/ig, ''))'  autocomplete='off'  onchange='saveData(this)' "
                                    + "type=text style='text-align: center;background-color:#A6A6A6; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExInId"].ToString() + ":" + dtSubInfo.Rows[j]["StudentId"].
                                    ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString() + "></td>";
                                       
                                        SubIdRange += dtSubInfo.Rows[j]["SubId"].ToString() + ",";
                                        dt_TotalMarksheetForReport.Rows[i][j + 1] = dtSubInfo.Rows[j]["Marks"].ToString();
                                    }
                                    row += dt.Rows.Count;
                                    HasMultipleOptSub = true;
                                }
                            }
                        }
                        //----------------------------------------------------End Blocks--------------------------------------------------------------
                        else
                        {
                            if (j == 0)
                            {
                                tblInfo += "<td style='font-weight: bold; font-size: 15px;'>" + dtSubInfo.Rows[j]["RollNo"].ToString() + "</td> <td><input  autocomplete='off' onchange='saveData(this)' "
                               + "type='text' style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExInId"].ToString() + ":"
                               + dtSubInfo.Rows[j]["StudentId"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString()
                               + "></td>";
                                dt_TotalMarksheetForReport.Rows.Add(dtSubInfo.Rows[j]["RollNo"].ToString(), dtSubInfo.Rows[j]["Marks"].ToString());
                            }
                            else
                            {
                                tblInfo += "<td><input onKeyUp='$(this).val($(this).val().replace(/[^/d]/ig, ''))'  autocomplete='off'  onchange='saveData(this)' "
                            + "type=text style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + " id='" + getTable + ":Marks:" + dtSubInfo.Rows[j]["ExInId"].ToString() + ":" + dtSubInfo.Rows[j]["StudentId"].
                            ToString() + ":" + dtSubInfo.Rows[j]["SubQPId"].ToString() + ":" + dtSubInfo.Rows[j]["QMarks"].ToString() + ":" + dtSubInfo.Rows[j]["ConvertToPercentage"].ToString() + ":" + dtSubInfo.Rows[j]["SubQPMarks"].ToString() + ":" + dtSubInfo.Rows[j]["SubId"].ToString() + ":" + dtSubInfo.Rows[j]["CourseId"].ToString() + ":" + dtSubInfo.Rows[j]["PassMarks"].ToString() + "' value=" + dtSubInfo.Rows[j]["Marks"].ToString() + "></td>";
                            

                                dt_TotalMarksheetForReport.Rows[i][j+1] = dtSubInfo.Rows[j]["Marks"].ToString();
                            }

                            row += dt.Rows.Count;
                        }

                        if (j == dtSubInfo.Rows.Count - 1) tblInfo += "</tr>";
                    }


                    if (dt_GroupSubjectList.Rows.Count>0)
                    {
                        if (HasSingleOptSub && HasMultipleOptSub)
                        {
                            SubIdRange = SubIdRange.Substring(0, SubIdRange.LastIndexOf(',')) + ")";
                            cmd = new SqlCommand("update " + getTable + " set Marks='0' where ExInId='" + ddlExamId.SelectedItem.Text + "' AND StudentId=" + dt.Rows[i]["StudentId"].ToString() + " AND SubId " + SubIdRange + "", DbConnection.Connection);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                tblInfo += "</table>";
                divMarksheet.Controls.Add(new LiteralControl(tblInfo));

                Session["__MarkSheet__"] = dt_TotalMarksheetForReport;
                string[] ExamId=ddlExamId.SelectedItem.Text.Split('_');
                string[] Year=ExamId[1].Split('-');
                Session["__lblExamName__"] = ExamId[0] + "-" + Year[2];
                Session["__lblShift__"] = ddlShift.SelectedItem.Text;
                Session["__lblBatch__"] = ddlBatch.SelectedItem.Text;
                Session["__lblSection__"] = ddlSectionName.SelectedItem.Text;
                btnPrintPreview.Visible = true;
                //------------------------------------------------------------------------------------------------------
            }
            catch { Session["__MarkSheet__"] = ""; }
        }

      

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            MarkSheetTitle.Visible = true;
            divMarksheet.Visible = true;
            //Panel1.Visible = true;
            loadMarksEntrySheet();
            chkForCoutAsFinalResult.Checked = false;
        }

        protected void btnPreviewMarksheet_Click(object sender, EventArgs e)
        {
            //Panel1.Visible = true;
            createSubjectWiseMarkList();
        }
        private void createSubjectWiseMarkList()
        {
            try
            {
                if (class_classWiseMarkSheet_Entry == null)
                {
                    class_classWiseMarkSheet_Entry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                }
                DataTable dt = new DataTable();
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = class_classWiseMarkSheet_Entry.GETSUBJECTWISEMARKSLIST(getClass,ddlShift.SelectedValue,
                    ddlBatch.SelectedValue,ddlGroup.SelectedValue,ddlSectionName.SelectedValue,ddlExamId.
                    SelectedItem.Text,ddlsubjectName.SelectedValue,ddlCourse.SelectedValue);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        Session["__SubjectWiseMarks__"] = dt;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me",
                            "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=SubjectWiseMarks');", true);  //Open New Tab for Sever side code      
                    }
                    else
                    {
                        lblMessage.InnerText = "warning->Please Result Process";
                    }
                }
                else
                {
                    lblMessage.InnerText = "warning->Please Result Process";
                }

            }
            catch { }
        }
        protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSectionName.Items.Clear();
            ddlCourse.Items.Clear();
            LoadSubject();
            BatchEntry.loadGroupByBatchId(ddlGroup, ddlBatch.SelectedValue.ToString());

            if (ddlGroup.Items.Count == 1)
            {
                ddlGroup.Enabled = false;
                ClassSectionEntry.GetSectionListByBatchId(ddlSectionName, ddlBatch.SelectedValue.ToString());
            }
            else
            {
                ddlGroup.Enabled = true;

            }

            //ExamInfoEntry.GetExamIdList(ddlExamId, ddlBatch.SelectedValue.ToString(),true);
            ExamInfoEntry.GetExamIdList(ddlExamId, ddlBatch.SelectedValue.ToString());
           
        }

        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassSectionEntry.GetSectionListByBatchId_ClsGrpId(ddlSectionName, ddlBatch.SelectedValue.ToString(), ddlGroup.SelectedItem.Value);
        }

        private void LoadSubject()
        {
            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = new DataTable();
                dt = CRUD.ReturnTableNull("Select DISTINCT SubId,SubName,Ordering From V_ClasswiseSubject where ClassName='" + getClass + "' order by Ordering");
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
       
        protected void btnDetailsMarks_Click(object sender, EventArgs e)
        {
            //Panel1.Visible = true;
            createDetailsMarkList();
        }

        private void createDetailsMarkList()
        {

            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                dt = new DataTable();
                if (class_classWiseMarkSheet_Entry == null)
                {
                    class_classWiseMarkSheet_Entry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                }
                dt = class_classWiseMarkSheet_Entry.GetExamResultDetails(getClass,ddlShift.SelectedValue,
                    ddlBatch.SelectedValue,ddlGroup.SelectedValue,ddlSectionName.SelectedValue,
                    ddlExamId.SelectedValue);
                if (dt == null)
                {
                    lblMessage.InnerText = "warning->Exam Result Not Found";
                    return;
                }
                if (dt.Rows.Count > 0)
                {
                    Session["__ExamResultDetails__"] = dt;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(),
                    "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=ExamResultDetails');", true);  //Open New Tab for Sever side code
                }
                else
                {
                    lblMessage.InnerText = "warning->Exam Result Not Found";                   
                }
            }

            catch { }
        }

        protected void btnTotalResultProcess_Click(object sender, EventArgs e)
        {
            Panel1.Visible = true;
            TotalResultProcessing();
            GetFinalResultReport();
          
        }
        
        DataTable dtCurrentSutdentList;
        private void TotalResultProcessing()
        {
            try
            {
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                string getTable = "Class_" + getClass + "MarksSheet";

                Check_And_DeleteExistingResultByExamId(getTable);

                // Load all sutdent of the selected batch------------
                if (ddlGroup.Enabled == false)
                    SQLOperation.selectBySetCommandInDatatable("select StudentId,RollNo from CurrentStudentInfo where BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId="
                    + ddlSectionName.SelectedItem.Value + " AND ConfigId=" + ddlShift.SelectedItem.Value + " order by RollNo", dtCurrentSutdentList = new DataTable(), sqlDB.connection);
                else
                    SQLOperation.selectBySetCommandInDatatable("select StudentId,RollNo from CurrentStudentInfo where BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId="
                   + ddlSectionName.SelectedItem.Value + " AND ConfigId=" + ddlShift.SelectedItem.Value + " AND ClsGrpId=" + ddlGroup.SelectedItem.Value + " order by RollNo", dtCurrentSutdentList = new DataTable(), sqlDB.connection);
                
                //--------------------------------------------------
             
                // now check and load all dependency exam by parent examid
                DataTable dtDependencyExamList = new DataTable();
                sqlDB.fillDataTable("select DependencyIExamId,ExId from v_ExamDependencyInfo where ParentExInId='" + ddlExamId.SelectedItem.Value + "'", dtDependencyExamList);
                ViewState["__ExId__"] = "";
                ViewState["__ExId__"] = null;
                for (byte b = 0;b< dtDependencyExamList.Rows.Count;b++)
                {
                    if(b==0)
                    ViewState["__ExId__"] += dtDependencyExamList.Rows[0]["ExId"].ToString()+",";
                    else
                    {
                        SQLOperation.selectBySetCommandInDatatable("select ExId From ExamInfo where ExInId='" + dtDependencyExamList.Rows[b]["DependencyIExamId"].ToString() + "'", dt = new DataTable(), DbConnection.Connection);

                        ViewState["__ExId__"] += dt.Rows[0]["ExId"].ToString() + ",";
                    }

                }

                if (ViewState["__ExId__"] != null)   // ViewState["__ExId__"] != null means has dependency exam 
                {
                    ViewState["__ExId__"] = ViewState["__ExId__"].ToString().Substring(0, ViewState["__ExId__"].ToString().LastIndexOf(','));
                    SQLOperation.selectBySetCommandInDatatable("select distinct ConvertTo,ExId from SubjectQuestionPattern where BatchId=" + ddlBatch.SelectedValue + " and ClsGrpID=" + ddlGroup.SelectedValue + " AND ExId in (" + ViewState["__ExId__"].ToString() + ")", dt = new DataTable(), DbConnection.Connection);
                    object SubQPTotal = dt.Compute("sum(ConvertTo)", null);
                    ViewState["__SubQPMarks__"]=SubQPTotal.ToString();
                }

                ResultProcessing_AsFinalOrOthersTypeExam(dtDependencyExamList, getTable);
                if (ViewState["__HasEror__"].ToString()=="False") GetSubList();        //Load Subject in DropdownList
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        
        }

       
       
        private void ResultProcessing_AsFinalOrOthersTypeExam(DataTable dtDependencyExamList,string getTable)
        {
            try
            {
                ViewState["__HasEror__"] = "False";
                // here subject list is set by subject question pattern list of the batcha;
                DataTable dtGetSubjectList  = new DataTable();
                DataTable dtTempExamSubInfo=new DataTable ();
                DataTable dtCommonSub = new DataTable();
                
                DataTable dtHasExistsOptionalOrBothTypeSubject = new DataTable();
                if (!ddlGroup.Enabled)
                {
                    sqlDB.fillDataTable("select Distinct SubId,CourseId,QPId from SubjectQuestionPattern where BatchId='" + ddlBatch.SelectedItem.Value + "' order by SubId", dtGetSubjectList);
                    sqlDB.fillDataTable("select Distinct SubId From ClassSubject Where ClassId=(select ClassId from BatchInfo where BatchId=" + ddlBatch.SelectedValue + ") AND IsCommon='True' and IsOptional='False' and BothType='False' order by SubId ", dtCommonSub);

                    sqlDB.fillDataTable("select Distinct SubId From ClassSubject Where ClassId=(select ClassId from BatchInfo where BatchId=" + ddlBatch.SelectedValue + ") AND  (IsOptional='True' OR BothType='True') order by SubId", dtHasExistsOptionalOrBothTypeSubject);
               
                }
                else
                {
                    sqlDB.fillDataTable("select Distinct SubId,CourseId,QPId from SubjectQuestionPattern where BatchId='" + ddlBatch.SelectedItem.Value + "' AND ClsGrpID=" + ddlGroup.SelectedValue + " order by SubId", dtGetSubjectList);
                    
                    sqlDB.fillDataTable("select Distinct SubId from ClassSubject where ClassID=(select ClassId from BatchInfo where BatchId="+ddlBatch.SelectedValue+") and  "+
                        " Groupid in(0,(select GroupId from Tbl_Class_Group where ClsGrpId =" + ddlGroup.SelectedValue + " ))  and IsCommon='True' and IsOptional='False' and BothType='False' order by SubId", dtCommonSub);

                    sqlDB.fillDataTable("select Distinct SubId from ClassSubject where ClassID=(select ClassId from BatchInfo where BatchId=" + ddlBatch.SelectedValue + ") and " +
                        " Groupid in(0,(select GroupId from Tbl_Class_Group where ClsGrpId =" + ddlGroup.SelectedValue + " ))  AND (IsOptional='True' OR BothType='True') order by SubId", dtHasExistsOptionalOrBothTypeSubject);
                }

               

                StringBuilder sb = new StringBuilder(" In ("); 
                for (byte x=0;x<dtCommonSub.Rows.Count;x++)
                {
                    sb.Append(dtCommonSub.Rows[x]["SubId"].ToString()+",").ToString();

                }
                //-----Common subject means which subject are same for all student in a class.e.g. Bangla,English,Math etc------------------
                if (dtCommonSub.Rows.Count > 0)
                {
                    ViewState["__SubIdRange__"] = sb.ToString().Substring(0, sb.ToString().LastIndexOf(',')) + ")";
                    dtTempExamSubInfo = dtGetSubjectList.Copy();
                    DataRow[] dr = dtTempExamSubInfo.Select("SubId " + ViewState["__SubIdRange__"].ToString() + "", "SubId");
                    dtGetSubjectList.Rows.Clear();
                    for (byte y=0;y<dr.Length;y++)
                    {
                        dtGetSubjectList.Rows.Add(int.Parse(dr[y]["SubId"].ToString()), int.Parse(dr[y]["CourseId"].ToString()), int.Parse(dr[y]["QPId"].ToString()));
                    }
                }

                //-------------------------Common subject count finish------------------------------------------------------------------------
                

               

                //get dependency subject list of selected batch--------------------------

                string ClsGrpID = (ddlGroup.Enabled) ? ddlGroup.SelectedItem.Value : "0";
                DataTable dtDependencySubList = new DataTable();
                sqlDB.fillDataTable("select distinct SubId,Count(distinct CourseId) as CourseAmount  from SubjectQuestionPattern where  CourseId !=0  and BatchId=" + ddlBatch.SelectedItem.Value + " AND ExId=(select top(1) ExId from SubjectQuestionPattern where QPId =" + dtGetSubjectList.Rows[0]["QPId"].ToString() + " AND BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsGrpID=" + ClsGrpID + ") group by SubId", dtDependencySubList);

                // add new column in dependencysublist datatable
                System.Data.DataColumn DepSubPassMarks = new DataColumn("DepSubPassMarks",typeof(float));
                DepSubPassMarks.DefaultValue = 0;
                dtDependencySubList.Columns.Add(DepSubPassMarks);

                // load dependency subjectpassmarks
                DataTable dtDependencySubjectPassMarksList = new DataTable();
                sqlDB.fillDataTable("select SubId,PassMarks from Class_DependencyPassMarks where ClassID=(select ClassId from BatchInfo where BatchId=" + ddlBatch.SelectedItem.Value + ") AND SubId in (select distinct SubId from SubjectQuestionPattern where  CourseId !=0  and BatchId=" + ddlBatch.SelectedItem.Value + " AND ExId=(select top(1) ExId from SubjectQuestionPattern where QPId =" + dtGetSubjectList.Rows[0]["QPId"].ToString() + " AND BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsGrpID=" + ClsGrpID + " order by subId)) order by subId ", dtDependencySubjectPassMarksList);
                

                for (byte b = 0; b < dtDependencySubList.Rows.Count; b++)
                {
                    if (dtDependencySubjectPassMarksList.Rows.Count == 0)
                    {
                        lblMessage.InnerText = "error->Please define pass marks of all dependency subject."; ViewState["__HasEror__"] = "True"; return;
                    }
                        
                    object getPassMarks = dtDependencySubjectPassMarksList.Compute(" sum (PassMarks)", "subId =" + dtDependencySubjectPassMarksList.Rows[b]["SubId"].ToString() + "");

                    DataRow dr = dtDependencySubList.Select("subId=" + dtDependencySubList.Rows[b]["SubId"].ToString() + "").FirstOrDefault();
                    dr["DepSubPassMarks"] = float.Parse(getPassMarks.ToString());
                }

                    //-----------------------------------------------------------------------

                    loadGrade();  // for load grade info 
                
                DataTable dtSubjectList = new DataTable();
                dtSubjectList.Columns.Add("SubId", typeof(int));
                dtSubjectList.Columns.Add("CourseId", typeof(int));
                dtSubjectList.Columns.Add("PatternId", typeof(int));
                dtSubjectList.Columns.Add("Marks", typeof(float));
                dtSubjectList.Columns.Add("MarksOfAllPatternSCId", typeof(float));
                dtSubjectList.Columns.Add("GradeOfAllPatternSCID", typeof(string));
                dtSubjectList.Columns.Add("PointOfAllPatternSCId", typeof(float));
                dtSubjectList.Columns.Add("MarksOfSubjectWithAllDependencySubject", typeof(float));
                dtSubjectList.Columns.Add("GradeOfSubjectWithAllDependencySubject", typeof(string));
                dtSubjectList.Columns.Add("PointOfSubjectWithAllDependencySubject", typeof(float));
                dtSubjectList.Columns.Add("SubQPMarks",typeof(float));
                dtSubjectList.Columns.Add("IsPassed", typeof(string));

                // now set subject course pattern id-------------------------------------------------------------
                for (byte r = 0; r < dtGetSubjectList.Rows.Count; r++)
                {
                    try
                    {
                        dtSubjectList.Rows.Add(dtGetSubjectList.Rows[r]["SubId"].ToString(), dtGetSubjectList.Rows[r]["CourseId"].ToString(), dtGetSubjectList.Rows[r]["QPId"].ToString());
                    }
                    catch (Exception ex)
                    {
                        lblMessage.InnerText = "error->"+ex.Message;
                    }
              
                }
                //------------------------------------------------------------------------------------------------
               
                
                // loop start for executing result by individual student-------------------------------------------

                DataView dvs = dtSubjectList.DefaultView; // for store common subject list
                for (int i = 0; i < dtCurrentSutdentList.Rows.Count; i++)
                {
                    dtSubjectList = dvs.ToTable();
                    // If Any optional subject or group mandatory subject are exists then below section are executed
                    if (dtHasExistsOptionalOrBothTypeSubject.Rows.Count > 0)
                    {
                        
                        DataTable dtStudentGroupSubList = new DataTable();
                        sqlDB.fillDataTable("select SubId from v_StudentGroupSubSetupDetails where StudentId=" + dtCurrentSutdentList.Rows[i]["StudentId"].ToString() + "", dtStudentGroupSubList);

                        for (byte x = 0; x < dtStudentGroupSubList.Rows.Count; x++)
                        {
                            DataRow[] dr = dtTempExamSubInfo.Select("SubId = " + dtStudentGroupSubList.Rows[x]["SubId"].ToString() + "", null);
                            
                            for (byte m=0;m<dr.Length;m++)
                            {
                                dtSubjectList.Rows.Add(dr[m]["SubId"].ToString(), dr[m]["CourseId"].ToString(), dr[m]["QPId"]);
                            }
                        
                        }

                        DataView dv = dtSubjectList.DefaultView;
                        dv.Sort = "SubId asc";
                        dtSubjectList = dv.ToTable();
                    }

                    
                    DataTable dt_GetAllExamMarksSheet = new DataTable();
                  

                    // if any dependency exam are not exists for selected examid then (if) block are executed,otherwise (else) block will be execute.
                    if (chkForCoutAsFinalResult.Checked && chkForCoutAsFinalResult.Visible)
                    {
                        sqlDB.fillDataTable(" select cms.Marks,cms.ConvertMarks,sbp.SubId,sbp.CourseId,sbp.QPId,sbp.SubQPMarks,cms.IsPassed,sbp.PassMarks,cms.ExInId,cms.TotalConvertMarksOfSub  from " + getTable + " as cms inner join SubjectQuestionPattern as  sbp on cms.SubQPId=sbp.SubQPId AND  cms.StudentId=" + dtCurrentSutdentList.Rows[i]["StudentId"].ToString() + " AND cms.ExInId='" + ddlExamId.SelectedItem.Value + "' Order by sbp.SubId,sbp.CourseId", dt_GetAllExamMarksSheet);
                    }
                    else // here else block means dependency exam are exists
                    {
                        // for load every dependency exam marks by examid.when Dependency are exists for selected examid then this loop block are started
                        for (byte j = 0; j < dtDependencyExamList.Rows.Count; j++)
                        {

                            string ExInId = (j == 0) ? ddlExamId.SelectedItem.ToString() : dtDependencyExamList.Rows[j]["DependencyIExamId"].ToString();
                            sqlDB.fillDataTable(" select cms.Marks,cms.ConvertMarks,sbp.SubId,sbp.CourseId,sbp.QPId,sbp.SubQPMarks,cms.IsPassed,sbp.PassMarks,cms.ExInId,cms.TotalConvertMarksOfSub  from " + getTable + " as cms inner join SubjectQuestionPattern as  sbp on cms.SubQPId=sbp.SubQPId AND  cms.StudentId=" + dtCurrentSutdentList.Rows[i]["StudentId"].ToString() + " AND cms.ExInId='" + ExInId + "' Order by sbp.SubId,sbp.CourseId ", dt_GetAllExamMarksSheet);
                        }
                        //-------------------------------------------------------------------------------------------------
                    }


                    //DataView dv2 = dt_GetAllExamMarksSheet.DefaultView;
                    //dv2.Sort = "SubId asc";
                    //dtSubjectList = dv2.ToTable();


                   // to get all marks of subject by querstion pattern

                    for (byte b = 0; b < dtSubjectList.Rows.Count; b++)
                    {
                        object TotalMarksOfSubject_ByPattern;

                        // dependency rows 0 means dependency not exists,then marks are counted as single exam as final exam.not convertMarks.Convert marks are counted for dependency exam.
                        if (dtDependencyExamList.Rows.Count > 0)
                        {
                            TotalMarksOfSubject_ByPattern = dt_GetAllExamMarksSheet.Compute("sum (ConvertMarks)", "SubId=" + dtSubjectList.Rows[b]["SubId"].ToString() + " AND CourseId=" + dtSubjectList.Rows[b]["CourseId"].ToString() + " AND QPId=" + dtSubjectList.Rows[b]["PatternId"].ToString() + "");
                            object SubQPPassMarks = dt_GetAllExamMarksSheet.Compute("sum (PassMarks)", "SubId=" + dtSubjectList.Rows[b]["SubId"].ToString() + " AND CourseId=" + dtSubjectList.Rows[b]["CourseId"].ToString() + " AND QPId=" + dtSubjectList.Rows[b]["PatternId"].ToString() + " AND ExInId='" + ddlExamId.SelectedValue + "' ");
                            string IsPassed = (float.Parse(TotalMarksOfSubject_ByPattern.ToString()) >= float.Parse(SubQPPassMarks.ToString())) ? "True" : "False";
                            dtSubjectList.Rows[b]["Marks"] = float.Parse(TotalMarksOfSubject_ByPattern.ToString());
                            dtSubjectList.Rows[b]["SubQPMarks"] = SubQPPassMarks;     // here entered SubQPMarks as Subject Question Pattern Pass Marks
                            dtSubjectList.Rows[b]["IsPassed"] = IsPassed;
                        }
                        else
                        {
                            TotalMarksOfSubject_ByPattern = dt_GetAllExamMarksSheet.Compute("sum (Marks)", "SubId=" + dtSubjectList.Rows[b]["SubId"].ToString() + " AND CourseId=" + dtSubjectList.Rows[b]["CourseId"].ToString() + " AND QPId=" + dtSubjectList.Rows[b]["PatternId"].ToString() + "");

                            dtSubjectList.Rows[b]["Marks"] = float.Parse(TotalMarksOfSubject_ByPattern.ToString());
                            dtSubjectList.Rows[b]["SubQPMarks"] = dt_GetAllExamMarksSheet.Rows[b]["SubQPMarks"].ToString();  // here entered actual Sub QPMarks
                            dtSubjectList.Rows[b]["IsPassed"] = dt_GetAllExamMarksSheet.Rows[b]["IsPassed"].ToString();
                        }
                    }
                    // here calculate marks as MarksOfAllPatternSCId
                    byte r=0;
                    while (r < dtSubjectList.Rows.Count)
                    {

                        object TotalMarksOfSubject_ByCourse = dtSubjectList.Compute("sum (Marks)", "SubId=" + dtSubjectList.Rows[r]["SubId"].ToString() + " AND CourseId=" + dtSubjectList.Rows[r]["CourseId"].ToString() + "");
                        DataRow dr = dtSubjectList.Select("SubId=" + dtSubjectList.Rows[r]["SubId"].ToString() + " AND CourseId=" + dtSubjectList.Rows[r]["CourseId"].ToString() + "").FirstOrDefault();


                        dr["MarksOfAllPatternSCId"] = float.Parse(TotalMarksOfSubject_ByCourse.ToString());
                        r++;
                    }

                    // get total marks for dependency subject 
                    for (r = 0; r < dtDependencySubList.Rows.Count; r++)
                    {
                        object TotalMarks_BySubject = dtSubjectList.Compute("sum (MarksOfAllPatternSCId)","SubId="+dtDependencySubList.Rows[r]["SubId"].ToString()+"");

                        DataRow dr = dtSubjectList.Select("SubId=" + dtDependencySubList.Rows[r]["SubId"].ToString() + "").FirstOrDefault();

                        TotalMarks_BySubject = Math.Round(double.Parse(TotalMarks_BySubject.ToString()) / int.Parse(dtDependencySubList.Rows[r]["CourseAmount"].ToString()), 0).ToString();

                        dr["MarksOfSubjectWithAllDependencySubject"] = float.Parse(TotalMarks_BySubject.ToString());

                    }

                    // load optonal subjct of this student-----------
                    bool HasOptional=false ;
                    DataTable dtGetOptionalSubject = new DataTable();
                    sqlDB.fillDataTable("select SubId from v_StudentGroupSubSetupDetails where StudentId=" + dtCurrentSutdentList.Rows[i]["StudentId"].ToString() + " AND MSStatus='False'", dtGetOptionalSubject);
                    if (dtGetOptionalSubject.Rows.Count>0)HasOptional=true;
                  //-----------------------------------------------


                    DataTable TempDependencySubList=dtDependencySubList.Copy();
                    DataTable TempDtSubjectList = dtSubjectList.Copy();
                    for (r = 0; r < TempDtSubjectList.Rows.Count; r++)
                    {
                        bool YesOptonal = false;
                        if (dtGetOptionalSubject.Rows.Count > 0) YesOptonal = (TempDtSubjectList.Rows[r]["SubId"].ToString().Equals(dtGetOptionalSubject.Rows[0]["SubId"].ToString())) ? true : false;


                        if (TempDependencySubList.Rows.Count > 0)
                        {

                        // this section for dependency subject marks and enterd database table by checking and setting essential informations 
                            for (byte b = 0; b < TempDependencySubList.Rows.Count; b++)
                            {
                                DataRow[] dr = null;
                                dr = TempDtSubjectList.Select("subId=" + TempDependencySubList.Rows[b]["SubId"].ToString() + "", null);
                                if (ViewState["__ExId__"]!=null) // this predicate means this result processing has dependency exam list
                                    ViewState["__IsPassed_AllPattern__"]="True";
                                else
                                {
                                    DataRow [] dr2 = TempDtSubjectList.Select("subId=" + TempDependencySubList.Rows[b]["SubId"].ToString() + " AND IsPassed='False'", null);
                                    try
                                    {
                                       // if(dr2==null)
                                        ViewState["__IsPassed_AllPattern__"] = dr2[0]["IsPassed"].ToString();
                                    }
                                    catch { ViewState["__IsPassed_AllPattern__"] = "True"; }
                                }

                                if (dr != null)
                                {
                                    object DSMarks = TempDtSubjectList.Compute(" sum (MarksOfSubjectWithAllDependencySubject)", "SubId=" + TempDependencySubList.Rows[b]["SubId"].ToString() + "");
                                    object SubQPMarks="";
                                    SubQPMarks = TempDtSubjectList.Compute(" sum (SubQPMarks)", "SubId=" + TempDependencySubList.Rows[b]["SubId"].ToString() + "");
                                    SubQPMarks = Math.Round(double.Parse(SubQPMarks.ToString()) / byte.Parse(TempDependencySubList.Rows[b]["CourseAmount"].ToString()), 0);
                                    ViewState["__DpendencySubQPMarks__"] = SubQPMarks.ToString();
                                    for (byte c = 0; c < dr.Length; c++)
                                    {
                                        object TotalCourseMarks = TempDtSubjectList.Compute("sum (MarksOfAllPatternSCId)", "SubId=" + dr[c]["SubId"].ToString() + " AND CourseId=" + dr[c]["CourseId"].ToString() + "");

                                        DataRow[] DependencySUbQPMarks = TempDtSubjectList.Select("SubId=" + dr[c]["SubId"].ToString() + " AND CourseId=" + dr[c]["CourseId"].ToString() + "", null);

                                        if (ViewState["__ExId__"] == null) ViewState["__SubQPMarks__"] = DependencySUbQPMarks[0]["SubQPMarks"].ToString();
                                        
                                        // for identify every subject and subject-course isPassed status.that is must be contain true/false
                                        ViewState["__IsPassed_AllPattern__"] = DependencySUbQPMarks[0]["IsPassed"].ToString();

                                        using (Class_ClasswiseMarksheet_TotalResultProcess c_ctmp= GetData(true, int.Parse(dtCurrentSutdentList.Rows[i]["StudentId"].ToString()), int.Parse(dtCurrentSutdentList.Rows[i]["RollNo"].ToString()), int.Parse(dr[c]["SubId"].ToString()), YesOptonal,
                                                 int.Parse(dr[c]["CourseId"].ToString()), int.Parse(dr[c]["PatternId"].ToString()), float.Parse(dr[c]["Marks"].ToString()), float.Parse(DSMarks.ToString()), float.Parse(TotalCourseMarks.ToString()), int.Parse(TempDependencySubList.Rows[b]["CourseAmount"].ToString()), float.Parse(TempDependencySubList.Rows[b]["DepSubPassMarks"].ToString())))
                                        {
                                            if (class_classWiseMarkSheet_Entry == null) class_classWiseMarkSheet_Entry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                                            class_classWiseMarkSheet_Entry.SetValues=c_ctmp;
                                            class_classWiseMarkSheet_Entry.Insert(getTable+"_TotalResultProcess");
                                        }
                                    }

                                    // here delete row from Tempsubjectlist and current row index decrease -- 
                                    var rows = TempDtSubjectList.Select("SubId=" + dr[0]["SubId"].ToString() + "");
                                    foreach (var row in rows) row.Delete();
                                    r--;
                                    TempDependencySubList.Rows.RemoveAt(b);
                                    break;
                                    //-----------------------------------------------------------------------
                                }                              
                            }
                        }
                        else
                        {

                            // when any dependency are not exists then this block is activated.and last parameter value is 0
                            object MarksForSubject = TempDtSubjectList.Compute("sum (Marks)", " SubId=" + TempDtSubjectList.Rows[r]["SubId"].ToString() + "");
                            if (ViewState["__ExId__"] == null) ViewState["__SubQPMarks__"] = TempDtSubjectList.Rows[r]["SubQPMarks"].ToString();
                                
                                

                            object tempSum = TempDtSubjectList.Compute("sum(marks)"," subId=" + TempDtSubjectList.Rows[r]["SubId"].ToString() + " AND IsPassed='False'");
                            
                            ViewState["__IsPassed_AllPattern__"]=(tempSum.ToString() == "")?"True":"False";
                            using (Class_ClasswiseMarksheet_TotalResultProcess c_ctmp = GetData(false, int.Parse(dtCurrentSutdentList.Rows[i]["StudentId"].ToString()), int.Parse(dtCurrentSutdentList.Rows[i]["RollNo"].ToString()), int.Parse(TempDtSubjectList.Rows[r]["SubId"].ToString()), YesOptonal,
                                  int.Parse(TempDtSubjectList.Rows[r]["CourseId"].ToString()), int.Parse(TempDtSubjectList.Rows[r]["PatternId"].ToString()), float.Parse(TempDtSubjectList.Rows[r]["Marks"].ToString()), float.Parse(MarksForSubject.ToString()), 0,0,0))
                            {
                                if (class_classWiseMarkSheet_Entry == null) class_classWiseMarkSheet_Entry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                                class_classWiseMarkSheet_Entry.SetValues = c_ctmp;
                                class_classWiseMarkSheet_Entry.Insert(getTable + "_TotalResultProcess");
                            
                            }
                            
                        }

                        
                    
                    }

                    createFinalResultSheet(dtCurrentSutdentList.Rows[i]["StudentId"].ToString(), getTable, HasOptional);  // generate table for store all processing result by clase wise
                    if (i == dtCurrentSutdentList.Rows.Count - 1)
                    {
                        lblMessage.InnerText = "success->Successfully Result Processing Completed";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->"+ex.Message;
            }
        }

        public Class_ClasswiseMarksheet_TotalResultProcess GetData(bool HasDependency,int studentId, int RollNo, int SubId, bool IsPotional, int CourseId, int QPId, float Marks, float MarksForSubject,float MarksForOnlyDependencySubQPattern,int NoOfCourse,float DependencySubPassMarks)
        {
            Class_ClasswiseMarksheet_TotalResultProcess c_ctmp = new Class_ClasswiseMarksheet_TotalResultProcess();

            c_ctmp.ExInId = ddlExamId.SelectedItem.Value;
            c_ctmp.StudentId = studentId;
            c_ctmp.RollNo = RollNo;
            c_ctmp.BatchId =int.Parse(ddlBatch.SelectedItem.Value);
            c_ctmp.ClsSecId =int.Parse(ddlSectionName.SelectedItem.Value);
            c_ctmp.ShiftId = int.Parse(ddlShift.SelectedItem.Value);
            c_ctmp.SubId = SubId;
            c_ctmp.IsOptional = IsPotional;
            c_ctmp.CourseId = CourseId;
            c_ctmp.QPId = QPId;
            c_ctmp.Marks = Marks;
            c_ctmp.ClsGrpID =(ddlGroup.Enabled)? int.Parse(ddlGroup.SelectedItem.Value):0;
           
            // here hasDependancy Means Dependency Subject such as bangla 1st+2nd paper
            if (HasDependency)
            {
                
                string[] GP = Grade_And_Point(HasDependency, MarksForOnlyDependencySubQPattern,false,0,0).Split('|');  // GP= Grade And Point
                c_ctmp.MarksOfAllPatternBySCId = MarksForOnlyDependencySubQPattern;
                c_ctmp.GradeOfAllPatternBySCId = GP[0];
                c_ctmp.PointOfAllPatternBySCId = float.Parse(GP[1]);

                GP = Grade_And_Point(HasDependency, MarksForSubject,true,NoOfCourse,DependencySubPassMarks).Split('|');  // GP= Grade And Point .when any dependency subject are exists then it will be work

                c_ctmp.MarksOfSubject_WithAllDependencySub = MarksForSubject;
                c_ctmp.GradeOfSubject_WithAllDependencySub = GP[0];
                c_ctmp.PointOfSubject_WithAllDependencySub = float.Parse(GP[1]);
            }
            else
            {
                string[] GP = Grade_And_Point(HasDependency, MarksForSubject,false,0,0).Split('|');  // GP= Grade And Point
                c_ctmp.MarksOfAllPatternBySCId = MarksForSubject;
                c_ctmp.GradeOfAllPatternBySCId = GP[0];
                c_ctmp.PointOfAllPatternBySCId = float.Parse(GP[1]);

                c_ctmp.MarksOfSubject_WithAllDependencySub = MarksForSubject;
                c_ctmp.GradeOfSubject_WithAllDependencySub = GP[0];
                c_ctmp.PointOfSubject_WithAllDependencySub = float.Parse(GP[1]);
            
            }
           
            return c_ctmp;           
        }

        public string Grade_And_Point(bool HasDependency, float marks, bool Marks_Grade_For_DependencySubject, int NoOfCourse,float DependencySubPassMarks)
        {
            try
            {
                // convert every marks in hundred percentage
                double PercentageMarks=0;
                bool Has_SubjectDependency = false;
                //----Frist Time Check of the dependency subject marks for count pass or fale-------------
                if (Marks_Grade_For_DependencySubject)
                {
                    if ((marks * NoOfCourse) < DependencySubPassMarks)
                    {
                        return "F" + "|" + "0.00";
                        
                    }
                  //  ViewState["__SubQPMarks__"] = ViewState["__DpendencySubQPMarks__"].ToString();
                    marks *= NoOfCourse;
                  //  Has_SubjectDependency = true;

                    marks = (int)marks/NoOfCourse;
                    Has_SubjectDependency = true;
                }
                //-------------------------------------------------------------------------------------------

                if (ViewState["__ExId__"] != null)  //  if any dependency exam are exists then must be this section will be execute
                {
                    if (ViewState["__IsPassed_AllPattern__"].ToString() == "False") return "F" + "|" + "0.00";

                    PercentageMarks = Math.Round(marks * 100 / double.Parse(ViewState["__SubQPMarks__"].ToString()), 0);
                }
                else if(Has_SubjectDependency)  // if any dependency exam are are exists and sub has dependency then this section will be execute
                {
                    if ((marks * NoOfCourse) < DependencySubPassMarks)
                    {
                        if (ViewState["__IsPassed_AllPattern__"].ToString() == "False") return "F" + "|" + "0.00";
                    }
                        
                    PercentageMarks = Math.Round(marks*NoOfCourse * 100 / (double.Parse(ViewState["__SubQPMarks__"].ToString())*NoOfCourse), 0);
                }
                else
                {
                    if (ViewState["__IsPassed_AllPattern__"].ToString() == "False") return "F" + "|" + "0.00";

                    PercentageMarks = Math.Round(marks * 100 / double.Parse(ViewState["__SubQPMarks__"].ToString()), 0);
                }
                
 
                for (byte b = 0; b < dtG.Rows.Count; b++)
                {
                    if (Math.Floor(PercentageMarks) >= double.Parse(dtG.Rows[b]["GMarkMin"].ToString()) && Math.Floor(PercentageMarks) <= double.Parse(dtG.Rows[b]["GMarkMax"].ToString()))
                    {
                        return  dtG.Rows[b]["GName"].ToString() + "|" + dtG.Rows[b]["GPointMin"].ToString();
                   
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->"+ex.Message;
                return null;
            }
        }


        DataTable dtG;
        private void loadGrade()  // for load grade info 
        {
            // here load grade list -----------------------------------------------------
            dtG = new DataTable();
            sqlDB.fillDataTable("select * from  Grading ", dtG);
            //---------------------------------------------------------------------------
                
        }

        
        private void Check_And_DeleteExistingResultByExamId(string getTableName)
        {
            try
            {
                // for delete result from Class wise marksheet (Class_ClassMarksSheet_TotalResultProcess).table name structure
                string sql;
                if (ddlGroup.Enabled == false)
                sql = "delete from " + getTableName + "_TotalResultProcess where BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId="
                    + ddlSectionName.SelectedItem.Value + " AND ShiftId=" + ddlShift.SelectedItem.Value + " AND ExInId='" + ddlExamId.SelectedItem.Text + "'";
                else
                    sql = "delete from " + getTableName + "_TotalResultProcess where BatchId=" + ddlBatch.SelectedItem.Value + " AND ClsSecId="
                    + ddlSectionName.SelectedItem.Value + " AND ShiftId=" + ddlShift.SelectedItem.Value + " AND ExInId='" + ddlExamId.SelectedItem.Text + "' AND ClsGrpID=" + ddlGroup.SelectedItem.Value + "";
               
                CRUD.ExecuteQuery(sql);

                int ClsGrpID = (ddlGroup.Enabled) ? int.Parse(ddlGroup.SelectedItem.Value) : 0;
                sql = "delete from Exam_Final_Result_Stock_Of_All_Batch where ExInId='" + ddlExamId.SelectedItem.Text + "' AND BatchId=" + ddlBatch.SelectedItem.Value + " AND ShiftId=" + ddlShift.SelectedItem.Value.ToString() + " AND ClsSecId=" + ddlSectionName.SelectedItem.Value + " AND ClsGrpID=" + ClsGrpID + "";
                CRUD.ExecuteQuery(sql);

                

             
            }
            catch { }
        
        }
        
        private void createFinalResultSheet(string studentId, string getTableName,bool HasOptional)    // generate table for store all processing result by clase wise
        {
            try
            {

                // load all subject marks of selected student
                DataTable dtTotalMarkSheet=new DataTable ();
                sqlDB.fillDataTable("select distinct SubId,MarksOfSubject_WithAllDependencySub,PointOfSubject_WithAllDependencySub,IsOptional  from " + getTableName + "_TotalResultProcess where StudentId=" + studentId + " AND ExInId='" + ddlExamId.SelectedItem.ToString() + "' AND BatchId=" + ddlBatch.SelectedItem.Value + "", dtTotalMarkSheet);

                DataTable GetFailSubjectList=new DataTable ();
                try
                {
                    GetFailSubjectList = dtTotalMarkSheet.Select("PointOfSubject_WithAllDependencySub=0 AND IsOptional='False'").CopyToDataTable();
                }
                catch { }
                object getTotalMarks_WithOptionalSub = 0;
                object getTotalMarks_WithOut_OptionalSub = 0;
                string getTotalPoint_WithOptionalSub = "0.00";
                string getGrade_WithOptionalSub = "F";
                string getTotalPoint_WithOut_OptionalSub = "0.00";
                string getGrade_WithOut_OptionalSub = "F";
                object getOptionSubjetMarks = 0;

                if (GetFailSubjectList.Rows.Count == 0)
                {
                    object GetOptionalPoint;
                    if (HasOptional)
                    {
                        GetOptionalPoint = dtTotalMarkSheet.Compute("sum(PointOfSubject_WithAllDependencySub)", "IsOptional='true'");
                        if (float.Parse(GetOptionalPoint.ToString()) >= 2)
                        {
                            GetOptionalPoint = float.Parse(GetOptionalPoint.ToString()) - 2;
                            getOptionSubjetMarks = dtTotalMarkSheet.Compute("sum(MarksOfSubject_WithAllDependencySub)", "IsOptional='true'");
                            getOptionSubjetMarks = float.Parse(getOptionSubjetMarks.ToString())-40;
                        }
                        else GetOptionalPoint = 0;
                    }
                    else GetOptionalPoint = 0;

                    object GetTotalPointsOfExam = dtTotalMarkSheet.Compute("sum(PointOfSubject_WithAllDependencySub)", "IsOptional='false'");

                    DataRow[] dr = null;
                    dr = dtTotalMarkSheet.Select("IsOptional='false'");

                   

                    // if has optional subject and optionalSubPoint not equqal 0 then execute this if block
                    if (HasOptional && float.Parse(GetOptionalPoint.ToString()) != 0)
                    {
                        getTotalPoint_WithOptionalSub = Math.Round((float.Parse(GetTotalPointsOfExam.ToString()) + float.Parse(GetOptionalPoint.ToString())) / dr.Length, 2).ToString();

                        if (double.Parse(getTotalPoint_WithOptionalSub) > double.Parse(dtG.Rows[0]["GPointMax"].ToString())) getTotalPoint_WithOptionalSub=dtG.Rows[0]["GPointMax"].ToString();
                        
                        getGrade_WithOptionalSub = GetFinalResultGrade(float.Parse(getTotalPoint_WithOptionalSub));   // to get result grade for with optional subject
                    }

                    // for get total point and grade for regular subject 
                    getTotalPoint_WithOut_OptionalSub = Math.Round((float.Parse(GetTotalPointsOfExam.ToString())) / dr.Length, 2).ToString();

                    if (double.Parse(getTotalPoint_WithOut_OptionalSub) > double.Parse(dtG.Rows[0]["GPointMax"].ToString())) getTotalPoint_WithOut_OptionalSub = dtG.Rows[0]["GPointMax"].ToString();

                    getGrade_WithOut_OptionalSub = GetFinalResultGrade(float.Parse(getTotalPoint_WithOut_OptionalSub));  // to get result grade for without optional subject


                    // for get total marks of exam with optional subject and without optional subject-------
                    DataTable dt = new DataTable();
                    sqlDB.fillDataTable("select  distinct SubId,CourseId,MarksOfAllPatternBySCId,IsOptional from " + getTableName + "_TotalResultProcess where StudentId=" + studentId + " AND ExInId='" + ddlExamId.SelectedItem.ToString() + "' AND BatchId=" + ddlBatch.SelectedItem.Value + "", dt);


                   
                   // if (float.Parse(GetOptionalPoint.ToString()) != 0) getTotalMarks_WithOptionalSub = dt.Compute("sum (MarksOfAllPatternBySCId)", null);
                   //  getTotalMarks_WithOptionalSub =float.Parse(getTotalMarks_WithOptionalSub.ToString())- float.Parse(getOptionSubjetMarks.ToString());
                    
                        getTotalMarks_WithOut_OptionalSub = dt.Compute("sum (MarksOfAllPatternBySCId)", "IsOptional='False'");

                        getTotalMarks_WithOptionalSub = float.Parse(getTotalMarks_WithOut_OptionalSub.ToString()) + float.Parse(getOptionSubjetMarks.ToString());

                    //----------------------------------------------------------------------------------------
                }

                using (Exam_Final_Result_Stock_Of_All_Batch e_frsab = GetData(studentId, float.Parse(getTotalPoint_WithOptionalSub), getGrade_WithOptionalSub,
                     float.Parse(getTotalMarks_WithOptionalSub.ToString()), float.Parse(getTotalPoint_WithOut_OptionalSub), getGrade_WithOut_OptionalSub,
                     float.Parse(getTotalMarks_WithOut_OptionalSub.ToString())))
                {
                    if (exam_Final_Result_Stock_Of_All_Batch_Entry == null) exam_Final_Result_Stock_Of_All_Batch_Entry = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                    exam_Final_Result_Stock_Of_All_Batch_Entry.SetValues = e_frsab;
                    exam_Final_Result_Stock_Of_All_Batch_Entry.Insert();
                
                }
            }
            catch { }
        }

        private Exam_Final_Result_Stock_Of_All_Batch GetData(string studentId,float FinalGPA_OfExam_WithOptionalSub,
            string FinalGrade_OfExam_WithOptionalSub,float FinalTotalMarks_OfExam_WithOptionalSub,
            float FinalGPA_OfExam, string FinalGrad_OfExam, float FinalTotalMarks_OfExam)
        {
            Exam_Final_Result_Stock_Of_All_Batch e_frsab = new Exam_Final_Result_Stock_Of_All_Batch();
            e_frsab.ExInId = ddlExamId.SelectedItem.Text;
            e_frsab.StudentId = int.Parse(studentId);
            e_frsab.BatchId = int.Parse(ddlBatch.SelectedValue.ToString());
            e_frsab.ShiftId = int.Parse(ddlShift.SelectedItem.Value);
            e_frsab.ClsSecId = int.Parse(ddlSectionName.SelectedValue.ToString());
            int ClsGrpID = (ddlGroup.Enabled) ? int.Parse(ddlGroup.SelectedItem.Value) : 0;
            e_frsab.ClsGrpID = ClsGrpID;
            e_frsab.FinalGPA_OfExam_WithOptionalSub = FinalGPA_OfExam_WithOptionalSub;
            e_frsab.FinalGrade_OfExam_WithOptionalSub = FinalGrade_OfExam_WithOptionalSub;
            e_frsab.FinalTotalMarks_OfExam_WithOptionalSub=FinalTotalMarks_OfExam_WithOptionalSub;
            e_frsab.FinalGPA_OfExam=FinalGPA_OfExam;
            e_frsab.FinalGrad_OfExam = FinalGrad_OfExam;
            e_frsab.FinalTotalMarks_OfExam = FinalTotalMarks_OfExam;
            e_frsab.PublishDate = DateTime.Now;
            e_frsab.IsFinalExam=(chkForCoutAsFinalResult.Checked)?false:true;
            return e_frsab;

        }

        private string GetFinalResultGrade(float getTotalPoint)
        {
            try
            {
                for (byte g = 0; g < dtG.Rows.Count; g++)
                {

                    if (getTotalPoint >= float.Parse(dtG.Rows[g]["GPointMin"].ToString()) && getTotalPoint <= double.Parse(dtG.Rows[g]["GPointMax"].ToString()))
                       
                        return dtG.Rows[g]["GName"].ToString();                 
                }
                return null;
            }
            catch { return null; }
        }

        private void displayTotalFinalResult(string getTable) //p
        {
            try
            {
               
                    

                

            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "Warning->" + ex.Message;
            }
        }

        protected void ddlExamId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                checkDependencyExam();
                GetSubList();
                ddlCourse.Items.Clear();
            }
            catch { }
        }
        private void GetSubList()
        {
            if (class_classWiseMarkSheet_Entry == null)
            {
                class_classWiseMarkSheet_Entry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
            }
            string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
            SubList = class_classWiseMarkSheet_Entry.GetEntitiesData(getClass).FindAll
                (c=>c.ExInId==ddlExamId.SelectedItem.Text&&c.ShiftId==int.Parse(ddlShift.SelectedValue)
                &&c.ClsSecId==int.Parse(ddlSectionName.SelectedValue)&&c.BatchId==int.Parse(ddlBatch.SelectedValue));
            var DistinctItems = SubList.GroupBy(x => x.SubId).Select(y => y.First());
            ddlsubjectName.DataSource = DistinctItems.ToList();
            ddlsubjectName.DataTextField = "SubName";
            ddlsubjectName.DataValueField = "SubId";
            ddlsubjectName.DataBind();
            ddlsubjectName.Items.Insert(0,new ListItem("...Select...","0"));
        }

        private void SaveFileListRecord(string studentId,string SubQPId,string ExInId,string getMarks)  // for entered record as fail 
        {
            try
            {
                // for delete exists record as fail by exam and studentId and others 
                cmd = new SqlCommand("delete from StudentFailList where ExInId='" +ExInId+ "'AND StudentId=" 
                + studentId + " AND SubQPId=" + SubQPId + "", sqlDB.connection);
                cmd.ExecuteNonQuery();

                // for entered fail recod in table
                cmd = new SqlCommand("insert into StudentFailList (StudentId,ExInId,SubQpId,getMarks) values (" 
                +studentId+ ",'" +ExInId+ "'," +SubQPId+ "," + getMarks + ")", sqlDB.connection);
                cmd.ExecuteNonQuery();
                
            }
            catch { }

        }

        private void checkDependencyExam()
        {
            try
            {
                result = ExamDependencyInfoEntry.checkHasDependencyExam(ddlExamId.SelectedItem.Text);

                if (result)
                {
                    btnTotalResultProcess.Visible = true;
                    chkForCoutAsFinalResult.Visible = false;
                }
                else
                {
                    btnTotalResultProcess.Visible = false;
                    chkForCoutAsFinalResult.Visible = true;
                }
            }
            catch { }
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlBatch.SelectedValue == "0" || ddlExamId.SelectedValue == "0" || ddlShift.SelectedValue == "0") 
                {
                    lblMessage.InnerText = "warning-> First Search then Print"; return;
                }
                MarkSheetTitle.Visible = true;
                divMarksheet.Visible = true;
                //Panel1.Visible = true;
                loadMarksEntrySheet();
                if (Session["__MarkSheet__"]==null)
                {
                    lblMessage.InnerText = "warning-> Marks Sheet isn't available,Please Search again."; return;
                }
                else
                {
                    DataTable dt = (DataTable)Session["__MarkSheet__"];
                }
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me",
               // "goToNewTab('/Report/MarksSheet.aspx');", true);  //Open New Tab for Sever side code
                string[] Exam = ddlExamId.SelectedItem.Text.Split('_');
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=MarksEntrySheet-" + ddlShift.SelectedItem.Text + "-" + ddlBatch.SelectedItem.Text + "-" + ddlGroup.SelectedItem.Text + "-" + ddlSectionName.SelectedItem.Text + "-" +Exam[0]+ "');", true);  //Open New Tab for Sever side code    
            }
            catch { }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                

            }
            catch { }
        }

        protected void btnDeleteCurrentResult_Click(object sender, EventArgs e)
        {
            string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
            string getTable = "Class_" + getClass + "MarksSheet";
            Check_And_DeleteExistingResultByExamId(getTable);
            lblMessage.InnerText = "success->Successfully Deleted";
        }

        

        protected void btnHideSearchOptions_Click(object sender, EventArgs e)
        {
            divSearchPanel.Visible = false;
            tblOp.Visible = false;
        }

        protected void chkForCoutAsFinalResult_CheckedChanged(object sender, EventArgs e)
        {
            if (chkForCoutAsFinalResult.Checked)
                btnTotalResultProcess.Visible = true;
            else btnTotalResultProcess.Visible = false;
        }
        private void GetFinalResultReport()
        {
            try
            {
                DataTable dtgetfinalResult;
                if (exam_Final_Result_Stock_Of_All_Batch_Entry == null)
                {
                    exam_Final_Result_Stock_Of_All_Batch_Entry = new Exam_Final_Result_Stock_Of_All_Batch_Entry();
                }
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());

                dt = exam_Final_Result_Stock_Of_All_Batch_Entry.getOptionalSub(getClass,ddlBatch.SelectedValue,
                    ddlExamId.SelectedValue,ddlShift.SelectedValue,ddlGroup.SelectedValue, ddlSectionName.SelectedValue);
                if (dt != null)
                {
                    dtgetfinalResult = new DataTable();
                    dtgetfinalResult = exam_Final_Result_Stock_Of_All_Batch_Entry.getExamFinalResult
                        (getClass,ddlShift.SelectedValue, ddlBatch.SelectedValue, ddlGroup.SelectedValue,
                        ddlSectionName.SelectedValue,ddlExamId.SelectedValue);
                    string FinalResultReportName = "";
                    if (dt.Rows.Count > 0)
                    {
                        FinalResultReportName = "FinalResultwithOptional";
                    }
                    else
                    {
                        FinalResultReportName = "FinalResultwithoutOptional";                        
                    }
                    if (dtgetfinalResult.Rows.Count > 0)
                    {
                        Session["__FinalResult__"] = dtgetfinalResult;
                        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me",
                        "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=FinalResult-" + FinalResultReportName + "');", true);  //Open New Tab for Sever side code                      
                    }                   
                }                
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "Warning->" + ex.Message;               
            }
        }

        protected void ddlsubjectName_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCourseList();
        }
        private void GetCourseList()
        {
            try
            {
                if (class_classWiseMarkSheet_Entry == null)
                {
                    class_classWiseMarkSheet_Entry = new Class_ClasswiseMarksheet_TotalResultProcess_Entry();
                }
                string getClass = new String(ddlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                SubList = class_classWiseMarkSheet_Entry.GetEntitiesData(getClass).FindAll
                    (c => c.ExInId == ddlExamId.SelectedItem.Text && c.ShiftId == int.Parse(ddlShift.SelectedValue)
                    && c.ClsSecId == int.Parse(ddlSectionName.SelectedValue) &&
                    c.BatchId == int.Parse(ddlBatch.SelectedValue)
                    &&c.SubId==int.Parse(ddlsubjectName.SelectedValue));
                ddlCourse.DataSource = SubList;
                ddlCourse.DataTextField = "CourseName";
                ddlCourse.DataValueField = "CourseId";
                ddlCourse.DataBind();
                ddlCourse.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
    }
}