using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using DS.BLL;

namespace DS.Forms
{
    public partial class FeesCollection : System.Web.UI.Page
    {
        DataTable dt;
        static float total = 0;
        static float fineTotal = 0;
        static string StudentId;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        Classes.commonTask.loadBatch(dlBatch);
                        Classes.commonTask.loadSection(dlSection);
                        StoresFine();
                    }
                }
                txtRoll.Focus();
            }
            catch { }
        }
        private void loadCurrentStudentInfoInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select StudentId, Class, Section, RollNo from CurrentStudentInfo where RollNo='" + txtRoll.Text.Trim() + "' and Section='"
                +dlSection.Text+"' ", dt);
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }
       
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                total = 0;

                dt = new DataTable();
                sqlDB.fillDataTable("select convert(varchar(11),DateOfStart,106) as DateOfStart, convert(varchar(11),DateOfEnd,106) as DateOfEnd from v_DateOfPaymentDetails where FeeCatName='" + dlFeesCategory.Text + "'", dt);
                if (dt.Rows.Count > 0) lblPaymentDateStatus.Text = "Payment start at " + dt.Rows[0]["DateOfStart"].ToString() + " and end at " + dt.Rows[0]["DateOfEnd"].ToString();
                else lblPaymentDateStatus.Text = "";
                txtRoll.Focus();
                loadParticularCategory("");
            }
            catch { }
        }
       
        protected void txtRoll_TextChanged(object sender, EventArgs e)   
        {
           //searchByRoll();
        }

        DataTable dtDis = new DataTable();

        private void searchByRoll()
        {
            try
            {
               
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select StudentId,FullName from v_CurrentStudentInfo where BatchName='" + dlBatch.SelectedItem.Text + "' and RollNo='" 
                + txtRoll.Text.Trim() + "' and SectionName='" + dlSection.SelectedItem.Text + "'  and Shift='" + dlShift.SelectedItem.Text + "' ", dt);
                lblMessage.InnerHtml = "";
                if (dt.Rows.Count > 0)
                {
                    lblName.Text = "Name : " + dt.Rows[0]["FullName"].ToString();
                    Session["__FullName__"] = "Name : " + dt.Rows[0]["FullName"].ToString();
                    Session["__Roll__"] = "Roll Number : " + txtRoll.Text.Trim();
                    Session["__Class__"] = "Class : " + new String(dlBatch.Text.Where(Char.IsLetter).ToArray()) + " ( " + dlSection.SelectedItem.Text + " )";
                    Session["__FeeCategory__"] = "Category : " + dlFeesCategory.SelectedItem.Text;

                    StudentId = dt.Rows[0]["StudentId"].ToString();

                    sqlDB.fillDataTable("select PName,Discount from v_DiscountParticularDetails where BatchName='" + dlBatch.SelectedItem.Text + "' and RollNo='" 
                    + txtRoll.Text.Trim() + "' and SectionName='" + dlSection.SelectedItem.Text + "' and Shift='" + dlShift.SelectedItem.Text + "'  ", dtDis);
                    if (dtDis.Rows.Count > 0) loadParticularCategoryWithDiscountList("");
                    else
                    {
                        total = 0;
                        loadParticularCategory("");
                    }
                   
                    loadStudentFineInfo();
                }
                else
                {
                    lblName.Text = "Roll not found";
                    lblFineNote.Text = "";
                }
                txtRoll.Focus();

            }
            catch { }
        }

        DataTable dtPL ;   //PL=Particular List
        private void loadParticularCategory(string sqlCmd)
        {
            try
            {
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select  CatPId, FeeCatName,PName, Amount from v_FeesCatDetails where FeeCatName='"+dlFeesCategory.Text+"' ";
                Session["__FeeCatName__"] = dlFeesCategory.Text;
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dtPL=new DataTable());

                int totalRows = dtPL.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Particular Category</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th>Particular Name</th>";
                divInfo += "<th class='numeric' style='width:100px;'>Amount</th>";

                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";


                for (int x = 0; x < dtPL.Rows.Count; x++)
                {
                    int sl = x + 1;
                    id = dtPL.Rows[x]["CatPId"].ToString();
                    divInfo += "<tr id='r_" + id + "' style='width:50px'>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td >" + dtPL.Rows[x]["PName"].ToString() + "</td>";
                    divInfo += "<td class='numeric'>" + dtPL.Rows[x]["Amount"].ToString() + "</td>";

                    total += float.Parse(dtPL.Rows[x]["Amount"].ToString());
                }

                divInfo += "</tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Total :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> " + total + "</td>";


                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                Session["__Total__"] = total;
               // divInfo += "<div class='dataTables_wrapper'><div class='head'><p style='float:left; width:90%; text-align:right;'>Total : </p> <p style='text-align:center;  width:10%; float:right;'>  " + total + "  </p>   </div></div>";

                divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));
                Session["__FeeCollectionReport__"] = divInfo;
            }
            catch { }
        }

        private void loadParticularCategoryWithDiscountList(string sqlCmd)
        {
            try
            {             
                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select  CatPId, FeeCatName,PName, Amount from v_FeesCatDetails where FeeCatName='"
                + dlFeesCategory.SelectedItem.Text + "' ";
                Session["__FeeCatName__"] = dlFeesCategory.Text;
                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dtPL=new DataTable ());

                int totalRows = dtPL.Rows.Count;
                string divInfo = "";
                total = 0;
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Particular Category</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblParticularCategory' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th>Particular Name</th>";
                divInfo += "<th class='numeric' style='width:100px;'>Amount</th>";

                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                string id = "";


                for (int x = 0; x < dtPL.Rows.Count; x++)
                {
                    int sl = x + 1;
                    id = dtPL.Rows[x]["CatPId"].ToString();
                    divInfo += "<tr id='r_" + id + "' style='width:50px'>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    float [] getCVal=checkDiscountParticular((byte)x);

                    if ((getCVal[0]==0.0)  && (getCVal[1]==0.0) && (getCVal[2]==0.0))
                    {
                        divInfo += "<td >" + dtPL.Rows[x]["PName"].ToString() + "</td>";
                        divInfo += "<td class='numeric'>" + dtPL.Rows[x]["Amount"].ToString() + "</td>";
                        total += float.Parse(dtPL.Rows[x]["Amount"].ToString());
                    }
                    else
                    {
                        divInfo += "<td >" + dtPL.Rows[x]["PName"].ToString() + " <span style='color:red; font-weight: bold;'>(D. " + getCVal[2] + "% " 
                        + dtPL.Rows[x]["Amount"].ToString() + "-" + getCVal[0] + ") </span>" + "</td>";
                        divInfo += "<td class='numeric'>" + getCVal[1] + "</td>";
                        total += getCVal[1];
                    }
                   
                }

                divInfo += "</tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Total :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> " + total + "</td>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table><br/>";
                Session["__Total__"] = total;
               //divInfo += "<div class='dataTables_wrapper'><div class='head'><p style='float:left; width:90%; text-align:right;'>Total : </p> <p style='text-align:center;  width:10%; float:right;'>  " + total + "  </p>   </div></div>";
              
                divParticularCategoryList.Controls.Add(new LiteralControl(divInfo));

                Session["__FeeCollectionReport__"] = divInfo;
            }
            catch { }
        }

        float discountTk = 0;
        private float [] checkDiscountParticular(byte rowIndex)
        {
            //try
            //{
                    float [] getCalculationValue=new float [3];
                  
                    for (byte b = 0; b < dtDis.Rows.Count; b++)
                    {
                        if (dtPL.Rows[rowIndex]["PName"].ToString().Equals(dtDis.Rows[b]["PName"].ToString()))
                        {
                            getCalculationValue[0] = (float.Parse(dtPL.Rows[rowIndex]["Amount"].ToString()) * float.Parse(dtDis.Rows[b]["Discount"].ToString())) / 100;
                            getCalculationValue[1]=(float.Parse(dtPL.Rows[rowIndex]["Amount"].ToString()))-getCalculationValue[0];
                            getCalculationValue[2] = float.Parse(dtDis.Rows[b]["Discount"].ToString());
                            discountTk += getCalculationValue[0];
                            break;
                        }
                       
                    }
                    Session["__DiscountTk__"] = discountTk;
                    return getCalculationValue;
            //}
            //catch { return float [] val=new float [2];}
        }

        private Boolean updateStudentPayment()
        {
            try
            {           
                DataTable dt = new DataTable();
                SqlParameter[] prms = { new SqlParameter("@FeeCatName", dlFeesCategory.SelectedItem.Text) };
                sqlDB.fillDataTable("Select FeeCatId from FeesCategoryInfo where FeeCatName=@FeeCatName ", prms, dt);


                DataTable dtst = new DataTable();
                sqlDB.fillDataTable("Select  FeeCatId, StudentId, RollNo, PayStatus from StudentPayment where BatchName='" + dlBatch.SelectedItem.Text+ "' and "
                +"SectionName='" + dlSection.SelectedItem.Text + "' and Shift='"+dlShift.SelectedItem.Text+"' and RollNo='" + txtRoll.Text.Trim() + "' and FeeCatId='" 
                + dt.Rows[0]["FeeCatId"].ToString() + "' ", dtst);

                if (dtst.Rows.Count > 0)
                {
                    if (dtst.Rows[0]["PayStatus"].ToString() == "True")
                    {
                        lblMessage.InnerText = "warning->Already paid this category";
                        Session["__PayStatus__"] = "Already Paid";
                        return false;
                    }
                }

                if (dtst.Rows.Count > 0)
                {
                    SqlCommand cmd = new SqlCommand("Update StudentPayment  Set DateOfPayment=@DateOfPayment, PayStatus=@PayStatus, AmountPaid=@AmountPaid, "
                    +"DiscountStatus=@DiscountStatus, DiscountTK=@DiscountTK, FineAmount=@FineAmount where BatchName=@BatchName and SectionName=@SectionName and "
                    +"Shift=@Shift and RollNo=@RollNo and FeeCatId=@FeeCatId ", sqlDB.connection);

                    cmd.Parameters.AddWithValue("@BatchName", dlBatch.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@SectionName", dlSection.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Shift", dlShift.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@RollNo", txtRoll.Text.Trim());
                    cmd.Parameters.AddWithValue("@FeeCatId", dt.Rows[0]["FeeCatId"].ToString());

                    cmd.Parameters.AddWithValue("@DateOfPayment", TimeZoneBD.getCurrentTimeBD().ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@PayStatus", "True");

                    if (Session["__TotalFine__"] == null)
                    {
                        cmd.Parameters.AddWithValue("@AmountPaid", total);
                        cmd.Parameters.AddWithValue("@FineAmount", 0);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@AmountPaid", total + float.Parse(Session["__TotalFine__"].ToString()));
                        cmd.Parameters.AddWithValue("@FineAmount", Session["__TotalFine__"].ToString());
                    }
                    
                    if (Session["__DiscountTk__"] != null) //for Discount amount
                    {
                        cmd.Parameters.AddWithValue("@DiscountStatus", 1);
                        cmd.Parameters.AddWithValue("@DiscountTK", Session["__DiscountTk__"]);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DiscountStatus", 0);
                        cmd.Parameters.AddWithValue("@DiscountTK", 0);
                    }

                    cmd.ExecuteNonQuery();
                    Session["__TotalFine__"] =null;
                    lblMessage.InnerText = "success-> Payment Successfull";
                       
                    txtRoll.Text = "";
                    lblFineNote.Text = "";
                    Session["__PayStatus__"] = "New Payment";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/FeeCollectionReport.aspx');", true);  //Open New Tab for Sever side code
                    return true;                 
                }

                return false;
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(checkInput()==true) updateStudentPayment();
        }

        bool checkInput()
        {
            try
            {
                if (total > 0 && txtRoll.Text!="")
                {
                    return true;
                }
                lblMessage.InnerText = "warning->Input roll number";
                return false;
            }
            catch { return false; }
        }

        private void loadStudentFineInfo() // Check student fine information
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select  sum(Fineamount) as Fineamount from StudentFine where BatchName='" + dlBatch.SelectedItem.Text + "' and Studentid='" 
                    + StudentId + "' and (FineamountPaid is null or FineamountPaid =0)  and PayDate is null ", dt);

                if (dt.Rows.Count < 1 )
                {
                    lblFineNote.Text = "You have no fine.";
                }
                else
                {
                    if (dt.Rows[0]["Fineamount"].ToString() != "")
                    {
                        fineTotal += float.Parse(dt.Rows[0]["Fineamount"].ToString());
                        lblFineNote.Text = "Total Fine : " + dt.Rows[0]["Fineamount"].ToString();
                    }

                    loadFeeFine("");   // generate studentfine information if his already fined
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        private void StoresFine()
        {
            try
            {              
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select BatchName, FeeCatId, StudentId, FeeFine, FeeCatName from v_getFineStudentInfo where DateOfEnd < '" 
                + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd") + "' and PayStatus='False' AND IsActive="+1+"", dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("Insert into  StudentFine (BatchName, StudentId, FinePurpose, Fineamount, Discount, FineamountPaid)  "
                    +"values (@BatchName, @StudentId, @FinePurpose, @Fineamount, @Discount, @FineamountPaid) ", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@BatchName", dt.Rows[i]["BatchName"].ToString());
                    cmd.Parameters.AddWithValue("@StudentId", dt.Rows[i]["StudentId"].ToString());
                    cmd.Parameters.AddWithValue("@FinePurpose", dt.Rows[i]["FeeCatName"].ToString());
                    cmd.Parameters.AddWithValue("@Fineamount", dt.Rows[i]["FeeFine"].ToString());
                    cmd.Parameters.AddWithValue("@Discount", "0");
                    cmd.Parameters.AddWithValue("@FineamountPaid", "0");          
                    int result = (int)cmd.ExecuteNonQuery();
                }

                sqlDB.fillDataTable("select distinct FeeCatId from v_getFineStudentInfo where DateOfEnd < '" + TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd") + "' AND IsActive=" 
                    +1+ "", dt=new DataTable ());
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand(" update DateOfPayment  Set IsActive=@IsActive where FeeCatId=@FeeCatId ", sqlDB.connection);

                    cmd.Parameters.AddWithValue("@FeeCatId", dt.Rows[i]["FeeCatId"].ToString());
                    cmd.Parameters.AddWithValue("@IsActive", "False");
                    cmd.ExecuteNonQuery();
                    lblMessage.InnerText = "success->Successfully saved";
                }
                    
            }
            catch { }
        }
        
        private void loadFeeFine(string sqlCmd)   // generate studentfine information if his already fined
        { 
            try
            {               
                Session["__storeFineInfo__"]="";

                if (string.IsNullOrEmpty(sqlCmd)) sqlCmd = "Select FineId,FinePurpose, Fineamount from StudentFine where BatchName='" + dlBatch.SelectedItem.Text 
                    + "' and Studentid='" + StudentId + "' and PayDate is null and (FineamountPaid is null or FineamountPaid=0) ";

                DataTable dt = new DataTable();
                sqlDB.fillDataTable(sqlCmd, dt);

                int totalRows = dt.Rows.Count;
                string divInfo = "";

                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Fee Fine</div>";
                   // divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divFineInfo.Controls.Add(new LiteralControl(divInfo));
                    return;
                }


                divInfo = " <table id='tblFine' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";

                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th>Fine Purpose</th>";             
                divInfo += "<th class='numeric' style='width:100px;'>Select</th>";
                divInfo += "<th class='numeric' style='width:100px;'>Fine Amount</th>";

                divInfo += "</tr>";

                divInfo += "</thead>";

                divInfo += "<tbody>";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    int sl = x + 1;
                    divInfo += "<tr>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FinePurpose"].ToString() + "</td>";
                    divInfo += "<td class='numeric'> <input onchange='checkFine(this)' type='checkbox' id='"+ dt.Rows[x]["FinePurpose"].ToString()+"_"
                    + dt.Rows[x]["FineId"].ToString() +"'  value='" + dt.Rows[x]["Fineamount"].ToString() + "' > </td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["Fineamount"].ToString() + "</td>";
                   // fineTotal += float.Parse(dt.Rows[x]["Fineamount"].ToString());
                }

                divInfo += "</tr>";
                divInfo += "<td ></td>";
                divInfo += "<td style='border-left:none'></td>";
                divInfo += "<td style='text-align:right; font-weight: bold; border-left:none'> Total :</td>";
                divInfo += "<td style='font-weight: bold; text-align:center'> " + fineTotal + "</td>";

                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
               // divInfo += "<div class='dataTables_wrapper'><div class='head'><p style='float:left; width:89%; text-align:right;'>Total : </p> <p style='text-align:center;  width:11%; float:right; '>  " + fineTotal + "  </p>   </div></div>";

                divFineInfo.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnSearchByRoll_Click(object sender, EventArgs e)
        {
            searchByRoll();
            fineTotal = 0;
        }
           
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dlFeesCategory.Items.Clear();
                sqlDB.loadDropDownList("Select FeeCatName From FeesCategoryInfo where BatchName='" + dlBatch.Text + "'  ", dlFeesCategory);
            }
            catch { }
        }

    }
}