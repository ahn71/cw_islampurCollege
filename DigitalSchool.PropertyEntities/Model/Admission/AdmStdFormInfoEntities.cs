using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.Admission
{
   public class AdmStdFormInfoEntities : IDisposable
    {
        public int AdmissionFormNo { get; set; }       
        public string NUAdmissionRoll { get; set; }       
        public DateTime AdmissionDate { get; set; }
        public int AdmissionYear { get; set; }
        public string FullName { get; set; }
        public string FullNameBn { get; set; }
        public int ClassID { get; set; }
        public int ClsGrpID { get; set; }
        public int ClsSecID { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public int ShiftId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string BloodGroup { get; set; }
        public string Session { get; set; }

        public string FathersName { get; set; }
        public string FathersNameBn { get; set; }
        public string FathersProfession { get; set; }        
        public string FathersProfessionBn { get; set; }        
        public string FathersMobile { get; set; }
        public string MothersName { get; set; }
        public string MothersNameBn { get; set; }
        public string MothersProfession { get; set; }
        public string MothersProfessionBn { get; set; }
        public string MothersMobile { get; set; }
        public string ParentsAddress { get; set; }
        public string ParentsAddressBn { get; set; }
        public int ParentsPostOfficeId { get; set; }
        public int ParentsThanaId { get; set; }
        public int ParentsDistrictId { get; set; } 
        
        public string GuardianName { get; set; }
        public string GuardianRelation { get; set; }
        public string GuardianMobileNo { get; set; }
        public string GuardianAddress { get; set; }

        public string PermanentAddress { get; set; }
        public string PermanentAddressBn { get; set; }
        public int PermanentPostOfficeId { get; set; }
        public int PermanentThanaId { get; set; }
        public int PermanentDistrictId { get; set; }

        public string PresentAddress { get; set; }
        public string PresentAddressBn { get; set; }
        public int PresentPostOfficeId { get; set; }
        public int PresentThanaId { get; set; }
        public int PresentDistrictId { get; set; } 
        
        public string PreSCBoard { get; set; }
        public int PreSCPassingYear { get; set; }
        public long PreSCRegistration { get; set; }
        public long PreSCRollNo { get; set; }
        public float PreSCGPA { get; set; }
        public string PreSchoolName { get; set; }

        public string PreSCBoardHSC { get; set; }
        public int PreSCPassingYearHSC { get; set; }
        public long PreSCRegistrationHSC { get; set; }
        public long PreSCRollNoHSC { get; set; }
        public float PreSCGPAHSC { get; set; }
        public string PreSchoolNameHSC { get; set; }

        public string PreSCBoardHonours { get; set; }
        public int PreSCPassingYearHonours { get; set; }
        public long PreSCRegistrationHonours { get; set; }
        public long PreSCRollNoHonours { get; set; }
        public float PreSCGPAHonours { get; set; }
        public string PreSchoolNameHonours { get; set; }

        public string TCCollege { get; set; }        
        public string TCDate { get; set; }

        public string ImageName { get; set; }
        public bool IsActive { get; set; }
        public bool IsAppoved { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
        public string MoneyReceiptNo { get; set; }

        public string ManSubIds { get; set; }
        public string OptSubId { get; set; }



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
