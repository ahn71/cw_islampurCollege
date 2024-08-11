using DS.BLL.Admission;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedClass;
using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Students
{
    public partial class CourseWiseStudent : System.Web.UI.Page
    {
        ClassGroupEntry clsgrpEntry;
        CurrentStdEntry currentstdEntry;
        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    BatchEntry.GetDropdownlist(dlBatch, "True");
                    dlBatch.Items.Insert(1, new ListItem("All", "All"));
                    dlBatch.SelectedValue = "All";
                    dlSection.Items.Insert(0, new ListItem("All", "All"));
                    dlSection.SelectedValue = "All";
                    ShiftEntry.GetDropDownList(dlShift);
                    dlShift.Items.Insert(1, new ListItem("All", "All"));
                    dlShift.SelectedValue = "All";
                }
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
            dlGroup.Items.Insert(1, new ListItem("All", "All"));
            if (dlGroup.Enabled == true)
                dlGroup.SelectedValue = "All";
            dlSection.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void dlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] BatchClsID = dlBatch.SelectedValue.Split('_');
            string GroupId = "0";
            if (dlGroup.SelectedValue != "All")
            {
                GroupId = dlGroup.SelectedValue;
            }
            ClassSectionEntry.GetEntitiesData(dlSection, int.Parse(BatchClsID[1]), GroupId);
            dlSection.Items.Insert(1, new ListItem("All", "All"));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadGuardianContactList("");
        }

        protected void btnPrintPreview_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlCmd = "";
                if (rdoWithImage.Checked)
                {
                    //if (dlShift.Text == "All") sqlCmd = "select FullName,RollNo,StudentId,Gender,ImageName,Mobile,SectionName,Shift from v_CurrentStudentInfo "
                    //        + "where BatchName='" + dlBatch.Text + "' and SectionName='" + dlSection.Items[i].Text + "'  ";
                    //if (dlShift.Text != "All") sqlCmd = "select FullName,RollNo,StudentId,Gender,ImageName,Mobile,SectionName,Shift "
                    //+ "from v_CurrentStudentInfo where BatchName='" + dlBatch.Text + "' and SectionName='" + dlSection.Items[i].Text + "' and Shift='"
                    //+ dlShift.Text + "' ";
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/CourseWiseStudentReport.aspx');", true);  //Open New Tab for Sever side code
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "call me", "goToNewTab('/Report/CourseWiseStudentReport.aspx');", true);  //Open New Tab for Sever side code
                }
            }
            catch { }
        }

        private void loadGuardianContactList(string sqlCmd) // for load student Guardian Contact List information
        {
            try
            {
                btnPrintPreview.Visible = true;
                Session["__Section__"] = dlSection.SelectedItem.Text;
                Session["__Batch__"] = "Batch : " + dlBatch.SelectedItem.Text + " ( " + dlSection.SelectedItem.Text + " )";

                if (dlSection.Text == "All")
                {
                    if (rdoWithImage.Checked)
                    {
                        Session["__Image__"] = "With image";
                        DataSet ds = new DataSet();
                        for (int i = 0; i < dlSection.Items.Count - 1; i++)
                        {
                            DataTable Tables = new DataTable();
                            ds.Tables.Add(Tables);
                            if (dlShift.Text == "All") sqlCmd = "select FullName,RollNo,StudentId,Gender,ImageName,Mobile,SectionName,Shift from v_CurrentStudentInfo "
                            + "where BatchName='" + dlBatch.Text + "' and SectionName='" + dlSection.Items[i].Text + "'  ";
                            if (dlShift.Text != "All") sqlCmd = "select FullName,RollNo,StudentId,Gender,ImageName,Mobile,SectionName,Shift "
                            + "from v_CurrentStudentInfo where BatchName='" + dlBatch.Text + "' and SectionName='" + dlSection.Items[i].Text + "' and Shift='"
                            + dlShift.Text + "' ";

                            sqlDB.fillDataTable(sqlCmd, ds.Tables[i]);
                        }

                        Session["__CourseWiseStudent__"] = ds;

                        for (int i = 0; i < ds.Tables.Count; i++)
                        {

                            int totalRows = ds.Tables[i].Rows.Count;
                            string divInfo = "";

                            divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                            if (ds.Tables[i].Rows.Count > 0)
                            {
                                divInfo += "<div class='dataTables_wrapper'><div class='head' style='font-size:16px; margin-top:5px'>" + " Section : "
                                    + ds.Tables[i].Rows[0]["SectionName"].ToString() + "</div></div>";
                            }
                            divInfo += "<thead>";
                            divInfo += "<tr>";

                            divInfo += "<th class='numeric' style='width:30px;'>SL</th>";
                            divInfo += "<th style='width:340px'>Name</th>";
                            divInfo += "<th style='width:90px' class='numeric'>Roll</th>";
                            divInfo += "<th style='width:90px' class='numeric'>Shift</th>";
                            divInfo += "<th style='width:90px' >Gender</th>";
                            divInfo += "<th style='width:120px'>Contact</th>";
                            divInfo += "<th class='numeric' style='width:45px' >Photo</th>";
                            divInfo += "</tr>";

                            divInfo += "</thead>";

                            divInfo += "<tbody>";

                            for (int x = 0; x < ds.Tables[i].Rows.Count; x++)
                            {
                                int sl = x + 1;

                                divInfo += "<tr >";
                                divInfo += "<td class='numeric'>" + sl + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["FullName"].ToString() + "</td>";
                                divInfo += "<td class='numeric'>" + ds.Tables[i].Rows[x]["RollNo"].ToString() + "</td>";
                                divInfo += "<td class='numeric'>" + ds.Tables[i].Rows[x]["Shift"].ToString() + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["Gender"].ToString() + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["Mobile"].ToString() + "</td>";

                                //divInfo += "<td class='numeric' >" + "<img src='/Images/profileImages/" + ds.Tables[i].Rows[x]["ImageName"].ToString()
                                //+ "' style='width:45px; height:50px;text-align:center' />";

                                divInfo += "<td class='numeric' >" + "<img src='http://www.placehold.it/29x29/EFEFEF/999&text=no+image' style='width:45px; height:50px;text-align:center' />";
                            }

                            if (ds.Tables[i].Rows.Count > 0)
                            {
                                divInfo += "</tbody>";
                                divInfo += "<tfoot>";

                                divInfo += "</table><br>";
                                divCourseWiseStudentList.Controls.Add(new LiteralControl(divInfo));
                            }
                        }
                    }
                    else
                    {
                        Session["__Image__"] = "No Image";
                        DataSet ds = new DataSet();
                        for (int i = 0; i < dlSection.Items.Count; i++)
                        {
                            DataTable Tables = new DataTable();
                            ds.Tables.Add(Tables);

                            if (dlShift.Text == "All") sqlCmd = "select FullName,RollNo,StudentId,Gender,Mobile,SectionName,Shift from v_CurrentStudentInfo "
                            + "'where BatchName='" + dlBatch.Text + "' and SectionName='" + dlSection.Items[i].Text + "'  ";
                            if (dlShift.Text != "All") sqlCmd = "select FullName,RollNo,StudentId,Gender,Mobile,SectionName,Shift from v_CurrentStudentInfo "
                            + "where BatchName='" + dlBatch.Text + "' and SectionName='" + dlSection.Items[i].Text + "' and Shift='" + dlShift.Text + "' ";

                            sqlDB.fillDataTable(sqlCmd, ds.Tables[i]);
                        }

                        Session["__CourseWiseStudent__"] = ds;

                        for (int i = 0; i < ds.Tables.Count; i++)
                        {

                            int totalRows = ds.Tables[i].Rows.Count;
                            string divInfo = "";

                            divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                            if (ds.Tables[i].Rows.Count > 0)
                            {
                                divInfo += "<div class='dataTables_wrapper'><div class='head' style='font-size:16px; margin-top:5px'>" + " Section : "
                                    + ds.Tables[i].Rows[0]["SectionName"].ToString() + "</div></div>";
                            }
                            divInfo += "<thead>";
                            divInfo += "<tr>";

                            divInfo += "<th class='numeric' style='width:30px;'>SL</th>";
                            divInfo += "<th style='width:340px'>Name</th>";
                            divInfo += "<th style='width:90px' class='numeric'>Roll</th>";
                            divInfo += "<th style='width:90px' class='numeric'>Shift</th>";
                            divInfo += "<th style='width:90px' >Gender</th>";
                            divInfo += "<th style='width:120px'>Contact</th>";
                            divInfo += "</tr>";

                            divInfo += "</thead>";

                            divInfo += "<tbody>";

                            for (int x = 0; x < ds.Tables[i].Rows.Count; x++)
                            {
                                int sl = x + 1;

                                divInfo += "<tr >";
                                divInfo += "<td class='numeric'>" + sl + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["FullName"].ToString() + "</td>";
                                divInfo += "<td class='numeric'>" + ds.Tables[i].Rows[x]["RollNo"].ToString() + "</td>";
                                divInfo += "<td class='numeric'>" + ds.Tables[i].Rows[x]["Shift"].ToString() + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["Gender"].ToString() + "</td>";
                                divInfo += "<td >" + ds.Tables[i].Rows[x]["Mobile"].ToString() + "</td>";

                            }

                            if (ds.Tables[i].Rows.Count > 0)
                            {
                                divInfo += "</tbody>";
                                divInfo += "<tfoot>";

                                divInfo += "</table><br>";
                                divCourseWiseStudentList.Controls.Add(new LiteralControl(divInfo));
                            }
                        }
                    }
                }
                else
                {
                    if (rdoWithImage.Checked)
                    {
                        Session["__Image__"] = "With image";
                        if (dlShift.Text == "All") sqlCmd = "select FullName,RollNo,StudentId,Gender,ImageName,Mobile,Shift from v_CurrentStudentInfo where BatchName='"
                               + dlBatch.Text + "'  and SectionName='" + dlSection.Text + "' ";
                        if (dlShift.Text != "All") sqlCmd = "select FullName,RollNo,StudentId,Gender,ImageName,Mobile,Shift from v_CurrentStudentInfo where BatchName='"
                            + dlBatch.Text + "'  and SectionName='" + dlSection.Text + "' and Shift='" + dlShift.Text + "' ";

                        DataTable dt = new DataTable();
                        sqlDB.fillDataTable(sqlCmd, dt);

                        Session["__Batch__"] = "Batch : " + dlBatch.Text + " ( " + dlSection.Text + " )";
                        Session["__CourseWistSt__"] = dt;
                        int totalRows = dt.Rows.Count;
                        string divInfo = "";


                        divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                        divInfo += "<thead>";
                        divInfo += "<tr>";

                        divInfo += "<th class='numeric' style='width:30px;'>SL</th>";
                        divInfo += "<th style='width:340px'>Name</th>";
                        divInfo += "<th style='width:90px' class='numeric'>Roll</th>";
                        divInfo += "<th style='width:90px' class='numeric'>Shift</th>";
                        divInfo += "<th style='width:90px' >Gender</th>";
                        divInfo += "<th style='width:120px'>Contact</th>";
                        divInfo += "<th class='numeric' style='width:45px' >Photo</th>";
                        divInfo += "</tr>";

                        divInfo += "</thead>";

                        divInfo += "<tbody>";

                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            int sl = x + 1;

                            divInfo += "<tr >";
                            divInfo += "<td class='numeric'>" + sl + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                            divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                            divInfo += "<td class='numeric'>" + dt.Rows[x]["Shift"].ToString() + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["Gender"].ToString() + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["Mobile"].ToString() + "</td>";

                            //divInfo += "<td class='numeric' >" + "<img src='/Images/profileImages/" + dt.Rows[x]["ImageName"].ToString() + "' style='width:45px; height:50px; text-align:center'/>";

                            divInfo += "<td class='numeric' >" + "<img src='http://www.placehold.it/29x29/EFEFEF/999&text=no+image' style='width:45px; height:50px; text-align:center'/>";

                        }

                        divInfo += "</tbody>";
                        divInfo += "<tfoot>";

                        divInfo += "</table>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                        divCourseWiseStudentList.Controls.Add(new LiteralControl(divInfo));
                    }
                    else
                    {
                        Session["__Image__"] = "No Image";

                        if (dlShift.Text == "All") sqlCmd = "select FullName,RollNo,StudentId,Gender,Mobile,Shift from v_CurrentStudentInfo where BatchName='"
                            + dlBatch.Text + "'  and SectionName='" + dlSection.Text + "' ";
                        if (dlShift.Text != "All") sqlCmd = "select FullName,RollNo,StudentId,Gender,Mobile,Shift from v_CurrentStudentInfo where BatchName='"
                            + dlBatch.Text + "'  and SectionName='" + dlSection.Text + "' and Shift='" + dlShift.Text + "' ";

                        DataTable dt = new DataTable();
                        sqlDB.fillDataTable(sqlCmd, dt);

                        Session["__Batch__"] = "Batch : " + dlBatch.Text + " ( " + dlSection.Text + " )";
                        Session["__CourseWistSt__"] = dt;
                        int totalRows = dt.Rows.Count;
                        string divInfo = "";


                        divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                        divInfo += "<thead>";
                        divInfo += "<tr>";

                        divInfo += "<th class='numeric' style='width:30px;'>SL</th>";
                        divInfo += "<th style='width:340px'>Name</th>";
                        divInfo += "<th style='width:90px' class='numeric'>Roll</th>";
                        divInfo += "<th style='width:90px' class='numeric'>Shift</th>";
                        divInfo += "<th style='width:90px' >Gender</th>";
                        divInfo += "<th style='width:120px'>Contact</th>";

                        divInfo += "</tr>";

                        divInfo += "</thead>";

                        divInfo += "<tbody>";

                        for (int x = 0; x < dt.Rows.Count; x++)
                        {
                            int sl = x + 1;

                            divInfo += "<tr >";
                            divInfo += "<td class='numeric'>" + sl + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["FullName"].ToString() + "</td>";
                            divInfo += "<td class='numeric'>" + dt.Rows[x]["RollNo"].ToString() + "</td>";
                            divInfo += "<td class='numeric'>" + dt.Rows[x]["Shift"].ToString() + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["Gender"].ToString() + "</td>";
                            divInfo += "<td >" + dt.Rows[x]["Mobile"].ToString() + "</td>";
                        }

                        divInfo += "</tbody>";
                        divInfo += "<tfoot>";

                        divInfo += "</table>";
                        divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

                        divCourseWiseStudentList.Controls.Add(new LiteralControl(divInfo));
                    }
                }
            }
            catch { }
        }

        protected void rdoWithImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                rdoWithImage.Checked = true;
                rdoNoImage.Checked = false;
            }
            catch { }
        }

        protected void rdoNoImage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                rdoWithImage.Checked = false;
                rdoNoImage.Checked = true;
            }
            catch { }
        }
    }
}