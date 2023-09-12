using DS.DAL.AdviitDAL;
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
    public partial class CurrentStudentView : System.Web.UI.Page
    {
        static string stid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                loadStudentProfileInfo();
            }
        }


        private void loadStudentProfileInfo()
        {
            try
            {
                stid = Request.QueryString["StudentId"];
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@StudentId", stid) };
                sqlDB.fillDataTable("Select StudentId, ClassName, SectionName, RollNo, FullName, Gender, Mobile,BloodGroup, ImageName, FathersName, FathersYearlyIncome,FathersProfession,MothersName,MothersProfession, MothersYearlyIncome, FathersMobile, MothersMoible, HomePhone, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo, GuardianAddress, IsActive, Comments,  Religion, Shift from v_CurrentStudentInfo where StudentId=@StudentId ", prms, dt);

                lblClass.Text = dt.Rows[0]["ClassName"].ToString();
                lblSection.Text = dt.Rows[0]["SectionName"].ToString();
                lblStRoll.Text = dt.Rows[0]["RollNo"].ToString();
                lblStudentName.Text = dt.Rows[0]["FullName"].ToString();
                lblGender.Text = dt.Rows[0]["Gender"].ToString();
                lblMobile.Text = dt.Rows[0]["Mobile"].ToString();
                lblBloodGroup.Text = dt.Rows[0]["BloodGroup"].ToString();

                lblReligion.Text = dt.Rows[0]["Religion"].ToString();
                lblShift.Text = dt.Rows[0]["Shift"].ToString();

                lblStatus.Text = dt.Rows[0]["IsActive"].ToString();

                lblFatherName.Text = dt.Rows[0]["FathersName"].ToString();
                lblFatherOccupation.Text = dt.Rows[0]["FathersProfession"].ToString();
                lblFatherYearlyIncome.Text = dt.Rows[0]["FathersYearlyIncome"].ToString();
                lblFathersMobile.Text = dt.Rows[0]["FathersMobile"].ToString();

                lblMotherName.Text = dt.Rows[0]["MothersName"].ToString();
                lblMotherOccupation.Text = dt.Rows[0]["MothersProfession"].ToString();
                lblMotherYearlyIncome.Text = dt.Rows[0]["MothersYearlyIncome"].ToString();
                lblMothersMobile.Text = dt.Rows[0]["MothersMoible"].ToString();

                lblHomePhone.Text = dt.Rows[0]["HomePhone"].ToString();


                lblTaVillage.Text = dt.Rows[0]["TAViIlage"].ToString();
                lblTaPostOffice.Text = dt.Rows[0]["TAPostOffice"].ToString();
                lblTaThana.Text = dt.Rows[0]["TAThana"].ToString();
                lblTaDistrict.Text = dt.Rows[0]["TADistrict"].ToString();

                lblGuardianName.Text = dt.Rows[0]["GuardianName"].ToString();
                lblRelation.Text = dt.Rows[0]["GuardianRelation"].ToString();
                lblGuardianMobile.Text = dt.Rows[0]["GuardianMobileNo"].ToString();
                lblGuardianAddress.Text = dt.Rows[0]["GuardianAddress"].ToString();

                

                string url = @"/Images/profileImages/" + Path.GetFileName(dt.Rows[0]["ImageName"].ToString());
                stImage.ImageUrl = url;

            }
            catch (Exception ex)
            {
                //lblMessage.InnerText = "error->" + ex.Message;
            }
        }



    }
}