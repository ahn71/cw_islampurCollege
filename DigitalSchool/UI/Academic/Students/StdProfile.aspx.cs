using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;
using System.IO;
using DS.DAL;
using DS.Classes;

namespace DS.UI.Academic.Students
{
    public partial class StdProfile : System.Web.UI.Page
    {
        static string stid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                //---url bind---
                aDashboard.HRef = "~/" + Classes.Routing.DashboardRouteUrl;
                aAcademicHome.HRef = "~/" + Classes.Routing.AcademicRouteUrl;
                aStudentHome.HRef = "~/" + Classes.Routing.StudentHomeRouteUrl;
                aStudentList.HRef = "~/" + Classes.Routing.StudentListRouteUrl;
                //---url bind end---
                try
                {
                        if (Request.QueryString["hasperm"].ToString() != null)
                        { 
                            lblMessage.InnerText = "warning->You don't have permission to Update.";                           
                        }
                }
                catch { }
                loadStudentProfileInfo();
            }
            lblMessage.InnerText = "";
        }

        private void loadStudentProfileInfo()
        {
            try
            {
               // stid = Request.QueryString["StudentId"].Replace("?"," ");
                stid =  RouteData.Values["id"].ToString();
                stid = commonTask.Base64Decode(stid);
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull(@"SELECT cs.StudentId, asi.AdmissionNo,cs.ImageName, convert(varchar(11),asi.AdmissionDate,106) as AdmissionDate, cs.RollNo,cs.FullName,cs.FullNameBn,cs.Gender,  convert(varchar(11),cs.DateOfBirth,106) as DateOfBirth, cs.BloodGroup,cs.Religion, cs.Mobile,sft.ShiftName,b.BatchName,b.ClassName,b.Year,s.SectionName,g.GroupName,cs.FathersName, cs.FatherNameBn,cs.FathersMobile,cs.FathersProfession,cs.FathersProfessionBn,cs.MothersName,cs.MotherNameBn, cs.MothersProfession,cs.MothersProfessionBn,cs.MothersMoible,cs.ParentsAddress,cs.ParentsAddressBn,pPO.PostOfficeName as ParentsPostOfficeName,pPO.PostOfficeNameBn as ParentsPostOfficeNameBn,pT.ThanaName as ParentsThanaName,pT.ThanaNameBn as ParentsThanaNameBn,pD.DistrictName as  ParentsDistrictName,pD.DistrictNameBn as ParentsDistrictNameBn,cs.GuardianName,cs.GuardianRelation,cs.GuardianMobileNo,cs.GuardianAddress, cs.paVillage,cs.paVillageBn,paPO.PostOfficeName as paPostOfficeName,paPO.PostOfficeNameBn as paPostOfficeNameBn,paT.ThanaName as paThanaName,paT.ThanaNameBn as paThanaNameBn, paD.DistrictName as paDistrictName,paD.DistrictNameBn as paDistrictNameBn,
 cs.TAViIlage, cs.TAViIlageBn, taPO.PostOfficeName as taPostOfficeName, taPO.PostOfficeNameBn as taPostOfficeNameBn, taT.ThanaName as taThanaName, taT.ThanaNameBn as taThanaNameBn, taD.DistrictName as taDistrictName, taD.DistrictNameBn as taDistrictNameBn,

  cs.MotherTongue, cs.Nationality,cs.PreviousExamType, cs.PreviousSchoolName, cs.PSCGPA, cs.PSCRollNo, cs.PSCBoard, cs.PSCPassingYear, cs.PSCJSCRegistration, case when convert(varchar(10),TCDate,105)='01-01-1900' then '' else convert(varchar(10),TCDate,105) end as TCDate, TCCollegeName,brd.BoardName

from CurrentStudentInfo cs INNER JOIN  TBL_STD_Admission_INFO asi ON cs.StudentId = asi.StudentID left join  v_BatchInfo b on cs.BatchID = b.BatchId LEFT OUTER  JOIN Tbl_Class_Section tcs ON cs.ClsSecID = tcs.ClsSecID LEFT OUTER JOIN Sections s ON tcs.SectionID = s.SectionID left join Tbl_Class_Group cg on cs.ClsGrpID = cg.ClsGrpID left join Tbl_Group g on cg.GroupID = g.GroupID left join Post_Office pPO on cs.ParentsPostOfficeId = pPO.PostOfficeID left join Thanas pT on cs.ParentsThanaId = pT.ThanaId left join Distritcts pD on cs.ParentsDistrictId = pD.DistrictId left join Post_Office paPO on cs.PAPostOfficeID = paPO.PostOfficeID left join Thanas paT on cs.PThanaId = paT.ThanaId left join Distritcts paD on cs.PDistrictId = paD.DistrictId
  left join Post_Office taPO on cs.taPostOfficeID = taPO.PostOfficeID left join Thanas taT on cs.TThanaId = taT.ThanaId left join Distritcts taD on cs.TDistrictId = taD.DistrictId left join ShiftConfiguration sft on cs.ConfigId=sft.ConfigId left join Boards brd on cs.PSCBoard=brd.BoardId  where cs.StudentId =" + stid);                           

                lblAdmissionNo.InnerText = dt.Rows[0]["AdmissionNo"].ToString();
                lblAdmissionDate.InnerText = dt.Rows[0]["AdmissionDate"].ToString();
                lblStRoll.InnerText = dt.Rows[0]["RollNo"].ToString();


                lblStudentName.InnerText = dt.Rows[0]["FullName"].ToString();
                lblStudentNameBn.InnerText = dt.Rows[0]["FullNameBn"].ToString();
                lblGender.InnerText = dt.Rows[0]["Gender"].ToString();
                lblDateOfBirth.InnerText = dt.Rows[0]["DateOfBirth"].ToString();
                lblMobile.InnerText = dt.Rows[0]["Mobile"].ToString();
                lblBloodGroup.InnerText = dt.Rows[0]["BloodGroup"].ToString();
                lblReligion.InnerText = dt.Rows[0]["Religion"].ToString();

                lblShift.InnerText = dt.Rows[0]["ShiftName"].ToString();
                lblYear.InnerText = dt.Rows[0]["Year"].ToString();               
                lblAdmissionClass.InnerText = dt.Rows[0]["ClassName"].ToString();
                lblGroup.InnerText = dt.Rows[0]["GroupName"].ToString();
                lblSection.InnerText = dt.Rows[0]["SectionName"].ToString();             

                lblFatherName.InnerText = dt.Rows[0]["FathersName"].ToString();
                lblFatherNameBn.InnerText = dt.Rows[0]["FatherNameBn"].ToString();
                lblFatherNameBn.InnerText = dt.Rows[0]["FatherNameBn"].ToString();
                lblFatherOccupation.InnerText = dt.Rows[0]["FathersProfession"].ToString();
                lblFatherOccupationBn.InnerText = dt.Rows[0]["FathersProfessionBn"].ToString();               
                lblFathersMobile.InnerText = dt.Rows[0]["FathersMobile"].ToString();
                lblFathersMobileBn.InnerText = dt.Rows[0]["FathersMobile"].ToString();

                lblMotherName.InnerText = dt.Rows[0]["MothersName"].ToString();
                lblMotherNameBn.InnerText = dt.Rows[0]["MotherNameBn"].ToString();
                lblMotherOccupation.InnerText = dt.Rows[0]["MothersProfession"].ToString();
                lblMotherOccupationBn.InnerText = dt.Rows[0]["MothersProfessionBn"].ToString();                
                lblMothersMobile.InnerText = dt.Rows[0]["MothersMoible"].ToString();
                lblMothersMobileBn.InnerText = dt.Rows[0]["MothersMoible"].ToString();  
                
                lblParentsVillage.InnerText= dt.Rows[0]["ParentsAddress"].ToString();
                lblParentsVillageBn.InnerText= dt.Rows[0]["ParentsAddressBn"].ToString();
                lblParentsPostOffice .InnerText= dt.Rows[0]["ParentsPostOfficeName"].ToString();
                lblParentsPostOfficeBn .InnerText= dt.Rows[0]["ParentsPostOfficeNameBn"].ToString();
                lblParentsUpazila.InnerText= dt.Rows[0]["ParentsThanaName"].ToString();
                lblParentsUpazilaBn.InnerText= dt.Rows[0]["ParentsThanaNameBn"].ToString();
                lblParentsDistrict.InnerText= dt.Rows[0]["ParentsDistrictName"].ToString();
                lblParentsDistrictBn.InnerText= dt.Rows[0]["ParentsDistrictNameBn"].ToString();

                lblGuardianName.InnerText = dt.Rows[0]["GuardianName"].ToString();
                lblRelation.InnerText = dt.Rows[0]["GuardianRelation"].ToString();
                lblGuardianMobile.InnerText = dt.Rows[0]["GuardianMobileNo"].ToString();
                lblGuardianAddress.InnerText = dt.Rows[0]["GuardianAddress"].ToString();

                lblPaVillage.InnerText = dt.Rows[0]["PAVillage"].ToString();
                lblPaVillageBn.InnerText = dt.Rows[0]["PAVillageBn"].ToString();
                lblPaPostOffice.InnerText = dt.Rows[0]["PAPostOfficeName"].ToString();
                lblPaPostOfficeBn.InnerText = dt.Rows[0]["PAPostOfficeNameBn"].ToString();
                lblPaThana.InnerText = dt.Rows[0]["PAThanaName"].ToString();
                lblPaThanaBn.InnerText = dt.Rows[0]["PAThanaNameBn"].ToString();
                lblPaDistrict.InnerText = dt.Rows[0]["PADistrictName"].ToString();
                lblPaDistrictBn.InnerText = dt.Rows[0]["PADistrictNameBn"].ToString();

                lblTaVillage.InnerText = dt.Rows[0]["TAViIlage"].ToString();
                lblTaVillageBn.InnerText = dt.Rows[0]["TAViIlageBn"].ToString();
                lblTaPostOffice.InnerText = dt.Rows[0]["TAPostOfficeName"].ToString();
                lblTaPostOfficeBn.InnerText = dt.Rows[0]["TAPostOfficeNameBn"].ToString();
                lblTaThana.InnerText = dt.Rows[0]["TAThanaName"].ToString();
                lblTaThanaBn.InnerText = dt.Rows[0]["TAThanaNameBn"].ToString();
                lblTaDistrict.InnerText = dt.Rows[0]["TADistrictName"].ToString();
                lblTaDistrictBn.InnerText = dt.Rows[0]["TADistrictNameBn"].ToString();

                lblPreviousExam.InnerText = dt.Rows[0]["PreviousExamType"].ToString();
                lblPreviousSchoolName.InnerText = dt.Rows[0]["PreviousSchoolName"].ToString();
                lblPreviousBoard.InnerText = dt.Rows[0]["BoardName"].ToString();
                lblPreviousPassingYear.InnerText= dt.Rows[0]["PSCPassingYear"].ToString();
                lblPreviousRoll.InnerText = dt.Rows[0]["PSCRollNo"].ToString();
                lblPreviousReg.InnerText = dt.Rows[0]["PSCJSCRegistration"].ToString();
                lblPreviousGPA.InnerText = dt.Rows[0]["PSCGPA"].ToString();
                

                lblTCInstituteName.InnerText = dt.Rows[0]["TCCollegeName"].ToString();
                lblTCDate.InnerText = dt.Rows[0]["TCDate"].ToString();
                string url = "";
                if (dt.Rows[0]["ImageName"].ToString() != string.Empty)
                {
                    url = @"/Images/profileImages/" + Path.GetFileName(dt.Rows[0]["ImageName"].ToString());
                }
                else
                {
                    url = "http://www.placehold.it/300x300/EFEFEF/999&text=no+image";
                }
                stImage.ImageUrl = url;
                Session["_IndivisualStudentList_"] = dt;
            }
            catch (Exception ex)
            {
                //lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        protected void btnPrintpreview_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlCmd = "";
                sqlCmd = "Select  v.StudentId,asi.AdmissionNo, "
               + "convert(varchar(11),asi.AdmissionDate,106) as  AdmissionDate, v.ClassName, v.SectionName, v.RollNo, v.FullName, v.Gender, convert(varchar(11),v.DateOfBirth,106) as  "
               + "DateOfBirth, v.BloodGroup, v.Mobile, v.ImageName, v.FathersName, v.FathersProfession, v.FathersYearlyIncome, v.MothersName, v.MothersProfession, v.MothersYearlyIncome, "
               + "v.FathersMobile, v.MothersMoible, v.HomePhone, v.PAVillage, v.PAPostOffice, v.PAThana, v.PADistrict, v.TAViIlage, v.TAPostOffice, v.TAThana, v.TADistrict, v.GuardianName, "
               + "v.GuardianRelation, v.GuardianMobileNo, v.GuardianAddress, v.MotherTongue, v.Nationality, v.PreviousSchoolName, v.TransferCertifiedNo, v.CertifiedDate, v.PreferredClass,"
               + "v.PSCGPA, v.PSCRollNo, v.PSCBoard, v.PSCPassingYear,"
               + " "
               + " v.AdmissionNo, v.AdmissionDate, v.RollNo, v.FullName, v.Gender, v.DateOfBirth, v.Mobile, v.FathersName, v.FathersProfession, v.FathersYearlyIncome, v.MothersName, v.MothersProfession,v. PAThana,"
               + " v.GuardianName, v.GuardianRelation, v.GuardianMobileNo,v.GuardianAddress, v.ClassName,v.SectionName, v.FathersMobile, v.MothersMoible, v.BloodGroup,v.Religion,v.ShiftName,v.FullNameBn, v.Session,"
               + " v.GroupName, v.BatchName, v.IdCard, v.PAPostOfficeName, v.TAPostOfficeName, v.StdTypeName,v.FatherNameBn, v.MotherNameBn, v.FatherPhone, v.MotherPhone, v.PSCGPA, v.PSCRollNo, v.PSCBoard, v.PSCPassingYear,"
               + " v.PSCJSCRegistration, v.FatherDesg, v.FatherOrg, v.MotherDesg, v.MotherOrg, v.BusName, v.LocationName, v.PlaceName, v.TCCollegeName, case when convert(varchar(10),v.TCDate,105)='01-01-1900' then '' else convert(varchar(10),v.TCDate,105) end as TCDate, v.TCClass, v.TCSemister,v.PreviousExamType,v.MotherEmail,"
               + " v.FatherEmail,v.MothersYearlyIncome,v.PADistrict,v.TAViIlage,v.TAThana, v.TADistrict, v.PAVillage, v.ImageName,v.SSCRoll "
               + " "
               + " from v_CurrentStudentInfo v INNER JOIN  TBL_STD_Admission_INFO asi ON v.StudentId = asi.StudentID " 
               + "where v.StudentId=" + stid + " ";
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);
                Session["_StudentProfile_"] = dt;
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=StudentProfile');", true);  //Open New Tab for Sever side code
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/IndividualStudentReport.aspx?studentId=' + stid);", true);  //Open New Tab for Sever side code
            }
            catch { }
        }

        protected void EditStd_Click(object sender, EventArgs e)
        {
          //  Response.Redirect("~/UI/Academic/Students/StdAdmission.aspx?StudentId=" + stid);
           // Response.Redirect("~/UI/Academic/Students/OldStudentEntry.aspx?StudentId=" + stid + "&Edit=True");
            //Response.Redirect("~/UI/Academic/Students/student-entry.aspx?StudentId=" + stid + "&Edit=True&requestFrom=Profile");
            stid = commonTask.Base64Encode(stid);
            string _requestFrom= commonTask.Base64Encode("Profile");
            Response.Redirect(GetRouteUrl("StudentEditRoute", new { id = stid, requestFrom = _requestFrom }));
        }
    }

}