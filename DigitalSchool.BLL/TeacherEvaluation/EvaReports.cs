using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace DS.BLL.TeacherEvaluation
{
    public static class EvaReports
    {
        private static DataTable dt;
        private static string sql = "";
        public static bool FinalGradeSheet(string SessionId)
        {
            try
            {
                sql = "SELECT ns.EID, ei.ECardNo, ei.EName, ei.DId, ei.DName, ns.SessionID, ns.SubCategoryID, sc.SubCategory, np.FullNumber, sc.Ordering," +
                    "c.Ordering AS OrderingC, ns.MemberID, ua.FirstName,ns.ObtainNumber " +
                    "FROM dbo.TE_NumberSheet AS ns INNER JOIN dbo.v_EmployeeInfo AS ei ON ns.EID = ei.EID INNER JOIN " +
                    "dbo.TE_SubCategory AS sc ON ns.SubCategoryID = sc.SubCategoryID INNER JOIN " +
                    "dbo.TE_Session AS s ON ns.SessionID = s.SessionID INNER JOIN " +
                    "dbo.TE_NumberPatternDetails AS np ON s.NumPatternID = np.NumPatternID AND ns.SubCategoryID = np.SubCategoryID INNER JOIN " +
                    "dbo.UserAccount AS ua ON ns.MemberID = ua.UserId INNER JOIN " +
                    "dbo.TE_Category AS c ON sc.CategoryID = c.CategoryID " +
                    "where ns.SessionID=" + SessionId + " " +
                    "ORDER BY Did,OrderingC,sc.Ordering";
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                HttpContext.Current.Session["__EvaFinalGradeSheet__"] = dt;
                if (dt == null || dt.Rows.Count == 0)
                    return false;
                else
                    return true;
            }
            catch { return false; }
            
        }

        public static bool CollegeRankReport(string SessionId)
        {
            try
            {
                sql = @" select sum(ns.ObtainNumber)/count(distinct ns.MemberID) ObtainNumber,ns.SessionId as SessionId1, s.Session+' ('+convert(varchar,s.StartDate,105)+' to '+convert(varchar,s.EndDate,105)+')' as SessionId,ns.EID,ei.EName,ei.DID,ei.DName,
	RANK() OVER(PARTITION BY ei.DID ORDER BY sum(ns.ObtainNumber)/count(distinct ns.MemberID) desc) Ordering,
	RANK() OVER (ORDER BY sum(ns.ObtainNumber)/count(distinct ns.MemberID) desc) OrderingC from TE_NumberSheet ns inner join v_employeeinfo ei on ns.EID=ei.EID inner join TE_Session s on ns.SessionID=s.SessionID
	where ns.SessionId=" + SessionId+" group by ns.SessionId,  ns.EID,ei.EName,ei.DID,ei.DName,s.StartDate,EndDate,s.Session";
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                HttpContext.Current.Session["__EvaTeachersPerformanceRanking__"] = dt;
                if (dt == null || dt.Rows.Count == 0)
                    return false;
                else
                    return true;
            }
            catch { return false; }

        }
        public static bool DepartmentRankReport(string SessionId)
        {
            try
            {
                sql = @"select sum(ns.ObtainNumber)/count(distinct ns.MemberID) ObtainNumber, ns.SessionId as SessionId1, s.Session+' ('+convert(varchar,s.StartDate,105)+' to '+convert(varchar,s.EndDate,105)+')' as SessionId,ns.EID,ei.EName,ei.DID,ei.DName,
	RANK() OVER(PARTITION BY ei.DID ORDER BY sum(ns.ObtainNumber)/count(distinct ns.MemberID) desc) Ordering,
	RANK() OVER (ORDER BY sum(ns.ObtainNumber)/count(distinct ns.MemberID) desc) OrderingC from TE_NumberSheet ns inner join v_employeeinfo ei on ns.EID=ei.EID inner join TE_Session s on ns.SessionID=s.SessionID
	where ns.SessionId=" + SessionId+" group by  ns.SessionId,ns.EID,ei.EName,ei.DID,ei.DName,s.StartDate,EndDate,s.Session";
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                HttpContext.Current.Session["__EvaDepartmentRank__"] = dt;
                if (dt == null || dt.Rows.Count == 0)
                    return false;
                else
                    return true;
            }
            catch { return false; }

        }
        public static bool IndividualPerformanceReport(string SessionId)
        {
            try
            {
                sql = @"select sum(ns.ObtainNumber)/count(distinct ns.MemberID) ObtainNumber, ns.SessionId as SessionId1, s.Session+' ('+convert(varchar,s.StartDate,105)+' to '+convert(varchar,s.EndDate,105)+')' as SessionId,ns.EID,ei.EName,ei.DID,ei.DName,
	RANK() OVER(PARTITION BY ei.DID ORDER BY sum(ns.ObtainNumber)/count(distinct ns.MemberID) desc) Ordering,
	RANK() OVER (ORDER BY sum(ns.ObtainNumber)/count(distinct ns.MemberID) desc) OrderingC from TE_NumberSheet ns inner join v_employeeinfo ei on ns.EID=ei.EID inner join TE_Session s on ns.SessionID=s.SessionID
	where ns.SessionId=" + SessionId + " group by  ns.SessionId,ns.EID,ei.EName,ei.DID,ei.DName,s.StartDate,EndDate,s.Session";
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                HttpContext.Current.Session["__IndividualPerformanceReport__"] = dt;
                if (dt == null || dt.Rows.Count == 0)
                    return false;
                else
                {
                    sql = "with ns as(select distinct EID,SessionID,SubCategoryID, sum(ObtainNumber)/count(MemberID) as ObtainNumber from TE_NumberSheet where SessionID="+ SessionId+" group by EID,SessionID,SubCategoryID) "+
                        "select EID,c.CategoryID,c.Category,sc.SubCategoryID,sc.SubCategory,npd.FullNumber,ns.ObtainNumber,"+
                        "case when ns.ObtainNumber>=npd.Excellent then 'Excellent' "+
                        "when ns.ObtainNumber>=npd.Good then 'Good' "+
                        "when ns.ObtainNumber>=npd.Medium then 'Medium' "+
                        "when ns.ObtainNumber>=npd.Weak then 'Weak' "+
                       // "when ns.ObtainNumber>=npd.SoWeak then 'So Weak' "+
                        "else 'So Weak' end as  Grade " +
                        "from   ns inner join TE_SubCategory sc on ns.SubCategoryID=sc.SubCategoryID inner join TE_Category c on sc.CategoryID=c.CategoryID inner join TE_Session s on ns.SessionID=s.SessionID inner join TE_NumberPattern np on np.NumPatternID=s.NumPatternID inner join TE_NumberPatternDetails npd on np.NumPatternID=npd.NumPatternID and ns.SubCategoryID=npd.SubCategoryID "+
                        "where s.SessionID="+SessionId+ "  order by c.Ordering,sc.Ordering";
                    dt = new DataTable();
                    dt = CRUD.ReturnTableNull(sql);
                    HttpContext.Current.Session["__IndividualPerformanceDetails__"] = dt;
                    if (dt == null || dt.Rows.Count == 0)
                        return false;
                    else
                    return true;
                }
                    
            }
            catch { return false; }

        }
        public static bool DepartmentPerformanceReport(string SessionId)
        {
            try
            {
                sql = @"with ns as(
select s.Session, EID, sum(ObtainNumber)/count(distinct MemberID) ObtainNumber from TE_NumberSheet ns inner join  TE_Session s on ns.SessionID=s.SessionID where  ns.SessionID="+SessionId+" " +
" group by EID, s.Session)" +
",ns1 as(select Session, ei.DId,ei.DName,round( sum(ObtainNumber)/count(distinct ns.EID),1) ObtainNumber from  ns inner join v_EmployeeInfo ei on ns.EID=ei.EID  group by ei.DId,ei.DName,Session)" +
"select Session as SessionID, DId,RANK() OVER (ORDER BY ObtainNumber desc) OrderingC, DName,ObtainNumber, case " +
"when ObtainNumber>=80 then 'A+' " +
"when ObtainNumber>=70 then 'A' " +
"when ObtainNumber>=60 then 'B' " +
"when ObtainNumber>=50 then 'C' " +
"else 'D' end as Grade " +
"from ns1 ";
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                HttpContext.Current.Session["__DepartmentPerformanceReport__"] = dt;
                if (dt == null || dt.Rows.Count == 0)
                    return false;               
                    else
                        return true;
                

            }
            catch { return false; }

        }
        public static bool SubIndicatorBasedPerformanceReport(string SessionId)
        {
            try
            {
                sql = @"with ns as(
select SessionID,Eid, SubCategoryID, sum(ObtainNumber)/count(distinct MemberID) ObtainNumber from  TE_NumberSheet where SessionID="+SessionId+"  group by SessionID,EID,SubCategoryID)" +
"select s.Session, ns. EID,ei.EName,ns. SubCategoryID,sc.SubCategory,ns.ObtainNumber, npd.Excellent,npd.Good,npd.Medium,npd.Weak,npd.SoWeak  from  ns inner join TE_Session s on ns.SessionID=s.SessionID inner join TE_NumberPattern np on s.NumPatternID=np.NumPatternID inner join TE_NumberPatternDetails npd on np.NumPatternID=npd.NumPatternID and ns.SubCategoryID=npd.SubCategoryID  inner join v_EmployeeInfo ei on ns.EID=ei.EID inner join TE_SubCategory sc on ns.SubCategoryID=sc.SubCategoryID order by sc.Ordering, ns.SubCategoryID";
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);

                if (dt == null || dt.Rows.Count == 0)
                    return false;
                else
                {
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add("SessionID");
                    dtTemp.Columns.Add("SubCategoryID");
                    dtTemp.Columns.Add("SubCategory");
                    dtTemp.Columns.Add("Category");
                    dtTemp.Columns.Add("Grade");                   
                    dtTemp.Columns.Add("EName");
                    string EName = "";
                    string GName = "";
                    string MName = "";
                    string WName = "";
                    string VWName ="";

                    string Session = "";
                    string SubCategoryID = "";
                    string SubCategory = "";

                    string E_NumberRange = "";
                    string G_NumberRange = "";
                    string M_NumberRange = "";
                    string W_NumberRange = "";
                    string VW_NumberRange = "";

                  



                    foreach (DataRow row in dt.Rows)
                    {

                        if (SubCategoryID != row["SubCategoryID"].ToString() && SubCategoryID!="")
                        {
                            dtTemp.Rows.Add(Session,SubCategoryID, SubCategory, E_NumberRange, "Excellent",(EName.Equals(""))?"": EName.Remove(0,1));
                            dtTemp.Rows.Add(Session, SubCategoryID, SubCategory, G_NumberRange, "Good", (GName.Equals("")) ? "" : GName.Remove(0, 1));
                            dtTemp.Rows.Add(Session, SubCategoryID, SubCategory, M_NumberRange, "Medium", (MName.Equals("")) ? "" : MName.Remove(0, 1));
                            dtTemp.Rows.Add(Session, SubCategoryID, SubCategory, W_NumberRange, "Weak", (WName.Equals("")) ? "" : WName.Remove(0, 1));
                            dtTemp.Rows.Add(Session, SubCategoryID, SubCategory, VW_NumberRange, "So Weak", (VWName.Equals("")) ? "" : VWName.Remove(0, 1));

                            EName = "";
                            GName = "";
                            MName = "";
                            WName = "";
                            VWName = "";

                        }
                        Session= row["Session"].ToString();
                        SubCategoryID = row["SubCategoryID"].ToString();
                        SubCategory = row["SubCategory"].ToString();

                        E_NumberRange = row["Excellent"].ToString();
                        G_NumberRange = row["Good"].ToString();
                        M_NumberRange = row["Medium"].ToString();
                        W_NumberRange = row["Weak"].ToString();
                        VW_NumberRange = row["SoWeak"].ToString();

                        if (float.Parse(row["ObtainNumber"].ToString()) >= float.Parse(row["Excellent"].ToString()))
                        {
                            EName += "," + row["EName"].ToString();
                        }
                        else if (float.Parse(row["ObtainNumber"].ToString()) >= float.Parse(row["Good"].ToString()))
                        {
                            GName += "," + row["EName"].ToString();
                        }
                        else if (float.Parse(row["ObtainNumber"].ToString()) >= float.Parse(row["Medium"].ToString()))
                        {
                            MName += "," + row["EName"].ToString();
                        }
                        else if (float.Parse(row["ObtainNumber"].ToString()) >= float.Parse(row["Weak"].ToString()))
                        {
                            WName += "," + row["EName"].ToString();
                        }
                        else
                        {
                            VWName+= "," + row["EName"].ToString();
                        }
                    }
                    //string Excellent = dt.Select("select EName where ObtainNumber>=3").ToString(); // How to write this query
                    //string Good = dt.Select("select EName where ObtainNumber>=Good").ToString(); // How to write this query
                    //string Excellent = dt.Select("select EName where ObtainNumber>=Excellent").ToString(); // How to write this query
                    //string Excellent = dt.Select("select EName where ObtainNumber>=Excellent").ToString(); // How to write this query
                    //string Excellent = dt.Select("select EName where ObtainNumber>=Excellent").ToString(); // How to write this query
                    //for (int i = 0; i < dt.Rows.Count; i++)
                    //{
                    //    float obNub = float.Parse(dt.Rows[i]["ObtainNumber"].ToString());
                    //    if (obNub >= float.Parse(dt.Rows[i]["Excellent"].ToString()))
                    //        EName += dt.Rows[i]["Ename"].ToString();

                    //}
                    

                    HttpContext.Current.Session["__SubIndicatorBasedPerformanceReport__"] = dtTemp;
                    return true;
                }
                    


            }
            catch { return false; }

        }
        public static DataTable returnDataTabelFinalGradeSheet(string SessionId)
        {
            try
            {
//                CRUD.ExecuteNonQuery(@"create view ns as
//SELECT ns.EID, ei.ECardNo, ei.EName, ei.DId, ei.DName, ns.SessionID, ns.SubCategoryID, sc.SubCategory, np.FullNumber, sc.Ordering,c.Ordering AS OrderingC,ns.MemberID, 
//ua.FirstName,ns.ObtainNumber FROM dbo.TE_NumberSheet AS ns INNER JOIN dbo.v_EmployeeInfo AS ei ON ns.EID = ei.EID INNER JOIN dbo.TE_SubCategory AS sc 
//ON ns.SubCategoryID = sc.SubCategoryID INNER JOIN dbo.TE_Session AS s ON ns.SessionID = s.SessionID INNER JOIN dbo.TE_NumberPatternDetails AS np ON 
//s.NumPatternID = np.NumPatternID AND ns.SubCategoryID = np.SubCategoryID INNER JOIN dbo.UserAccount AS ua ON ns.MemberID = ua.UserId INNER JOIN dbo.TE_Category    
//AS c ON sc.CategoryID = c.CategoryID where ns.SessionID=" + SessionId + "  ");
//                dt = new DataTable();
//                dt = CRUD.ReturnTableNull(@"DECLARE @cols AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX); 
//select @cols =  STUFF((SELECT ',' + QUOTENAME(ns.SubCategory)  from ns group by ns.SubCategory, OrderingC,ordering  order by OrderingC,ordering FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')	set @query = N'SELECT DName,EName,ECardNo,EId,FirstName, ' + @cols + N' from (select DName,EName,ECardNo, EId,MemberId,FirstName,ObtainNumber, SubCategory from ns ) x pivot ( sum(ObtainNumber) for SubCategory in (' + @cols + N') )  p  order by ECardNo' 
// exec sp_executesql @query;");
//                CRUD.ExecuteNonQuery("drop view ns");
                sql = @"DECLARE @cols AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) select @cols =  STUFF((SELECT ',' + QUOTENAME(sc.SubCategory) 
 from dbo.TE_NumberSheet AS ns INNER JOIN dbo.v_EmployeeInfo AS ei ON ns.EID = ei.EID INNER JOIN dbo.TE_SubCategory AS sc ON ns.SubCategoryID = sc.SubCategoryID INNER JOIN dbo.TE_Session AS s ON ns.SessionID = s.SessionID INNER JOIN dbo.TE_NumberPatternDetails AS np ON s.NumPatternID = np.NumPatternID AND ns.SubCategoryID = np.SubCategoryID INNER JOIN dbo.UserAccount AS ua ON ns.MemberID = ua.UserId INNER JOIN dbo.TE_Category    AS c ON sc.CategoryID = c.CategoryID 
 where ns.SessionID="+SessionId+"group by sc.SubCategory, c.Ordering,sc.ordering  order by c.Ordering,sc.ordering FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')	set @query = N'SELECT DName,EName,ECardNo,EId,FirstName, ' + @cols + N' from (select ei.DName,ei.EName,ei.ECardNo, ei.EId,ns.MemberId,ua.FirstName,ns.ObtainNumber, sc.SubCategory from dbo.TE_NumberSheet AS ns INNER JOIN dbo.v_EmployeeInfo AS ei ON ns.EID = ei.EID INNER JOIN dbo.TE_SubCategory AS sc ON ns.SubCategoryID = sc.SubCategoryID INNER JOIN dbo.TE_Session AS s ON ns.SessionID = s.SessionID INNER JOIN dbo.TE_NumberPatternDetails AS np ON s.NumPatternID = np.NumPatternID AND ns.SubCategoryID = np.SubCategoryID INNER JOIN dbo.UserAccount AS ua ON ns.MemberID = ua.UserId INNER JOIN dbo.TE_Category    AS c ON sc.CategoryID = c.CategoryID where ns.SessionID="+SessionId+" ) x pivot ( sum(ObtainNumber) for SubCategory in (' + @cols + N') )  p  order by ECardNo' exec sp_executesql @query;";
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return null; }

        }
        public static DataTable returnDataTabelFinalGradeSheet(string SessionId,string MemberID)
        {
            try
            {
               
                sql = @"DECLARE @cols AS NVARCHAR(MAX),@query  AS NVARCHAR(MAX) select @cols =  STUFF((SELECT ',' + QUOTENAME(sc.SubCategory) 
 from dbo.TE_NumberSheet AS ns INNER JOIN dbo.v_EmployeeInfo AS ei ON ns.EID = ei.EID INNER JOIN dbo.TE_SubCategory AS sc ON ns.SubCategoryID = sc.SubCategoryID INNER JOIN dbo.TE_Session AS s ON ns.SessionID = s.SessionID INNER JOIN dbo.TE_NumberPatternDetails AS np ON s.NumPatternID = np.NumPatternID AND ns.SubCategoryID = np.SubCategoryID INNER JOIN dbo.UserAccount AS ua ON ns.MemberID = ua.UserId INNER JOIN dbo.TE_Category    AS c ON sc.CategoryID = c.CategoryID 
 where ns.SessionID=" + SessionId + " and ns.MemberID="+MemberID+" group by sc.SubCategory, c.Ordering,sc.ordering  order by c.Ordering,sc.ordering FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)') ,1,1,'')	set @query = N'SELECT DName,EName,ECardNo,EId,FirstName, ' + @cols + N' from (select ei.DName,ei.EName,ei.ECardNo, ei.EId,ns.MemberId,ua.FirstName,ns.ObtainNumber, sc.SubCategory from dbo.TE_NumberSheet AS ns INNER JOIN dbo.v_EmployeeInfo AS ei ON ns.EID = ei.EID INNER JOIN dbo.TE_SubCategory AS sc ON ns.SubCategoryID = sc.SubCategoryID INNER JOIN dbo.TE_Session AS s ON ns.SessionID = s.SessionID INNER JOIN dbo.TE_NumberPatternDetails AS np ON s.NumPatternID = np.NumPatternID AND ns.SubCategoryID = np.SubCategoryID INNER JOIN dbo.UserAccount AS ua ON ns.MemberID = ua.UserId INNER JOIN dbo.TE_Category    AS c ON sc.CategoryID = c.CategoryID where ns.SessionID=" + SessionId + " and ns.MemberID="+MemberID+") x pivot ( sum(ObtainNumber) for SubCategory in (' + @cols + N') )  p  order by ECardNo' exec sp_executesql @query;";
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return null; }

        }
        public static DataTable returnDataTabelEvalutaroComparativeReport(string SessionId)
        {
            try
            {

                sql = @"with  ms as(select EID,MemberID, sum(ObtainNumber) ObtainNumber,RANK() OVER (PARTITION BY MemberID ORDER BY sum(ObtainNumber)  desc) RankByInd from TE_NumberSheet where SessionID=" + SessionId+"   Group by EID,MemberID ) ,  ms1 as(" +
                    "select EID, sum(ObtainNumber) ObtainNumberAll,RANK() OVER (ORDER BY sum(ObtainNumber)  desc) RankByAll from TE_NumberSheet where SessionID= " + SessionId+"    Group by EID) " +
                    "select  ms.EID,emp.EName,emp.DName,MemberID,ua.FirstName,RankByInd,ObtainNumber,RankByAll,ObtainNumberAll from  ms left join ms1  on ms.Eid=ms1.Eid inner join UserAccount ua on ms.MemberID=ua.UserId inner join v_EmployeeInfo emp on ms.EID=emp.EID order by emp.DName, ms.EID,ms.MemberID";
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return null; }

        }
        public static DataTable returnSubCatergory(string SessionId)
        {
            try
            {
                sql = "select sc.SubCategory,np.FullNumber from TE_Session s inner join TE_NumberPatternDetails np on s.NumPatternId=np.NumPatternId inner join TE_SubCategory sc on np.SubCategoryID=sc.SubCategoryID "+
                    "where s.SessionID=" + SessionId + "";
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return null; }

        }
        public static DataTable returnCommitteeMember(string SessionId)
        {
            try
            {
                sql ="SELECT FirstName FROM TE_Committee c inner join UserAccount us on c.MemberId=us.Userid "+
                     "where SessionID=" + SessionId + "";
                dt = new DataTable();
                dt = CRUD.ReturnTableNull(sql);
                return dt;
            }
            catch { return null; }

        }
    }
}
