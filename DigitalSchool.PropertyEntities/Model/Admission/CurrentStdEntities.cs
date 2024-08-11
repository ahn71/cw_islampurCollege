using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Admission
{
    public class CurrentStdEntities : IDisposable
    {
        public int StudentID { get; set; }
        public int AdmissionNo { get; set; }
        public int AdmissionID { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string FullName { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public int? ClsGrpID { get; set; }
        public int ClsSecID { get; set; }
        public string SectionName { get; set; }
        public int? RollNo { get; set; }
        public string Religion { get; set; }
        public int? ConfigId { get; set; }
        public string Shift { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public string Session { get; set; }
        public string BloodGroup { get; set; }
        public string FathersName { get; set; }       
        public string FathersProfession { get; set; }
        public string FathersProfessionBn { get; set; }
        public int? FathersYearlyIncome { get; set; }
        public string FathersMobile { get; set; }
        public string FatherEmail { get; set; }
        public string MothersName { get; set; }
        public string MothersProfession { get; set; }
        public string MothersProfessionBn { get; set; }
        public int? MothersYearlyIncome { get; set; }
        public string MothersMobile { get; set; }
        public string MotherEmail { get; set; }
        public string HomePhone { get; set; }
        public string ParentsAddress { get; set; }
        public string ParentsAddressBn { get; set; }
        public int ParentsPostOfficeId { get; set; }
        public int ParentsThanaId { get; set; }
        public int ParentsDistrictId { get; set; }
        public string GuardianName { get; set; }
        public string GuardianRelation { get; set; }
        public string GuardianMobileNo { get; set; }
        public string GuardianAddress { get; set; }
        public string PAVillage { get; set; }
        public string PAVillageBn { get; set; }
        public string PAPostOffice { get; set; }
        public int? PThanaId { get; set; }
        public string PAThana { get; set; }
        public int? PDistrictId { get; set; }
        public string PADistrict { get; set; }
        public string TAViIlage { get; set; }
        public string TAViIlageBn { get; set; }
        public string TAPostOffice { get; set; }
        public int? TThanaId { get; set; }
        public string TAThana { get; set; }
        public int? TDistrictId { get; set; }
        public string TADistrict { get; set; }
        public string MotherTongue { get; set; }
        public string Nationality { get; set; }
        public string PreviousExamType { get; set; }
        public long? PSCRollNo { get; set; }
        public int? PSCPassingYear { get; set; }
        public double? PSCGPA { get; set; }
        public string PSCBoard { get; set; }
        public DateTime? CertifiedDate { get; set; }
        public string PreviousSchoolName { get; set; }
        public string PSCJSCRegistration { get; set; }
        public int? TransferCertifiedNo { get; set; }
        public string PreferredClass { get; set; }
        public Boolean IsActive { get; set; }
        public string Comments { get; set; }
        public int BatchID { get; set; }
        public string BatchName { get; set; }
        public string Status { get; set; }
        public Boolean PaymentStatus { get; set; }
        public int? StartBatchID { get; set; }
        public int? EndBatchID { get; set; }
        public Boolean? StdStatus { get; set; }
        public int? SpendYear { get; set; }
        public string ImageName{ get; set; }
        public string FullNameBn { get; set; }
        public string FathersNameBn { get; set; }
        public string MothersNameBn { get; set; }
        public string IdCard { get; set; }
        public int StdTypeId { get; set; }
        public string FatherDesg { get; set; }
        public string FatherOrg { get; set; }
        public string FatherPhone { get; set; }
        public string MotherDesg { get; set; }
        public string MotherOrg { get; set; }
        public string MotherPhone { get; set; }
        public int BusID { get; set; }
        public int LocationID { get; set; }
        public int PlaceID { get; set; }
        public int PAPostOfficeID { get; set; }
        public int TAPostOfficeID { get; set; }
        public int? SSCRoll { get; set; }
        public string TCCollegeName { get; set; }
        public string TCClass { get; set; }
        public string TCSemister { get; set; }
        public DateTime? TCDate { get; set; }
        public int CreateBy { get; set; }
        public DateTime CreateOn { get; set; }
        public int UpdateBy { get; set; }
        public DateTime UpdateOn { get; set; }

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
