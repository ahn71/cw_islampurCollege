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
    public partial class Income : System.Web.UI.Page
    {
        TitleEntry tleEntry;
        IncomeEntry icomeEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                tleEntry = new TitleEntry();
                tleEntry.GetAccounts_Title(ddlTitle,"0");
                LoadExpenses();
                txtExDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
        private void LoadExpenses()
        {
            try
            {
                string divInfo = string.Empty;
                if (icomeEntry == null)
                {
                    icomeEntry = new IncomeEntry();
                }
                List<IncomeEntities> incomeList = icomeEntry.GetEntitiesData();
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
                if (incomeList == null)
                {
                    divInfo = "<div class='noData'>No Income available</div>";
                    divExpensesList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                string id = string.Empty;
                for (int x = 0; x < incomeList.Count; x++)
                {
                    id = incomeList[x].IncomeID.ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td><span id=title" + id + ">" + incomeList[x].Ac_Title.Title.ToString() + "</span></td>";
                    divInfo += "<td><span id=amount" + id + ">" + incomeList[x].Amount.ToString() + "</span></td>";
                    divInfo += "<td><span id=date" + id + ">" + incomeList[x].Date.ToString("dd-MM-yyyy") + "</span></td>";
                    divInfo += "<td><span id=particular" + id + ">" + incomeList[x].Particular.ToString() + "</span></td>";
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editexpenses(" + id + "," + incomeList[x].Ac_Title.ID.ToString() + ");'/>";
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/del.png' class='editImg' onclick='deleteincome(" + incomeList[x].IncomeID + ");'/>";
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
                using (IncomeEntities entities = GetFormData())
                {
                    if (icomeEntry == null)
                    {
                        icomeEntry = new IncomeEntry();
                    }
                    icomeEntry.AddEntities = entities;
                    bool result = icomeEntry.Insert();
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
        private IncomeEntities GetFormData()
        {
            IncomeEntities incomeEntities = new IncomeEntities();
            incomeEntities.IncomeID = int.Parse(lblExpensesID.Value);
            incomeEntities.Ac_Title = new TitleEntities
            {
                ID = int.Parse(ddlTitle.SelectedValue)
            };
            incomeEntities.Amount = float.Parse(txtAmount.Text);

            incomeEntities.Date = convertDateTime.getCertainCulture(txtExDate.Text);
            incomeEntities.Particular = txtParticular.Text.Trim();
            return incomeEntities;
        }
        private Boolean UpdateName()
        {
            try
            {
                using (IncomeEntities entities = GetFormData())
                {
                    if (icomeEntry == null)
                    {
                        icomeEntry = new IncomeEntry();
                    }
                    icomeEntry.AddEntities = entities;
                    bool result = icomeEntry.Update();
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
        public static string Delete(string incomeid)
        {

            IncomeEntry incomeEntry = new IncomeEntry();
            bool result = incomeEntry.Delete(incomeid);
            string divInfo = string.Empty;
            List<IncomeEntities> incomeList = incomeEntry.GetEntitiesData();
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
            if (incomeList == null)
            {
                divInfo = "<div class='noData'>No Income available</div>";

                return divInfo;
            }
            string id = string.Empty;
            for (int x = 0; x < incomeList.Count; x++)
            {
                id = incomeList[x].IncomeID.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td><span id=title" + id + ">" + incomeList[x].Ac_Title.Title.ToString() + "</span></td>";
                divInfo += "<td><span id=amount" + id + ">" + incomeList[x].Amount.ToString() + "</span></td>";
                divInfo += "<td><span id=date" + id + ">" + incomeList[x].Date.ToString("dd-MM-yyyy") + "</span></td>";
                divInfo += "<td><span id=particular" + id + ">" + incomeList[x].Particular.ToString() + "</span></td>";
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg' onclick='editexpenses(" + id + "," + incomeList[x].Ac_Title.ID.ToString() + ");'/>";
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/del.png' class='editImg' onclick='deleteincome(" + incomeList[x].IncomeID + ");'/>";
            }
            divInfo += "</tbody>";
            divInfo += "<tfoot>";
            divInfo += "</table>";            
            return divInfo;


        }

    }
}