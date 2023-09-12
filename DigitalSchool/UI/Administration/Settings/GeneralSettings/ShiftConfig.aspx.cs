using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using DS.DAL.ComplexScripting;
using DS.BLL.ControlPanel;
using DS.DAL;

namespace DS.UI.Academic.Timetable.SetTimings
{
    public partial class ShiftConfig : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "ShiftConfig.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    loadShiftConfugurationInfo();
                }
        }
        private void loadShiftConfugurationInfo()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("select * from ShiftConfiguration", dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                divInfo = " <table id='ConfigId' class='table table-striped table-bordered dt-responsive nowrap' cellspacing='0' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Shift Name </th>";               
                divInfo += "<th>Start Time</th>";
                divInfo += "<th>Close Time</th>";
                divInfo += "<th>Late Time(Min)</th>";
                divInfo += "<th>Absent Time(Min)</th>";
                divInfo += "<th>Type</th>";
                if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<th>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                if (totalRows == 0)
                {
                    divInfo += "</tbody></table>";
                    divShiftList.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                string id = "";
                for (int x = 0; x < dt.Rows.Count; x++)                {
                    id = dt.Rows[x]["ConfigId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td>" + dt.Rows[x]["ShiftName"].ToString() + "</td>";
                 
                    divInfo += "<td>" + dt.Rows[x]["StartTime"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["CloseTime"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["LateTime"].ToString() + "</td>";
                    divInfo += "<td>" + dt.Rows[x]["AbsentTime"].ToString() + "</td>";
                    if (dt.Rows[x]["Type"].ToString().Equals("False"))
                        divInfo += "<td>Students</td>";
                    else if (dt.Rows[x]["Type"].ToString().Equals("True"))
                        divInfo += "<td>Teachers/Staff</td>";
                    else
                        divInfo += "<td></td>";
                    if (Session["__Update__"].ToString().Equals("true"))
                        divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'  onclick='editShift(" + id +");'  />";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divShiftList.Controls.Add(new LiteralControl(divInfo));
            }
            catch { }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hfShiftConfigId.Value.Length.Equals(0))
            {
                if (Session["__Save__"].ToString().Equals("false")) { lblMessage.InnerText = "warning-> You don't have permission to save!"; loadShiftConfugurationInfo(); return; }
                saveShiftConfiguration();
            }
            else updateShiftConfiguration();
        }
        private void saveShiftConfiguration()
        {
            try
            {

                string[] getColumns = { "ShiftName", "StartTime", "CloseTime", "LateTime", "AbsentTime","Type" };
                string[] getValues = { txtShiftName.Text.Trim(), ddlStartHour.SelectedItem.ToString() + ":" + ddlStartMinute.SelectedItem.ToString() + ":00" + " " + ddlStartAMPM.SelectedItem.ToString(), ddlCloseHour.SelectedItem.ToString() + ":" + ddlCloseMinute.SelectedItem.ToString() + ":00" + " " + ddlCloseAMPM.SelectedItem.ToString(), ddlLateMinute.SelectedItem.ToString(), ddlAbsentTime.SelectedItem.ToString(), rblShiftType.SelectedValue };
                if (SQLOperation.forSaveValue("ShiftConfiguration", getColumns, getValues, DbConnection.Connection) == true)
                {
                    lblMessage.InnerText = "success->Save Successfully";
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
                string[] getColumns = { "ShiftName", "StartTime", "CloseTime", "LateTime", "AbsentTime", "Type" };
                string[] getValues = { txtShiftName.Text.Trim(), ddlStartHour.SelectedItem.ToString() + ":" + ddlStartMinute.SelectedItem.ToString() + ":00" + " " + ddlStartAMPM.SelectedItem.ToString(), ddlCloseHour.SelectedItem.ToString() + ":" + ddlCloseMinute.SelectedItem.ToString() + ":00" + " " + ddlCloseAMPM.SelectedItem.ToString(), ddlLateMinute.SelectedItem.ToString(), ddlAbsentTime.SelectedItem.ToString(), rblShiftType.SelectedValue };
                if (SQLOperation.forUpdateValue("ShiftConfiguration", getColumns, getValues, "ConfigId", hfShiftConfigId.Value.ToString(), DbConnection.Connection) == true)
                {
                    lblMessage.InnerText = "success->Update Successfully";
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