using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;
using DS.Classes;

namespace DS.Forms
{
    public partial class PeriodSettings : System.Web.UI.Page
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["__UserId__"] = "oitl";
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                    if (!IsPostBack)
                    {
                        loadPeriod();

                        for (byte h = 1; h <= 12; h++)
                        {
                            if (h < 10)
                            {
                                dlHours.Items.Add("0" + h.ToString());
                                dlHoursEndTime.Items.Add("0" + h.ToString());
                            }
                            else
                            {
                                dlHours.Items.Add(h.ToString());
                                dlHoursEndTime.Items.Add(h.ToString());
                            }
                        }
                        dlHours.Text = "07";
                        dlHoursEndTime.Text = "10";
                        for (byte m = 0; m < 60; m++)
                        {
                            if (m < 10)
                            {
                                dlMinute.Items.Add("0" + m.ToString());
                                dlMinuteEndTime.Items.Add("0" + m.ToString());
                            }
                            else
                            {
                                dlMinute.Items.Add(m.ToString());
                                dlMinuteEndTime.Items.Add(m.ToString());
                            }
                        }
                    }
                }
            }
            catch { }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblPeriod.Value.ToString().Length == 0) savePeriod();
            else updatePeriod();
        }

        private void savePeriod()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Insert Into PeriodSetting (PeriodName,PeriodStartTime,PeriodEndTime) Values (@PeriodName,@PeriodStartTime,@PeriodEndTime)", sqlDB.connection);
                cmd.Parameters.AddWithValue("@PeriodName", txtPeriodName.Text.Trim());
                cmd.Parameters.AddWithValue("@PeriodStartTime", (dlHours.SelectedItem.Text + ":" + dlMinute.SelectedItem.Text));
                cmd.Parameters.AddWithValue("@PeriodEndTime", (dlHoursEndTime.SelectedItem.Text + ":" + dlMinuteEndTime.SelectedItem.Text));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    lblMessage.InnerText = "success->Save Successfull";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearInputBox();", true);
                    loadPeriod();
                }
            }
            catch { }
        }

        private void updatePeriod()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Update PeriodSetting Set PeriodName=@PeriodName, PeriodStartTime=@PeriodStartTime, PeriodEndTime=@PeriodEndTime Where PeriodId=@PeriodId ", sqlDB.connection);
                cmd.Parameters.AddWithValue("@PeriodId", lblPeriod.Value);
                cmd.Parameters.AddWithValue("@PeriodName",txtPeriodName.Text.Trim());
                cmd.Parameters.AddWithValue("@PeriodStartTime", (dlHours.SelectedItem.Text + ":" + dlMinute.SelectedItem.Text));
                cmd.Parameters.AddWithValue("@PeriodEndTime", (dlHoursEndTime.SelectedItem.Text + ":" + dlMinuteEndTime.SelectedItem.Text));
                if (cmd.ExecuteNonQuery() > 0)
                {
                    lblMessage.InnerText = "success->Update Successfull";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "clearInputBox();", true);
                    loadPeriod();
                    btnSave.Text = "Save";
                }

            }
            catch { }
        }

        private void loadPeriod()
        {
            try
            {
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select PeriodId,PeriodName,PeriodStartTime,PeriodEndTime From PeriodSetting", dt);
                int totalRows = dt.Rows.Count;
                string divInfo = "";
                if (totalRows == 0)
                {
                    divInfo = "<div class='noData'>No Period available</div>";
                    divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                    divPeriod.Controls.Add(new LiteralControl(divInfo));
                    return;
                }

                divInfo = " <table id='tblPeriodList' class='display'  > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th>Period Name</th>";
                divInfo += "<th style='width:80px'>Start Time</th>";
                divInfo += "<th style='width:80px'>End Time</th>";
                divInfo += "<th class='numeric_control'>Edit</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                divInfo += "<tbody>";
                string id = "";

                for (int x = 0; x < dt.Rows.Count; x++)
                {
                    id = dt.Rows[x]["PeriodId"].ToString();
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td >" + dt.Rows[x]["PeriodName"].ToString() + "</td>";
                    divInfo += "<td class='numeric_control'>" + dt.Rows[x]["PeriodStartTime"].ToString() + "</td>";
                    divInfo += "<td class='numeric_control'>" + dt.Rows[x]["PeriodEndTime"].ToString() + "</td>";
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editPeriod(" + id + ");'  />";
                }

                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divPeriod.Controls.Add(new LiteralControl(divInfo));                
            }
            catch { }
        }

    }
}