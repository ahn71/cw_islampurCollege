using adviitRuntimeScripting;
using DS.BLL;
using DS.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS
{
    public partial class ajax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {           
            loadAjaxCode();
        }

        private void loadAjaxCode()
        {
            string dataId = Request.QueryString["id"];
            if (Request.QueryString["todo"] == "Discount")
            {
                try
                {
                    string sqlcmd = "";
                    string BatchName = Request.QueryString["BatchName"];
                    string SectionName = Request.QueryString["SectionName"];
                    if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select PName,Discount From V_Discount where BatchName='" + BatchName + "' and SectionName='" + SectionName + "' and RollNo=" + Request.QueryString["Roll"] + " ";
                    DataTable dt = new DataTable();
                    sqlDB.fillDataTable(sqlcmd, dt);

                    int totalRows = dt.Rows.Count;
                    string divInfo = "";

                    if (totalRows == 0)
                    {
                        divInfo = "<div class='noData'>No Discount List available</div>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                        Response.Write(divInfo);
                        return;
                    }

                    divInfo = " <table id='tblBatch' class='display'  > ";
                    divInfo += "<thead>";
                    divInfo += "<tr>";
                    divInfo += "<th>Particulars Name</th>";
                    divInfo += "<th>Discount (%)</th>";
                    divInfo += "</tr>";

                    divInfo += "</thead>";

                    divInfo += "<tbody>";
                    int id = 0;
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        id = x + 1; ;
                        divInfo += "<tr id='r_" + id + "'>";
                        divInfo += "<td >" + dt.Rows[x]["PName"].ToString() + "</td>";

                        divInfo += "<td >" + dt.Rows[x]["Discount"].ToString() + "</td>";
                    }

                    divInfo += "</tbody>";
                    divInfo += "<tfoot>";

                    divInfo += "</table>";
                    Response.Write(divInfo);
                }
                catch { }
            }
            if (Request.QueryString["todo"] == "loadthana")
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt = CRUD.ReturnTableNull("select ThanaName,ThanaId from Thanas where DistrictId='" + dataId + "'");
                dt.TableName = "Thana";
                ds.Tables.Add(dt);
                Response.Write(ds.GetXml());
            }

            if (Request.QueryString["todo"] == "OrderingInfo")
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select Ordering From NewSubject where SubId=" + dataId + "", dt);
                string ordering = dt.Rows[0]["Ordering"].ToString();
                Response.Write(ordering);
            }

            if (Request.QueryString["todo"] == "plist")
            {
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@FeeCatId", dataId) };
                sqlDB.fillDataTable("Select CatPId, FeeCatId, PId, Amount from ParticularsCategory where FeeCatId=@FeeCatId ", dt);
                string divInfo = "";
                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th>Particular Name</th>";
                divInfo += "<th class='numeric'>Amount</th>";

                divInfo += "<th class='numeric control'>Delete</th>";
                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["CatPId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";

                    divInfo += "<td >" + dt.Rows[x]["PId"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["Amount"].ToString() + "</td>";

                    divInfo += "<td style='max-width:20px;' class='numeric control' >" + "<img src='/Images/action/delete.png'  onclick='delParticular(" + id + ',' + dt.Rows[x]["Amount"].ToString() + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                // divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                Response.Write(divInfo);
            }
            else if (Request.QueryString["todo"] == "delp")
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM ParticularsCategory WHERE CatPId = @CatPId", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@CatPId", dataId);
                    cmd.ExecuteNonQuery();

                    float val = float.Parse(Request.QueryString["val"]);
                    SqlCommand nm = new SqlCommand("UPDATE FeesCategoryInfo SET FeeAmount = (FeeAmount - " + val + ") ", sqlDB.connection);
                    nm.ExecuteNonQuery();

                    Response.Write("ok");
                }
                catch { }
            }
            else if (Request.QueryString["todo"] == "addp")
            {
                try
                {
                    string feeCategory = Request.QueryString["feeCategory"];
                    string particular = Request.QueryString["particular"];
                    string amount = Request.QueryString["amount"];

                    DataTable dtFeeCat = new DataTable();
                    SqlParameter[] prms = { new SqlParameter("@FeeCatName", feeCategory) };
                    sqlDB.fillDataTable("Select FeeCatId,FeeAmount from FeesCategoryInfo where FeeCatName=@FeeCatName ", dtFeeCat);

                    DataTable dtp = new DataTable();
                    SqlParameter[] prmsp = { new SqlParameter("@PName", particular) };
                    sqlDB.fillDataTable("Select PId  from ParticularsInfo where PName=@PName ", dtp);

                    DataTable dtpchk = new DataTable();
                    sqlDB.fillDataTable("Select PId,FeeCatId  from ParticularsCategory where FeeCatId='" + dtFeeCat.Rows[0]["FeeCatId"] + "' and PId='" + dtp.Rows[0]["PId"] + "' ", dtpchk);
                    if (dtpchk.Rows.Count > 0)
                    {
                        Response.Write("ExitsData");
                        return;
                    }

                    float TotalAmount = 0;

                    SqlCommand cmd = new SqlCommand("INSERT INTO ParticularsCategory (FeeCatId, PId, Amount) VALUES (@FeeCatId, @PId, @Amount)", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@FeeCatId", dtFeeCat.Rows[0]["FeeCatId"].ToString());
                    cmd.Parameters.AddWithValue("@PId", dtp.Rows[0]["PId"].ToString());
                    cmd.Parameters.AddWithValue("@Amount", amount);
                    cmd.ExecuteNonQuery();

                    if (dtFeeCat.Rows[0]["FeeAmount"].ToString().Length > 0) TotalAmount += float.Parse(dtFeeCat.Rows[0]["FeeAmount"].ToString());
                    else TotalAmount += 0;

                    SqlCommand cmdup = new SqlCommand("update FeesCategoryInfo  Set FeeAmount=@FeeAmount where FeeCatId=@FeeCatId ", sqlDB.connection);
                    cmdup.Parameters.AddWithValue("@FeeCatId", dtFeeCat.Rows[0]["FeeCatId"].ToString());
                    float tAmount = TotalAmount + float.Parse(amount);
                    cmdup.Parameters.AddWithValue("@FeeAmount", tAmount);
                    cmdup.ExecuteNonQuery();


                    DataTable pdt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT max(CatPId) as CatPId FROM ParticularsCategory ", sqlDB.connection);
                    da.Fill(pdt);

                    var lastInsertedID = pdt.Rows[0]["CatPId"];
                    Response.Write(lastInsertedID);
                }
                catch { }
            }
            else if (Request.QueryString["todo"] == "getAmountTotal")
            {
                try
                {
                    var FeeCategory = Request.QueryString["feeCategory"];
                    DataTable dtFeeCat = new DataTable();
                    SqlParameter[] prms = { new SqlParameter("@FeeCatName", FeeCategory) };
                    sqlDB.fillDataTable("Select FeeCatId,FeeAmount from FeesCategoryInfo where FeeCatName=@FeeCatName ", dtFeeCat);
                    var feeCatId = dtFeeCat.Rows[0]["FeeCatId"];

                    DataTable amountTotal = new DataTable();
                    SqlDataAdapter at = new SqlDataAdapter("SELECT sum(FeeAmount) as FeeAmount FROM FeesCategoryInfo WHERE FeeCatId = '" + feeCatId + "'", sqlDB.connection);
                    at.Fill(amountTotal);
                    var Total = amountTotal.Rows[0]["FeeAmount"];
                    Response.Write(Total);
                }
                catch { }
            }
            else if (Request.QueryString["todo"] == "putDate")
            {
                try
                {
                    if (Session["__PayStatus__"] != null)
                    {
                        if (Session["__PayStatus__"].ToString() == "Already Paid") return;
                        string famount = Request.QueryString["amount"];

                        SqlCommand cmd = new SqlCommand("Update StudentFine Set FineamountPaid=@FineamountPaid, PayDate=@PayDate where FineId=@FineId ", sqlDB.connection);

                        string fid = new String(dataId.Where(Char.IsNumber).ToArray());
                        cmd.Parameters.AddWithValue("@FineId", fid);
                        cmd.Parameters.AddWithValue("@FineamountPaid", famount);
                        cmd.Parameters.AddWithValue("@PayDate", TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
                    }
                }
                catch { }
            }

            else if (Request.QueryString["todo"].Equals("storeFineInfo"))
            {
                string[] getNeededInfo = Request.QueryString["id"].Split('_');
                storeFineDetails(getNeededInfo, float.Parse(Request.QueryString["amount"].ToString()), bool.Parse(Request.QueryString["status"].ToString()));
            }

            else if (Request.QueryString["todo"].Equals("fineCollection")) //Only Fine Collection for Any Time
            {
                string[] getNeededInfo = Request.QueryString["id"].Split('_');
                storeFineCollection(getNeededInfo, float.Parse(Request.QueryString["amount"].ToString()), bool.Parse(Request.QueryString["status"].ToString()));
            }

            else if (Request.QueryString["todo"] == "putFineDate") //Fine Collection Collection 
            {
                try
                {
                    if (Session["__SaveStatus__"] != null)
                    {
                        Session["__SaveStatus__"] = null;
                        return;
                    }
                    string famount = Request.QueryString["amount"];

                    SqlCommand cmd = new SqlCommand(" update StudentFine Set FineamountPaid=@FineamountPaid, PayDate=@PayDate where FineId=@FineId ", sqlDB.connection);

                    string fid = new String(dataId.Where(Char.IsNumber).ToArray());
                    cmd.Parameters.AddWithValue("@FineId", fid);
                    cmd.Parameters.AddWithValue("@FineamountPaid", famount);
                    cmd.Parameters.AddWithValue("@PayDate", TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd"));
                    cmd.ExecuteNonQuery();

                }
                catch { }
            }
            else if (Request.QueryString["todo"] == "AdmissionCollection")
            {
                try
                {
                    if (Request.QueryString["type"] == " Discount ")
                    {
                        double total = double.Parse(Session["__Total__"].ToString());
                        double payble = double.Parse(Session["__Payble__"].ToString());
                        double Paid = double.Parse(Session["__Paid__"].ToString());
                        double discount = double.Parse(dataId);
                        double due = double.Parse(Session["__Due__"].ToString());

                        payble = Math.Round((total - discount), 0);
                        due = Math.Round((payble - Paid), 0);
                        Session["__Discount__"] = Math.Round(discount);
                        Session["__Payble__"] = Math.Round(payble);
                        Session["__Paid__"] = Math.Round(Paid);
                        Session["__Due__"] = Math.Round(due);
                    }
                    else
                    {
                        double total = double.Parse(Session["__Total__"].ToString());
                        double payble = double.Parse(Session["__Payble__"].ToString());
                        double Paid = double.Parse(dataId);
                        double discount = double.Parse(Session["__Discount__"].ToString());
                        double due = double.Parse(Session["__Due__"].ToString());

                        payble = Math.Round((total - discount), 0);
                        due = Math.Round((payble - Paid), 0);
                        Session["__Discount__"] = Math.Round(discount);
                        Session["__Payble__"] = Math.Round(payble);
                        Session["__Paid__"] = Math.Round(Paid);
                        Session["__Due__"] = Math.Round(due);
                    }
                    Response.Write(Session["__Discount__"].ToString() + "_" + Session["__Payble__"].ToString() + "_" + Session["__Paid__"].ToString() + "_" + Session["__Due__"].ToString());
                    //Response.Write(Session["__Payble__"].ToString());
                }
                catch { }
            }
            else if (Request.QueryString["todo"] == "OthersParticularsAddition") 
            {

                if (Request.QueryString["type"] == " txtOthers ")
                {
                    Session["__Others__"] = dataId;
                    double total = double.Parse(Session["__PreTotal__"].ToString()) + double.Parse(dataId);
                    double discount = double.Parse(Session["__Discount__"].ToString());
                    double Paid = double.Parse(Session["__Paid__"].ToString());
                    double payble = Math.Round((total - discount), 0);
                    double due = Math.Round((payble - Paid), 0);
                    Session["__Total__"] = total;
                    Session["__Payble__"] = Math.Round(payble);
                    Session["__Due__"] = Math.Round(due);
                    Response.Write(Session["__Total__"].ToString() + "_" + Session["__Payble__"].ToString() + "_" + Session["__Due__"].ToString());
                }
                else if (Request.QueryString["type"] == " txtOthersText ")
                {
                    Session["__OthersText__"] = dataId;
                }
            }
        }
        DataTable dtStoreFineInfo = new DataTable();
        private void storeFineDetails(string [] getNeededInfo,float fineAmount,bool getStatus)
        {
            try
            {
                DataTable dt = new DataTable();
                if (getStatus)
                {
                    dtStoreFineInfo.Columns.Add("Id", typeof(int));
                    dtStoreFineInfo.Columns.Add("FinePurpose", typeof(string));
                    dtStoreFineInfo.Columns.Add("FineAmount", typeof(float));

                    dtStoreFineInfo.Rows.Add(getNeededInfo[1],getNeededInfo[0], fineAmount);
                    bool status = false;  //  when session is empty then status must be true.true menas new activities
                    try
                    {
                        dt = (DataTable)Session["__storeFineInfo__"];
                    }
                    catch { status = true; }
                    if (status)        // this circle for status active
                    {
                        Session["__storeFineInfo__"] = dtStoreFineInfo;
                        dt = (DataTable)Session["__storeFineInfo__"];
                    }
                    else
                    {
                        dt = (DataTable)Session["__storeFineInfo__"];
                        dt.Rows.Add(getNeededInfo[1],getNeededInfo[0], fineAmount);
                        Session["__storeFineInfo__"] = dt;
                    }
                }
                else
                {
                    dt = (DataTable)Session["__storeFineInfo__"];
                     DataRow [] row=dt.Select("Id="+getNeededInfo[1]+"");
                     dt.Rows.Remove(row[0]);
                     Session["__storeFineInfo__"] = dt;
                }
                var getTotalFine = dt.Compute("sum (FineAmount)", "");
                if (getTotalFine.ToString() == "")
                {
                    Session["__TotalFine__"] = "0.0";
                }
                else
                {
                    Session["__TotalFine__"] = getTotalFine;
                }
                if (dt.Rows.Count > 0) Response.Write(float.Parse(getTotalFine.ToString()) + float.Parse(Session["__Total__"].ToString()));
                else Response.Write(float.Parse(Session["__Total__"].ToString()));

            }
            catch { }
        }

        DataTable dtFineCollection = new DataTable();
        private void storeFineCollection(string[] getNeededInfo, float fineAmount, bool getStatus)   //Only Fine Collection for Any Time
        {
            try
            {
                DataTable dt = new DataTable();               
                if (getStatus)
                {
                    dtFineCollection.Columns.Add("Id", typeof(int));
                    dtFineCollection.Columns.Add("FinePurpose", typeof(string));
                    dtFineCollection.Columns.Add("FineAmount", typeof(float));

                    dtFineCollection.Rows.Add(getNeededInfo[1], getNeededInfo[0], fineAmount);
                    bool status = false;  //  when session is empty then status must be true.true menas new activities
                    try
                    {                     
                        dt = (DataTable)Session["__fineCollection__"];
                    } 
                    catch { status = true; }
                    if (status)        // this circle for status active
                    {
                        Session["__fineCollection__"] = dtFineCollection;
                        dt = (DataTable)Session["__fineCollection__"];
                    }
                    else
                    {
                        dt = (DataTable)Session["__fineCollection__"];
                        dt.Rows.Add(getNeededInfo[1], getNeededInfo[0], fineAmount);
                        Session["__fineCollection__"] = dt;
                    }
                }
                else
                {
                    dt = (DataTable)Session["__fineCollection__"];
                    DataRow[] row = dt.Select("Id=" + getNeededInfo[1] + "");
                    dt.Rows.Remove(row[0]);
                    Session["__fineCollection__"] = dt;
                }
                var getTotalFine = dt.Compute("sum (FineAmount)", "");
                Session["__TotalFineCollection__"] = getTotalFine;

                if (Session["__TotalFineCollection__"].ToString() != "") Response.Write(float.Parse(Session["__TotalFineCollection__"].ToString()));
                else Response.Write("0");
            }
            catch { }
        }



    }
}