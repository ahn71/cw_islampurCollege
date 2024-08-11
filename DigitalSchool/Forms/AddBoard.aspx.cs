using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ComplexScriptingSystem;
using DS.DAL.AdviitDAL;
using System.Data;

namespace DS.Admin
{
    public partial class AddBoard : System.Web.UI.Page
    {
        DataTable dt;
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
                    loadBorads("");
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblBoardId.Value.ToString().Length == 0) saveBoard();
            else updateBoard();
        }


        private void updateBoard()
        {
            try
            {
                string[] getColumns = { "BoardName" };
                string[] getValues = { txtBoardName.Text};
                string getIdentifierValue = lblBoardId.Value.ToString();
                if (ComplexScriptingSystem.SQLOperation.forUpdateValue("Boards", getColumns, getValues, "BoardId", getIdentifierValue, sqlDB.connection) == true)
                {
                    btnSave.Text = "Save";
                    txtBoardName.Text = "";
                    lblBoardId.Value = "";
                    loadBorads("");
                    lblMessage.InnerText = "success->Successfully updated";
                }
            }
            catch { }
        }

        private void saveBoard()
        {
            try
            {
                string[] getColumns = { "BoardName" };
                string[] getValues = {txtBoardName.Text.Trim()};
                if (ComplexScriptingSystem.SQLOperation.forSaveValue("Boards", getColumns, getValues, sqlDB.connection) == true)
                {
                    loadBorads("");
                    clear();
                    lblMessage.InnerText = "success->Successfully save";
                }
            }
            catch { }
        }

        private void loadBorads(string sqlcmd)
        {
            if (string.IsNullOrEmpty(sqlcmd)) sqlcmd = "Select *  from Boards  Order by BoardId ";
            DataTable dt = new DataTable();
            sqlDB.fillDataTable(sqlcmd, dt);

            int totalRows = dt.Rows.Count;
            string divInfo = "";
            divBoard.Controls.Clear();
            if (totalRows == 0)
            {
                divInfo = "<div class='noData'>No Boards available</div>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divBoard.Controls.Add(new LiteralControl(divInfo));
                return;
            }

            divInfo = " <table id='tblDesignationList' class='display'  > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Board Name Type</th>";
            divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            string id = "";

            for (int x = 0; x < dt.Rows.Count; x++)
            {

                id = dt.Rows[x]["BoardId"].ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td>" + dt.Rows[x]["BoardName"].ToString() + "</td>";
                divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editBoards(" + id + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";
            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

            divBoard.Controls.Add(new LiteralControl(divInfo));

        }

        private void clear()
        {
            txtBoardName.Text = "";
            txtBoardName.Focus();
        }
        
        }
    }
