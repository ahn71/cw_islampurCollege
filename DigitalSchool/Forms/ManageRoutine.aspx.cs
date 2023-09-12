using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DS.DAL.AdviitDAL;
using ComplexScriptingSystem;
using System.Globalization;
using System.Drawing;
using System.Collections;
using DS.BLL;

namespace DS.Forms
{
    public partial class ManageRoutine : System.Web.UI.Page
    {
        static int SelectedIndex;
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
                        Classes.commonTask.loadBatchWithId(dlBatch);
                        loadSection(dlSection);
                        Classes.commonTask.loadExamType(dlExamType);
                        LoadDepartment(dlDepartment);
                        for (byte h = 1; h <= 12; h++)
                        {
                            if (h < 10) dlHours.Items.Add("0" + h.ToString());
                            else dlHours.Items.Add(h.ToString());
                        }
                        dlHours.Text = "09";
                        for (byte m = 0; m < 60; m++)
                        {
                            if (m < 10) dlMinute.Items.Add("0" + m.ToString());
                            else dlMinute.Items.Add(m.ToString());
                        }
                    }
                }

                lblMessage.InnerText = "";
            }
            catch { }
        }

        public void loadSection(DropDownList dl)
        {
            sqlDB.loadDropDownList("Select  SectionName from Sections  Order by SectionName", dl);
            dl.Items.Add("All");
            dl.Items.Add("--Select--");
            dl.SelectedIndex = dl.Items.Count - 1;
        }
        public void LoadDepartment(DropDownList dl)
        {
            try
            {
                dl.Items.Clear();
                dt = new DataTable();
                sqlDB.fillDataTable("Select DId,DName From Departments_HR where DStatus='True' and DName!='MLS'", dt);
                dl.DataSource = dt;             
                dl.DataTextField = "DName";
                dl.DataValueField = "DId";
                dl.DataBind();
                dl.Items.Add("--Select--");
                dl.Text = "--Select--";
            }
            catch { }
        }

        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblBatchName.Text = dlBatch.SelectedItem.Text.ToUpper();
                loadSectionClassWise();
                string ClassName = new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray());
                DataTable dtClsId = new DataTable();
                sqlDB.fillDataTable("Select ClassID From Classes Where ClassName='"+ClassName+"' ", dtClsId);
                DataTable dtSubject = new DataTable();
                sqlDB.fillDataTable("Select SubName from V_ClassSubject where ClassID=" + dtClsId.Rows[0]["ClassID"].ToString() + " Order by CSId ", dtSubject);
                dlSubject.DataSource = dtSubject;
                dlSubject.DataTextField = "SubName";       
                dlSubject.DataBind();
            }
            catch { }
        }

        private void loadSectionClassWise()
        {
            try
            {
                DataTable dt;
                SQLOperation.selectBySetCommandInDatatable("Select ClassOrder From Classes where ClassName='" + new String(dlBatch.SelectedItem.Text.Where(Char.IsLetter).ToArray()) + "'", dt = new DataTable(), sqlDB.connection);
                if(byte.Parse(dt.Rows[0]["ClassOrder"].ToString()) >=9 )
                {
                    dlSection.Items.Clear();
                    dlSection.Items.Add("--Select--");
                    dlSection.Items.Add("Science");
                    dlSection.Items.Add("Commerce");
                    dlSection.Items.Add("Arts");
                    dlSection.SelectedIndex = dlSection.Items.Count - dlSection.Items.Count;
                    lblSectionOrGroup.Text = "Group";
                }
                else
                {
                    dlSection.Items.Clear();
                    sqlDB.loadDropDownList("Select  SectionName from Sections where SectionName<>'Science' AND SectionName<>'Commerce' AND SectionName<>'Arts' Order by SectionName", dlSection);
                    dlSection.Items.Add("All");
                    dlSection.Items.Add("--Select--");
                    dlSection.SelectedIndex = dlSection.Items.Count - 1;
                    lblSectionOrGroup.Text = "Section";
                }
            }
            catch { }
        }

        protected void dlExamType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string routineId = dlBatch.SelectedItem.Text + "_" + dlShift.SelectedItem.Text + "_" + dlSection.SelectedItem.Text + "_" + dlExamType.SelectedItem.Text;
                lblRoutineId.Text = routineId;
                loadClassRoutine(routineId);
                dt = new DataTable();
                sqlDB.fillDataTable("Select Day, Convert(char(5),StartTime,108) AS StartTime, Convert(char(5),EndTime,108) AS EndTime,SubName,TCodeNo,OrderNo From v_ClassRoutine where RoutineId='" + lblRoutineId.Text + "' ", dt);
                if (dt.Rows.Count > 0) count = 1;
                gvDay.DataSource = dt;
                gvDay.DataBind();
            }
            catch { }
        }

        protected void dlDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sqlDB.bindDropDownList("Select TCodeNo,EName From v_EmployeeInfo Where DName='" + dlDepartment.SelectedItem.Text + "' ", "TCodeNo", "EName", dlTeacher);
                dlTeacher.Items.Add("--Select--");
                dlTeacher.Text = "--Select--";
            }
            catch { }
        }

        static DataTable dt;
        int OrderNo;
        static byte count = 0;
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var duration = TimeSpan.FromMinutes(Convert.ToInt32(txtDuration.Text));
                var endT = TimeSpan.Parse((dlHours.SelectedItem.Text + ":" + dlMinute.SelectedItem.Text)) + TimeSpan.Parse(duration.ToString());
                DateTime time = DateTime.Today.Add(endT);
                string endTime = time.ToString("hh:mm"); // It will give "03:00"
                if (count == 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("Day", typeof(string));
                    dt.Columns.Add("StartTime", typeof(string));
                    dt.Columns.Add("EndTime", typeof(string));
                    dt.Columns.Add("SubName", typeof(string));
                    dt.Columns.Add("TCodeNo", typeof(string));
                    dt.Columns.Add("OrderNo", typeof(int));
                    count++;
                }            
                if (btnAdd.Text == "Edit")
                {
                    dt.Rows[SelectedIndex]["Day"] = dlDay.SelectedItem.Text;
                    dt.Rows[SelectedIndex]["SubName"] = dlSubject.SelectedItem.Text;
                    dt.Rows[SelectedIndex]["TCodeNo"] = dlTeacher.SelectedValue;
                    dt.Columns[1].ReadOnly = false;
                    dt.Columns[2].ReadOnly = false;
                    string stTime = (dlHours.SelectedItem.Text + ":" + dlMinute.SelectedItem.Text);
                    dt.Rows[SelectedIndex]["StartTime"] = stTime;
                    dt.Rows[SelectedIndex]["EndTime"] = endTime;

                    btnAdd.Text = "Add";
                    btnSave.Text = "Update";
                }
                else
                {
                    if (checkTeacherExistingClass() == false) return;

                    for (int k = 0; k < gvDay.Rows.Count; k++)
                    {
                        string subjectName = gvDay.Rows[k].Cells[3].Text;
                        if (dlSubject.SelectedItem.Text == gvDay.Rows[k].Cells[3].Text && dlDay.SelectedItem.Text == gvDay.Rows[k].Cells[0].Text)
                        {
                            lblMessage.InnerText = "warning->This Subject already exist";
                            return;
                        }
                    }

                    if (dlDay.SelectedItem.Text == "Saturday") OrderNo = 1;
                    else if (dlDay.SelectedItem.Text == "Sunday") OrderNo = 2;
                    else if (dlDay.SelectedItem.Text == "Monday") OrderNo = 3;
                    else if (dlDay.SelectedItem.Text == "Tuesday") OrderNo = 4;
                    else if (dlDay.SelectedItem.Text == "Wednesday") OrderNo = 5;
                    else if (dlDay.SelectedItem.Text == "Thursday") OrderNo = 6;

                    dt.Rows.Add(dlDay.SelectedItem.Text, (dlHours.SelectedItem.Text + ":" + dlMinute.SelectedItem.Text), endTime, dlSubject.SelectedValue, dlTeacher.SelectedValue, OrderNo.ToString());
                }

                DataTable dtN = dt.Select("", "OrderNo ASC").CopyToDataTable();               
                dt = dtN;            
                gvDay.DataSource = dt;
                gvDay.DataBind();
                byte index = byte.Parse(gvDay.Rows.Count.ToString());
                string [] startTime = gvDay.Rows[index - 1].Cells[2].Text.Split(':');
                dlHours.Text = startTime[0].ToString();
                dlMinute.Text = startTime[1].ToString();
            }
            catch (Exception ex)
            {
                lblMessage.InnerText = "error->" + ex.Message;
            }
        }

        Boolean checkTeacherExistingClass() 
        {
            try
            {                
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select StartTime From v_ClassRoutine Where TCodeNo='"+ dlTeacher.SelectedValue +"' And StartTime='"+ (dlHours.SelectedItem.Text + ":" + dlMinute.SelectedItem.Text) +"' And Day='"+dlDay.SelectedItem.Text+"'  ", dt);
                if (dt.Rows.Count > 0)
                {
                    lblMessage.InnerText = "warning-> This Teacher  Already Occupy in this Time";
                    return false;
                }
                else return true;                   
            }
            catch { return false; }
        }

        string startTime;
        protected void dlDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dlHours.Text = "09";
                dlMinute.Text = "00";
                
                for (int i = 0; i < gvDay.Rows.Count; i++)
                {
                    string day = gvDay.Rows[i].Cells[0].Text;
                    if (day == dlDay.SelectedItem.Text)
                    {
                        startTime = gvDay.Rows[i].Cells[2].Text;
                    }
                }
                dlHours.Text = startTime.Substring(0,2);
                dlMinute.Text = startTime.Substring(3, 2);
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                sqlDB.executeCommand("Delete From RoutineDetails Where RoutineId='" + lblRoutineId.Text + "' ");

                saveRoutine();
            }
            catch { }
        }

        private void saveRoutine()
        {
            try
            {                
                string routineid = lblRoutineId.Text;
                int j = 0;
                while ((j = routineid.IndexOf("Select", j)) != -1)
                {
                    lblMessage.InnerText = "warning->Select Session Type";
                    return;
                }

                DataTable dtRoutineId = new DataTable();
                sqlDB.fillDataTable("Select RoutineId From RoutineInfo where RoutineId='"+lblRoutineId.Text+"' ", dtRoutineId);

                if (dtRoutineId.Rows.Count == 0)
                {
                    SqlCommand cmd = new SqlCommand("Insert Into RoutineInfo (RoutineId,BatchId,Shift,SectionName,CreatedBy,DateOfUpdate) Values (@RoutineId,@BatchId,@Shift,@SectionName,@CreatedBy,@DateOfUpdate) ", sqlDB.connection);
                    cmd.Parameters.AddWithValue("@RoutineId", lblRoutineId.Text);
                    cmd.Parameters.AddWithValue("@BatchId", dlBatch.SelectedValue);
                    cmd.Parameters.AddWithValue("@Shift", dlShift.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@SectionName", dlSection.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@CreatedBy", "");
                    cmd.Parameters.AddWithValue("@DateOfUpdate", TimeZoneBD.getCurrentTimeBD("yyyy-MM-dd"));
                    cmd.ExecuteNonQuery();
                }
                else lblMessage.InnerText = "warning->Already exist";

                DataTable dtRidAndDay = new DataTable();
                sqlDB.fillDataTable("Select Day From RoutineDetails Where RoutineId='" + lblRoutineId.Text + "' And Day='"+dlDay.SelectedItem.Text+"' ", dtRidAndDay);

                if (dtRidAndDay.Rows.Count == 0)
                {
                    for (int i = 0; i < gvDay.Rows.Count; i++)
                    {
                        DataTable dtsubid = new DataTable();
                        sqlDB.fillDataTable("Select SubId From NewSubject Where SubName ='" + gvDay.Rows[i].Cells[3].Text + "' ", dtsubid);

                        DataTable dtEmId = new DataTable();
                        sqlDB.fillDataTable("Select EID From EmployeeInfo Where TCodeNo ='" + gvDay.Rows[i].Cells[4].Text + "' ", dtEmId);

                        SqlCommand cmdr = new SqlCommand("Insert Into RoutineDetails (RoutineId,Day,StartTime,EndTime,SubId,EID,OrderNo) Values (@RoutineId,@Day,@StartTime,@EndTime,@SubId,@EID,@OrderNo) ", sqlDB.connection);
                        cmdr.Parameters.AddWithValue("@RoutineId", lblRoutineId.Text);
                        cmdr.Parameters.AddWithValue("@Day", gvDay.Rows[i].Cells[0].Text);
                        cmdr.Parameters.AddWithValue("@StartTime", gvDay.Rows[i].Cells[1].Text);
                        cmdr.Parameters.AddWithValue("@EndTime", gvDay.Rows[i].Cells[2].Text);
                        cmdr.Parameters.AddWithValue("@SubId", dtsubid.Rows[0]["SubId"].ToString());
                        cmdr.Parameters.AddWithValue("@EID", dtEmId.Rows[0]["EID"].ToString());

                        string tt = gvDay.Rows[i].Cells[5].Text;

                        cmdr.Parameters.AddWithValue("@OrderNo", gvDay.Rows[i].Cells[5].Text);
                        cmdr.ExecuteNonQuery();
                    }
                    
                    lblMessage.InnerText = "success->Routine Save Successfull";
                    clearText();
                }
                else lblMessage.InnerText = "warning->Already exist";                              
            }
            catch (Exception ex)
            {
                lblMessage.InnerText ="warning->"+ ex.Message;
            }
        }

        private void clearText()
        {
            try
            {
                dt.Clear();
                dlBatch.Text = "--Select Batch--";
                dlShift.Text = "Morning";
                dlSection.Text = "--Select--";
                dlExamType.Text = "--Select--";
                lblRoutineId.Text = "";                
                txtDuration.Text = "";
                count = 0;
                gvDay.DataSource = dt;
                gvDay.DataBind();

            }
            catch { }
        }

        protected void dlTeacher_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtEmId = new DataTable();
                sqlDB.fillDataTable("Select EID From EmployeeInfo Where TCodeNo='" + dlTeacher.SelectedValue + "' ", dtEmId);
                DataTable dt = new DataTable();
                sqlDB.fillDataTable("Select EID From RoutineDetails Where EID='" + dtEmId.Rows[0]["EID"] + "' And Day='" + dlDay.SelectedItem.Text + "' ", dt);
                lblClassDay.Text = "Total Occupy " + dlDay.SelectedItem.Text + "-" + dt.Rows.Count ;
                DataTable dtweek = new DataTable();
                sqlDB.fillDataTable("Select EID From RoutineDetails Where EID='" + dtEmId.Rows[0]["EID"] + "' ",dtweek);
                lblClassWeek.Text = "Total Occupy in week -"  + dtweek.Rows.Count;
                lblTeacherName.Text ="Teacher Name : "+ dlTeacher.SelectedItem.Text;
            }
            catch { }
        }
    
        protected void gvDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string[] startTime = gvDay.SelectedRow.Cells[1].Text.Split(':');
                dlHours.Text = startTime[0].ToString();
                dlMinute.Text = startTime[1].ToString();
                var startTim = TimeSpan.Parse(gvDay.SelectedRow.Cells[1].Text);
                var endTime = TimeSpan.Parse(gvDay.SelectedRow.Cells[2].Text);
                DateTime startTimes = DateTime.Today.Add(startTim);
                DateTime endTimes = DateTime.Today.Add(endTime);
                TimeSpan duration = endTimes.Subtract(startTimes);
                string timeDuration = duration.ToString("mm");
                txtDuration.Text = timeDuration.ToString();
                dlTeacher.SelectedValue = gvDay.SelectedRow.Cells[4].Text;
                dlSubject.Text = gvDay.SelectedRow.Cells[3].Text;
                dlDay.Text = gvDay.SelectedRow.Cells[0].Text;
                btnAdd.Text = "Edit";
                SelectedIndex = gvDay.SelectedIndex;
            }
            catch { }
        }

        protected void gvDay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            for (int i = 0; i <= gvDay.Rows.Count - 1; i++)
            {
                string myClassVal = gvDay.Rows[i].Cells[0].Text;
                if (myClassVal == "Saturday") gvDay.Rows[i].BackColor = Color.LemonChiffon;
                else if (myClassVal == "Sunday") gvDay.Rows[i].BackColor = Color.MistyRose;
                else if (myClassVal == "Monday") gvDay.Rows[i].BackColor = Color.Lavender;
                else if (myClassVal == "Tuesday") gvDay.Rows[i].BackColor = Color.Thistle;
                else if (myClassVal == "Wednesday") gvDay.Rows[i].BackColor = Color.LightCyan;
                else if (myClassVal == "Thursday") gvDay.Rows[i].BackColor = Color.Pink;
                else gvDay.Rows[i].BackColor = Color.Magenta;              
            }
        }
        DataSet ds;
        string totalTable;
        int clm = 0;
        private void loadClassRoutine(string RouteID)
        {
            try
            {
                DataTable dtday = new DataTable();
                sqlDB.fillDataTable("Select distinct Day, OrderNo, Shift From v_ClassRoutine where RoutineId='" + RouteID + "' ", dtday);
                DataTable dtDays = dtday.Select("", "OrderNo ASC").CopyToDataTable();
                dtday = dtDays;
                ds = new DataSet();
                for (int j = 0; j < dtday.Rows.Count; j++)
                {
                    DataTable dt = new DataTable();
                    sqlDB.fillDataTable("Select Day, SubName, Convert(char(5), StartTime,108) AS StartTime, Convert(char(5),EndTime,108) AS EndTime, TCodeNo From v_ClassRoutine where RoutineId='" + RouteID + "' and Day='" + dtday.Rows[j]["Day"] + "' Order By StartTime ", dt);

                    ds.Tables.Add(dt);
                }
                Session["__Shift__"] = dtday.Rows[0]["Shift"].ToString();
                int tableColumn = 0;
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
                string[] classN = RouteID.Split('_');
                Session["__ClassName__"] = classN[0].ToString();
            }
            catch { }
        } 
    }
}