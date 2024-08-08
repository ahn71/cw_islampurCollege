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
        { }
        public CurrentStdEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }

        public int Insert()
        {
            sql = string.Format("INSERT INTO [dbo].[CurrentStudentInfo] "
                + "([AdmissionNo],[FullName],[ClassID],"
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
                + ",[TADistrict],[MotherTongue],[Nationality],"
                + "[PreviousExamType],[PSCRollNo],[PSCPassingYear],"
                + "[PSCGPA],[PSCBoard],[CertifiedDate],[PreviousSchoolName],"
                + "[PSCJSCRegistration],[TransferCertifiedNo],[PreferredClass],"
                + "[IsActive],[Comments],[BatchID],[BatchName],[Status],[PaymentStatus],"
                + "[FullNameBn],[FatherNameBn],[MotherNameBn],[IdCard],[StdTypeId],[PAPostOfficeID],[TAPostOfficeID],[FatherDesg],[FatherOrg],[FatherPhone],[MotherDesg],[MotherOrg],[MotherPhone],[BusID],[LocationID],[PlaceID],[SSCRoll],[TCCollegeName],[TCClass],[TCSemister],[TCDate],[CreateBy],[CreateOn],[FathersProfessionBn],[MothersProfessionBn],[TAViIlageBn],[PAVillageBn],[ParentsAddress],[ParentsAddressBn],[ParentsPostOfficeId],[ParentsThanaId],[ParentsDistrictId])"
                + " VALUES (" + _Entities.AdmissionNo + ", "
                + "'" + _Entities.FullName + "','" + _Entities.ClassID + "',"
                + "'" + _Entities.ClassName + "','" + _Entities.ClsGrpID + "',"
                + "'" + _Entities.ClsSecID + "','" + _Entities.SectionName + "',"
                + "'" + _Entities.RollNo + "','" + _Entities.Religion + "',"
                + "'" + _Entities.ConfigId + "','" + _Entities.Shift + "',"
                + "'" + _Entities.DateOfBirth?.ToString("yyyy-MM-dd") + "','" + _Entities.Gender + "',"
                + "'" + _Entities.Mobile + "','" + _Entities.Session + "',"
                + "'" + _Entities.BloodGroup + "','" + _Entities.FathersName + "',"
                + "'" + _Entities.FathersProfession + "','" + _Entities.FathersYearlyIncome + "',"
                + "'" + _Entities.FathersMobile + "','" + _Entities.FatherEmail + "',"
                + "'" + _Entities.MothersName + "','" + _Entities.MothersProfession + "',"
                + "'" + _Entities.MothersYearlyIncome + "','" + _Entities.MothersMobile + "',"
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

                + "'" + _Entities.PreviousExamType + "','" + _Entities.PSCRollNo + "',"
                + "'" + _Entities.PSCPassingYear + "','" + _Entities.PSCGPA + "',"
                + "'" + _Entities.PSCBoard + "','" + _Entities.CertifiedDate?.ToString("yyyy-MM-dd") + "',"
                + "'" + _Entities.PreviousSchoolName + "','" + _Entities.PSCJSCRegistration + "',"
                + "'" + _Entities.TransferCertifiedNo + "','" + _Entities.PreferredClass + "',"
                + "'" + _Entities.IsActive + "','" + _Entities.Comments + "',"
                + "'" + _Entities.BatchID + "','" + _Entities.BatchName + "',"
                + "'" + _Entities.Status + "','" + _Entities.PaymentStatus + "',"
                + "N'" + _Entities.FullNameBn + "',N'" + _Entities.FathersNameBn + "',N'" + _Entities.MothersNameBn + "','" + _Entities.IdCard + "','" + _Entities.StdTypeId + "','" + _Entities.PAPostOfficeID + "','" + _Entities.TAPostOfficeID + "','" + _Entities.FatherDesg + "','" + _Entities.FatherOrg + "','" + _Entities.FatherPhone + "','" + _Entities.MotherDesg + "','" + _Entities.MotherOrg + "','" + _Entities.MotherPhone + "','" + _Entities.BusID + "','" + _Entities.LocationID + "','" + _Entities.PlaceID + "','" + _Entities.SSCRoll + "','" + _Entities.TCCollegeName + "','" + _Entities.TCClass + "','" + _Entities.TCSemister + "','" + _Entities.TCDate?.ToString("yyyy-MM-dd") + "','" + _Entities.CreateBy + "','" + _Entities.CreateOn.ToString("yyyy-MM-dd HH:mm:ss") + "',N'" + _Entities.FathersProfessionBn + "',N'" + _Entities.MothersProfessionBn + "',N'" + _Entities.TAViIlageBn + "',N'" + _Entities.PAVillageBn + "','" + _Entities.ParentsAddress + "',N'" + _Entities.ParentsAddressBn + "','" + _Entities.ParentsPostOfficeId + "','" + _Entities.ParentsThanaId + "','" + _Entities.ParentsDistrictId + "');  SELECT SCOPE_IDENTITY()");
            int MaxId = CRUD.GetMaxID(sql);
            if (MaxId > 0)
            {
                result = StdAdmissionInfoInsert(MaxId, _Entities.ClassID, _Entities.ClsSecID, _Entities.RollNo,
                    _Entities.Session, _Entities.AdmissionNo, _Entities.AdmissionDate);
            }
            return MaxId;
        }
        private bool StdAdmissionInfoInsert(int studentId, int classId, int sectionId,
            int? rollNo, string session, long AdmissionNo, DateTime AdmissionDate)
        {
            sql = string.Format("INSERT INTO [dbo].[Tbl_STD_Admission_INFO] " +
                 "([AdmissionNo],[AdmissionDate],[StudentID],[ClassID],[ClsSecID],[RollNo],[StartBatchID],[EndBatchID],[Session])"
                 + " VALUES ( '" + AdmissionNo + "','" + AdmissionDate.ToString("yyyy-MM-dd") + "','" + studentId + "','" +
                 _Entities.ClassID + "','" + sectionId + "','" + rollNo + "',"
                 + "'" + 0 + "','" + 0 + "','" + session + "')");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool updateImageName(int StudentId, string ImageName)
        {
            sql = "Update [CurrentStudentInfo]  Set [ImageName]='" + ImageName + "' Where StudentId=" + StudentId;
            return CRUD.ExecuteQuery(sql);
        }
        public bool CurrentStdInfo_Log()
        {
            sql = string.Format("INSERT INTO [dbo].[CurrentStudent_Log] "
                + "([StudentId],[ClassID],[ClsGrpID],[ClsSecID],[RollNo],"
                + "[ConfigId],[BatchID],[SpendYear]) VALUES ( "
                + "'" + _Entities.StudentID + "','" + _Entities.ClassID + "',"
                + "" + _Entities.ClsGrpID + "," + _Entities.ClsSecID + ","
                + "" + _Entities.RollNo + "," + _Entities.ConfigId + ","
                + "" + _Entities.BatchID + "," + _Entities.SpendYear + ")");
            result = CRUD.ExecuteQuery(sql);
            return result;
        }
        private string nullDate(DateTime? date)
        {

            if (date == null)
                return null;
            else
                return date?.ToString("yyyy-MM-dd");

        }

        public bool Update()
        {
            sql = string.Format("Update [dbo].[CurrentStudentInfo] "
                 + "set "
                 + "[FullName]='" + _Entities.FullName + "',"
                 + "[ClassID]='" + _Entities.ClassID + "',"
                 + "[ClassName]='" + _Entities.ClassName + "',"
                 + "[BatchID]='" + _Entities.BatchID + "',"
                 + "[BatchName]='" + _Entities.BatchName + "',"
                 + "[ClsGrpID]='" + _Entities.ClsGrpID + "',"
                 + "[ClsSecID]='" + _Entities.ClsSecID + "',"
                 + "[SectionName]='" + _Entities.SectionName + "',"
                 + "[RollNo]='" + _Entities.RollNo + "',"
                 + "[Religion]='" + _Entities.Religion + "',"
                 + "[ConfigId]='" + _Entities.ConfigId + "',"
                 + "[Shift]='" + _Entities.Shift + "',"
                 + "[DateOfBirth]='" + _Entities.DateOfBirth?.ToString("yyyy-MM-dd") + "',"
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
                 + "[MothersMoible]='" + _Entities.MothersMobile + "',"
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
                 + "[CertifiedDate]='" + _Entities.CertifiedDate?.ToString("yyyy-MM-dd") + "',"
                 + "[PreviousSchoolName]='" + _Entities.PreviousSchoolName + "',"
                 + "[PSCJSCRegistration]='" + _Entities.PSCJSCRegistration + "',"
                 + "[TransferCertifiedNo]='" + _Entities.TransferCertifiedNo + "',"
                 + "[PreferredClass]='" + _Entities.PreferredClass + "',"
                 + "[Comments]='" + _Entities.Comments + "',"
                 + "[PaymentStatus]='" + _Entities.PaymentStatus + "',"
                 + "[FullNameBn]=N'" + _Entities.FullNameBn + "',"
                 + "[PAPostOfficeID]='" + _Entities.PAPostOfficeID + "',"
                 + "[TAPostOfficeID]='" + _Entities.TAPostOfficeID + "',"
                 + "[FatherNameBn]=N'" + _Entities.FathersNameBn + "',"
                 + "[MotherNameBn]=N'" + _Entities.MothersNameBn + "',"
                 + "[FatherDesg]='" + _Entities.FatherDesg + "',"
                 + "[FatherOrg]='" + _Entities.FatherOrg + "',"
                 + "[FatherPhone]='" + _Entities.FatherPhone + "',"
                 + "[MotherDesg]='" + _Entities.MotherDesg + "',"
                 + "[MotherOrg]='" + _Entities.MotherOrg + "',"
                 + "[MotherPhone]='" + _Entities.MotherPhone + "',"
                 + "[BusID]='" + _Entities.BusID + "',"
                 + "[LocationID]='" + _Entities.LocationID + "',"
                 + "[PlaceID]='" + _Entities.PlaceID + "',"
                 + "[SSCRoll]='" + _Entities.SSCRoll + "',"
                 + "[TCCollegeName]='" + _Entities.TCCollegeName + "',"
                 + "[TCClass]='" + _Entities.TCClass + "',"
                 + "[TCSemister]='" + _Entities.TCSemister + "',"
                 + "[TCDate]='" + _Entities.TCDate?.ToString("yyyy-MM-dd") + "',"
                 + "[IdCard]='" + _Entities.IdCard + "',"
                 + "[StdTypeId]='" + _Entities.StdTypeId + "',"
                 + "[FathersProfessionBn]=N'" + _Entities.FathersProfessionBn + "',"
                 + "[MothersProfessionBn]=N'" + _Entities.MothersProfessionBn + "',"
                 + "[TAViIlageBn]=N'" + _Entities.TAViIlageBn + "',"
                 + "[PAVillageBn]=N'" + _Entities.PAVillageBn + "',"
                 + "[ParentsAddress]='" + _Entities.ParentsAddress + "',"
                 + "[ParentsAddressBn]=N'" + _Entities.ParentsAddressBn + "',"
                 + "[ParentsPostOfficeId]=" + _Entities.ParentsPostOfficeId + ","
                 + "[ParentsThanaId]=" + _Entities.ParentsThanaId + ","
                 + "[ParentsDistrictId]=" + _Entities.ParentsDistrictId + ","
                 + "[UpdateBy]=" + _Entities.UpdateBy + ","
                 + "[UpdateOn]='" + _Entities.UpdateOn.ToString("yyyy-MM-dd HH:mm:ss") + "' "
                 + " where StudentID='" + _Entities.StudentID + "' ");
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
                 + "[AdmissionDate]='" + AdmissionDate.ToString("yyyy-MM-dd") + "',"
                 + "[ClassID]='" + classId + "',"
                 + "[ClsSecID]='" + sectionId + "',"
                 + "[RollNo]='" + rollNo + "',"
                 + "[Session]='" + session + "'"
                 + " where StudentID='" + studentId + "'");
            return result = CRUD.ExecuteQuery(sql);
        }
        public bool UpdateStdStatus(string studentId, string stdStatus)
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

        public bool InsertToActivationLog(string studentId, string batchId, string Note, string ActivationType)
        {
            sql = string.Format("INSERT INTO [dbo].[StudentActivation_Log] (StudentID,BatchID,Note,ActivationType,EntryDate) values(" + studentId + "," + batchId + ",'" + Note + "'," + ActivationType + ",'" + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd HH:mm:ss") + "')");

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
                                        MothersMobile = row["MothersMoible"].ToString(),
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
                                        StdStatus = row["StdStatus"].ToString() == "" ? null : (bool?)bool.Parse(row["StdStatus"].ToString()),
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
                dt = new DataTable();
                sql = string.Format("SELECT AdmissionNo,convert(varchar,RegistrationNo) as RegistrationNo, BatchId,StudentId,ShiftName,ClassName,GroupName,SectionName,FullName,"
                + "RollNo,Gender,GuardianMobileNo,ClsSecId,Format(CreateOn,'dd-MM-yyyy HH:mm:ss') as CreateOn ,CreateBy,FirstName from v_CurrentStudentInfo " + condition + " ORDER BY ShiftName,ClassName,GroupName,SectionName,RollNo");
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
                sql = string.Format("SELECT  StudentId,AdmissionNo, convert(varchar(10),AdmissionDate,105) as  AdmissionDate,FullName,FullNameBn,BatchID,BatchName, ClassName,GroupID,GroupName,SectionID,SectionName,Religion,RollNo,Gender,Session,BloodGroup,IdCard,StdTypeName, convert(varchar(10),DateOfBirth,105) as  DateOfBirth, BloodGroup, case when mobile='+88' then '' else right(mobile,11) end Mobile, ImageName, FathersName,FatherNameBn,FathersProfession,FatherEmail,FatherDesg,FatherOrg,FatherPhone,FathersYearlyIncome,MothersName,MotherNameBn, MothersProfession,MotherEmail,MotherDesg,MotherOrg,MothersYearlyIncome,MotherPhone,FathersMobile, MothersMoible, HomePhone, PAVillage, PAPostOfficeName, PAThana, PADistrict, TAViIlage, TAPostOfficeName, TAThana, TADistrict, GuardianName, GuardianRelation, GuardianMobileNo, GuardianAddress, MotherTongue, Nationality,PreviousExamType,PSCBoard,PSCPassingYear,PSCJSCRegistration,PreviousSchoolName,PSCRollNo,SSCRoll,PSCGPA,TCCollegeName,TCClass,TCSemister,convert(varchar(11),TCDate,106) as TCDate, BusName,LocationName,PlaceName,BatchName,ConfigId,ShiftName,IdCard,Year,Session FROM v_CurrentStudentInfo " + condition + " Order By ShiftName,ClassName,RollNo ");
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
                sql = string.Format("SELECT SectionName as Section,ConfigId,ShiftName,BatchID,BatchName,FullName as Name,"
                + "RollNo as Roll,case when mobile='+88' then '' else right(mobile,11) end Mobile, GroupID,GroupName,SectionID, ClassName as Class FROM v_CurrentStudentInfo " + condition + " Order By ClassName,SectionName,RollNo ");
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
                sql = string.Format("SELECT ClassName,SectionName,FullName,RollNo,case when mobile='+88' then '' else right,"
                    + "(mobile,11) end Mobile, case when GuardianMobileNo='+88' then '' else right(GuardianMobileNo,11) end"
                    + "GuardianMobileNo  AS HomePhone,BatchName FROM "
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
                sql = string.Format("SELECT ClassName,SectionID,SectionName,FullName,RollNo,BatchID,BatchName,GroupID,GroupName, "
                    + "ConfigId,ShiftName, GuardianName,GuardianRelation,case when GuardianMobileNo='+88' then '' else right"
                    + "(GuardianMobileNo,11) end GuardianMobileNo,GuardianAddress FROM "
                      + "v_CurrentStudentInfo  " + condition + " Order By ClassName,GroupName,SectionName,RollNo ");
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
                sql = string.Format("select SectionName,FullName,RollNo,ConfigId,ShiftName,FathersName,"
                    + "FathersProfession,MothersName,BatchID,BatchName,GroupID,GroupName,SectionID,"
                    + "case when FathersMobile='+88' then '' else right(FathersMobile,11) end FathersMobile,"
                    + "case when MothersMoible='+88' then '' else right(MothersMoible,11) end MothersMoible,"
                    + "MothersProfession from v_CurrentStudentInfo  " + condition + " Order By ClassName,GroupName,SectionName,RollNo ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public DataTable GetAllContactInfo(string condition)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("select RollNo, FullName, BatchName,ShiftName as Shift,"
                + "case when mobile='+88' then '' else right(mobile,11) end Mobile,"
                + "case when FathersMobile='+88' then '' else right(FathersMobile,11) end FathersMobile,"
                + "case when GuardianMobileNo='+88' then '' else right(GuardianMobileNo,11) end GuardianMobileNo,"
                + "case when MothersMoible='+88' then '' else right(MothersMoible,11) end MothersMoible,"
                + "SectionName, GroupID, GroupName, SectionID, BatchID, ShiftID from v_CurrentStudentInfo  " + condition + " Order By RollNo ");
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
                + "SectionName,ClassName FROM v_CurrentStudentInfo  " + condition + " Order By ClassName ");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public void GetRollNo(DropDownList dl, string shiftID, string batchID, string clsgrpID, string clssectionID)
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
                    + clssectionID + "'ORDER BY RollNo ");
            }
            dt = CRUD.ReturnTableNull(sql);
            dl.DataSource = dt;
            dl.DataTextField = "RollNo";
            dl.DataValueField = "StudentId";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public void GetRollNoCondition(DropDownList dl, string condition)
        {

            sql = string.Format("Select  RollNo, StudentId from v_CurrentStudentInfo " + condition + " ");

            dt = CRUD.ReturnTableNull(sql);
            dl.DataSource = dt;
            dl.DataTextField = "RollNo";
            dl.DataValueField = "StudentId";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public void GetRollNo(DropDownList dl, string shiftID, string batchID, string clsgrpID, string clssectionID, string stdtype)
        {
            if (clssectionID == "All")
            {
                sql = string.Format("Select  RollNo, StudentId from v_CurrentStudentInfo WHERE ConfigId='"
                    + shiftID + "' AND  BatchID='" + batchID + "' AND ClsGrpID='" + clsgrpID + "' and StdTypeId='" + stdtype + "'  ORDER BY RollNo ");
            }
            else
            {
                sql = string.Format("Select  RollNo, StudentId from v_CurrentStudentInfo WHERE ConfigId='"
                    + shiftID + "' AND  BatchID='" + batchID + "' AND ClsGrpID='" + clsgrpID + "' AND ClsSecID='"
                    + clssectionID + "' and StdTypeId='" + stdtype + "' ORDER BY RollNo ");
            }
            dt = CRUD.ReturnTableNull(sql);
            dl.DataSource = dt;
            dl.DataTextField = "RollNo";
            dl.DataValueField = "StudentId";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public string GetSearchCondition(string shiftID, string batchID, string groupID, string sectionID)
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
        public string GetSearchCondition2(string shiftID, string batchID, string groupID, string sectionID)
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
                condition = " WHERE ShiftId='" + shiftID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE ShiftId='" + shiftID + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE ShiftId='" + shiftID + "' AND ClsGrpID='" + groupID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE ShiftId='" + shiftID + "' AND ClsGrpID='"
                + groupID + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID != "All" && groupID == "All" && sectionID == "All")
            {
                condition = " WHERE  ShiftId='" + shiftID + "' AND BatchID='" + BatchClsID[0] + "' ";
            }
            else if (shiftID != "All" && batchID != "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE ShiftId='" + shiftID + "' AND BatchID='"
                + BatchClsID[0] + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID != "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE ShiftId='" + shiftID + "' AND BatchID='"
                + BatchClsID[0] + "' AND ClsGrpID='" + groupID + "' ";
            }
            else if (shiftID != "All" && batchID != "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE ShiftId='" + shiftID + "' AND BatchID='" + BatchClsID[0]
                + "' AND ClsGrpID='" + groupID + "' AND ClsSecID='" + sectionID + "' ";
            }
            return condition;
        }
        public string GetSearchCondition3(string shiftID, string batchID, string groupID, string sectionID)
        {
            string condition = "";
            string[] BatchClsID = batchID.Split('_');
            if (shiftID == "All" && batchID == "All" && groupID == "All" && sectionID == "All")
            {
                condition = "";
            }
            else if (shiftID == "All" && batchID == "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE DailyAttendanceRecord.ClsSecID='" + sectionID + "'";
            }
            else if (shiftID == "All" && batchID == "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE DailyAttendanceRecord.ClsGrpID='" + groupID + "'";
            }
            else if (shiftID == "All" && batchID == "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE DailyAttendanceRecord.ClsGrpID='" + groupID + "' AND DailyAttendanceRecord.ClsSecID='" + sectionID + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID == "All" && sectionID == "All")
            {
                condition = " WHERE DailyAttendanceRecord.BatchID='" + BatchClsID[0] + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE DailyAttendanceRecord.BatchID='" + BatchClsID[0] + "' AND DailyAttendanceRecord.ClsSecID='" + sectionID + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE DailyAttendanceRecord.BatchID='" + BatchClsID[0] + "' AND DailyAttendanceRecord.ClsGrpID='" + groupID + "'";
            }
            else if (shiftID == "All" && batchID != "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE DailyAttendanceRecord.BatchID='" + BatchClsID[0] + "' AND DailyAttendanceRecord.ClsGrpID='"
                + groupID + "' AND DailyAttendanceRecord.ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID == "All" && sectionID == "All")
            {
                condition = " WHERE DailyAttendanceRecord.ShiftId='" + shiftID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE DailyAttendanceRecord.ShiftId='" + shiftID + "' AND DailyAttendanceRecord.ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE DailyAttendanceRecord.ShiftId='" + shiftID + "' AND DailyAttendanceRecord.ClsGrpID='" + groupID + "'";
            }
            else if (shiftID != "All" && batchID == "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE DailyAttendanceRecord.ShiftId='" + shiftID + "' AND DailyAttendanceRecord.ClsGrpID='"
                + groupID + "' AND DailyAttendanceRecord.ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID != "All" && groupID == "All" && sectionID == "All")
            {
                condition = " WHERE  DailyAttendanceRecord.ShiftId='" + shiftID + "' AND DailyAttendanceRecord.BatchID='" + BatchClsID[0] + "' ";
            }
            else if (shiftID != "All" && batchID != "All" && groupID == "All" && sectionID != "All")
            {
                condition = " WHERE DailyAttendanceRecord.ShiftId='" + shiftID + "' AND BatchID='"
                + BatchClsID[0] + "' AND ClsSecID='" + sectionID + "'";
            }
            else if (shiftID != "All" && batchID != "All" && groupID != "All" && sectionID == "All")
            {
                condition = " WHERE DailyAttendanceRecord.ShiftId='" + shiftID + "' AND DailyAttendanceRecord.BatchID='"
                + BatchClsID[0] + "' AND DailyAttendanceRecord.ClsGrpID='" + groupID + "' ";
            }
            else if (shiftID != "All" && batchID != "All" && groupID != "All" && sectionID != "All")
            {
                condition = " WHERE DailyAttendanceRecord.ShiftId='" + shiftID + "' AND DailyAttendanceRecord.BatchID='" + BatchClsID[0]
                + "' AND DailyAttendanceRecord.ClsGrpID='" + groupID + "' AND DailyAttendanceRecord.ClsSecID='" + sectionID + "' ";
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
            sql = string.Format("SELECT FullName FROM CurrentStudentInfo WHERE StudentId='" + stdID + "'");
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
        public DataTable GetUnassignStudentList(string shiftId, string batchId, string clsgrpId, string clssecId)
        {
            try
            {
                sql = string.Format("SELECT StudentId, FullName,RollNo,Gender,Mobile FROM v_Guide_Teacher WHERE BatchID='" + batchId + "' "
                + "AND ConfigId='" + shiftId + "' AND ClsGrpID='" + clsgrpId + "' AND ClsSecID='" + clssecId + "' AND GuideStatus is null");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt; }
        }
        public DataTable GetUnassignStudentListM(string shiftId, string batchId, string clsgrpId, string clssecId)
        {
            try
            {
                sql = string.Format("SELECT StudentId, FullName,RollNo,Gender,Mobile FROM v_CurrentStudentInfo WHERE BatchID='" + batchId + "' "
                + "AND ConfigId='" + shiftId + "' AND ClsGrpID='" + clsgrpId + "' AND ClsSecID='" + clssecId + "' Order By RollNo");
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
            if (CurrentStdList != null)
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
        public DataTable GetStudentContacts_WS(string RollNo) // Get Student Data for web site
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT ClassName, SectionName,GroupName, RollNo, FullName, ImageName, " +
                                    "  ShiftName,StdStatus FROM v_CurrentStudentInfo where RollNo ='" + RollNo + "'");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public DataTable GetStudentInfoByAdmissionNo(string AdmissionNo) // Get Student Data for web site
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT ClassName,SectionName,GroupName, RollNo, FullName, ImageName,ShiftName,StdStatus,FathersName,MothersName,Session,Religion,convert(varchar(10),DateOfBirth,105) as DateOfBirth,BatchId,ClassID,ClsGrpId,StudentId FROM v_CurrentStudentInfo where IsActive=1 and AdmissionNo ='" + AdmissionNo + "'");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public DataTable GetStudentInfoByAdmissionNo(string BatchName, string AdmissionNo) // Get Student Data for web site
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT  ClassName,SectionName,GroupName, RollNo, FullName, ImageName,ShiftName,StdStatus,FathersName,MothersName,convert(varchar(4),Year)+'-'+convert(varchar(4),Year+1) as Session,Religion,convert(varchar(10),DateOfBirth,105) as DateOfBirth,BatchId,ClassID,ClsGrpId,StudentId FROM v_CurrentStudentInfo where  BatchName='" + BatchName +
                    "' and ( AdmissionNo ='" + AdmissionNo + "' or SSCRoll='" + AdmissionNo + "' or PSCRollNo='" + AdmissionNo + "')");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public bool RollValidation(string shiftID, string batchID, string clsgrpID, string clssecID, string stdID, string rollNo)
        {
            try
            {
                dt = CRUD.ReturnTableNull("SELECT RollNo FROM CurrentStudentInfo WHERE StudentId!='"
                    + stdID + "' And RollNo='" + rollNo + "' AND BatchID='" + batchID + "' AND ClsSecID='"
                    + clssecID + "' AND ClsGrpID='" + clsgrpID + "' and ConfigId='" + shiftID + "'");
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
        public DataTable LoadGroupStudentList(string batchID, string clsGroupID)
        {
            try
            {
                dt = new DataTable();
                sql = string.Format("SELECT SectionName, RollNo, FullName, StudentID  "
                + "FROM v_CurrentStudentInfo where batchID ='" + batchID + "' AND clsgrpID='" + clsGroupID + "' ORDER BY RollNo");
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return dt = null; }
        }
        public DataTable GetTransferCertificate(string shiftID, string batchID, string clsgrpID, string clssecID, string stdID, string rollNo)
        {
            try
            {
                dt = CRUD.ReturnTableNull("SELECT RollNo,FullName,CONVERT(varchar, DateOfBirth, 106) as DateOfBirth,FathersName,PAVillage,PAPostOffice,PADistrict,PAThana,CONVERT(varchar, AdmissionDate, 106) as AdmissionDate,ClassName FROM v_CurrentStudentInfo WHERE StudentId='"
                    + stdID + "' And RollNo='" + rollNo + "' AND BatchID='" + batchID + "' AND ClsSecID='"
                    + clssecID + "' AND ClsGrpID='" + clsgrpID + "' and ConfigId='" + shiftID + "'");
                return dt;
            }
            catch { return dt = null; }
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
