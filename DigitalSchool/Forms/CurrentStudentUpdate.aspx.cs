using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class CurrentStudentUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Classes.commonTask.loadClass(ddlClass);
                Classes.commonTask.loadSection(ddlSection);
                Classes.commonTask.loadDistrict(ddlTADistrict);

                loadCurrentStudentInfoInfo();
            }
        }

        protected void ddlTADistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadTAThanas();
        }

        private void loadTAThanas()  //Show Thana/Upazila name whre District name for Present Address
        {
            try
            {
                sqlDB.bindDropDownList("select ThanaName from v_ThanaDetails  where DistrictName='" + ddlTADistrict.SelectedItem.Text + "' ", "ThanaName", ddlTAThana);
            }
            catch { }
        }

        private void loadCurrentStudentInfoInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@StudentId", Request.QueryString["StudentId"].ToString()) };
                sqlDB.fillDataTable("Select  ClassName, SectionName,FullName, RollNo, Mobile, BloodGroup, ImageName,FathersName, FathersYearlyIncome,FathersProfession,MothersName,MothersProfession, MothersYearlyIncome,  FathersMobile, MothersMoible, HomePhone, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo, GuardianAddress, IsActive, Comments, Session, Religion, Shift from v_CurrentStudentInfo where StudentId=@StudentId ", prms, dt);

                ddlClass.Text = dt.Rows[0]["ClassName"].ToString();
                ddlSection.Text = dt.Rows[0]["SectionName"].ToString();
                txtStudentName.Text = dt.Rows[0]["FullName"].ToString();
                txtRoll.Text = dt.Rows[0]["RollNo"].ToString();
                txtMobile.Text = dt.Rows[0]["Mobile"].ToString();
                dlBloodGroup.Text = dt.Rows[0]["BloodGroup"].ToString();

                dlReligion.Text = dt.Rows[0]["Religion"].ToString();
                dlShift.Text = dt.Rows[0]["Shift"].ToString();

                txtFatherName.Text = dt.Rows[0]["FathersName"].ToString();
                txtFatherOccupation.Text = dt.Rows[0]["FathersProfession"].ToString();
                txtFatherYearlyIncome.Text = dt.Rows[0]["FathersYearlyIncome"].ToString();
                txtFathersMobile.Text = dt.Rows[0]["FathersMobile"].ToString();
               
                txtMotherName.Text = dt.Rows[0]["MothersName"].ToString();
                txtMotherOccupation.Text = dt.Rows[0]["MothersProfession"].ToString();
                txtMotherYearlyIncome.Text = dt.Rows[0]["MothersYearlyIncome"].ToString();
                txtMothersMobile.Text = dt.Rows[0]["MothersMoible"].ToString();

                txtHomePhone.Text = dt.Rows[0]["HomePhone"].ToString();

                txtTAVillage.Text = dt.Rows[0]["TAViIlage"].ToString();
                txtTAPostOffice.Text = dt.Rows[0]["TAPostOffice"].ToString();
                ddlTAThana.Items.Add(dt.Rows[0]["TAThana"].ToString());
                ddlTADistrict.Text = dt.Rows[0]["TADistrict"].ToString();
                txtGuardianName.Text = dt.Rows[0]["GuardianName"].ToString();
                txtGuardianRelation.Text = dt.Rows[0]["GuardianRelation"].ToString();
                txtGurdianMobile.Text = dt.Rows[0]["GuardianMobileNo"].ToString();
                txtGuardianAddress.Text = dt.Rows[0]["GuardianAddress"].ToString();
                if (dt.Rows[0]["IsActive"].ToString() == "True") chkStatus.Checked=true;
                else chkStatus.Checked = false;
                
                txtSession.Text = dt.Rows[0]["Session"].ToString();

                string url = @"/Images/profileImages/" + Path.GetFileName(dt.Rows[0]["ImageName"].ToString());
                imgProfile.ImageUrl = url;


            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        private Boolean updateCurrentStudentInfo()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(" update CurrentStudentInfo  Set  ClassName=@ClassName, SectionName=@SectionName, RollNo=@RollNo, Mobile=@Mobile, BloodGroup=@BloodGroup, FathersYearlyIncome=@FathersYearlyIncome, FathersMobile=@FathersMobile, MothersYearlyIncome=@MothersYearlyIncome, MothersMoible=@MothersMoible, HomePhone=@HomePhone, TAViIlage=@TAViIlage, TAPostOffice=@TAPostOffice, TAThana=@TAThana, TADistrict=@TADistrict, GuardianName=@GuardianName, GuardianRelation=@GuardianRelation, GuardianMobileNo=@GuardianMobileNo, GuardianAddress=@GuardianAddress, IsActive=@IsActive, Session=@Session, Religion=@Religion, Shift=@Shift where StudentId=@StudentId ", sqlDB.connection);

                cmd.Parameters.AddWithValue("@StudentId", Request.QueryString["StudentId"].ToString());
                cmd.Parameters.AddWithValue("@ClassName", ddlClass.Text);
                cmd.Parameters.AddWithValue("@SectionName", ddlSection.Text);
                cmd.Parameters.AddWithValue("@RollNo", txtRoll.Text.Trim());
                cmd.Parameters.AddWithValue("@Mobile", txtMobile.Text.Trim());

                if (dlBloodGroup.Text == "Unknown") cmd.Parameters.AddWithValue("@BloodGroup", "");
                else cmd.Parameters.AddWithValue("@BloodGroup", dlBloodGroup.Text.Trim());
                
                cmd.Parameters.AddWithValue("@Religion", dlReligion.Text.Trim());
                cmd.Parameters.AddWithValue("@Shift", dlShift.Text.Trim());

                cmd.Parameters.AddWithValue("@FathersYearlyIncome", txtFatherYearlyIncome.Text.Trim());

                cmd.Parameters.AddWithValue("@MothersYearlyIncome", txtMotherYearlyIncome.Text.Trim());

                cmd.Parameters.AddWithValue("@FathersMobile", txtFathersMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@MothersMoible", txtMothersMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@HomePhone", txtHomePhone.Text.Trim());

                cmd.Parameters.AddWithValue("@TAViIlage", txtTAVillage.Text.Trim());
                cmd.Parameters.AddWithValue("@TAPostOffice", txtTAPostOffice.Text.Trim());
                cmd.Parameters.AddWithValue("@TAThana", ddlTAThana.Text);
                cmd.Parameters.AddWithValue("@TADistrict", ddlTADistrict.Text);
                cmd.Parameters.AddWithValue("@GuardianName", txtGuardianName.Text.Trim());
                cmd.Parameters.AddWithValue("@GuardianRelation", txtGuardianRelation.Text.Trim());
                cmd.Parameters.AddWithValue("@GuardianMobileNo", txtGurdianMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@GuardianAddress", txtGuardianAddress.Text.Trim());

                if (chkStatus.Checked) cmd.Parameters.AddWithValue("@IsActive", "1");
                else cmd.Parameters.AddWithValue("@IsActive", "0");
                
                cmd.Parameters.AddWithValue("@Session", txtSession.Text.Trim());

                cmd.ExecuteNonQuery();
                lblMessage.InnerHtml = "success->Update Successfull";
                Response.Redirect("/Forms/CurrentStudentInfo.aspx");
                return true;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            updateCurrentStudentInfo();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSectionClayseWise();
        }

        private void loadSectionClayseWise()
        {
            try
            {
                DataTable dt;
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='" + ddlClass.SelectedItem.Text.Trim() + "'", dt = new DataTable(), sqlDB.connection);
                if ((dt.Rows[0]["ClassOrder"].ToString().Equals("9") || (dt.Rows[0]["ClassOrder"].ToString().Equals("10"))))
                {
                    ddlSection.Items.Clear();
                    ddlSection.Items.Add("...Select...");
                    ddlSection.Items.Add("Science");
                    ddlSection.Items.Add("Commerce");
                    ddlSection.Items.Add("Arts");
                    ddlSection.SelectedIndex = ddlSection.Items.Count - ddlSection.Items.Count;
                }
                else
                {
                    ddlSection.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' AND SectionName<>'Arts' Order by SectionName", ddlSection);
                    ddlSection.Items.Add("...Select...");
                    ddlSection.SelectedIndex = ddlSection.Items.Count - 1;
                }
            }
            catch { }
        }
    }
}