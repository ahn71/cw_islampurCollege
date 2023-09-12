using DS.BLL.ControlPanel;
using DS.BLL.TeacherEvaluation;
using DS.PropertyEntities.Model.TeacherEvaluation;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.HR.TeacherEvaluation
{
    public partial class EvaNumberEntry : System.Web.UI.Page
    {
        private SessionEntry sEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["__UserId__"] == null)
                {
                    Response.Redirect("~/UserLogin.aspx");
                }
                else
                {
                  //  Button btnSave;
           //         if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddDepartment.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                    SessionEntry.GetDropdownlist(ddlEvaSession);
                    ddlCommitteeMember.Enabled = false;
                }
            }
        }
        private void loadNumberSheet() 
        {
            if (ddlEvaSession.SelectedIndex < 1)
                return;
            string tblInfo = "";
            string tblHeader = "";
            //-----------for create table of marks entry point ------------------------------------------
            tblInfo = "<Table id=tblMarkEntryPoint class=table table-striped table-bordered dt-responsive nowrap cellspacing='0' width='100%'>";

            tblHeader += "<th >SL</th><th>Name <br>( Department )</th>";

            //-------------------Bellow statements for header------------------------------------------------
            DataTable dt_TotalMarksheetForReport = new DataTable();
            DataTable dt_JustMarkSheetColumnsName = new DataTable();
            DataTable dtSQPInfo = new DataTable();
            if(sEntry==null)
                sEntry=new SessionEntry();
            List<SessionDetailsEntities> ListSessionDetails = new List<SessionDetailsEntities>();
            ListSessionDetails = sEntry.GetSessionDetailsBySession(ddlEvaSession.SelectedValue);
            foreach (SessionDetailsEntities lst in ListSessionDetails)
            {
                string CategoryName = "", SubCategoryName = "", FullMark = "";     
                SubCategoryName = lst.SubCetegory.SubCategory;
                FullMark = lst.NumPattern.FullNumber.ToString();

                tblHeader += "<th style='text-align: center;'>" + SubCategoryName + "<br /> ( " + FullMark + " )</th>";
              

            }
            tblInfo += tblHeader;
            DataTable dt = new DataTable();
            dt = NumberEntry.GetNumberSheet(ddlEvaSession.SelectedValue,ddlCommitteeMember.SelectedValue);
            if (dt != null)
            {
                int j = ListSessionDetails.Count;
                int r = 0;
                int sl = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    int row = i + 1;
                        if (r == 0)
                        {
                            tblInfo += "<tr><td style='font-weight: bold; font-size: 15px;'>"+sl+"</td><td style='font-size: 11px;'>" + dt.Rows[i]["EName"].ToString() + "<br /> (" + dt.Rows[i]["DName"].ToString() + ")</td>";
                            sl++;
                            tblInfo += "<td><input  onKeyUp='$(this).val($(this).val().replace(/[^/d]/ig, ''))'  autocomplete='off'  onchange='saveData(this)' "
                        + "type=text style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row +
                        " id='" + dt.Rows[i]["EID"].ToString() + ":" + dt.Rows[i]["SessionID"].ToString() + ":" + dt.Rows[i]["MemberID"].ToString() +
                        ":" + dt.Rows[i]["SubCategoryID"].ToString() + ":" + dt.Rows[i]["FullNumber"].ToString() + "' " +
                        "value=" + dt.Rows[i]["ObtainNumber"].ToString() + "></td>";
                        }
                        else
                        {
                            tblInfo += "<td><input  onKeyUp='$(this).val($(this).val().replace(/[^/d]/ig, ''))'  autocomplete='off'  onchange='saveData(this)' "
                        + "type=text style='text-align: center; font-size: 15px; font-weight: bold;color: blue; width: 100%;' tabindex=" + row + 
                        " id='" + dt.Rows[i]["EID"].ToString() + ":" + dt.Rows[i]["SessionID"].ToString() +":" + dt.Rows[i]["MemberID"].ToString() +
                        ":" + dt.Rows[i]["SubCategoryID"].ToString() + ":"+ dt.Rows[i]["FullNumber"].ToString() + "' " +
                        "value=" + dt.Rows[i]["ObtainNumber"].ToString() + "></td>";
                           
                        }
                        r++;
                        if (j == r)
                        {
                            tblInfo += "</tr>";
                            r = 0;
                        }
                       
                 
                   
                }
            }
            //Session["__JustColumnsName__"] = dt_JustMarkSheetColumnsName;
            tblInfo += "</table>";
            divMarksheet.Controls.Add(new LiteralControl(tblInfo));
            //--------------------------------------------------------------------------------------------------

        }

        protected void ddlEvaSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            SessionEntry.GetCommitteeMemberDropdownlist(ddlCommitteeMember,ddlEvaSession.SelectedValue);
            ddlCommitteeMember.SelectedValue = Session["__UserId__"].ToString();
            loadNumberSheet();
        }

        protected void ddlCommitteeMember_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadNumberSheet();
        }

        //protected void btnExportToExcel_Click(object sender, EventArgs e)
        //{
        //    try 
        //    {
        //        Response.ClearContent();
        //        Response.AddHeader("Content-Disposition", "attachment;filename=ExcelFile.xls");
        //        Response.ContentType = "application/excel";
        //        //Response.ContentEncoding = Encoding.UTF8; 
        //        StringWriter tw = new StringWriter();
        //        HtmlTextWriter hw = new HtmlTextWriter(tw);
        //       // tbltest.RenderControl(hw);
        //        Response.Write(tw.ToString());
        //        Response.End();
        //    }
        //    catch { }

           
        //}
        //public override void VerifyRenderingInServerForm(Control control)
        //{
            
        //}
    }
}