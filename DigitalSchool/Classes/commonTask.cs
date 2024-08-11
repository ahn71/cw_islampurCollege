using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

using DS.DAL.AdviitDAL;
using DS.DAL;
using DS.BLL;

namespace DS.Classes
{ 
    public static  class commonTask
    {
        public static DataTable dt;
        public static double OnlineChargPer_Nagad=.013;
        public static double OnlineChargPer_SSL=.019;

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string Replase(string text, char _old,string _new)
        {
            try {
                string[] temp = text.Split(_old);
                string  _text = "";
                foreach (string s in temp)
                {
                    _text += _new+s;
                }
                _text = _text.Remove(0,_new.Count());
                return _text;
            }
            catch(Exception ex) { return text; }
            
        }
        public static string ddMMyyyyToyyyyMMdd(string date)
        {
            try {
                string[] d = date.Split('-');
                return d[2] + "-" + d[1] + "-" + d[0];
            }
            catch (Exception ex) { return null; }
            
        }
        public static string markToGPA(float Mark)
        {
            string result = "0_F";
            if (Mark >= 80)
                result = "5_A+";
            else if(Mark >= 70)
                result = "4_A";
            else if(Mark >= 60)
                result = "3.5_A-";
            else if(Mark >= 50)
                result = "3_B";
            else if(Mark >= 40)
                result = "2_C";
            else if(Mark >= 33)
                result = "1_D";
            return result;
        }
        public static string markToGPA(float Mark,float BaseMark)
        {
            Mark = (Mark / BaseMark) * 100;
            string result = "0_F";
            if (Mark >= 80)
                result = "5_A+";
            else if (Mark >= 70)
                result = "4_A";
            else if (Mark >= 60)
                result = "3.5_A-";
            else if (Mark >= 50)
                result = "3_B";
            else if (Mark >= 40)
                result = "2_C";
            else if (Mark >= 33)
                result = "1_D";
            return result;
        }

        public static void loadClass(DropDownList dl)
        {
            dl.Items.Clear();
            sqlDB.loadDropDownList("Select ClassName from Classes order by ClassOrder", dl);
            dl.Items.Add(new ListItem("...Select...", "0"));
            dl.SelectedIndex = dl.Items.Count - 1;
        }
        public static void loadClasses(DropDownList ddl)
        {
            try
            {
                sqlDB.fillDataTable("Select ClassId, ClassName from Classes where Isnull(IsActive,1)=1 order by ClassOrder", dt = new DataTable());
                ddl.DataValueField = "ClassId";
                ddl.DataTextField = "ClassName";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch (Exception ex)
            {

            }            
        }
        public static void loadGroupsByClass(DropDownList ddl,string ClassId)
        {
            try
            {
                sqlDB.fillDataTable("select ClsGrpID,g.GroupName from Tbl_Class_Group cg inner join Tbl_Group g on cg.GroupID=g.GroupID where cg.ClassID="+ ClassId, dt = new DataTable());
                ddl.DataValueField = "ClsGrpID";
                ddl.DataTextField = "GroupName";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch (Exception ex)
            {

            }            
        }
        public static void loadYearFromBatch(DropDownList ddl)
        {
            try
            {
                sqlDB.fillDataTable("select distinct Year  from BatchInfo where IsUsed=1 order by Year Desc", dt = new DataTable());
                ddl.DataValueField = "Year";
                ddl.DataTextField = "Year";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        public static void loadClassFromBatch(DropDownList ddl)
        {
            try
            {
                sqlDB.fillDataTable("select distinct LEFT(BatchName, LEN(BatchName)-4) as Class  from BatchInfo where IsUsed=1", dt = new DataTable());
                ddl.DataValueField = "Class";
                ddl.DataTextField = "Class";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        public static void loadWsPagseList(DropDownList ddl)
        {
            try
            {
                sqlDB.fillDataTable("select PageId,Page from WSPages where Status=1 order by Ordering", dt = new DataTable());
                ddl.DataValueField = "PageId";
                ddl.DataTextField = "Page";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        public static void loadShift(DropDownList ddl)
        {
            try
            {
                sqlDB.fillDataTable("select ConfigId,ShiftName from ShiftConfiguration ", dt = new DataTable());
                ddl.DataValueField = "ConfigId";
                ddl.DataTextField = "ShiftName";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        public static void loadShift(DropDownList ddl,string Type )
        {
            try
            {
                sqlDB.fillDataTable("select ConfigId,ShiftName from ShiftConfiguration where Type="+Type+" ", dt = new DataTable());
                ddl.DataValueField = "ConfigId";
                ddl.DataTextField = "ShiftName";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("Select", "0"));
            }
            catch { }
        }
        public static void loadShift(CheckBoxList ckb)
        {
            try
            {
                sqlDB.fillDataTable("select ConfigId,ShiftName from ShiftConfiguration ", dt = new DataTable());
                ckb.DataValueField = "ConfigId";
                ckb.DataTextField = "ShiftName";
                ckb.DataSource = dt;
                ckb.DataBind();
                
            }
            catch { }
        }
        public static void loadShiftAll(DropDownList ddl)
        {
            try
            {
                sqlDB.fillDataTable("select ShiftName from ShiftConfiguration ", dt = new DataTable());
                ddl.DataValueField = "ShiftName";
                ddl.DataTextField = "ShiftName";
                ddl.DataSource = dt;
                ddl.DataBind();
                ddl.Items.Add(new ListItem("All", "All"));
                ddl.SelectedIndex = ddl.Items.Count - 1;
            }
            catch { }
        }
        public static void loadSection(DropDownList dl)
        {
            dl.Items.Clear();
            sqlDB.loadDropDownList("Select  SectionName from Sections  Order by SectionName", dl);
            dl.Items.Add(new ListItem("...Select...", "0"));
            dl.SelectedIndex = dl.Items.Count - 1;
        }
        public static void loadSectionAll(DropDownList dl)
        {
            dl.Items.Clear();
            sqlDB.loadDropDownList("Select  SectionName from Sections  Order by SectionName", dl);
            dl.Items.Add(new ListItem("All", "All"));
            dl.SelectedIndex = dl.Items.Count - 1;
        }
        public static void loadDistrict(DropDownList dl)
        {
            sqlDB.loadDropDownList("select DistrictName from Distritcts order by DistrictName ", dl);
        }
        //public static void loadSession(DropDownList dl)
        //{
        //    sqlDB.loadDropDownList("Select Distinct Session From CurrentStudentInfo order by Session desc ", dl);
        //}
        public static void loadSession(DropDownList dl)
        {
            dt = new DataTable();
            sqlDB.fillDataTable("Select  Year   From YearInfo  order by YEAR desc", dt);
            dl.DataSource = dt;
            dl.DataValueField = "Year";
            dl.DataTextField = "Year";
            dl.DataBind();
            dl.SelectedValue = TimeZoneBD.getCurrentTimeBD("yyyy");

        }
        public static void loadFeesCategory(DropDownList dl)
        {
            dt = new DataTable();
            sqlDB.fillDataTable("Select Distinct FeeCatId, FeeCatName From v_FeesCatDetails order by FeeCatName desc", dt);
            dl.DataSource = dt;
            dl.DataValueField = "FeeCatId";
            dl.DataTextField = "FeeCatName";
            dl.DataBind();
            dl.Items.Add(new ListItem("...Select Fee Category...", "0"));
            dl.SelectedIndex = dl.Items.Count - 1;
        }
        public static void LoadFeesCategoryInfo(DropDownList dl)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select FeeCatId,FeeCatName From FeesCategoryInfo order by FeeCatName desc", dt);
                dl.DataSource = dt;
                dl.DataTextField = "FeeCatName";
                dl.DataValueField = "FeeCatId";
                dl.DataBind();
                dl.Items.Add("...Select Fees Category...");
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }
        }
        public static void loadParticular(DropDownList dl)
        {
            sqlDB.loadDropDownList("Select PName From ParticularsInfo order by PName desc ", dl);
        }
        public static void loadParticularInfo(DropDownList dl)
        {
            dt = new DataTable();
            sqlDB.fillDataTable("Select PId, PName From ParticularsInfo order by PName desc", dt);
            dl.DataSource = dt;
            dl.DataTextField = "PName";
            dl.DataValueField = "PId";
            dl.DataBind();
            dl.Items.Add(new ListItem("...Select Particulars...", "0"));
            dl.SelectedIndex = dl.Items.Count - 1;
            
        }
        public static void loadBatch(DropDownList dl)
        {
            dl.Items.Clear();
            sqlDB.loadDropDownList("Select BatchName From BatchInfo WHERE IsUsed = 'True'", dl);
            //dl.Items.Add("...Select Batch...");
            dl.Items.Add(new ListItem("...Select Batch...", "0"));
            dl.SelectedIndex = dl.Items.Count - 1;
        }
        public static void loadBatchAll(DropDownList dl)
        {
            dl.Items.Clear();
            sqlDB.loadDropDownList("Select BatchName From BatchInfo WHERE IsUsed = 'True'", dl);
            //dl.Items.Add("...Select Batch...");
            dl.Items.Add(new ListItem("All", "All"));
            dl.SelectedIndex = dl.Items.Count - 1;
        }
        public static void loadBatchWithId(DropDownList dl)
        {
            dl.Items.Clear();
            sqlDB.loadDropDownList("Select BatchId,BatchName From BatchInfo ", dl);
            dl.DataTextField = "BatchName";
            dl.DataValueField = "BatchId";
            dl.Items.Add("...Select Batch...");
            dl.SelectedIndex = dl.Items.Count - 1;
        }
        public static int get_batchid(string classId,string year)
        {
            try {
                dt = new DataTable();
                dt = CRUD.ReturnTableNull("Select BatchId From BatchInfo where Year="+ year + " and ClassID="+ classId + " and IsUsed=1  ");
                if (dt != null && dt.Rows.Count > 0)
                    return int.Parse(dt.Rows[0]["BatchId"].ToString());
                else
                    return 0;
            } catch(Exception ex) { return 0; }
            
        }
        public static void loadBatchWithId(DropDownList dl,string like)
        {
            dl.Items.Clear();
            sqlDB.loadDropDownList("Select BatchId,BatchName From BatchInfo where batchName like '%NINE%' or batchName like '%TEN%' or "
            + " batchName like '%ELEVEN%' or batchName like '%TWELVE%'", dl);
            dl.DataTextField = "BatchName";
            dl.DataValueField = "BatchId";
            dl.Items.Add("...Select Batch...");
            dl.SelectedIndex = dl.Items.Count - 1;
        }
        public static void loadBatchLog(DropDownList dl)
        {
            sqlDB.loadDropDownList("Select BatchName From BatchLog ", dl);
        }
        public static void loadPreviousBatch(DropDownList dl)
        {
            sqlDB.loadDropDownList("Select BatchName From BatchLog ", dl);
        }
        public static void loadExamType(DropDownList dl)
        {
            sqlDB.loadDropDownList("select ExName from ExamType", dl);
        }
        public static void loadAttendanceSheet(DropDownList dl)
        {
            sqlDB.loadDropDownList("Select ASName from AttendanceSheetInfo order by ASName", dl);
        }
        public static void LoadFacultyStaffAttendanceSheet(DropDownList dl)
        {
            sqlDB.loadDropDownList("Select ASName from FacultyNStaffAttendenceSheetInfo order by ASName", dl);
        }
        public static void loadMonths(DropDownList dl)
        {
            dl.Items.Clear();
            string year = TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd HH:mm:ss");
            dl.Items.Add(new ListItem("...Select Month...", "0"));
            dl.Items.Add("January" + year);
            dl.Items.Add("February" + year);
            dl.Items.Add("March" + year);
            dl.Items.Add("April" + year);
            dl.Items.Add("May" + year);
            dl.Items.Add("June" + year);
            dl.Items.Add("July" + year);
            dl.Items.Add("August" + year);
            dl.Items.Add("September" + year);
            dl.Items.Add("October" + year);
            dl.Items.Add("November" + year);
            dl.Items.Add("December" + year);
        }
        public static void LoadSubClass(DropDownList dl)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select ClassID,ClassName From Classes order by ClassOrder", dt);
                dl.DataSource = dt;
                dl.DataTextField = "ClassName";
                dl.DataValueField = "ClassID";
                dl.DataBind();
            }
            catch (Exception ex)
            {
                
            }
        }
        public static void LoadSubject(DropDownList dl)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select SubId,SubName From NewSubject", dt);
                dl.DataSource = dt;
                dl.DataTextField = "SubName";
                dl.DataValueField = "SubId";
                dl.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        public static void LoadDepartment(DropDownList dl)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select DId,DName From Departments_HR where DStatus='True' and DName!='MLS'", dt);
                dl.DataSource = dt;             
                dl.DataTextField = "DName";
                dl.DataValueField = "DId";
                dl.DataBind();
                dl.Items.Add("...Select...");
                dl.Text = "...Select...";
            }
            catch { }
        }
        public static void LoadDepartmentList(DropDownList dl)
        {
            sqlDB.loadDropDownList("Select DId,DName From Departments_HR where DStatus='True'", dl);
        }
        public static void LoadDeprtmentAtttedence(DropDownList dl)
        {
             try
            {

            dt = new DataTable();
            sqlDB.fillDataTable("Select DId,DName From Departments_HR where DStatus='True'", dt);
            dl.DataSource = dt;
            dl.DataTextField = "DName";
            dl.DataValueField = "DId";
            dl.DataBind();
            //dl.Items.Add("All");
            dl.Items.Insert(0,new ListItem("All", "0"));            
            }
             catch { }
        }
        public static void LoadDesignation(DropDownList dl)
        {
            try
            {
                dt = new DataTable();
                sqlDB.fillDataTable("Select DesId,DesName From Designations", dt);
                dl.DataSource = dt;
                dl.DataTextField = "DesName";
                dl.DataValueField = "DesId";
                dl.DataBind();
                dl.Items.Insert(dl.Items.Count, new ListItem("All", "0"));
                dl.SelectedIndex = dl.Items.Count - 1;
            }
            catch { }
        }
        public static void LoadRollNo(DropDownList dl, string Class, string Section, string Batch)
        {
            try
            {
                sqlDB.loadDropDownList("Select RollNo From CurrentStudentInfo where ClassName='" + Class + "' and SectionName='" + Section + "' and BatchName='" + Batch + "' ORDER BY RollNo", dl);
            }
            catch { }
        }
        public static DataTable LoadShoolInfo()
        {
            try
            {
                dt = DS.DAL.CRUD.ReturnTableNull("Select SchoolName,Address,LogoName From School_Setup");
                return dt;
            }
            catch { return dt; }
        }

        public static DataTable LoadEavluator(string SessionId)
        {
            try
            {
                dt = DS.DAL.CRUD.ReturnTableNull(@"DECLARE @Names VARCHAR(8000) 
SELECT @Names = COALESCE(@Names + ', ', '') + FirstName
FROM UserAccount where UserId  in (select distinct MemberId from TE_NumberSheet where SessionID="+SessionId+");"+
                " select @Names as Evaluator");
                return dt;
            }
            catch { return dt; }
        }

        public static string  getPrincipalSignature()
        {
            try
            {
                dt = DS.DAL.CRUD.ReturnTableNull(" select ESignName from EmployeeInfo where vp =1");
                if (dt.Rows.Count > 0)
                    return dt.Rows[0]["ESignName"].ToString();
                else
                    return "";
            }
            catch { return ""; }
        }
        public static void loadEmployeeType(RadioButtonList rbl)
        {
            try
            {
                sqlDB.fillDataTable("select EmployeeTypeID,EmployeeType from EmployeeType where Status=1 order by Ordering", dt = new DataTable());
                rbl.DataValueField = "EmployeeTypeID";
                rbl.DataTextField = "EmployeeType";
                rbl.DataSource = dt;
                rbl.DataBind();
                rbl.SelectedIndex = 0;


            }
            catch { }
        }
        public static void loadEmployeeTypeWithAll(RadioButtonList rbl)
        {
            try
            {
                sqlDB.fillDataTable("select EmployeeTypeID,EmployeeType from EmployeeType where Status=1 order by Ordering", dt = new DataTable());
                rbl.DataValueField = "EmployeeTypeID";
                rbl.DataTextField = "EmployeeType";
                rbl.DataSource = dt;
                rbl.DataBind();
                rbl.Items.Insert(0, new ListItem("All", "All"));
                rbl.SelectedIndex = 0;


            }
            catch { }
        }
        public static void loadBoard(DropDownList ddl)
        {
            try
            {
                sqlDB.fillDataTable("select BoardId,BoardName from Boards", dt = new DataTable());
                ddl.DataValueField = "BoardId";
                ddl.DataTextField = "BoardName";
                ddl.DataSource = dt;
                ddl.DataBind();
                if(ddl.Items.Count>1)
                 ddl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        public static void loadBoard(DropDownList[] dl)
        {
            try
            {
                sqlDB.fillDataTable("select BoardId,BoardName from Boards", dt = new DataTable());
                foreach (var item in dl)
                {
                    item.DataTextField = "BoardName";
                    item.DataValueField = "BoardId";
                    item.DataSource = dt;
                    item.DataBind();
                    item.Items.Insert(0, new ListItem("...Select...", "0"));
                    item.Enabled = true;
                }
            }
            catch { }
        }
        public static void loadPassingYearForAdmission(DropDownList ddl)
        {
            try
            {
                sqlDB.fillDataTable("select Year from Student_PassingYearForAdmission order by Year desc", dt = new DataTable());
              
                ddl.DataValueField = "Year";
                ddl.DataTextField = "Year";
                ddl.DataSource = dt;
                ddl.DataBind();
                if(ddl.Items.Count>1)
                ddl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        public static void loadPassingYearForAdmission(DropDownList[] ddl)
        {
            try
            {
                sqlDB.fillDataTable("select Year from Student_PassingYearForAdmission order by Year desc", dt = new DataTable());
                
                foreach(var item in ddl)
                {
                    item.DataValueField = "Year";
                    item.DataTextField = "Year";
                    item.DataSource = dt;
                    item.DataBind();
                    if (item.Items.Count > 1)
                    {
                        item.Items.Insert(0, new ListItem("...Select...", "0"));
                    }
                }
                //ddl.DataValueField = "Year";
                //ddl.DataTextField = "Year";
                //ddl.DataSource = dt;
                //ddl.DataBind();
                //if (ddl.Items.Count > 1)
                //    ddl.Items.Insert(0, new ListItem("...Select...", "0"));
            }
            catch { }
        }
        public static string getBatchID(string BatchName )
        {
            try
            {
                sqlDB.fillDataTable("select BatchId  from BatchInfo Where BatchName='"+BatchName+"' ", dt = new DataTable());
                return dt.Rows[0]["BatchId"].ToString();
            }
            catch(Exception ex ) { return ""; }
        }
        public static void loadPostoffice(DropDownList dl, string did, string tid)
        {
            DataTable dt = new DataTable();
            dt = CRUD.ReturnTableNull("select PostOfficeID,PostOfficeName from Post_Office where DistrictId='" + did + "' and ThanaId='" + tid + "'");
            dl.DataTextField = "PostOfficeName";
            dl.DataValueField = "PostOfficeID";
            dl.DataSource = dt;
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }

        public static void LoadBatchwiseFeeCat(string PaymentFor, string batchName, DropDownList dl)
        {

            dt = new DataTable();
            if(PaymentFor== "openPayment")
            dt = CRUD.ReturnTableNull("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo WHERE PaymentFor='"+ PaymentFor + "' order by FeeCatId ASC");
            else
                dt = CRUD.ReturnTableNull("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo WHERE BatchId in(select BatchId from BatchInfo where BatchName='" + batchName + "') and IsNull(PaymentFor,'regular')='" + PaymentFor + "' order by FeeCatId ASC");
            dl.DataSource = dt;
            dl.DataTextField = "FeeCatName";
            dl.DataValueField = "FeeCatId";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public static void LoadBatchwiseFeeCatWithAll(string PaymentFor, string batchName, DropDownList dl)
        {

            dt = new DataTable();
            if (PaymentFor == "openPayment")
                dt = CRUD.ReturnTableNull("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo WHERE PaymentFor='" + PaymentFor + "' order by FeeCatId ASC");
            else
                dt = CRUD.ReturnTableNull("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo WHERE BatchId in(select BatchId from BatchInfo where BatchName='" + batchName + "') and IsNull(PaymentFor,'regular')='" + PaymentFor + "' order by FeeCatId ASC");
            dl.DataSource = dt;
            dl.DataTextField = "FeeCatName";
            dl.DataValueField = "FeeCatId";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("All", "0"));
        }

        public static void LoadFeesCategoryPaymentForType(DropDownList dl)
        {

            dt = new DataTable();
            dt = CRUD.ReturnTableNull("select Id,PaymentFor from FeesCategoryPaymentForType where IsActive=1 Order by Ordering");
            dl.DataSource = dt;
            dl.DataTextField = "PaymentFor";
            dl.DataValueField = "Id";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public static void LoadPaymentStores(DropDownList dl)
        {

            dt = new DataTable();
            dt = CRUD.ReturnTableNull("select StoreNameKey,StoreTitle from PaymentStores where IsActive=1 Order by Ordering");
            dl.DataSource = dt;
            dl.DataTextField = "StoreTitle";
            dl.DataValueField = "StoreNameKey";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public static void LoadBatchwiseFeeCat(string PaymentFor, string batchName, string ClsGrpID, DropDownList dl)
        {

            dt = new DataTable();
            if(PaymentFor== "openPayment")
                dt = CRUD.ReturnTableNull("SELECT cat.FeeCatId,FeeCatName +' [ Last Date : '+ convert(varchar(10),dp.DateOfEnd,105)+' ]' as FeeCatName FROM FeesCategoryInfo cat left join DateOfPayment dp on cat.FeeCatId=dp.FeeCatId  Where  cat.PaymentFor='openPayment' and DateOfEnd>='"+ServerTimeZone.GetBangladeshNowDate("yyyy-MM-dd")+"' order by dp.DateOfEnd ");
            else if (PaymentFor == "admission")
                dt = CRUD.ReturnTableNull("SELECT ct.FeeCatId,FeeCatName +' [ Last Date : '+ convert(varchar(10),dp.DateOfEnd,105)+' ]' as FeeCatName FROM FeesCategoryInfo ct inner join DateOfPayment dp on ct.FeeCatId=dp.FeeCatId where IsNull(ct.PaymentFor,'regular')='" + PaymentFor + "' and DateOfEnd>='" + ServerTimeZone.GetBangladeshNowDate("yyyy-MM-dd") + "' and ct.BatchId in(select BatchId from BatchInfo where BatchName='" + batchName + "') and (ClsGrpId=" + ClsGrpID + " or ClsGrpId=0)  order by dp.DateOfEnd ");
            else 
                dt = CRUD.ReturnTableNull("SELECT ct.FeeCatId,FeeCatName +' [ Last Date : '+ convert(varchar(10),dp.DateOfEnd,105)+' ]' as FeeCatName FROM FeesCategoryInfo ct inner join DateOfPayment dp on ct.FeeCatId=dp.FeeCatId  left join ExamInfo ex on ct.ExInSl=ex.ExInSl where IsNull(ct.PaymentFor,'regular')='"+ PaymentFor + "' and DateOfEnd>='" + ServerTimeZone.GetBangladeshNowDate("yyyy-MM-dd") + "' and ct.BatchId in(select BatchId from BatchInfo where BatchName='" + batchName + "') and (ct.ClsGrpID=" + ClsGrpID + " or ISNULL(ct.ClsGrpID,0)=0) order by dp.DateOfEnd ");
            dl.DataSource = dt;
            dl.DataTextField = "FeeCatName";
            dl.DataValueField = "FeeCatId";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("...Select...", "0"));
        }
        public static DataTable getFeeCatIdBatchwiseFeeCat(string batchName, string ClsGrpID,string FeeCatId)
        {
           return CRUD.ReturnTableNull("SELECT FeeCatId,FeeCatName FROM FeesCategoryInfo ct left join ExamInfo ex on ct.ExInSl=ex.ExInSl where ct.BatchId in(select BatchId from BatchInfo where BatchName='" + batchName + "') and (ex.ClsGrpID=" + ClsGrpID + " or ISNULL(ex.ClsGrpID,0)=0) and ISNULL(IsDemo,0)<>1 and FeeCatId<" + FeeCatId + " order by FeeCatId ASC");            
        }
        public static string IsPaidReturnOrderNo(string FeeCatId, string StudentId)
        {
            try
            {
                dt = new DataTable();
                dt = CRUD.ReturnTableNull("select OrderNo from PaymentInfo where StudentId='" + StudentId + "' and FeeCatId=" + FeeCatId + " and IsPaid=1 and status='Success'");
                if (dt == null || dt.Rows.Count == 0)
                    return "";
                return dt.Rows[0]["OrderNo"].ToString();
            }
            catch (Exception) { return ""; }
        }
        public static string IsPaidReturnOrderNoByAdmissionFormNo(string FeeCatId, string AdmissionFormNo)
        {
            try
            {
                dt = new DataTable();
                dt = CRUD.ReturnTableNull("select OrderNo from PaymentInfo where AdmissionFormNo='" + AdmissionFormNo + "' and FeeCatId=" + FeeCatId + " and IsPaid=1 and status='Success'");
                if (dt == null || dt.Rows.Count == 0)
                    return "";
                return dt.Rows[0]["OrderNo"].ToString();
            }
            catch (Exception) { return ""; }
        }

        public static string[] getTimeDuration(DateTime Date)
        {
            try { 
            string[] duration=new string[3];
             DateTime zeroTime = new DateTime(1, 1, 1);
            DateTime olddate = Date;

            DateTime curdate = TimeZoneBD.getCurrentTimeBD();


            TimeSpan span = curdate - olddate;

            // because we start at year 1 for the Gregorian 
            // calendar, we must subtract a year here.

            int years = (zeroTime + span).Year - 1;
            int months = (zeroTime + span).Month - 1;
            int days = (zeroTime + span).Day;


            duration[0] = years.ToString();
            duration[1] = months.ToString();
            duration[2] = days.ToString();
            return duration;
            }
            catch (Exception ex) { return null; }

            //DateTime today = DateTime.Now;


            //int age = 0;
            //age = DateTime.Now.Year - dt.Year;
            //if (DateTime.Now.DayOfYear < dt.DayOfYear)
            //    age = age - 1;


            //int Years = new DateTime(DateTime.Now.Subtract(dt).Ticks).Year - 1;
            //DateTime PastYearDate = dt.AddYears(Years);
            //int Months = 0;
            //for (int i = 1; i <= 12; i++)
            //{
            //    if (PastYearDate.AddMonths(i) == today)
            //    {
            //        Months = i;
            //        break;
            //    }
            //    else if (PastYearDate.AddMonths(i) >= today)
            //    {
            //        Months = i - 1;
            //        break;
            //    }
            //}
            //int Days = today.Subtract(PastYearDate.AddMonths(Months)).Days;
            //dlcYear.Text = Years.ToString() + " Years";
            //dlcMonth.Text = Months.ToString() + " Months";
            //dlcDay.Text = Days.ToString() + " Days";
        }

        public static DataTable getStudentInfo(int admissionNo)
        {
            string query = "select AdmissionFormNo, FullName,FathersName,adm.classId,c.ClassName, AdmissionYear,adm.ClsGrpID,g.GroupName,ImageName,adm.Mobile from Student_AdmissionFormInfo adm left join Classes c on adm.ClassID=c.ClassID left join Tbl_Class_Group cg on adm.ClsGrpID=cg.ClsGrpID left join Tbl_Group g on cg.GroupID=g.GroupID Where AdmissionFormNo=" + admissionNo + " and adm.classId is not null ";
            dt = CRUD.ReturnTableNull(query);
           return dt;
        }
    }
}