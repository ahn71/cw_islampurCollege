using DS.DAL;
using DS.PropertyEntities.Model.Admission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace DS.BLL.Admission
{
    public class CurrentStdEntry : IDisposable
    {
        private CurrentStdEntities _Entities;
        static List<CurrentStdEntities> CurrentStdList;
        string sql = string.Empty;
        bool result = true;
        DataTable dt;
        public CurrentStdEntry()
        {}
        public CurrentStdEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public bool Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[CurrentStudentInfo] "
                + "([FullName],[ClassID],"
                + "[ClassName],[ClsGrpID],[ClsSecID],[SectionName],"
                + "[RollNo],[Religion],[ConfigId],[Shift],[DateOfBirth],"
                + "[Gender],[Mobile],[Session],[BloodGroup],[FathersName]"
                + ",[FathersProfession],[FathersYearlyIncome],[FathersMobile],"
                + "[FatherEmail],[MothersName],[MothersProfession],"
                + "[MothersYearlyIncome],[MothersMoible],[MotherEmail],"
                + "[HomePhone],[GuardianName],[GuardianRelation],[GuardianMobileNo]"
                + ",[GuardianAddress],[PAVillage],[PAPostOffice],[PThanaId]"
                + ",[PAThana],[PDistrictId],[PADistrict],[TAViIlage],"
                + "[TAPostOffice],[TThanaId],[TAThana],[TDistrictId]"
                + ",[TADistrict],[MotherTongue],[Nationality],[ImageName],"
                + "[PreviousExamType],[PSCRollNo],[PSCPassingYear],"
                + "[PSCGPA],[PSCBoard],[CertifiedDate],[PreviousSchoolName],"
                + "[PSCJSCRegistration],[TransferCertifiedNo],[PreferredClass],"
                + "[IsActive],[Comments],[BatchID],[BatchName],[Status],[PaymentStatus])"
                + " VALUES ( "
                + "'" + _Entities.FullName + "','" + _Entities.ClassID + "',"
                + "'" + _Entities.ClassName + "','" + _Entities.ClsGrpID + "',"
                + "'" + _Entities.ClsSecID + "','" + _Entities.SectionName + "',"
                + "'" + _Entities.RollNo + "','" + _Entities.Religion + "',"
                + "'" + _Entities.ConfigId + "','" + _Entities.Shift + "',"
                + "'" + _Entities.DateOfBirth + "','" + _Entities.Gender + "',"
                + "'" + _Entities.Mobile + "','" + _Entities.Session + "',"
                + "'" + _Entities.BloodGroup + "','" + _Entities.FathersName + "',"
                + "'" + _Entities.FathersProfession + "','" + _Entities.FathersYearlyIncome + "',"
                + "'" + _Entities.FathersMobile + "','" + _Entities.FatherEmail + "',"
                + "'" + _Entities.MothersName + "','" + _Entities.MothersProfession + "',"
                + "'" + _Entities.MothersYearlyIncome + "','" + _Entities.MothersMoible + "',"
                + "'" + _Entities.MotherEmail + "','" + _Entities.HomePhone + "',"
                + "'" + _Entities.GuardianName + "','" + _Entities.GuardianRelation + "',"
                + "'" + _Entities.GuardianMobileNo + "','" + _Entities.GuardianAddress + "',"
                + "'" + _Entities.PAVillage + "','" + _Entities.PAPostOffice + "',"
                + "'" + _Entities.PThanaId + "','" + _Entities.PAThana + "',"
                + "'" + _Entities.PDistrictId + "','" + _Entities.PADistrict + "',"
                + "'" + _Entities.TAViIlage + "','" + _Entities.TAPostOffice + "',"
                + "'" + _Entities.TThanaId + "','" + _Entities.TAThana + "',"
                + "'" + _Entities.TDistrictId + "','" + _Entities.TADistrict + "',"
                + "'" + _Entities.MotherTongue + "','" + _Entities.Nationality + "',"
                + "'"  +_Entities.ImageName+"',"
                + "'" + _Entities.PreviousExamType + "','" + _Entities.PSCRollNo + "',"
                + "'" + _Entities.PSCPassingYear + "','" + _Entities.PSCGPA + "',"
                + "'" + _Entities.PSCBoard + "','" + _Entities.CertifiedDate + "',"
                + "'" + _Entities.PreviousSchoolName + "','" + _Entities.PSCJSCRegistration + "',"
                + "'" + _Entities.TransferCertifiedNo + "','" + _Entities.PreferredClass + "',"
                + "'" + _Entities.IsActive + "','" + _Entities.Comments + "',"
                + "'" + _Entities.BatchID + "','" + _Entities.BatchName + "',"
                + "'" + _Entities.Status + "','" + _Entities.PaymentStatus + "'); " +               
                " SELECT SCOPE_IDENTITY()");
               int MaxId = CRUD.GetMaxID(sql);
            if(MaxId > 0)
            {
                result = StdAdmissionInfoInsert(MaxId, _Entities.ClassID, _Entities.ClsSecID, _Entities.RollNo,
                    _Entities.Session, _Entities.AdmissionNo, _Entities.AdmissionDate);
            }
            return result;             
        }
        private bool StdAdmissionInfoInsert(int studentId, int classId,int sectionId,
            int? rollNo,string session,long AdmissionNo, DateTime AdmissionDate)
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_STD_Admission_INFO] " +
                 "([AdmissionNo],[AdmissionDate],[StudentID],[ClassID],[ClsSecID],[RollNo],[StartBatchID],[EndBatchID],[Session])"
                 + " VALUES ( '" + AdmissionNo + "','" + AdmissionDate + "','" + studentId + "','" +
                 _Entities.ClassID + "','" + sectionId + "','" + rollNo + "',"
                 + "'" + 0 + "','" + 0 + "','" + session + "')");
            return result = CRUD.ExecuteQuery(sql);    
        }
        public bool CurrentStdInfo_Log()
        {
            sql = string.Format("INSERT INTO [dbo].[CurrentStudent_Log] "
                + "([StudentId],[ClassID],[ClsGrpID],[ClsSecID],[RollNo],"
                + "[ConfigId],[BatchID],[SpendYear]) VALUES ( "
                +"'"+_Entities.StudentID+"','"+_Entities.ClassID+"',"
                + "" + _Entities.ClsGrpID + "," + _Entities.ClsSecID + ","
                + "" + _Entities.RollNo + "," + _Entities.ConfigId + ","
                + "" + _Entities.BatchID + "," + _Entities.SpendYear + ")");
                result = CRUD.ExecuteQuery(sql);
                return result;
        }

        public bool Update()
        {
            sql = string.Format("Update [dbo].[CurrentStudentInfo] "
                 + "set "
                 + "[FullName]='" + _Entities.FullName + "',"
                 + "[ClassID]='" + _Entities.ClassID + "',"
                 + "[ClassName]='" + _Entities.ClassName + "',"
                 + "[ClsGrpID]='" + _Entities.ClsGrpID + "',"
                 + "[ClsSecID]='" + _Entities.ClsSecID + "',"
                 + "[SectionName]='" + _Entities.SectionName + "',"
                 + "[RollNo]='" + _Entities.RollNo + "',"
                 + "[Religion]='" + _Entities.Religion + "',"
                 + "[ConfigId]='" + _Entities.ConfigId + "',"
                 + "[Shift]='" + _Entities.Shift + "',"
                 + "[DateOfBirth]='" + _Entities.DateOfBirth + "',"
                 + "[Gender]='" + _Entities.Gender + "',"
                 + "[Mobile]='" + _Entities.Mobile + "',"
                 + "[Session]='" + _Entities.Session + "',"
                 + "[BloodGroup]='" + _Entities.BloodGroup + "',"
                 + "[FathersName]='" + _Entities.FathersName + "',"
                 + "[FathersProfession]='" + _Entities.FathersProfession + "',"
                 + "[FathersYearlyIncome]='" + _Entities.FathersYearlyIncome + "',"
                 + "[FathersMobile]='" + _Entities.FathersMobile + "',"
                 + "[FatherEmail]='" + _Entities.FatherEmail + "',"
                 + "[MothersName]='" + _Entities.MothersName + "',"
                 + "[MothersProfession]='" + _Entities.MothersProfession + "',"
                 + "[MothersYearlyIncome]='" + _Entities.MothersYearlyIncome + "',"
                 + "[MothersMoible]='" + _Entities.MothersMoible + "',"
                 + "[MotherEmail]='" + _Entities.MotherEmail + "',"
                 + "[HomePhone]='" + _Entities.HomePhone + "',"
                 + "[GuardianName]='" + _Entities.GuardianName + "',"
                 + "[GuardianRelation]='" + _Entities.GuardianRelation + "',"
                 + "[GuardianMobileNo]='" + _Entities.GuardianMobileNo + "',"
                 + "[GuardianAddress]='" + _Entities.GuardianAddress + "',"
                 + "[PAVillage]='" + _Entities.PAVillage + "',"
                 + "[PAPostOffice]='" + _Entities.PAPostOffice + "',"
                 + "[PThanaId]='" + _Entities.PThanaId + "',"
                 + "[PAThana]='" + _Entities.PAThana + "',"
                 + "[PDistrictId]='" + _Entities.PDistrictId + "',"
                 + "[PADistrict]='" + _Entities.PADistrict + "',"
                 + "[TAViIlage]='" + _Entities.TAViIlage + "',"
                 + "[TAPostOffice]='" + _Entities.TAPostOffice + "',"
                 + "[TThanaId]='" + _Entities.TThanaId + "',"
                 + "[TAThana]='" + _Entities.TAThana + "',"
                 + "[TDistrictId]='" + _Entities.TDistrictId + "',"
                 + "[TADistrict]='" + _Entities.TADistrict + "',"
                 + "[MotherTongue]='" + _Entities.MotherTongue + "',"
                 + "[Nationality]='" + _Entities.Nationality + "',"
                 + "[PreviousExamType]='" + _Entities.PreviousExamType + "',"
                 + "[PSCRollNo]='" + _Entities.PSCRollNo + "',"
                 + "[PSCPassingYear]='" + _Entities.PSCPassingYear + "',"
                 + "[PSCGPA]='" + _Entities.PSCGPA + "',"
                 + "[PSCBoard]='" + _Entities.PSCBoard + "',"
                 + "[CertifiedDate]='" + _Entities.CertifiedDate + "',"
                 + "[PreviousSchoolName]='" + _Entities.PreviousSchoolName + "',"
                 + "[PSCJSCRegistration]='" + _Entities.PSCJSCRegistration + "',"
                 + "[TransferCertifiedNo]='" + _Entities.TransferCertifiedNo + "',"
                 + "[PreferredClass]='" + _Entities.PreferredClass + "',"                 
                 + "[Comments]='" + _Entities.Comments + "',"                 
                 + "[Status]='" + _Entities.Status + "',"
                 + "[PaymentStatus]='" + _Entities.PaymentStatus + "',"
                 + "[ImageName]='" + _Entities.ImageName + "'"
                 + " where StudentID='"+_Entities.StudentID+"' ");
                 result = CRUD.ExecuteQuery(sql);
                 if (result == true)
                 {
                     result = UpdateStdAdmission(_Entities.StudentID, _Entities.ClassID, _Entities.ClsSecID, _Entities.RollNo, 
                         _Entities.Session, _Entities.AdmissionNo, _Entities.AdmissionDate);
                 }
            return result;
        }
        private bool UpdateStdAdmission(int studentId, int classId, int sectionId, int? rollNo, 
            string session, long AdmissionNo, DateTime AdmissionDate)
        {           
            sql = string.Format("Update [dbo]."
                 + "[Tbl_STD_Admission_INFO] Set "
                 + "[AdmissionNo]='" + AdmissionNo + "',"
                 + "[AdmissionDate]='" + AdmissionDate + "',"
                 + "[ClassID]='" + classId + "',"
                 + "[ClsSecID]='" + sectionId + "',"
                 + "[RollNo]='" + rollNo + "',"                
                 + "[Session]='" + session + "'"
                 + " where StudentID='" + studentId + "'");
            return result = CRUD.ExecuteQuery(sql);            
        }
        public bool UpdateStdStatus(string studentId,string stdStatus)
        {
            sql = string.Format("Update [dbo]."
                + "[Tbl_STD_Admission_INFO] Set "
                 + "[StdStatus]='" + stdStatus + "'"
                 + " where StudentID='" + studentId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool UpdateCurrentStudentActive(string studentId, string stdStatus)
        {
            sql = string.Format("Update [dbo]."
                + "[CurrentStudentInfo] Set "
                 + "[IsActive]='" + stdStatus + "'"
                 + " where StudentId='" + studentId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }

        public List<CurrentStdEntities> GetEntitiesData()
        {
            List<CurrentStdEntities> ListEntities = new List<CurrentStdEntities>();
            sql = string.Format("SELECT [dbo].[CurrentStudentInfo].*,TBL_STD_Admission_INFO.* "
                    + "FROM [dbo].[CurrentStudentInfo] Inner Join TBL_STD_Admission_INFO"
                    + " ON  [CurrentStudentInfo].StudentId=TBL_STD_Admission_INFO.StudentID");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new CurrentStdEntities
                                    {
                                        StudentID = int.Parse(row["StudentId"].ToString()),
                                        AdmissionNo = int.Parse(row["AdmissionNo"].ToString()),
                                        AdmissionDate = Convert.ToDateTime(row["AdmissionDate"].ToString()),
                                        FullName = row["FullName"].ToString(),
                                        ClassID = int.Parse(row["ClassID"].ToString()),
                                        ClassName = row["ClassName"].ToString(),
                                        ClsGrpID = int.Parse(row["ClsGrpID"].ToString()),
                                        ClsSecID = int.Parse(row["ClsSecID"].ToString()),
                                        SectionName = row["SectionName"].ToString(),
                                        RollNo = int.Parse(row["RollNo"].ToString()),
                                        Religion = row["Religion"].ToString(),
                                        ConfigId = int.Parse(row["ConfigId"].ToString()),
                                        Shift = row["Shift"].ToString(),
                                        DateOfBirth = Convert.ToDateTime(row["DateOfBirth"].ToString()),
                                        Gender = row["Gender"].ToString(),
                                        Mobile = row["Mobile"].ToString(),
                                        Session = row["Session"].ToString(),
                                        BloodGroup = row["BloodGroup"].ToString(),
                                        FathersName = row["FathersName"].ToString(),
                                        FathersProfession = row["FathersProfession"].ToString(),
                                        FathersYearlyIncome = int.Parse(row["FathersYearlyIncome"].ToString()),
                                        FathersMobile = row["FathersMobile"].ToString(),
                                        FatherEmail = row["FatherEmail"].ToString(),
                                        MothersName = row["MothersName"].ToString(),
                                        MothersProfession = row["MothersProfession"].ToString(),
                                        MothersYearlyIncome = int.Parse(row["MothersYearlyIncome"].ToString()),
                                        MothersMoible = row["MothersMoible"].ToString(),
                                        MotherEmail = row["MotherEmail"].ToString(),
                                        HomePhone = row["HomePhone"].ToString(),
                                        GuardianName = row["GuardianName"].ToString(),
                                        GuardianRelation = row["GuardianRelation"].ToString(),
                                        GuardianMobileNo = row["GuardianMobileNo"].ToString(),
                                        GuardianAddress = row["GuardianAddress"].ToString(),
                                        PAVillage = row["PAVillage"].ToString(),
                                        PAPostOffice = row["PAPostOffice"].ToString(),
                                        PThanaId = int.Parse(row["PThanaId"].ToString()),
                                        PDistrictId = int.Parse(row["PDistrictId"].ToString()),
                                        TAViIlage = row["TAViIlage"].ToString(),
                                        TAPostOffice = row["TAPostOffice"].ToString(),
                                        TThanaId = int.Parse(row["TThanaId"].ToString()),
                                        TDistrictId = int.Parse(row["TDistrictId"].ToString()),
                                        MotherTongue = row["MotherTongue"].ToString(),
                                        Nationality = row["Nationality"].ToString(),
                                        PreviousExamType = row["PreviousExamType"].ToString(),
                                        PSCRollNo = int.Parse(row["PSCRollNo"].ToString()),
                                        PSCPassingYear = int.Parse(row["PSCPassingYear"].ToString()),
                                        PSCGPA = double.Parse(row["PSCGPA"].ToString()),
                                        PSCBoard = row["PSCBoard"].ToString(),
                                        CertifiedDate = Convert.ToDateTime(row["CertifiedDate"].ToString()),
                                        PreviousSchoolName = row["PreviousSchoolName"].ToString(),
                                        PSCJSCRegistration = row["PSCJSCRegistration"].ToString(),
                                        TransferCertifiedNo = int.Parse(row["TransferCertifiedNo"].ToString()),
                                        PreferredClass = row["PreferredClass"].ToString(),
                                        IsActive = bool.Parse(row["IsActive"].ToString()),
                                        Comments = row["Comments"].ToString(),
                                        BatchName = row["BatchName"].ToString(),
                                        Status = row["Status"].ToString(),
                                        StdStatus =row["StdStatus"].ToString()==""?null:(bool?)bool.Parse(row["StdStatus"].ToString()),
                                        PaymentStatus = bool.Parse(row["PaymentStatus"].ToString())                 
                                    }).ToList();
                    return ListEntities;

                }

            }
            return ListEntities = null;
        }       
        public List<CurrentStdEntities> GetEntitiesData(string stdStatus)
        {
            List<CurrentStdEntities> ListEntities = new List<CurrentStdEntities>();
            sql = string.Format("SELECT [dbo].[CurrentStudentInfo].StudentId,[TBL_STD_Admission_INFO].AdmissionNo "
                    + "FROM [dbo].[CurrentStudentInfo] Inner Join TBL_STD_Admission_INFO"
                    + " ON  [CurrentStudentInfo].StudentId=TBL_STD_Admission_INFO.StudentID"
                    + " where TBL_STD_Admission_INFO.StdStatus is NULL");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new CurrentStdEntities
                                    {
                                        StudentID = int.Parse(row["StudentId"].ToString()),
                                        AdmissionNo = int.Parse(row["AdmissionNo"].ToString())                                        
                                    }).ToList();
                    return ListEntities;

                }

            }
            return ListEntities = null;
        }
        public List<CurrentStdEntities> GetEntitiesData(int studentId)
        {
            List<CurrentStdEntities> ListEntities = new List<CurrentStdEntities>();
            sql = string.Format("SELECT ClassID "
                    + "FROM [dbo].[CurrentStudentInfo] "
                    + " where StudentId='" + studentId + "'");
            DataTable dt = new DataTable();

            dt = CRUD.ReturnTableNull(sql);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    ListEntities = (from DataRow row in dt.Rows

                                    select new CurrentStdEntities
                                    {
                                        ClassID = int.Parse(row["ClassID"].ToString())
                                    }).ToList();
                    return ListEntities;

                }

            }
            return ListEntities = null;
        }
        public DataTable GetCurrentStudent(string condition)
        {
            try
            {
                dt=new DataTable();
                sql =string.Format("SELECT StudentId,ShiftName,ClassName,GroupName,SectionName,FullName,"
                +"RollNo,Gender,GuardianMobileNo from v_CurrentStudentInfo " + condition + "");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public DataTable GetCurrentStudentProfile(string condition)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT  StudentId,AdmissionNo, "
                + "convert(varchar(11),AdmissionDate,106) as  AdmissionDate, ClassName, SectionName, RollNo, FullName, Gender, convert(varchar(11),DateOfBirth,106) as  "
                + "DateOfBirth, BloodGroup, Mobile, ImageName, FathersName, FathersProfession, FathersYearlyIncome, MothersName, MothersProfession, MothersYearlyIncome, "
                + "FathersMobile, MothersMoible, HomePhone, PAVillage, PAPostOffice, PAThana, PADistrict, TAViIlage, TAPostOffice, TAThana, TADistrict, GuardianName, "
                + "GuardianRelation, GuardianMobileNo, GuardianAddress, MotherTongue, Nationality, PreviousSchoolName, TransferCertifiedNo, CertifiedDate, PreferredClass, "
                + "PSCGPA, PSCRollNo, PSCBoard, PSCPassingYear,BatchName,ShiftName FROM v_CurrentStudentInfo " + condition + " Order By ClassName ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public DataTable GetGenderwiseStdList(string condition)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT SectionName as Section,FullName as Name,"
                +"RollNo as Roll,Mobile,BatchName,ClassName as Class FROM v_CurrentStudentInfo " + condition + " Order By ClassName ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public DataTable GetstdconList(string condition)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT ClassName,SectionName,FullName,RollNo,Mobile,GuardianMobileNo AS HomePhone,BatchName FROM "
                     + "v_CurrentStudentInfo " + condition + " Order By ClassName ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public DataTable GetGuardianInfo(string condition)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT ClassName,SectionName,FullName,RollNo,BatchName,ShiftName "
                    + "as Shift,GuardianName,GuardianRelation,GuardianMobileNo,GuardianAddress FROM "
                      + "v_CurrentStudentInfo  " + condition + " Order By ClassName ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }

        public DataTable GetParentsInfo(string condition)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("select SectionName,FullName,RollNo,FathersName,FathersMobile,"
                    + "FathersProfession,MothersName,MothersMoible,BatchName,"
                    + "MothersProfession from v_CurrentStudentInfo  " + condition + " Order By ClassName ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public DataTable GetAdmitCard(string condition)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT FullName,RollNo,ShiftName,GroupName,"
                +"SectionName,ClassName FROM v_CurrentStudentInfo  " + condition + " Order By ClassName ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public void GetRollNo(DropDownList dl,string shiftID,string batchID,string clsgrpID,string clssectionID)
        {
            if (clssectionID == "All")
            {
                sql = string.Format("Select  RollNo, StudentId from v_CurrentStudentInfo WHERE ConfigId='"
                    + shiftID + "' AND  BatchID='" + batchID + "' AND ClsGrpID='" + clsgrpID + "'  ORDER BY RollNo ");
            }
            else
            {
                sql = string.Format("Select  RollNo, StudentId from v_CurrentStudentInfo WHERE ConfigId='"
                    + shiftID + "' AND  BatchID='" + batchID + "' AND ClsGrpID='" + clsgrpID + "' AND ClsSecID='"
                    + clssectionID + "' ORDER BY RollNo ");
            }
            dt = CRUD.ReturnTableNull(sql);
            dl.DataSource = dt;
            dl.DataTextField = "RollNo";
            dl.DataValueField = "StudentId";
            dl.DataBind();
            dl.Items.Insert(0,new ListItem("...Select...","0"));
        }
        public string GetSearchCondition(string shiftID,string batchID,string groupID,string sectionID)
        {
            string condition = "";
            string[] BatchClsID = batchID.Split('_');
            if (shiftID == "All" && batchID == "All" && groupID == "All" && sectionID == "All")
            {
                condition = "";
            }
            else if (shiftID == "All" && batchID == "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE ClsSecID='" + sectionID + "'";
            }
            else if (shiftID == "All" && batchID == "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE ClsGrpID='" + groupID + "'";
            }
            else if (shiftID == "All" && batchID == "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE ClsGrpID='" + groupID + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID == "All" && sectionID == "All")
            {
                condition = " WHERE BatchID='" + BatchClsID[0] + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE BatchID='" + BatchClsID[0] + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE BatchID='" + BatchClsID[0] + "' AND ClsGrpID='" + groupID + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE BatchID='" + BatchClsID[0] + "' AND ClsGrpID='" 
                + groupID + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID == "All" && sectionID == "All")
            {
                condition = " WHERE ConfigId='" + shiftID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE ConfigId='" + shiftID + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE ConfigId='" + shiftID + "' AND ClsGrpID='" + groupID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE ConfigId='" + shiftID + "' AND ClsGrpID='" 
                + groupID + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID != "All" && groupID == "All" && sectionID == "All")
            {
                condition = " WHERE  ConfigId='" + shiftID + "' AND BatchID='" + BatchClsID[0] + "' ";
            }
            else if (shiftID != "All" && batchID != "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE ConfigId='" + shiftID + "' AND BatchID='" 
                + BatchClsID[0] + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID != "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE ConfigId='" + shiftID + "' AND BatchID='" 
                + BatchClsID[0] + "' AND ClsGrpID='" + groupID + "' ";
            }
            else if (shiftID != "All" && batchID != "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE ConfigId='" + shiftID + "' AND BatchID='" + BatchClsID[0] 
                + "' AND ClsGrpID='" + groupID + "' AND ClsSecID='" + sectionID + "' ";
            }
            return condition;
        }
        public static int GetMaxID()
        {
            string sql = string.Format("SELECT MAX(AdmissionNo) as AdmissionNo FROM TBL_STD_Admission_INFO");
            int MaxId = CRUD.GetMaxID(sql);
            if (MaxId == null)
            {
                return MaxId = 1;
            }
            {
                return MaxId + 1;
            }
        }
        public DataTable GetStudentNameforStdFine(string stdID)
        {
            sql = string.Format("SELECT FullName FROM CurrentStudentInfo WHERE StudentId='"+stdID+"'");
            dt = CRUD.ReturnTableNull(sql);
            return dt;
        }
        public DataTable GetLoginStudentInfo(string stdID)
        {
            try
            {
                sql = string.Format("SELECT FullName,ShiftName,BatchName,GroupName,SectionName,RollNo,"
                + "ImageName,BatchId,ConfigId,ClsGrpID,ClsSecID FROM v_CurrentStudentInfo WHERE StudentId='" + stdID + "' ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public DataTable  GetUnassignStudentList(string shiftId,string batchId,string clsgrpId,string clssecId)
        {
            try
            {
                sql = string.Format("SELECT StudentId, FullName,RollNo,Gender,Mobile FROM v_Guide_Teacher WHERE BatchID='"+batchId+"' "
                +"AND ConfigId='"+shiftId+"' AND ClsGrpID='"+clsgrpId+"' AND ClsSecID='"+clssecId+"' AND GuideStatus is null");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public static void GetDropDownList(DropDownList dl)
        {
            CurrentStdEntry clsGrp = new CurrentStdEntry();
            CurrentStdList = clsGrp.GetEntitiesData("");                      
            dl.DataTextField = "AdmissionNo";
            dl.DataValueField = "StudentID";
            if(CurrentStdList!=null)
            { 
            dl.DataSource = CurrentStdList;
            dl.DataBind();
            }
            else
            {
                dl.Items.Clear();
            }
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
            dl.Enabled = true;
        }
        public bool RollValidation(string shiftID,string batchID,string clsgrpID,string clssecID,string stdID,string rollNo)
        {
            try
            {
                dt = CRUD.ReturnTableNull("SELECT RollNo FROM CurrentStudentInfo WHERE StudentId!='"
                    +stdID+"' And RollNo='"+rollNo+"' AND BatchID='"+batchID+"' AND ClsSecID='"
                    +clssecID+"' AND ClsGrpID='"+clsgrpID+"' and ConfigId='"+shiftID+"'");
                if (dt.Rows.Count > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }
        }
        bool disposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;
            disposed = true;
        }
    }    
}
