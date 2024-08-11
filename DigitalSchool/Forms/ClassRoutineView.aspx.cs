using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;

namespace DS.Forms
{
    public partial class ClassRoutineView : System.Web.UI.Page
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
                    sqlDB.bindDropDownList("Select RoutineId From RoutineInfo ", "RoutineId", "RoutineId", dlClass);
                    dlClass.Items.Add("--Select--");
                    dlClass.Text = "--Select--";
                }
            }

        }
        DataSet ds;
        string totalTable;
        int clm = 0;
        private void loadClassRoutine()
        {
            try
            {
                DataTable dtday = new DataTable();
                sqlDB.fillDataTable("Select distinct Day, OrderNo, Shift From v_ClassRoutine where RoutineId='" + dlClass.SelectedItem.Text + "' ", dtday);

                DataTable dtDays = dtday.Select("", "OrderNo ASC").CopyToDataTable();
                dtday = dtDays;

                ds = new DataSet();
                
                for (int j = 0; j < dtday.Rows.Count; j++)
                {
                    DataTable dt = new DataTable();
                    sqlDB.fillDataTable("Select Day, SubName, Convert(char(5), StartTime,108) AS StartTime, Convert(char(5),EndTime,108) AS EndTime, TCodeNo From v_ClassRoutine where RoutineId='" + dlClass.SelectedItem.Text + "' and Day='" + dtday.Rows[j]["Day"] + "' Order By StartTime ", dt);

                    ds.Tables.Add(dt);
                }
                Session["__Shift__"] = dtday.Rows[0]["Shift"].ToString();
                int tableColumn=0;
                for (byte y = 0; y < ds.Tables.Count; y++)
                {
                    if (ds.Tables[y].Rows.Count > tableColumn)
                    {
                        tableColumn = ds.Tables[y].Rows.Count;
                    }
                }

                string divInfo = "";
                divInfo += "<div style='width:100%'>";//s
                divInfo = " <table id='tblClassRoutine' class='displayRoutine'  > ";
                divInfo += "<thead>";

                for (int x = 0; x < ds.Tables.Count; x++) //Main Loop
                {
                    divInfo += "<tr>";
                    for (byte b = 0; b < tableColumn; b++)
                    {
                        if (b == 0) divInfo += "<th>" + ds.Tables[x].Rows[b]["Day"] + "<br/> (" + ds.Tables[x].Rows[b]["StartTime"] + ")</th>";
                        
                        if (ds.Tables[x].Rows.Count > clm)
                        {
                            divInfo += "<th>" + ds.Tables[x].Rows[b]["StartTime"] + "-" + ds.Tables[x].Rows[b]["EndTime"] + "<br/>" + ds.Tables[x].Rows[b]["SubName"] + " <br/>(" + ds.Tables[x].Rows[b]["TCodeNo"] + ")</th>";
                            clm++;
                        }
                        else divInfo += "<th> &nbsp; </th>";
                    }
                    clm = 0;
                    divInfo += "</tr>";
                }

                divInfo += "</thead>";
                divInfo += "</table>";
                divRoutineInfo.Controls.Add(new LiteralControl(divInfo));
                totalTable += divInfo;

                Session["__ClassRoutine__"] = totalTable;
                string [] classN =dlClass.SelectedItem.Text.Split('_');
                Session["__ClassName__"] = classN[0].ToString();
            }
            catch { }
        }

        protected void dlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadClassRoutine();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/RoutinePrint.aspx');", true);  //Open New Tab for Sever side code
        }

    }
}