using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace DS.Forms
{
    public partial class AllReport : System.Web.UI.Page
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
                        loadYear();
                    }
                }
            }
            catch { }
        }

        private void loadYear()
        {
            try
            {
                sqlDB.bindDropDownList("Select distinct Year From v_CurrentStudentInfo", "Year", dlYear);
            }
            catch { }
        }

        protected void btnStudentList_Click(object sender, EventArgs e)
        {
            try
            {
                loadStudentInfo("");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/CrystalReport/ReportViewerForm.aspx?for=StudentListAll');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        private void loadStudentInfo(string sqlCmd)
        {
            try
            {
                sqlCmd = "Select BatchName, StudentId, ClassName, SectionName, Shift, year, RollNo, FullName, Gender, Mobile from v_CurrentStudentInfo Where Year ='" + dlYear.SelectedItem.Text + "'Group By BatchName, StudentId, ClassName, SectionName, Shift, year, RollNo, FullName, Gender, Mobile Order By ClassName, SectionName, Shift ";
             
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                Session["__StudentListAll__"] = dt;
            }
            catch { }
        }

        protected void btnStudentContactList_Click(object sender, EventArgs e)
        {
            try
            {
                loadStudentContactList("");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/CrystalReport/ReportViewerForm.aspx?for=StudentContactList');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        private void loadStudentContactList(string sqlCmd) // for load student Guardian Contact List information
        {
            try
            {
                sqlCmd = "select BatchName, ClassName,SectionName,Shift,FullName,RollNo,Mobile,HomePhone   from v_CurrentStudentInfo where  Year ='" + dlYear.SelectedItem.Text + "' Group By BatchName,ClassName,SectionName,Shift,FullName,RollNo,Mobile,HomePhone  Order By ClassName, SectionName, Shift ";               
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                Session["__StudentContactList__"] = dt;                 
            }
            catch { }
        }

        protected void btnGuardianInformation_Click(object sender, EventArgs e)
        {
            try
            {
                loadGuardianInformation();
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/CrystalReport/ReportViewerForm.aspx?for=GuardianInformation');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        private void loadGuardianInformation()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select BatchName,ClassName,SectionName,FullName,RollNo,Shift,GuardianName,GuardianRelation,GuardianMobileNo from v_CurrentStudentInfo Year ='" + dlYear.SelectedItem.Text + "' Group By BatchName,ClassName,SectionName,FullName,RollNo,Shift,GuardianName,GuardianRelation,GuardianMobileNo Order By ClassName, SectionName, Shift", dt);
                Session["__GuardianInformation__"] = dt;
            }
            catch { }
        }

        private void loadGuardianContactList(string sqlCmd) // for load student Guardian Contact List information
        {
            try
            {
                sqlCmd = "select BatchName,ClassName,SectionName,FullName,RollNo,Shift,GuardianName,GuardianRelation,GuardianMobileNo,GuardianAddress from v_CurrentStudentInfo where Year='" + dlYear.SelectedItem.Text + "' Group By BatchName,ClassName,SectionName,FullName,RollNo,Shift,GuardianName,GuardianRelation,GuardianMobileNo,GuardianAddress Order By ClassName, SectionName, Shift  ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                Session["__GuardianContactList__"] = dt;             
            }
            catch { }
        }

        protected void btnGuardianContractList_Click(object sender, EventArgs e)
        {
            try
            {
                loadGuardianContactList("");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/CrystalReport/ReportViewerForm.aspx?for=GuardianContactList');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void btnParentsInformation_Click(object sender, EventArgs e)
        {
            try
            {
                loadParentsInformationList("");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/CrystalReport/ReportViewerForm.aspx?for=ParentsInformationList');", true);  //Open New Tab for Sever side code
            }
            catch { }
        }
        private void loadParentsInformationList(string sqlCmd) // for load student Parents information list
        {
            try
            {
                sqlCmd = "select BatchName,ClassName,SectionName,FullName,RollNo,Shift,FathersName,FathersMobile,FathersProfession,MothersName,MothersMoible,MothersProfession from v_CurrentStudentInfo where Year='" + dlYear.SelectedItem.Text + "' Group By BatchName,ClassName,SectionName,FullName,RollNo,Shift,FathersName,FathersMobile,FathersProfession,MothersName,MothersMoible,MothersProfession Order By ClassName, SectionName, Shift  ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                Session["__ParentsInformationList__"] = dt;
            }
            catch { }
        }
    }
}