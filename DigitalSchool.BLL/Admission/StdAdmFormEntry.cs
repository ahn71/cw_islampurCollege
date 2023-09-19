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
   public class StdAdmFormEntry : IDisposable
    {
        private AdmStdFormInfoEntities _Entities;
        static List<AdmStdFormInfoEntities> AdmStdInfoList;
        string query = string.Empty;
        bool result = false;
        DataTable dt;
        static DataTable dt_list;
        public AdmStdFormInfoEntities AddEntities
        {
            set
            {
                _Entities = value;
            }
        }
        public int Insert(bool HSCInfo,bool HonoursInfo)
        {
            if (HonoursInfo)
            {
                query = string.Format(@"INSERT INTO [dbo].[Student_AdmissionFormInfo]
           ([AdmissionFormNo],[AdmissionDate],[FullName],[FullNameBn],[ClassID],[ClsGrpID],[ClsSecID],[Gender],[Religion],[ShiftId],[DateOfBirth],[Mobile],[BloodGroup],[Session],[FathersName],[FathersNameBn],[FathersProfession],[FathersProfessionBn],[FathersMobile],[MothersName],[MothersNameBn],[MothersProfession],[MothersProfessionBn],[MothersMobile],[ParentsAddress],[ParentsAddressBn],[ParentsPostOfficeId],[ParentsThanaId],[ParentsDistrictId],[GuardianName],[GuardianRelation],[GuardianMobileNo],[GuardianAddress],[PermanentAddress],[PermanentAddressBn],[PermanentPostOfficeId],[PermanentThanaId],[PermanentDistrictId],[PresentAddress],[PresentAddressBn],[PresentPostOfficeId],[PresentThanaId],[PresentDistrictId],[PreSCBoard],[PreSCPassingYear],[PreSCRegistration],[PreSCRollNo],[PreSCGPA],[PreSchoolName],[TCCollege],[TCDate],[ImageName],[IsActive],[CreateOn],[MoneyReceiptNo],[AdmissionYear],[NUAdmissionRoll],[PreSCBoardHSC],[PreSCPassingYearHSC],[PreSCRegistrationHSC],[PreSCRollNoHSC],[PreSCGPAHSC],[PreSchoolNameHSC],[PreSCBoardHonours],[PreSCPassingYearHonours],[PreSCRegistrationHonours],[PreSCRollNoHonours],[PreSCGPAHonours],[PreSchoolNameHonours])
     VALUES
           (" + _Entities.AdmissionFormNo + ",'" + _Entities.AdmissionDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + _Entities.FullName + "',N'" + _Entities.FullNameBn + "'," + _Entities.ClassID + "," + _Entities.ClsGrpID + "," + _Entities.ClsSecID + ",'" + _Entities.Gender + "','" + _Entities.Religion + "'," + _Entities.ShiftId + ",'" + _Entities.DateOfBirth.ToString("yyyy-MM-dd") + "','" + _Entities.Mobile + "','" + _Entities.BloodGroup + "','" + _Entities.Session + "','" + _Entities.FathersName + "',N'" + _Entities.FathersNameBn + "','" + _Entities.FathersProfession + "',N'" + _Entities.FathersProfessionBn + "','" + _Entities.FathersMobile + "','" + _Entities.MothersName + "',N'" + _Entities.MothersNameBn + "','" + _Entities.MothersProfession + "',N'" + _Entities.MothersProfessionBn + "','" + _Entities.MothersMobile + "','" + _Entities.ParentsAddress + "',N'" + _Entities.ParentsAddressBn + "'," + _Entities.ParentsPostOfficeId + "," + _Entities.ParentsThanaId + "," + _Entities.ParentsDistrictId + ",'" + _Entities.GuardianName + "','" + _Entities.GuardianRelation + "','" + _Entities.GuardianMobileNo + "','" + _Entities.GuardianAddress + "','" + _Entities.PermanentAddress + "',N'" + _Entities.PermanentAddressBn + "'," + _Entities.PermanentPostOfficeId + "," + _Entities.PermanentThanaId + "," + _Entities.PermanentDistrictId + ",'" + _Entities.PresentAddress + "',N'" + _Entities.PresentAddressBn + "'," + _Entities.PresentPostOfficeId + "," + _Entities.PresentThanaId + "," + _Entities.PresentDistrictId + ",'" + _Entities.PreSCBoard + "'," + _Entities.PreSCPassingYear + ",'" + _Entities.PreSCRegistration + "','" + _Entities.PreSCRollNo + "','" + _Entities.PreSCGPA + "','" + _Entities.PreSchoolName + "','" + _Entities.TCCollege + "','" + _Entities.TCDate + "','" + _Entities.ImageName + "','" + _Entities.IsActive.ToString() + "','" + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd HH:mm:ss") + "','" + _Entities.MoneyReceiptNo + "'," + _Entities.AdmissionYear + ",'" + _Entities.NUAdmissionRoll + "','" + _Entities.PreSCBoardHSC + "'," + _Entities.PreSCPassingYearHSC + ",'" + _Entities.PreSCRegistrationHSC + "','" + _Entities.PreSCRollNoHSC + "','" + _Entities.PreSCGPAHSC + "','" + _Entities.PreSchoolNameHSC + "','" + _Entities.PreSCBoardHonours + "'," + _Entities.PreSCPassingYearHonours + ",'" + _Entities.PreSCRegistrationHonours + "','" + _Entities.PreSCRollNoHonours + "','" + _Entities.PreSCGPAHonours + "','" + _Entities.PreSchoolNameHonours + "'); " +
           " SELECT SCOPE_IDENTITY()");

            }
            else if (HSCInfo)
            {
                query = string.Format(@"INSERT INTO [dbo].[Student_AdmissionFormInfo]
           ([AdmissionFormNo],[AdmissionDate],[FullName],[FullNameBn],[ClassID],[ClsGrpID],[ClsSecID],[Gender],[Religion],[ShiftId],[DateOfBirth],[Mobile],[BloodGroup],[Session],[FathersName],[FathersNameBn],[FathersProfession],[FathersProfessionBn],[FathersMobile],[MothersName],[MothersNameBn],[MothersProfession],[MothersProfessionBn],[MothersMobile],[ParentsAddress],[ParentsAddressBn],[ParentsPostOfficeId],[ParentsThanaId],[ParentsDistrictId],[GuardianName],[GuardianRelation],[GuardianMobileNo],[GuardianAddress],[PermanentAddress],[PermanentAddressBn],[PermanentPostOfficeId],[PermanentThanaId],[PermanentDistrictId],[PresentAddress],[PresentAddressBn],[PresentPostOfficeId],[PresentThanaId],[PresentDistrictId],[PreSCBoard],[PreSCPassingYear],[PreSCRegistration],[PreSCRollNo],[PreSCGPA],[PreSchoolName],[TCCollege],[TCDate],[ImageName],[IsActive],[CreateOn],[MoneyReceiptNo],[AdmissionYear],[NUAdmissionRoll],[PreSCBoardHSC],[PreSCPassingYearHSC],[PreSCRegistrationHSC],[PreSCRollNoHSC],[PreSCGPAHSC],[PreSchoolNameHSC])
     VALUES
           (" + _Entities.AdmissionFormNo + ",'" + _Entities.AdmissionDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + _Entities.FullName + "',N'" + _Entities.FullNameBn + "'," + _Entities.ClassID + "," + _Entities.ClsGrpID + "," + _Entities.ClsSecID + ",'" + _Entities.Gender + "','" + _Entities.Religion + "'," + _Entities.ShiftId + ",'" + _Entities.DateOfBirth.ToString("yyyy-MM-dd") + "','" + _Entities.Mobile + "','" + _Entities.BloodGroup + "','" + _Entities.Session + "','" + _Entities.FathersName + "',N'" + _Entities.FathersNameBn + "','" + _Entities.FathersProfession + "',N'" + _Entities.FathersProfessionBn + "','" + _Entities.FathersMobile + "','" + _Entities.MothersName + "',N'" + _Entities.MothersNameBn + "','" + _Entities.MothersProfession + "',N'" + _Entities.MothersProfessionBn + "','" + _Entities.MothersMobile + "','" + _Entities.ParentsAddress + "',N'" + _Entities.ParentsAddressBn + "'," + _Entities.ParentsPostOfficeId + "," + _Entities.ParentsThanaId + "," + _Entities.ParentsDistrictId + ",'" + _Entities.GuardianName + "','" + _Entities.GuardianRelation + "','" + _Entities.GuardianMobileNo + "','" + _Entities.GuardianAddress + "','" + _Entities.PermanentAddress + "',N'" + _Entities.PermanentAddressBn + "'," + _Entities.PermanentPostOfficeId + "," + _Entities.PermanentThanaId + "," + _Entities.PermanentDistrictId + ",'" + _Entities.PresentAddress + "',N'" + _Entities.PresentAddressBn + "'," + _Entities.PresentPostOfficeId + "," + _Entities.PresentThanaId + "," + _Entities.PresentDistrictId + ",'" + _Entities.PreSCBoard + "'," + _Entities.PreSCPassingYear + ",'" + _Entities.PreSCRegistration + "','" + _Entities.PreSCRollNo + "','" + _Entities.PreSCGPA + "','" + _Entities.PreSchoolName + "','" + _Entities.TCCollege + "','" + _Entities.TCDate + "','" + _Entities.ImageName + "','" + _Entities.IsActive.ToString() + "','" + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd HH:mm:ss") + "','" + _Entities.MoneyReceiptNo + "'," + _Entities.AdmissionYear + ",'" + _Entities.NUAdmissionRoll + "','" + _Entities.PreSCBoardHSC + "'," + _Entities.PreSCPassingYearHSC + ",'" + _Entities.PreSCRegistrationHSC + "','" + _Entities.PreSCRollNoHSC + "','" + _Entities.PreSCGPAHSC + "','" + _Entities.PreSchoolNameHSC + "'); " +
           " SELECT SCOPE_IDENTITY()");
            }
            else
            {
                query = string.Format(@"INSERT INTO [dbo].[Student_AdmissionFormInfo]
           ([AdmissionFormNo],[AdmissionDate],[FullName],[FullNameBn],[ClassID],[ClsGrpID],[ClsSecID],[Gender],[Religion],[ShiftId],[DateOfBirth],[Mobile],[BloodGroup],[Session],[FathersName],[FathersNameBn],[FathersProfession],[FathersProfessionBn],[FathersMobile],[MothersName],[MothersNameBn],[MothersProfession],[MothersProfessionBn],[MothersMobile],[ParentsAddress],[ParentsAddressBn],[ParentsPostOfficeId],[ParentsThanaId],[ParentsDistrictId],[GuardianName],[GuardianRelation],[GuardianMobileNo],[GuardianAddress],[PermanentAddress],[PermanentAddressBn],[PermanentPostOfficeId],[PermanentThanaId],[PermanentDistrictId],[PresentAddress],[PresentAddressBn],[PresentPostOfficeId],[PresentThanaId],[PresentDistrictId],[PreSCBoard],[PreSCPassingYear],[PreSCRegistration],[PreSCRollNo],[PreSCGPA],[PreSchoolName],[TCCollege],[TCDate],[ImageName],[IsActive],[CreateOn],[MoneyReceiptNo],[AdmissionYear],[NUAdmissionRoll],[ManSubId],[OpSubId])
     VALUES
           (" + _Entities.AdmissionFormNo + ",'" + _Entities.AdmissionDate.ToString("yyyy-MM-dd HH:mm:ss") + "','" + _Entities.FullName + "',N'" + _Entities.FullNameBn + "'," + _Entities.ClassID + "," + _Entities.ClsGrpID + "," + _Entities.ClsSecID + ",'" + _Entities.Gender + "','" + _Entities.Religion + "'," + _Entities.ShiftId + ",'" + _Entities.DateOfBirth.ToString("yyyy-MM-dd") + "','" + _Entities.Mobile + "','" + _Entities.BloodGroup + "','" + _Entities.Session + "','" + _Entities.FathersName + "',N'" + _Entities.FathersNameBn + "','" + _Entities.FathersProfession + "',N'" + _Entities.FathersProfessionBn + "','" + _Entities.FathersMobile + "','" + _Entities.MothersName + "',N'" + _Entities.MothersNameBn + "','" + _Entities.MothersProfession + "',N'" + _Entities.MothersProfessionBn + "','" + _Entities.MothersMobile + "','" + _Entities.ParentsAddress + "',N'" + _Entities.ParentsAddressBn + "'," + _Entities.ParentsPostOfficeId + "," + _Entities.ParentsThanaId + "," + _Entities.ParentsDistrictId + ",'" + _Entities.GuardianName + "','" + _Entities.GuardianRelation + "','" + _Entities.GuardianMobileNo + "','" + _Entities.GuardianAddress + "','" + _Entities.PermanentAddress + "',N'" + _Entities.PermanentAddressBn + "'," + _Entities.PermanentPostOfficeId + "," + _Entities.PermanentThanaId + "," + _Entities.PermanentDistrictId + ",'" + _Entities.PresentAddress + "',N'" + _Entities.PresentAddressBn + "'," + _Entities.PresentPostOfficeId + "," + _Entities.PresentThanaId + "," + _Entities.PresentDistrictId + ",'" + _Entities.PreSCBoard + "'," + _Entities.PreSCPassingYear + ",'" + _Entities.PreSCRegistration + "','" + _Entities.PreSCRollNo + "','" + _Entities.PreSCGPA + "','" + _Entities.PreSchoolName + "','" + _Entities.TCCollege + "','" + _Entities.TCDate + "','" + _Entities.ImageName + "','" + _Entities.IsActive.ToString() + "','" + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd HH:mm:ss") + "','" + _Entities.MoneyReceiptNo + "'," + _Entities.AdmissionYear + ",'" + _Entities.NUAdmissionRoll + "','"+_Entities.ManSubIds+"','"+_Entities.OptSubId+"'); " +
           " SELECT SCOPE_IDENTITY()");
            }            
            return CRUD.GetMaxID(query);
           
        }
        public int Insert(int AdmissionFormNo,string AdmissionDate)
        {
            query = string.Format(@"INSERT INTO [dbo].[Student_AdmissionFormInfo]
           ([AdmissionFormNo],[AdmissionDate])
     VALUES
           ("+ AdmissionFormNo + ",'"+ AdmissionDate + "'); " +
           " SELECT SCOPE_IDENTITY()");
            return CRUD.GetMaxID(query);

        }
        public bool delete(string SL)
        {
            query = "Delete [Student_AdmissionFormInfo] Where SL=" + SL;
            return CRUD.ExecuteQuery(query);
        }
        public bool  updateImageName(string SL, string ImageName)
        {
            query= "Update [Student_AdmissionFormInfo]  Set [ImageName]='"+ ImageName + "' Where SL="+SL;
           return CRUD.ExecuteQuery(query);
        }
        public bool  updateApprovalInfo(string SL, string IsActive,string IsAppoved,string AppovedBy,string AppovedTime,string Note)
        {
            query= "update Student_AdmissionFormInfo set [IsActive]="+ IsActive + ",[IsAppoved]="+ IsAppoved + ",[AppovedBy]='"+ AppovedBy + "',[AppovedTime]='"+ AppovedTime + "',Note=N'"+Note+"' Where SL=" + SL;
           return CRUD.ExecuteQuery(query);
        }
        public static DataTable getAdmissionFormInfo(string SL)
        {
          

            dt_list = new DataTable();
            dt_list = CRUD.ReturnTableNull(@"select SL,AdmissionYear,AdmissionFormNo,convert(varchar(10),AdmissionDate,105) as AdmissionDate,FullName,FullNameBn,adm.ClassId,c.ClassName,adm.ClsGrpID,cgs.GroupName,adm.ClsSecID,cgs.SectionName,Gender,Religion,adm.ShiftId,sft.ShiftName,convert(varchar(10),DateOfBirth,105) as DateOfBirth,Mobile,case when BloodGroup='0' then '' else BloodGroup end as BloodGroup,FathersName,FathersNameBn,FathersProfession,FathersProfessionBn,FathersMobile,MothersName,MothersNameBn,MothersProfession,MothersProfessionBn,MothersMobile,ParentsAddress,ParentsAddressBn,GuardianName,GuardianRelation,GuardianMobileNo,GuardianAddress,PermanentAddress,PermanentAddressBn,PermanentPostOfficeId,PermanentThanaId,PermanentDistrictId,PresentAddress,PresentAddressBn,PresentPostOfficeId,PresentThanaId,PresentDistrictId,b.BoardName,PreSCPassingYear,PreSCRegistration,PreSCRollNo,PreSCGPA,PreSchoolName,TCCollege,case when convert(varchar(10),TCDate,120)='1900-01-01' then '' else convert(varchar(10),TCDate,105) end as TCDate ,pPO.PostOfficeName as ParentsPostOffice,pPO.PostOfficeNameBn as ParentsPostOfficeBn,pT.ThanaName as ParentsThana,pT.ThanaNameBn as ParentsThanaBn,pD.DistrictName as  ParentsDistrict,pD.DistrictNameBn as  ParentsDistrictBn,
              adm.ManSubId,ns.Subname + ' ' + '(' + CAST(cs.SubCode AS NVARCHAR(10)) + ')' as Subname

             , pePO.PostOfficeName as PermanentPostOffice, pePO.PostOfficeNameBn as PermanentPostOfficeBn, peT.ThanaName as PermanentThana, peT.ThanaNameBn as PermanentThanaBn, peD.DistrictName as PermanentDistrict, peD.DistrictNameBn as PermanentDistrictBn

             , prPO.PostOfficeName as PresentPostOffice, prPO.PostOfficeNameBn as PresentPostOfficeBn, prT.ThanaName as PresentThana, prT.ThanaNameBn as PresentThanaBn, prD.DistrictName as PresentDistrict, prD.DistrictNameBn as PresentDistrictBn,ImageName,MoneyReceiptNo,NuAdmissionRoll,bHSC.BoardName as BoardNameHSC,PreSCPassingYearHSC,PreSCRegistrationHSC,PreSCRollNoHSC,PreSCGPAHSC,PreSchoolNameHSC,bHnr.BoardName as BoardNameHonours,PreSCPassingYearHonours,PreSCRegistrationHonours,PreSCRollNoHonours,PreSCGPAHonours,PreSchoolNameHonours

              from Student_AdmissionFormInfo adm inner join Classes c on adm.ClassID = c.ClassID inner join v_Class_Group_Section cgs on adm.ClsGrpID = cgs.ClsGrpID and adm.ClsSecID = cgs.ClsSecID inner join ShiftConfiguration sft on adm.ShiftId = sft.ConfigId left join Post_Office pPO on adm.ParentsPostOfficeId = pPO.PostOfficeID left join Thanas pT on adm.ParentsThanaId = pT.ThanaId left join Distritcts pD on adm.ParentsDistrictId = pD.DistrictId

               left join Post_Office pePO on adm.PermanentPostOfficeId = pePO.PostOfficeID left join Thanas peT on adm.PermanentThanaId = peT.ThanaId left join Distritcts peD on adm.PermanentDistrictId = peD.DistrictId

               left join Post_Office prPO on adm.PresentPostOfficeId = prPO.PostOfficeID left join Thanas prT on adm.PresentThanaId = prT.ThanaId left join Distritcts prD on adm.PresentDistrictId = prD.DistrictId
               left join Boards b on adm.PreSCBoard = b.BoardId left join Boards bHSC on adm.PreSCBoardHSC = bHSC.BoardId left join Boards bHnr on adm.PreSCBoardHonours = bHnr.BoardId 
                 left join NewSubject ns on adm.OpSubId=ns.SubId
                 left join ClassSubject  cs on ns.SubId=cs.SubId

               where adm.SL =" + SL);
            return dt_list;
        }

        public static DataTable getAdmissionFormInfoForApproval(string SL)
        {


            dt_list = new DataTable();
            dt_list = CRUD.ReturnTableNull(@"SELECT [SL]
      ,[AdmissionFormNo]
      ,format([AdmissionDate],'yyyy-MM-dd HH:mm:ss') as [AdmissionDate]
      ,[FullName]
      ,[FullNameBn]
      ,a.[ClassID]
	  ,b.[BatchId]
      ,[ClsGrpID]
      ,[ClsSecID]
      ,[Gender]
      ,[Religion]
      ,[ShiftId]
      ,format([DateOfBirth],'yyyy-MM-dd') as [DateOfBirth]
      ,[Mobile]
      ,[BloodGroup]
      ,[Session]
      ,[FathersName]
      ,[FathersNameBn]
      ,[FathersProfession]
      ,[FathersProfessionBn]
      ,[FathersMobile]
      ,[MothersName]
      ,[MothersNameBn]
      ,[MothersProfession]
      ,[MothersProfessionBn]
      ,[MothersMobile]
      ,[ParentsAddress]
      ,[ParentsAddressBn]
      ,[ParentsPostOfficeId]
      ,[ParentsThanaId]
      ,[ParentsDistrictId]
      ,[GuardianName]
      ,[GuardianRelation]
      ,[GuardianMobileNo]
      ,[GuardianAddress]
      ,[PermanentAddress]
      ,[PermanentAddressBn]
      ,[PermanentPostOfficeId]
      ,[PermanentThanaId]
      ,[PermanentDistrictId]
      ,[PresentAddress]
      ,[PresentAddressBn]
      ,[PresentPostOfficeId]
      ,[PresentThanaId]
      ,[PresentDistrictId]
      ,[PreSCBoard]
      ,[PreSCPassingYear]
      ,[PreSCRegistration]
      ,[PreSCRollNo]
      ,[PreSCGPA]
      ,[PreSchoolName]
      ,[TCCollege]
      ,case when [TCDate]='1900-01-01'then '' else convert(varchar(10),[TCDate],120) end as [TCDate]
      ,[ImageName]
      ,[IsActive]
      ,[IsAppoved]
      ,[CreateOn]
      ,[UpdateOn]
      ,[AppovedBy]
      ,[AppovedTime]
      ,[Note]
      ,[MoneyReceiptNo]
       FROM [dbo].[Student_AdmissionFormInfo] a left join BatchInfo b on a.ClassID=b.ClassID and  a.AdmissionYear=b.Year where a.SL =" + SL);
       return dt_list;
        }
        public static  DataTable getAdmissionList(string condition)
        {

            dt_list = new DataTable();
            dt_list = CRUD.ReturnTableNull(@"select SL,adm.AdmissionFormNo,convert(varchar(10),AdmissionDate,105) as AdmissionDate,FullName,FullNameBn,c.ClassName,cgs.GroupName,cgs.SectionName,Gender,Religion,sft.ShiftName,convert(varchar(10),DateOfBirth,105) as DateOfBirth,Mobile,BloodGroup,FathersName,FathersNameBn,FathersProfession,FathersProfessionBn,FathersMobile,MothersName,MothersNameBn,MothersProfession,MothersProfessionBn,MothersMobile,ParentsAddress,ParentsAddressBn,GuardianName,GuardianRelation,GuardianMobileNo,GuardianAddress,PermanentAddress,PermanentAddressBn,PermanentPostOfficeId,PermanentThanaId,PermanentDistrictId,PresentAddress,PresentAddressBn,PresentPostOfficeId,PresentThanaId,PresentDistrictId,b.BoardName,PreSCPassingYear,PreSCRegistration,PreSCRollNo,PreSCGPA,PreSchoolName,TCCollege,case when convert(varchar(10),TCDate,120)='1900-01-01' then '' else convert(varchar(10),TCDate,105) end as TCDate ,pPO.PostOfficeName as ParentsPostOffice,pPO.PostOfficeNameBn as ParentsPostOfficeBn,pT.ThanaName as ParentsThana,pT.ThanaNameBn as ParentsThanaBn,pD.DistrictName as  ParentsDistrict,pD.DistrictNameBn as  ParentsDistrictBn,ns.subName as Optinalsubject,adm.ManSubId,adm.OpSubId

             , pePO.PostOfficeName as PermanentPostOffice, pePO.PostOfficeNameBn as PermanentPostOfficeBn, peT.ThanaName as PermanentThana, peT.ThanaNameBn as PermanentThanaBn, peD.DistrictName as PermanentDistrict, peD.DistrictNameBn as PermanentDistrictBn

             , prPO.PostOfficeName as PresentPostOffice, prPO.PostOfficeNameBn as PresentPostOfficeBn, prT.ThanaName as PresentThana, prT.ThanaNameBn as PresentThanaBn, prD.DistrictName as PresentDistrict, prD.DistrictNameBn as PresentDistrictBn,case when ImageName<>'' then '~/Images/studentAdmissionImages/'+ImageName else '' end as ImageName,format(CreateOn,'dd-MM-yyyy hh:mm:ss tt') as CreateOn,MoneyReceiptNo, isnull(bi.BatchId,0) as BatchId,bi.BatchName,p.IsPaid,case when p.IsPaid=1 then 'Paid' else '' end as PaymentStatus

              from Student_AdmissionFormInfo adm inner join Classes c on adm.ClassID = c.ClassID inner join v_Class_Group_Section cgs on adm.ClsGrpID = cgs.ClsGrpID and adm.ClsSecID = cgs.ClsSecID inner join ShiftConfiguration sft on adm.ShiftId = sft.ConfigId left join Post_Office pPO on adm.ParentsPostOfficeId = pPO.PostOfficeID left join Thanas pT on adm.ParentsThanaId = pT.ThanaId left join Distritcts pD on adm.ParentsDistrictId = pD.DistrictId

               left join Post_Office pePO on adm.PermanentPostOfficeId = pePO.PostOfficeID left join Thanas peT on adm.PermanentThanaId = peT.ThanaId left join Distritcts peD on adm.PermanentDistrictId = peD.DistrictId

               left join Post_Office prPO on adm.PresentPostOfficeId = prPO.PostOfficeID left join Thanas prT on adm.PresentThanaId = prT.ThanaId left join Distritcts prD on adm.PresentDistrictId = prD.DistrictId
               left join Boards b on adm.PreSCBoard = b.BoardId left join BatchInfo bi on adm.ClassID=bi.ClassID and adm.AdmissionYear=bi.Year and bi.IsUsed=1	left join 	PaymentInfo p on adm.AdmissionFormNo =p.AdmissionFormNo	  and p.IsPaid=1	
                left join NewSubject ns on adm.[OpSubId]=ns.SubId
			   where adm.IsActive=1 " + condition + " order by AdmissionFormNo ");
            return dt_list;
        }

        public static  int getAdmissionFormNo(int year)
        {
            try {

                dt_list = new DataTable();
                dt_list = CRUD.ReturnTableNull("select ISNULL(max(AdmissionFormNo),0) as AdmissionFormNo  from Student_AdmissionFormInfo where year(AdmissionDate)="+ year);
                if (dt_list != null && dt_list.Rows.Count > 0)
                {
                    if (int.Parse(dt_list.Rows[0]["AdmissionFormNo"].ToString()) == 0)
                    {
                       
                        return int.Parse(year + "0001");
                    }
                    else
                        return (int.Parse(dt_list.Rows[0]["AdmissionFormNo"].ToString()) + 1);
                    

                }
                return 0;
            } catch(Exception ex) { return 0; }
        }
        public static bool isAdmissionOpen()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select IsAdmissionOpen from WSCommonSettings");
                if (dt != null && dt.Rows.Count > 0)
                    return bool.Parse(dt.Rows[0]["IsAdmissionOpen"].ToString());
                else
                    return false;
                
            }
            catch (Exception ex) { return false; }
        }
        public static void bindAdmissionInfo(CheckBox ckbIsAdmission, TextBox txtMsg)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select IsAdmissionOpen,AdmissionMsg from WSCommonSettings");
                if (dt != null && dt.Rows.Count > 0)
                {
                    ckbIsAdmission.Checked = bool.Parse(dt.Rows[0]["IsAdmissionOpen"].ToString());
                    txtMsg.Text = dt.Rows[0]["AdmissionMsg"].ToString();
                }
                else
                {
                    ckbIsAdmission.Checked =false;
                    txtMsg.Text ="";
                }

            }
            catch (Exception ex) {}
        }
        public static string getAdmissionMsg()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select AdmissionMsg from WSCommonSettings");
                if (dt != null && dt.Rows.Count > 0)
                    return dt.Rows[0]["AdmissionMsg"].ToString();
                else
                    return "";
                
            }
            catch (Exception ex) { return ""; }
        }
        public static bool setIsAdmissionOpen(bool IsAdmissionOpen,string AdmissionMsg)
        {
            try
            {
                
              return CRUD.ExecuteQuery("update WSCommonSettings set IsAdmissionOpen='"+IsAdmissionOpen.ToString()+ "',AdmissionMsg=N'"+ AdmissionMsg + "'");
                

            }
            catch (Exception ex) { return false; }
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