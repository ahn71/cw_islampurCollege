using ComplexScriptingSystem;
using DS.BLL.Finance;
using DS.PropertyEntities.Model.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.Finance.Accounts
{
    public partial class Expenses : System.Web.UI.Page
    {
        TitleEntry tleEntry;
        ExpensesEntry expenEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                tleEntry = new TitleEntry();
                tleEntry.GetAccounts_Title(ddlTitle,"1");
                LoadExpenses();
                txtExDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
        private void LoadExpenses()
        {
            try
            {
                string divInfo = string.Empty;
                if (expenEntry == null)
                {
                    expenEntry = new ExpensesEntry();
                }
                List<ExpensesEntities> ExpensesList = expenEntry.GetEntitiesData();
                divInfo = " <table id='tblexpenseslist' class='display'> ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Title</th>";
                divInfo += "<th>Amount</th>";
                divInfo += "<th>Date</th>";
                divInfo += "<th>Particular</th>";
                divInfo += "<th>Edit</th>";
                divInfo += "<th>Delete</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (ExpensesList == null)
                {
                    divInfo = "<div class='noData'>No Expenses available</div>";
                    divExpensesList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                string id = string.Empty;
                for (int x = 0; x < ExpensesList.Count; x++)
                {
                    id = ExpensesList[x].ExpensesID.ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td><span id=title" + id + ">" + ExpensesList[x].Ac_Title.Title.ToString() + "</span></td>";
                    divInfo += "<td><span id=amount" + id + ">" + ExpensesList[x].Amount.ToString() + "</span></td>";
                    divInfo += "<td><span id=date" + id + ">" + ExpensesList[x].Date.ToString("dd-MM-yyyy") + "</span></td>";
                    divInfo += "<td><span id=particular" + id + ">" + ExpensesList[x].Particular.ToString() + "</span></td>";
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editexpenses(" + id + "," + ExpensesList[x].Ac_Title.ID.ToString()+ ");'/>";
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/del.png' class='editImg' onclick='deleteexpenses(" + ExpensesList[x].ExpensesID + ");'/>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divExpensesList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblExpensesID.Value == "0")
            {
                if (SaveName() == true)
                {
                    LoadExpenses();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "SavedSuccess();", true);
                }
            }
            else
            {
                if (UpdateName() == true)
                {
                    LoadExpenses();
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "updateSuccess();", true);
                }
            }
        }  
         private Boolean SaveName()
        {
            try
            {
                using (ExpensesEntities entities = GetFormData())
                {
                    if (expenEntry == null)
                    {
                        expenEntry = new ExpensesEntry();
                    }
                    expenEntry.AddEntities = entities;
                    bool result = expenEntry.Insert();
                    lblExpensesID.Value = "0";
                    if (!result)
                    {
                        lblMessage.InnerText = "error-> Unable to save";
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }
         private ExpensesEntities GetFormData()
        {
            ExpensesEntities expenEntities = new ExpensesEntities();
            expenEntities.ExpensesID = int.Parse(lblExpensesID.Value);
            expenEntities.Ac_Title = new TitleEntities
            {
                ID = int.Parse(ddlTitle.SelectedValue)
            };
            expenEntities.Amount = float.Parse(txtAmount.Text);
            expenEntities.Date = convertDateTime.getCertainCulture(txtExDate.Text);
            expenEntities.Particular = txtParticular.Text.Trim();
            return expenEntities;
        }
        private Boolean UpdateName()
        {
            try
            {
                using (ExpensesEntities entities = GetFormData())
                {
                    if (expenEntry == null)
                    {
                        expenEntry = new ExpensesEntry();
                    }
                    expenEntry.AddEntities = entities;
                    bool result = expenEntry.Update();
                    lblExpensesID.Value = "0";
                    if (!result)
                    {
                        lblMessage.InnerText = "error-> Unable to update";
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
                return false;
            }
        }

        [System.Web.Services.WebMethod]
        public static string Delete(string expensesid)
        {

            ExpensesEntry expenEntry = new ExpensesEntry();
            bool result = expenEntry.Delete(expensesid);
            string divInfo = string.Empty;
            List<ExpensesEntities> ExpensesList = expenEntry.GetEntitiesData();
            divInfo = " <table id='tblexpenseslist' class='display'> ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Title</th>";
            divInfo += "<th>Amount</th>";
            divInfo += "<th>Date</th>";
            divInfo += "<th>Particular</th>";
            divInfo += "<th>Edit</th>";
            divInfo += "<th>Delete</th>";
            divInfo += "</tr>";
            divInfo += "</thead>";
            divInfo += "<tbody>";
            if (ExpensesList == null)
            {
                divInfo = "<div class='noData'>No Expenses available</div>";
                return divInfo;
            }
            string id = string.Empty;
            for (int x = 0; x < ExpensesList.Count; x++)
            {
                id = ExpensesList[x].ExpensesID.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td><span id=title" + id + ">" + ExpensesList[x].Ac_Title.Title.ToString() + "</span></td>";
                divInfo += "<td><span id=amount" + id + ">" + ExpensesList[x].Amount.ToString() + "</span></td>";
                divInfo += "<td><span id=date" + id + ">" + ExpensesList[x].Date.ToString("dd-MM-yyyy") + "</span></td>";
                divInfo += "<td><span id=particular" + id + ">" + ExpensesList[x].Particular.ToString() + "</span></td>";
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editexpenses(" + id + "," + ExpensesList[x].Ac_Title.ID.ToString() + ");'/>";
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/del.png' class='editImg' onclick='deleteexpenses(" + ExpensesList[x].ExpensesID + ");'/>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";
            return divInfo;
            
            
            
        }
       

    }
}