using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace DS.BLL.ControlPanel
{
    public class PrivilegeOperation
    {
        SqlCommand cmd;
        
        public static bool RecordIsExists(string PageNameId,string UserTypeId)
        {
            try
            {

                DataTable dt = CRUD.ReturnTableNull("select PageNameId from UserTypePageInfo where UserTypeId =" + UserTypeId + " AND PageNameId=" + PageNameId + "");

                if (dt.Rows.Count > 0) return true;
                else return false;
            }
            catch { return false; }
        }
        public static void setViewPrivilege(string PageNameId, byte Action,string UserTypeId)
        {
            SqlCommand cmd;
            if (!RecordIsExists(PageNameId,UserTypeId))
            {
                cmd = new SqlCommand("insert into UserTypePageInfo (UserTypeId,PageNameId,MenuId,ViewAction,SaveAction,UpdateAction,DeleteAction,GenerateAction) " +
                    " values(" + UserTypeId + "," + PageNameId + ",'0'," + Action + ",'0','0','0','0')", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new SqlCommand("Update UserTypePageInfo set ViewAction=" + Action + " Where UserTypeId=" + UserTypeId + " AND  PageNameId=" + PageNameId + "", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
        }

        public static void setSavePrivilege(string PageNameId, byte Action, string UserTypeId)
        {
            SqlCommand cmd;
            if (!RecordIsExists(PageNameId, UserTypeId))
            {
                cmd = new SqlCommand("insert into UserTypePageInfo (UserTypeId,PageNameId,MenuId,ViewAction,SaveAction,UpdateAction,DeleteAction,GenerateAction) " +
                    " values(" + UserTypeId + "," + PageNameId + ",'0','0','" + Action + "','0','0','0')", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new SqlCommand("Update UserTypePageInfo set SaveAction=" + Action + " Where UserTypeId=" + UserTypeId + " AND  PageNameId=" + PageNameId + "", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
        }

        public static void setUpdatePrivilege(string PageNameId, byte Action, string UserTypeId)
        {
            SqlCommand cmd;
            if (!RecordIsExists(PageNameId, UserTypeId))
            {
                cmd = new SqlCommand("insert into UserTypePageInfo (UserTypeId,PageNameId,MenuId,ViewAction,SaveAction,UpdateAction,DeleteAction,GenerateAction) " +
                    " values(" + UserTypeId + "," + PageNameId + ",'0','0','0','" + Action + "','0','0')", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new SqlCommand("Update UserTypePageInfo set UpdateAction=" + Action + " Where UserTypeId=" + UserTypeId + " AND  PageNameId=" + PageNameId + "", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
        }

        public static void setDeletePrivilege(string PageNameId, byte Action, string UserTypeId)
        {
            SqlCommand cmd;
            if (!RecordIsExists(PageNameId, UserTypeId))
            {
                cmd = new SqlCommand("insert into UserTypePageInfo (UserTypeId,PageNameId,MenuId,ViewAction,SaveAction,UpdateAction,DeleteAction,GenerateAction) " +
                    " values(" + UserTypeId + "," + PageNameId + ",'0','0','0','0','" + Action + "','0')", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new SqlCommand("Update UserTypePageInfo set DeleteAction=" + Action + " Where UserTypeId=" + UserTypeId + " AND  PageNameId=" + PageNameId + "", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }

        }
        public static void setGeneratePrivilege(string PageNameId, byte Action, string UserTypeId)
        {
            SqlCommand cmd;
            if (!RecordIsExists(PageNameId, UserTypeId))
            {
                cmd = new SqlCommand("insert into UserTypePageInfo (UserTypeId,PageNameId,MenuId,ViewAction,SaveAction,UpdateAction,DeleteAction,GenerateAction) " +
                    " values(" + UserTypeId + "," + PageNameId + ",'0','0','0','0','0','" + Action + "')", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new SqlCommand("Update UserTypePageInfo set GenerateAction=" + Action + " Where UserTypeId=" + UserTypeId + " AND  PageNameId=" + PageNameId + "", DbConnection.Connection);
                cmd.ExecuteNonQuery();
            }
        }
        //---------------
        public static void setAcademicModule(string UserTypeDId, byte Action)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("update UserTypeInfo_ModulePrivilege set AcademicModule=" + Action + " where UserTypeDId=" + UserTypeDId + "", DbConnection.Connection);
            cmd.ExecuteNonQuery();
        }
        public static void setAdministrationModule(string UserTypeDId, byte Action)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("update UserTypeInfo_ModulePrivilege set AdministrationModule=" + Action + " where UserTypeDId=" + UserTypeDId + "", DbConnection.Connection);
            cmd.ExecuteNonQuery();
        }
        public static void setNotificationModule(string UserTypeDId, byte Action)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("update UserTypeInfo_ModulePrivilege set NotificationModule=" + Action + " where UserTypeDId=" + UserTypeDId + "", DbConnection.Connection);
            cmd.ExecuteNonQuery();
        }

        public static void setReportsModule(string UserTypeDId, byte Action)
        {
            SqlCommand cmd;
            cmd = new SqlCommand("update UserTypeInfo_ModulePrivilege set ReportsModule=" + Action + " where UserTypeDId=" + UserTypeDId + "", DbConnection.Connection);
            cmd.ExecuteNonQuery();
        }
        //--------------
        public static DataTable  HasPermissionForPage(string PageName, string UserTypeId)
        {
            try
            {
                DataTable dt = CRUD.ReturnTableNull("select ViewAction,SaveAction,UpdateAction,DeleteAction,GenerateAction from UserTypePageInfo where UserTypeId=" + UserTypeId + " AND PageNameId = (select PageNameId from PageInfo where PageName='" + PageName + "')");

                if (dt != null && dt.Rows.Count > 0) return dt;
                else return null;
            }
            catch { return null; }
        }


        // for 1 generate button privilege
        public static bool SetPrivilegeControl(string UserTypeId, string PageName, Button btnGenerate)
        {
            try
            {
                bool status = false;
                DataTable dt = HasPermissionForPage(PageName, UserTypeId);
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;
                    if (bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false)
                    {
                        btnGenerate.Enabled = false;
                        
                    }
                    else
                    {
                        btnGenerate.Enabled = true;
                        status = true;
                    }
                    if (!status) return false;
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }


        // for 2 control,that is used for only save and update button,generate button for generate operation
        /*Pge name
          OffDaysSet.aspx
         
         */
        public static bool SetPrivilegeControl(string UserTypeId, string PageName, Button buttonSave_Update,Button buttonGenerate)
        {
            try
            {
               
                
                
                bool status = false;
                DataTable dt = HasPermissionForPage(PageName, UserTypeId);
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;
                  
                    
                    if (bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                    {
                        buttonSave_Update.Enabled = false;
                        buttonSave_Update.CssClass = "";
                        HttpContext.Current.Session["__Update__"] = "false";
                    }
                    else
                    {
                        buttonSave_Update.Enabled = true;
                        buttonSave_Update.CssClass = "btn btn-primary";
                        buttonSave_Update.Text = "Update"; status = true;
                        HttpContext.Current.Session["__Update__"] = "true";
                    }
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false)
                    {
                        buttonSave_Update.Enabled = false;
                        buttonSave_Update.CssClass = "";
                        HttpContext.Current.Session["__Save__"] = "false";
                    }
                    else
                    {
                        buttonSave_Update.Enabled = true;
                        buttonSave_Update.CssClass = "btn btn-primary";
                        buttonSave_Update.Text = "Save"; status = true;
                        HttpContext.Current.Session["__Save__"] = "true";
                    }

                    if (bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false)
                    {
                        buttonGenerate.Enabled = false;
                        buttonGenerate.CssClass = "";
                        HttpContext.Current.Session["__Generate__"] = "true";
                    }
                    else
                    {
                        buttonGenerate.Enabled = true;
                        buttonGenerate.CssClass = "btn btn-primary"; status = true;
                        HttpContext.Current.Session["__Generate__"] = "false";
                    }
                    if (!status) return false;
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }

        // for two generate button
        /*
         * import_data.aspx
         */
        public static bool SetPrivilegeControl(int UserTypeId, string PageName, Button btnGenerate1,Button btnGenerate2)
        {
            try
            {
                bool status = false;
                DataTable dt = HasPermissionForPage(PageName, UserTypeId.ToString());
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;

                    if (bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false)
                    {
                        btnGenerate1.Enabled = false;
                        //btnGenerate1.CssClass = "";
                        btnGenerate2.Enabled = false;
                       // btnGenerate2.CssClass = "";
                        HttpContext.Current.Session["__Generate__"] = "true";
                    }
                    else
                    {
                        btnGenerate1.Enabled = true;
                        btnGenerate1.CssClass = "btn btn-primary"; status = true;
                        btnGenerate2.Enabled = true;
                        btnGenerate2.CssClass = "btn btn-primary"; status = true;
                        HttpContext.Current.Session["__Generate__"] = "false";
                    }
                    if (!status) return false;
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }

        // for viewaction where maximum 5 view buttons are exests
        /*
         * AbsentDetails.aspx
         */
        public static bool SetPrivilegeControl(string UserTypeId, string PageName, Button btnView1,Button btnView2,Button btnView3,Button btnView4,Button btnView5)
        {
            try
            {
                bool status = false;
                DataTable dt = HasPermissionForPage(PageName, UserTypeId);
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false)
                    {
                        btnView1.Enabled = false;
                        btnView1.CssClass = "";
                        btnView2.Enabled = false;
                        btnView2.CssClass = "";
                        btnView3.Enabled = false;
                        btnView3.CssClass = "";
                        btnView4.Enabled = false;
                        btnView4.CssClass = "";
                        btnView5.Enabled = false;
                        btnView5.CssClass = "";
                    }
                    else
                    {
                        btnView1.Enabled = true;
                        btnView1.CssClass = "btn btn-primary litleMargin";
                        btnView2.Enabled = true;
                        btnView2.CssClass = "btn btn-primary litleMargin";
                        btnView3.Enabled = true;
                        btnView3.CssClass = "btn btn-primary litleMargin";
                        btnView4.Enabled = true;
                        btnView4.CssClass = "btn btn-primary litleMargin";
                        btnView5.Enabled = true;
                        btnView5.CssClass = "btn btn-success litleMargin"; status = true;
                    }
                    if (!status) return false;
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }

        // for maintain 2 gridview.That is used for leave application  
        public static bool SetPrivilegeControl(string UserTypeId, string PageName, Button btnSave,GridView gv,GridView gvRejectedList)
        {
            try
            {
                //bool status = false;
                DataTable dt = HasPermissionForPage(PageName, UserTypeId);
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;

                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false)
                    {
                        foreach (GridViewRow row in gv.Rows)
                        {
                            row.Cells[8].Enabled = false;
                            gvRejectedList.Visible = false;
                        }
                    }

                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false)
                    {
                        btnSave.Enabled = false;                                              
                    }                    
                    if (bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false)
                    {
                        foreach (GridViewRow row in gv.Rows)
                        {
                            Button btnDelete = (Button)row.FindControl("btnDelete");
                            btnDelete.Enabled = false;                          
                        }
                    }
                    
                    if (bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                    {
                        foreach (GridViewRow row in gv.Rows)
                        {                                                     
                            Button btn = (Button)row.FindControl("btnEdit");
                            btn.Enabled = false;                       
                           
                        }
                    }                   
                   // if (!status) return false;
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }


        // for maintain 1 gridview that is used leave approved list
        public static bool SetPrivilegeControl(string UserTypeId, string PageName, GridView gv)
        {
            try
            {
               DataTable dt = HasPermissionForPage(PageName, UserTypeId);
               if (dt != null)
               {
                   if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;

                   if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false)
                   {
                       foreach (GridViewRow row in gv.Rows)
                       {
                           gv.Visible = false;
                           return true;
                       }

                   }
                   if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false)
                   {
                       foreach (GridViewRow row in gv.Rows)
                       {
                           Button btnYes = (Button)row.FindControl("btnYes");
                           btnYes.Enabled = false;
                           Button btnNo = (Button)row.FindControl("btnNot");
                           btnNo.Enabled = false;
                       }
                   }
                   if (bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                   {
                       foreach (GridViewRow row in gv.Rows)
                       {
                           Button btn = (Button)row.FindControl("btnEdit");
                           btn.Enabled = false;
                       }
                   }            
                   return true;
               }
               else return false;
                
            }
            catch { return false; }
        }
        //---------------------------Md.Abid Hasan (Nayem)----------------------------------------
        /*..................Uesd For only 1 Button (Save/Update)...............................
         * NewSubject.aspx, AddCourseWithSubject.aspx, AddExam.aspx, QuestionPattern.aspx, ExamInfo.aspx, 
         * Grading.aspx, ManagedBuildings.aspx, Allocated.aspx, ShiftConfig.aspx, ClassTimeSetName.aspx,
         * SessionDateTime.aspx, ExamTimeSpecification.aspx, ExamTimeSetName.aspx, ClassTimeSpecification.aspx,
         * leave_configuration.aspx, DateOfPayment.aspx, AdmissionFeesCategories.aspx, FeesCategoriesInfo.aspx
         * AddParticular, AddDepartment.aspx, AddDesignation.aspx, SalaryAllowanceType.aspx, AddSection.aspx
         * ManageClassGroup.aspx, ManageClassSection.aspx, ManageGroup.aspx, AddDistrict.aspx, AddThana.aspx
         * AttendanceSettings.aspx, AddBoard, SchoolSetup.aspx
         */
        public static bool SetPrivilegeControl(int UserTypeId, string PageName, Button btnSave)
        {
            try
            {
                bool status = false;
                DataTable dt = HasPermissionForPage(PageName, UserTypeId.ToString());
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                    {
                        btnSave.Enabled = false;
                        //btnSave.CssClass = "";
                        HttpContext.Current.Session["__Update__"] = "false";
                        HttpContext.Current.Session["__Save__"] = "false";
                    }
                    else
                    {
                        if (bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                        {
                            HttpContext.Current.Session["__Update__"] = "false";
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnSave.CssClass = "btn btn-primary";
                            btnSave.Text = "Update"; status = true;
                            HttpContext.Current.Session["__Update__"] = "true";
                        }
                        if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false)
                        {
                            HttpContext.Current.Session["__Save__"] = "false";
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnSave.CssClass = "btn btn-primary";
                            btnSave.Text = "Save"; status = true;
                            HttpContext.Current.Session["__Save__"] = "true";
                        }
                    }

                    if (!status) return false;
                    return true;
                }
                else return false;
            }
            catch { return false; }

        }
        /*---------------------------Used for 1 button Save/Update and Edit option nagivate from another page-------------------------------
         *  OldStudentEntry.aspx, StdAdmission.aspx, EmpRegForm.aspx, SetSalary.aspx
         */
        public static bool SetPrivilegeControl(int UserTypeId, string PageName, Button btnSave, string EditMode)
        {
            try
            {
                bool status = false;
                DataTable dt = HasPermissionForPage(PageName, UserTypeId.ToString());
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false) return false;
                    if (bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == true)
                    {
                        btnSave.Enabled = true;
                        btnSave.CssClass = "btn btn-primary";
                        btnSave.Text = "Update"; status = true;
                    }
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == true)
                    {
                        btnSave.Enabled = true;
                        btnSave.CssClass = "btn btn-primary";
                        btnSave.Text = "Save"; status = true;
                    }
                    if (EditMode == "Yes")
                    {
                        if (bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                            return false;
                    }
                    else
                    {
                        if (bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == true && btnSave.Text == "Update")
                        {
                            btnSave.Enabled = false;
                            // btnSave.CssClass = "";
                        }
                    }
                    if (!status) return false;
                    return true;
                }
                else return false;
            }
            catch { return false; }

        }
        /*---------------Used For only View or Only Save---------------
         * CurrentStudentInfo.aspx, AdmissionDetails.aspx,
         * -----------------------------------------------------
         * AdmStdAssign.aspx, StdPromotion.aspx, AdmFeesCollection.aspx, FeesCollection.aspx
         * EmpDetails.aspx,CreateBatch.aspx,StudentFineCollection.aspx
         */
        public static bool SetPrivilegeControl(float UserTypeId, string PageName)
        {
            try
            {
                DataTable dt = HasPermissionForPage(PageName, UserTypeId.ToString());
                if (dt != null)
                {
                    if (PageName == "AdmStdAssign.aspx" || PageName == "StdPromotion.aspx" || PageName == "AdmFeesCollection.aspx" || PageName == "FeesCollection.aspx" || PageName == "CreateBatch.aspx" || PageName == "StudentFineCollection.aspx")
                    {
                        if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false) return false;
                        else return true;
                    }
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false)
                        HttpContext.Current.Session["__View__"] = "false";
                    else
                        HttpContext.Current.Session["__View__"] = "true";

                    return true;
                }
                else return false;
            }
            catch { return false; }

        }
        /*...................Used for 2 buttons Save/Update & View..........................
         * ClassSubjectSetup.aspx
        */
        public static bool SetPrivilegeControl(float UserTypeId, string PageName, Button btnSave, Button btnView)
        {
            try
            {
                bool status = false;
                DataTable dt = HasPermissionForPage(PageName, UserTypeId.ToString());
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false)
                    {
                        btnView.Enabled = false;
                        btnView.CssClass = "";
                        HttpContext.Current.Session["__View__"] = "false";
                    }
                    else
                    {
                        btnView.Enabled = true;
                        btnView.CssClass = "btn btn-primary";
                        status = true;
                        HttpContext.Current.Session["__View__"] = "true";
                    }
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                    {
                        btnSave.Enabled = false;
                        btnSave.CssClass = "";
                        HttpContext.Current.Session["__Update__"] = "false";
                        HttpContext.Current.Session["__Save__"] = "false";
                    }
                    else
                    {
                        if (bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                        {
                            HttpContext.Current.Session["__Update__"] = "false";
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnSave.CssClass = "btn btn-primary";
                            btnSave.Text = "Update"; status = true;
                            HttpContext.Current.Session["__Update__"] = "true";
                        }
                        if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false)
                        {
                            HttpContext.Current.Session["__Save__"] = "false";
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnSave.CssClass = "btn btn-primary";
                            btnSave.Text = "Save"; status = true;
                            HttpContext.Current.Session["__Save__"] = "true";
                        }
                    }

                    if (!status) return false;
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }
        /*---------------Used for 4 buttons Save/Update,Eidit,Delete,View-----------------------------
         * SubjectQuestionPattern.aspx
        */
        public static bool SetPrivilegeControl(int UserTypeId, string PageName, Button btnSave, Button btnEdit, Button btnDelete, Button btnView)
        {
            try
            {
                bool status = false;
                DataTable dt = HasPermissionForPage(PageName, UserTypeId.ToString());
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                    {
                        btnSave.Enabled = false;
                        btnSave.CssClass = "";
                        HttpContext.Current.Session["__Update__"] = "false";
                        HttpContext.Current.Session["__Save__"] = "false";
                    }
                    else
                    {
                        if (bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                        {
                            HttpContext.Current.Session["__Update__"] = "false";
                            btnEdit.Enabled = false;
                            btnEdit.CssClass = "";
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnSave.CssClass = "btn btn-primary";
                            btnSave.Text = "Update"; status = true;
                            HttpContext.Current.Session["__Update__"] = "true";
                        }
                        if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false)
                        {
                            HttpContext.Current.Session["__Save__"] = "false";
                        }
                        else
                        {
                            btnSave.Enabled = true;
                            btnSave.CssClass = "btn btn-primary";
                            btnSave.Text = "Save"; status = true;
                            HttpContext.Current.Session["__Save__"] = "true";
                        }
                    }
                    if (bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false)
                        HttpContext.Current.Session["__Delete__"] = "false";
                    else
                        HttpContext.Current.Session["__Delete__"] = "true";
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false)
                        HttpContext.Current.Session["__View__"] = "false";
                    else
                        HttpContext.Current.Session["__View__"] = "true";
                    if (!status) return false;
                    return true;
                }
                else return false;
            }
            catch { return false; }

        }
        // -----------Used 4 buttons Save/Update,Generat,View1,View2------------
        public static bool SetPrivilegeControl(string UserTypeId, string PageName, Button btnSave, CheckBox ChkGenerate, Button btnView1, Button btnView2, Button btnView3)
        {
            try
            {
                bool status = false;
                DataTable dt = HasPermissionForPage(PageName, UserTypeId);
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                    {
                        btnSave.Enabled = false;
                        btnSave.CssClass = "";
                    }
                    else
                    {
                        btnSave.Enabled = true;
                        btnSave.CssClass = "btn btn-primary";
                        status = true;
                    }
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false)
                    {
                        btnView1.Enabled = false;
                        btnView2.Enabled = false;
                        btnView3.Enabled = false;
                        btnView1.CssClass = "";
                        btnView2.CssClass = "";
                        btnView3.CssClass = "";
                    }
                    else
                    {
                        btnView1.Enabled = true;
                        btnView2.Enabled = true;
                        btnView3.Enabled = true;
                        btnView1.CssClass = "btn btn-primary";
                        btnView2.CssClass = "btn btn-primary";
                        btnView3.CssClass = "btn btn-success pull-right";
                        status = true;
                    }
                    if (bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false)
                        ChkGenerate.Enabled = false;
                    else { status = true; ChkGenerate.Enabled = true; }
                    if (!status) return false;
                    return true;
                }
                else return false;
            }
            catch { return false; }

        }
        /*--------------------------Used for Button less Page. Save & Update---------------------------------
         * CreateWeekdays.aspx, WorkAllotment.aspx, SetExamTimings.aspx, UserType.aspx, UserRegister.aspx,
         * UserPrivilege.aspx, StudentAccount.aspx,ChangePageInfo.aspx, OffMainModule.aspx, ChangeStudentAccount.aspx
         * ChangeUserAccount.aspx
         */
        public static bool SetPrivilegeControl(string UserTypeId, string PageName)
        {
            try
            {

                DataTable dt = HasPermissionForPage(PageName, UserTypeId);
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false || bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false) return false;
                    else return true;
                }
                else return false;
            }
            catch { return false; }
        }
        /*--------------------------Used for  Save/Update & View--------------------------------- 
         * SetClassTimings.aspx
         */
        public static bool SetPrivilegeControl(int UserTypeId, string PageName)
        {
            try
            {

                DataTable dt = HasPermissionForPage(PageName, UserTypeId.ToString());
                if (dt != null)
                {
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false) return false;
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false || bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                        HttpContext.Current.Session["__SaveUpdate__"] = "false";
                    else
                        HttpContext.Current.Session["__SaveUpdate__"] = "true";
                    if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false)
                        HttpContext.Current.Session["__View__"] = "false";
                    else HttpContext.Current.Session["__View__"] = "true";
                    return true;
                }
                else return false;
            }
            catch { return false; }
        }
        /*----------------Used For 2 buttons Save/Update And Edit------------------------
         * AdmissionAssignParticular.aspx, ParticularCategories.aspx, DiscountSet.aspx
         */
        public static bool SetPrivilegeControl(int UserTypeId, string PageName, Button btnSave, Button btnEdit,string a)
        {
            try
            {
                bool status = false;
                DataTable dt = HasPermissionForPage(PageName, UserTypeId.ToString());
                if (dt != null)
                {
                    //if (bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false && bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false && bool.Parse(dt.Rows[0]["DeleteAction"].ToString()) == false && bool.Parse(dt.Rows[0]["GenerateAction"].ToString()) == false) return false;
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false && bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false) return false;
                    if (bool.Parse(dt.Rows[0]["UpdateAction"].ToString()) == false)
                    {
                        HttpContext.Current.Session["__Update__"] = "false";
                        btnEdit.Enabled = false;
                        // btnEdit.CssClass = "";
                    }
                    else
                    {
                        btnEdit.Enabled = true;
                        // btnSave.CssClass = "btn btn-primary";
                        btnSave.Text = "Update"; status = true;
                        HttpContext.Current.Session["__Update__"] = "true";
                    }
                    if (bool.Parse(dt.Rows[0]["SaveAction"].ToString()) == false)
                    {
                        HttpContext.Current.Session["__Save__"] = "false";
                    }
                    else
                    {
                        //btnSave.Enabled = true;
                        //btnSave.CssClass = "btn btn-primary";
                        btnSave.Text = "Save"; status = true;
                        HttpContext.Current.Session["__Save__"] = "true";
                    }
                    if (!status) return false;
                    return true;
                }
                else return false;
            }
            catch { return false; }

        }
        /*------------------------Used For Only View Page-----------------------------
         * FeeCollectionDetails.aspx, SalaryDetailsInfo.aspx, SalarySetDetails.aspx
         *  UserAccountList, StudentAccountList.aspxs,MonthWiseAttendanceSheet.aspx,MonthWiseAttendanceSheetSummary.aspx,ClassRoutineReport.aspx,ExamRoutine.aspx
         *  ClassRoutine_For_Teacher,AcademicTranscript,ExamReports,ExamOverView,BloodGroup,DesignationwiseReport,ProfileStafforFaculty,EmployeeList,DepartmentwiseReport
         *  GenderwiseStdList,StudentContactList,GuardianContactList,GuardianInformation,ParentsInformationList,StudentList,AdmitCardGenerator
         */
        public static bool SetPrivilegeControl(int UserTypeId, string PageName,string a)
        {
            try
            {
                DataTable dt = HasPermissionForPage(PageName, UserTypeId.ToString());
                if (dt != null)
                {
                    if ( bool.Parse(dt.Rows[0]["ViewAction"].ToString()) == false) return false;
                    return true;                  
                }
                else return false;
            }
            catch { return false; }
        }
        //---------------------------------------------End-----------------------------------------------------
    }
}
