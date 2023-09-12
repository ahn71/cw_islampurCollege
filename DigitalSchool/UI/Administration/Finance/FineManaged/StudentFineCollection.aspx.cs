using DS.BLL.Admission;
using DS.BLL.ControlPanel;
using DS.BLL.Finance;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.DAL.AdviitDAL;
using DS.PropertyEntities.Model.Finance;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Finance.FineManaged
{
    public partial class StudentFineCollection : System.Web.UI.Page
    {
        static string studentId;
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        StudentFine stdFine;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                    if (!IsPostBack)
                    {
                        if (!PrivilegeOperation.SetPrivilegeControl(float.Parse(Session["__UserTypeId__"].ToString()), "StudentFineCollection.aspx")) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                        BatchEntry.GetDropdownlist(dlBatch, "True");
                        ShiftEntry.GetDropDownList(dlShift);                                                  
                    }
                lblMessage.InnerText = "";
            }
            catch { }
        }
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownListClsGrpId(int.Parse(BatchClsID[1]), dlGroup);
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), dlGroup.SelectedValue);
        }

        protected void dlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), dlGroup.SelectedValue);
        }

        protected void dlSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            if (currentstdEntry == null)
            {
                currentstdEntry = new CurrentStdEntry();
            }
            currentstdEntry.GetRollNo(dlRoll, dlShift.SelectedValue, BatchClsID[0], dlGroup.SelectedValue, dlSection.SelectedValue);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadFeeFine("");
            Session["__fineCollection__"] = " ";
        }


        DataTable dt = new DataTable();
        private void loadFeeFine(string sqlCmd)   // generate studentfine information if his already fined
        {            
            try
            {
                

                string divInfo = "";
                bool dataRow = false;                
                if(currentstdEntry==null)
                {
                    currentstdEntry=new CurrentStdEntry();
                }
                dt = currentstdEntry.GetStudentNameforStdFine(dlRoll.SelectedValue);   //..........load Student Name From Current Student Table
                if (dt.Rows.Count > 0) lblStudentName.Text = "Name : " + dt.Rows[0]["FullName"].ToString();
                if (stdFine == null)
                {
                    stdFine = new StudentFine();
                }
                dt = new DataTable();
                dt = stdFine.GetStudentFine(dlRoll.SelectedValue);        //....Load Student Fine From Student Payment Table        
                int totalRows = dt.Rows.Count;
                if (totalRows > 0)
                {
                    dataRow = true;
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
                 int sl=0;
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    sl = sl + 1;
                    divInfo += "<tr>";
                    divInfo += "<td class='numeric'>" + sl + "</td>";
                    divInfo += "<td >" + dt.Rows[x]["FinePurpose"].ToString() + "</td>";
                    divInfo += "<td class='numeric'> <input onchange='checkFine(this)' type='checkbox' id='" + dt.Rows[x]["FinePurpose"].ToString() + "_" + dt.Rows[x]["FineId"].ToString() + "'  value='" + dt.Rows[x]["Fineamount"].ToString() + "' > </td>";
                    divInfo += "<td class='numeric'>" + dt.Rows[x]["Fineamount"].ToString() + "</td>";
                }
                dt = new DataTable();
                dt = stdFine.GetAbsentFine(dlRoll.SelectedValue);     //....Load Absent Fine From Student Absent Details Table           
                if (dt.Rows[0]["Fineamount"].ToString() != "")
                {
                    dataRow = true;
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        sl = sl + 1;
                        divInfo += "<tr>";
                        divInfo += "<td class='numeric'>" + sl + "</td>";
                        divInfo += "<td >absent</td>";
                        divInfo += "<td class='numeric'> <input onchange='checkFine(this)' type='checkbox' id='absent_0'  value='" + dt.Rows[x]["Fineamount"].ToString() + "' > </td>";
                        divInfo += "<td class='numeric'>" + dt.Rows[x]["Fineamount"].ToString() + "</td>";
                    }
                }
                if (dataRow == false)
                {
                    divInfo += "<tr><td colspan='4'>No Fine</td></tr></tbody></table>";
                     divFineInfo.Controls.Add(new LiteralControl(divInfo));
                     btnPayNow.Visible = false; ;
                     return;                   
                }
                btnPayNow.Visible = true; ;
                divInfo += "</tbody>";
                divInfo += "<tfoot>";

                divInfo += "</table>";
                divFineInfo.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnPayNow_Click(object sender, EventArgs e)
        {
            try
            {
                bool status = true;
                //-----------------------------------------------------------------------------------------------------------------
                DataTable dt = new DataTable();
                try
                {
                    dt = (DataTable)Session["__fineCollection__"];   //Load From ajax.aspx 
                }
                catch { status = false; }
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    if (dt.Rows[x]["Id"].ToString() == "0")
                    {
                        if (stdFine == null)
                        {
                            stdFine = new StudentFine();
                        }
                        StudentFineEntities stdfineEntities = new StudentFineEntities();                        
                        stdfineEntities.PayDate = DateTime.Now;
                        stdFine.AddEntities = stdfineEntities;
                        stdFine.AbsentUpdate(dlRoll.SelectedValue);
                        
                    }
                    else
                    {
                        if (stdFine == null)
                        {
                            stdFine = new StudentFine();
                        }
                        StudentFineEntities stdfineEntities = new StudentFineEntities();
                        stdfineEntities.FineId = int.Parse(dt.Rows[x]["Id"].ToString());
                        stdfineEntities.FineamountPaid = double.Parse(dt.Rows[x]["FineAmount"].ToString());
                        stdfineEntities.PayDate = DateTime.Now;
                        stdFine.AddEntities = stdfineEntities;
                        stdFine.Update();
                    }
                }               
                loadFeeFine("");
                if (status == false)
                {
                    lblMessage.InnerText = "warning->No Fine ";
                }
                else
                {
                    Session["__fineCollection__"] = "";
                    lblMessage.InnerText = "success->Payment Successfull";
                    DataTable dtFine = new DataTable();
                    dtFine.Columns.Add("FullName", typeof(string));
                    dtFine.Columns.Add("ShiftName", typeof(string));
                    dtFine.Columns.Add("BatchName", typeof(string));
                    dtFine.Columns.Add("GroupName", typeof(string));
                    dtFine.Columns.Add("SectionName", typeof(string));
                    dtFine.Columns.Add("RollNo", typeof(int));
                    dtFine.Columns.Add("FinePurpose", typeof(string));
                    dtFine.Columns.Add("FineAmount", typeof(decimal));
                    dtFine.Columns.Add("PaymentDate",typeof(string));
                    string[] Name = lblStudentName.Text.Split(':');
                    for (int x = 0; x < dt.Rows.Count; x++)
                    {
                        dtFine.Rows.Add(Name[1], dlShift.SelectedItem.Text,
                            dlBatch.SelectedItem.Text,dlGroup.SelectedItem.Text,
                            dlSection.SelectedItem.Text,dlRoll.SelectedItem.Text,
                            dt.Rows[x]["FinePurpose"].ToString(), dt.Rows[x]["FineAmount"].ToString(),
                            DateTime.Now.ToString("dd-MM-yyyy"));
                    }
                    Session["__IndFineCollectionReport__"]=dtFine;
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/UI/Reports/CrystalReport/ReportViewer.aspx?for=IndFineCollectionReport');", true);  //Open New Tab for Sever side code
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }       
    }
}