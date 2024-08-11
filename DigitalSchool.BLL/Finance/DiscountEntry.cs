using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.Finance
{
    public class DiscountEntry
    {
        public DataTable LoadDiscountList(string condition)
        {
            DataTable dt = CRUD.ReturnTableNull("SELECT * FROM v_Discount "+condition+"");
            return dt;
        }
        public DataTable LoadDiscountSummary(string condition)
        {
            DataTable dt = CRUD.ReturnTableNull("SELECT FeeCatId,StudentId,RollNo,"+
                "Format(DateOfPayment,'dd-MM-yyyy') as DateOfPayment,FeeCatName,FullName,"+
                "ShiftID,ShiftName,BatchID,BatchName,GroupName,ClsGrpID,SectionName,"+
                "DiscountTK,ClsSecID FROM v_StudentPaymentInfo " + condition + "");
            return dt;
        }
    }
}
