using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class ShiftConfiguration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["__UserId__"] == null)
            {
                Response.Redirect("~/UserLogin.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    loadShiftConfugurationInfo();
                }
            }
        }

        private void loadShiftConfugurationInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select * from ShiftConfiguration",dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                divShiftList.Controls.Add(new LiteralControl(divInfo));
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Shift available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divShiftList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='ConfigId' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th  style='width: 90px;'>Shift Name </th>";
                divInfo += "<th style='width: 92px;'>Start Time</th>";
                divInfo += "<th style='width: 92px;' >Close Time</th>";
                divInfo += "<th style='width: 85px;'>Late Time</th>";
               divInfo += "<th style='width: 90px;'>Weekend</th>";
                divInfo += "<th>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";
                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["ConfigId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td>" + dt.Rows[x]["ShiftName"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["StartTime"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["CloseTime"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["LateTime"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["Weekend"].ToString() + "</td>";
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editShift(" + id + ");'  />";


                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'> </div></div>";
                divShiftList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hfShiftConfigId.Value.Length.Equals(0)) saveShiftConfiguration();
            else updateShiftConfiguration();
        }

        private void saveShiftConfiguration()
        {
            try
            {

                string[] getColumns = { "ShiftName", "StartTime", "CloseTime","LateTime","Weekend" };
                string[] getValues = { txtShiftName.Text.Trim(), ddlStartHour.SelectedItem.ToString() + ":" + ddlStartMinute.SelectedItem.ToString() + ":00" + " " + ddlStartAMPM.SelectedItem.ToString(), ddlCloseHour.SelectedItem.ToString() + ":" + ddlCloseMinute.SelectedItem.ToString() + ":00" + " " + ddlCloseAMPM.SelectedItem.ToString(),ddlLateMinute.SelectedItem.ToString(), ddlDay.SelectedItem.ToString() };
                if (SQLOperation.forSaveValue("ShiftConfiguration", getColumns, getValues,sqlDB.connection) == true)
                {
                    lblMessage.InnerText = "success->Successfully Save Shift Configured";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearIt();", true);
                    loadShiftConfugurationInfo();
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = ex.Message;
            }
        }

        private void updateShiftConfiguration()
        {
            try
            {
                string[] getColumns = { "ShiftName", "StartTime", "CloseTime", "LateTime", "Weekend" };
                string[] getValues = { txtShiftName.Text.Trim(), ddlStartHour.SelectedItem.ToString() + ":" + ddlStartMinute.SelectedItem.ToString() + ":00" + " " + ddlStartAMPM.SelectedItem.ToString(), ddlCloseHour.SelectedItem.ToString() + ":" + ddlCloseMinute.SelectedItem.ToString() + ":00" + " " + ddlCloseAMPM.SelectedItem.ToString(),ddlLateMinute.SelectedItem.ToString(), ddlDay.SelectedItem.ToString() };
                if (SQLOperation.forUpdateValue("ShiftConfiguration", getColumns, getValues, "ConfigId",hfShiftConfigId.Value.ToString(),sqlDB.connection) == true)
                {
                    lblMessage.InnerText = "success->Successfully Updated Shift Configured";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearIt();", true);
                    loadShiftConfugurationInfo();
                }
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = ex.Message;
            }
        }
    }
}