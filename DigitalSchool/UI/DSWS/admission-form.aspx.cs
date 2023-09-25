using ComplexScriptingSystem;
using DS.BLL.Admission;
using DS.DAL;
using DS.PropertyEntities.Model.ManagedClass;
using PdfSharp;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace DS.UI.DSWS
{
    public partial class admission_form : System.Web.UI.Page
    {
        StdAdmFormEntry stdAdmFormEntry;
        DataTable dt;
       
        string ClassName1;
        protected void Page_Load(object sender, EventArgs e)
        {
            try {
                string sl = Request.QueryString["SL"].ToString();
                load_and_download_admission_form(sl);
            }
            catch (Exception ex)
            { }
        }
        private bool load_and_download_admission_form(string sl)
        {
            try
            {
                //if (stdAdmFormEntry == null)
                //{
                //    stdAdmFormEntry = new StdAdmFormEntry();
                //}
                dt = new DataTable();
                dt = StdAdmFormEntry.getAdmissionFormInfo(sl);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int year = int.Parse(dt.Rows[0]["AdmissionYear"].ToString().Trim());
                    lblSession1.Text = lblSession.Text = year.ToString() + "-" + (year + 1).ToString();

                    aPayNow.HRef = "http://islampurcollege.edu.bd/admission-payment/" + dt.Rows[0]["AdmissionFormNo"].ToString().Trim();
                    imgStudent1.Src= imgStudent.Src = "~/Images/studentAdmissionImages/" + dt.Rows[0]["ImageName"].ToString().Trim();                  
                    lblMoneyReceiptNo1.Text = lblMoneyReceiptNo.Text = dt.Rows[0]["MoneyReceiptNo"].ToString().Trim();
                    lblAdmissionFormNo1.Text = lblAdmissionFormNo.Text = dt.Rows[0]["AdmissionFormNo"].ToString().Trim();
                    lblAdmissionDate1.Text = lblAdmissionDate.Text = dt.Rows[0]["AdmissionDate"].ToString().Trim();
                    lblClass1.Text = lblClass.Text= dt.Rows[0]["ClassName"].ToString().Trim();

                    ClassName1= lblClass1.Text.ToString();    
                       

                    lblStudentsName1.Text = lblStudentsName.Text= dt.Rows[0]["FullName"].ToString().Trim();
                    lblStudentsNameBn1.Text = lblStudentsNameBn.Text= dt.Rows[0]["FullNameBn"].ToString().Trim();
                    lblGroup1.Text = lblGroup.Text= dt.Rows[0]["GroupName"].ToString().Trim();
                    lblGender1.Text = lblGender.Text= dt.Rows[0]["Gender"].ToString().Trim();
                    lblReligion1.Text = lblReligion.Text= dt.Rows[0]["Religion"].ToString().Trim();
                    lblDateOfBirth1.Text = lblDateOfBirth.Text= dt.Rows[0]["DateOfBirth"].ToString().Trim();
                    lblStudentMobile1.Text = lblStudentMobile.Text= dt.Rows[0]["Mobile"].ToString().Trim();
                    lblBloodGroup1.Text = lblBloodGroup.Text= dt.Rows[0]["BloodGroup"].ToString().Trim();

                    lblFathersName1.Text = lblFathersName.Text= dt.Rows[0]["FathersName"].ToString().Trim();
                    lblFathersNameBn1.Text = lblFathersNameBn.Text= dt.Rows[0]["FathersNameBn"].ToString().Trim();
                    lblFathersOccupation1.Text = lblFathersOccupation.Text= dt.Rows[0]["FathersProfession"].ToString().Trim();
                    lblFathersOccupationBn1.Text = lblFathersOccupationBn.Text= dt.Rows[0]["FathersProfessionBn"].ToString().Trim();

                    lblMothersName1.Text = lblMothersName.Text = dt.Rows[0]["MothersName"].ToString().Trim();
                    lblMothersNameBn1.Text = lblMothersNameBn.Text = dt.Rows[0]["MothersNameBn"].ToString().Trim();
                    lblMothersOccupation1.Text = lblMothersOccupation.Text = dt.Rows[0]["MothersProfession"].ToString().Trim();
                    lblMothersOccupationBn1.Text = lblMothersOccupationBn.Text = dt.Rows[0]["MothersProfessionBn"].ToString().Trim();

                    lblParentsVillage1.Text = lblParentsVillage.Text= dt.Rows[0]["ParentsAddress"].ToString().Trim();
                    lblParentsVillageBn1.Text = lblParentsVillageBn.Text= dt.Rows[0]["ParentsAddressBn"].ToString().Trim();
                    lblParentsPostOffice1.Text = lblParentsPostOffice.Text= dt.Rows[0]["ParentsPostOffice"].ToString().Trim();
                    lblParentsPostOfficeBn1.Text = lblParentsPostOfficeBn.Text= dt.Rows[0]["ParentsPostOfficeBn"].ToString().Trim();
                    lblParentsUpazila1.Text = lblParentsUpazila.Text= dt.Rows[0]["ParentsThana"].ToString().Trim();
                    lblParentsUpazilaBn1.Text = lblParentsUpazilaBn.Text= dt.Rows[0]["ParentsThanaBn"].ToString().Trim();
                    lblParentsDistrict1.Text = lblParentsDistrict.Text= dt.Rows[0]["ParentsDistrict"].ToString().Trim();
                    lblParentsDistrictBn1.Text = lblParentsDistrictBn.Text= dt.Rows[0]["ParentsDistrictBn"].ToString().Trim();

                    lblGuardianName1.Text = lblGuardianName.Text= dt.Rows[0]["GuardianName"].ToString().Trim();
                    lblGuardianRelation1.Text = lblGuardianRelation.Text= dt.Rows[0]["GuardianRelation"].ToString().Trim();
                    lblGuardianMobile1.Text = lblGuardianMobile.Text= dt.Rows[0]["GuardianMobileNo"].ToString().Trim();
                    lblGuardianAddress1.Text = lblGuardianAddress.Text= dt.Rows[0]["GuardianAddress"].ToString().Trim();

                    lblPermanentVillage1.Text = lblPermanentVillage.Text = dt.Rows[0]["PermanentAddress"].ToString().Trim();
                    lblPermanentVillageBn1.Text = lblPermanentVillageBn.Text = dt.Rows[0]["PermanentAddressBn"].ToString().Trim();
                    lblPermanentPostOffice1.Text = lblPermanentPostOffice.Text = dt.Rows[0]["PermanentPostOffice"].ToString().Trim();
                    lblPermanentPostOfficeBn1.Text = lblPermanentPostOfficeBn.Text = dt.Rows[0]["PermanentPostOfficeBn"].ToString().Trim();
                    lblPermanentUpazila1.Text = lblPermanentUpazila.Text = dt.Rows[0]["PermanentThana"].ToString().Trim();
                    lblPermanentUpazilaBn1.Text = lblPermanentUpazilaBn.Text = dt.Rows[0]["PermanentThanaBn"].ToString().Trim();
                    lblPermanentDistrict1.Text = lblPermanentDistrict.Text = dt.Rows[0]["PermanentDistrict"].ToString().Trim();
                    lblPermanentDistrictBn1.Text = lblPermanentDistrictBn.Text = dt.Rows[0]["PermanentDistrictBn"].ToString().Trim();

                    lblPresentVillage1.Text = lblPresentVillage.Text = dt.Rows[0]["PresentAddress"].ToString().Trim();
                    lblPresentVillageBn1.Text = lblPresentVillageBn.Text = dt.Rows[0]["PresentAddressBn"].ToString().Trim();
                    lblPresentPostOffice1.Text = lblPresentPostOffice.Text = dt.Rows[0]["PresentPostOffice"].ToString().Trim();
                    lblPresentPostOfficeBn1.Text = lblPresentPostOfficeBn.Text = dt.Rows[0]["PresentPostOfficeBn"].ToString().Trim();
                    lblPresentUpazila1.Text = lblPresentUpazila.Text = dt.Rows[0]["PresentThana"].ToString().Trim();
                    lblPresentUpazilaBn1.Text = lblPresentUpazilaBn.Text = dt.Rows[0]["PresentThanaBn"].ToString().Trim();
                    lblPresentDistrict1.Text = lblPresentDistrict.Text = dt.Rows[0]["PresentDistrict"].ToString().Trim();
                    lblPresentDistrictBn1.Text = lblPresentDistrictBn.Text = dt.Rows[0]["PresentDistrictBn"].ToString().Trim();

                    lblPreSchoolName1.Text = lblPreSchoolName.Text= dt.Rows[0]["PreSchoolName"].ToString().Trim();
                    lblPreBoard1.Text= lblPreBoard.Text = dt.Rows[0]["BoardName"].ToString().Trim();
                    lblPrePassingYear1.Text = lblPrePassingYear.Text = dt.Rows[0]["PreSCPassingYear"].ToString().Trim();
                    lblPreRegistration1.Text = lblPreRegistration.Text= dt.Rows[0]["PreSCRegistration"].ToString().Trim();
                    lblPreRoll1.Text = lblPreRoll.Text= dt.Rows[0]["PreSCRollNo"].ToString().Trim();
                    lblPreGPA1.Text = lblPreGPA.Text= dt.Rows[0]["PreSCGPA"].ToString().Trim();

                    lblPreSchoolNameHSC1.Text = lblPreSchoolNameHSC.Text = dt.Rows[0]["PreSchoolNameHSC"].ToString().Trim();
                    lblPreBoardHSC1.Text = lblPreBoardHSC.Text = dt.Rows[0]["BoardNameHSC"].ToString().Trim();
                    lblPrePassingYearHSC1.Text = lblPrePassingYearHSC.Text = dt.Rows[0]["PreSCPassingYearHSC"].ToString().Trim();
                    lblPreRegistrationHSC1.Text = lblPreRegistrationHSC.Text = dt.Rows[0]["PreSCRegistrationHSC"].ToString().Trim();
                    lblPreRollHSC1.Text = lblPreRollHSC.Text = dt.Rows[0]["PreSCRollNoHSC"].ToString().Trim();
                    lblPreGPAHSC1.Text = lblPreGPAHSC.Text = dt.Rows[0]["PreSCGPAHSC"].ToString().Trim();

                    lblPreSchoolNameHonours1.Text = lblPreSchoolNameHonours.Text = dt.Rows[0]["PreSchoolNameHonours"].ToString().Trim();
                    lblPreBoardHonours1.Text = lblPreBoardHonours.Text = dt.Rows[0]["BoardNameHonours"].ToString().Trim();
                    lblPrePassingYearHonours1.Text = lblPrePassingYearHonours.Text = dt.Rows[0]["PreSCPassingYearHonours"].ToString().Trim();
                    lblPreRegistrationHonours1.Text = lblPreRegistrationHonours.Text = dt.Rows[0]["PreSCRegistrationHonours"].ToString().Trim();
                    lblPreRollHonours1.Text = lblPreRollHonours.Text = dt.Rows[0]["PreSCRollNoHonours"].ToString().Trim();
                    lblPreGPAHonours1.Text = lblPreGPAHonours.Text = dt.Rows[0]["PreSCGPAHonours"].ToString().Trim();

                    lblTCCollege1.Text = lblTCCollege.Text= dt.Rows[0]["TCCollege"].ToString().Trim();
                    lblTCDate1.Text = lblTCDate.Text= dt.Rows[0]["TCDate"].ToString().Trim();
                    lblOptSubject1.Text = lblOptSubject.Text = dt.Rows[0]["Subname"].ToString().Trim();

                    string mansubsId = dt.Rows[0]["ManSubId"].ToString().Trim();
                    ShowManSubjectName(mansubsId);
                    


                    lblNuAdmissionRoll1.Text= lblNuAdmissionRoll.Text= dt.Rows[0]["NuAdmissionRoll"].ToString().Trim();
                    if (!lblClass.Text.Trim().Equals("Eleven"))
                    {
                        pnlSubjectList.Visible = false;
                        pnlSubjectList1.Visible = false;
                    }
                    if (ClassName1.Contains("Eleven")) 
                    {
                        divHsc_and_HonorsInfo.Visible = false;
                        divHsc_and_HonorsInfos.Visible= false;  
                    }
                        

                }
                return true;
            }
            catch (Exception ex) { return false; }
        }

        protected void btnDowloadAsp_Click(object sender, EventArgs e)
        {
            try {
                string s = divForm.InnerText.ToString();
                PdfDocument pdf = PdfGenerator.GeneratePdf(s, PageSize.A4);
                pdf.Save("D:\\dd\\document.pdf");
            } catch(Exception ex) { }
            
        }

        //Show GetMandatorySubject 
        private void ShowManSubjectName(string manSubIds) 
        {
            DataTable dt = new DataTable();

           
            dt = CRUD.ReturnTableNull("SELECT STRING_AGG(subname + ' ' +'(' + CAST(SubCode AS NVARCHAR(10)) + ')', ',') as subname FROM NewSubject ns inner JOIN ClassSubject cs ON ns.SubId = cs.SubId WHERE ns.SubId IN(" + manSubIds + ")");
            String subname = dt.Rows[0]["subname"].ToString();
            List<string> _manSub = subname.Split(',').ToList();
            if( _manSub.Count > 0 )
            {
                try
                {
                    lblManSub1_1.Text = lblManSub1.Text = _manSub[0];
                }
                catch (Exception ex) { }


                try
                {
                    lblManSub2_1.Text = lblManSub2.Text = _manSub[1];
                }
                catch (Exception ex) { }


                try
                {
                    lblManSub3_1.Text = lblManSub3.Text = _manSub[2];
                }
                catch (Exception ex) { }

            }
           







        }


      
    }
}