using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using DS.BLL;

namespace DS.Forms
{
    public partial class PromotionClassNine : System.Web.UI.Page
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
                    loadClassNineTenBatch();
                    //loadClassNineStudent();
                }
            }
        }

        private void loadClassNineTenBatch()
        {
            try
            {
                string Nine="",Ten="";
                dt = new DataTable();
                sqlDB.fillDataTable("Select ClassName,ClassOrder From Classes where ClassOrder>=9", dt);
                if (dt.Rows.Count == 0) return;
                try
                {
                    Session["__ClassNine__"] = dt.Rows[0]["ClassName"].ToString();
                    Nine = dt.Rows[0]["ClassName"].ToString() + TimeZoneBD.getCurrentTimeBD().ToString("yyyy");
                    Session["__ClassTen__"] = dt.Rows[1]["ClassName"].ToString();
                    Ten = dt.Rows[1]["ClassName"].ToString() + TimeZoneBD.getCurrentTimeBD().ToString("yyyy");
                }
                catch { }

                for (byte i = 0; i < dt.Rows.Count; i++)
                {
                    ddlBatchName.Items.Add(dt.Rows[i]["ClassName"].ToString()+ TimeZoneBD.getCurrentTimeBD().ToString("yyyy"));
                    
                }
               
            }
            catch { }

        }

        private void loadClassNineStudent()
        {
            ViewState["__tableInfoScience__"] = null;
            ViewState["__tableInfoCommerce__"] = null;
            ViewState["__tableInfoArts__"] = null;
            ViewState["__tableInfo__"] = null;

            try
            {
                dt = new DataTable();
                if (dlShift.Text == "All")
                {
                    sqlDB.fillDataTable("Select StudentId,FullName,SectionName from V_ClassNineStudent where BatchName='" + ddlBatchName.Text + "' and ClassName='" + Session["__ClassNine__"].ToString() + "'", dt);  
                }
                else
                    sqlDB.fillDataTable("Select StudentId,FullName,SectionName from V_ClassNineStudent where BatchName='" + ddlBatchName.Text + "' and ClassName='" + Session["__ClassNine__"].ToString() + "' and Shift='"+dlShift.Text+"'", dt);  
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        
                        if (dt.Rows[i]["SectionName"].ToString() == "Science")
                        {
                            DataTable dt1 = new DataTable();
                            string[] value = new string[2];
                            value[0] = dt.Rows[i]["StudentId"].ToString();
                            value[1] = dt.Rows[i]["FullName"].ToString();

                            try
                            {
                                dt1 = (DataTable)ViewState["__tableInfoScience__"];
                                if (dt1 == null) dt1 = new DataTable();
                            }
                            catch { }

                            if (dt1.Columns.Count == 0)
                            {
                                dt1 = new DataTable();
                                dt1.Columns.Add("StudentId");
                                dt1.Columns.Add("FullName");
                            }

                            dt1.Rows.Add(value);
                            ViewState["__tableInfoScience__"] = dt1;
                            ChkblScience.DataSource = dt1;
                            ChkblScience.DataTextField = "FullName";
                            ChkblScience.DataValueField = "StudentId";
                            ChkblScience.DataBind();
                        }
                        else if (dt.Rows[i]["SectionName"].ToString() == "Commerce")
                        {
                            DataTable dt1 = new DataTable();
                            string[] value = new string[2];
                            value[0] = dt.Rows[i]["StudentId"].ToString();
                            value[1] = dt.Rows[i]["FullName"].ToString();


                            try
                            {
                                dt1 = (DataTable)ViewState["__tableInfoCommerce__"];
                                if (dt1 == null) dt1 = new DataTable();
                            }
                            catch { }

                            if (dt1.Columns.Count == 0)
                            {
                                dt1 = new DataTable();
                                dt1.Columns.Add("StudentId");
                                dt1.Columns.Add("FullName");
                            }

                            dt1.Rows.Add(value);
                            ViewState["__tableInfoCommerce__"] = dt1;
                            ChkblCommerce.DataSource = dt1;
                            ChkblCommerce.DataTextField = "FullName";
                            ChkblCommerce.DataValueField = "StudentId";
                            ChkblCommerce.DataBind();
                        }
                        else if (dt.Rows[i]["SectionName"].ToString() == "Arts")
                        {
                            DataTable dt1 = new DataTable();
                            string[] value = new string[2];
                            value[0] = dt.Rows[i]["StudentId"].ToString();
                            value[1] = dt.Rows[i]["FullName"].ToString();


                            try
                            {
                                dt1 = (DataTable)ViewState["__tableInfoArts__"];
                                if (dt1 == null) dt1 = new DataTable();
                            }
                            catch { }

                            if (dt1.Columns.Count == 0)
                            {
                                dt1 = new DataTable();
                                dt1.Columns.Add("StudentId");
                                dt1.Columns.Add("FullName");
                            }

                            dt1.Rows.Add(value);
                            ViewState["__tableInfoArts__"] = dt1;
                            ChkblArts.DataSource = dt1;
                            ChkblArts.DataTextField = "FullName";
                            ChkblArts.DataValueField = "StudentId";
                            ChkblArts.DataBind();
                        }
                        else if ((dt.Rows[i]["SectionName"].ToString() != "Arts") || (dt.Rows[i]["SectionName"].ToString() != "Commerce") || (dt.Rows[i]["SectionName"].ToString() != "Science"))
                        {
                            DataTable dt1 = new DataTable();
                            string[] value = new string[2];
                            value[0] = dt.Rows[i]["StudentId"].ToString();
                            value[1] = dt.Rows[i]["FullName"].ToString();


                            try
                            {
                                dt1 = (DataTable)ViewState["__tableInfo__"];
                                if (dt1 == null) dt1 = new DataTable();
                            }
                            catch { }

                            if (dt1.Columns.Count == 0)
                            {
                                dt1 = new DataTable();
                                dt1.Columns.Add("StudentId");
                                dt1.Columns.Add("FullName");
                            }

                            dt1.Rows.Add(value);
                            ViewState["__tableInfo__"] = dt1;
                            chkbl.DataSource = dt1;
                            chkbl.DataTextField = "FullName";
                            chkbl.DataValueField = "StudentId";
                            chkbl.DataBind();
                        }
                    }
                    foreach (ListItem chkitem in chkbl.Items)
                    {
                        chkitem.Selected = false;
                    }
                    foreach (ListItem chkitem in ChkblScience.Items)
                    {
                        chkitem.Selected = false;
                    }
                    foreach (ListItem chkitem in ChkblCommerce.Items)
                    {
                        chkitem.Selected = false;
                    }
                    foreach (ListItem chkitem in ChkblArts.Items)
                    {
                        chkitem.Selected = false;
                    }
                   
                

            }
            catch { }
        }

        protected void btnScience_Click(object sender, EventArgs e)
        {
            if (chkbl.Items.Count == 0)
            {
                ViewState["__tableInfo__"] = null;
                return;
            }
            AddScience();
        }

        private void AddScience()
        {
            try
            {
                DataTable dt = new DataTable();
                foreach (ListItem li in chkbl.Items)
                {
                    if (li.Selected)
                    {
                        string[] value = new string[2];
                        value[0] = li.Value;
                        value[1] = li.Text;
                        try
                        {
                            dt = (DataTable)ViewState["__tableInfoScience__"];
                            if (dt == null) dt = new DataTable();
                        }
                        catch { }

                        if (dt.Columns.Count == 0)
                        {
                            dt = new DataTable();
                            dt.Columns.Add("StudentId");
                            dt.Columns.Add("FullName");
                        }

                        dt.Rows.Add(value);
                        ViewState["__tableInfoScience__"] = dt;


                    }
                }
                
                ChkblScience.DataSource = dt;
                ChkblScience.DataTextField = "FullName";
                ChkblScience.DataValueField = "StudentId";
                ChkblScience.DataBind();
                List<ListItem> toBeRemoved = new List<ListItem>();
                for (int i = 0; i < chkbl.Items.Count; i++)
                {
                    if (chkbl.Items[i].Selected == true)
                        toBeRemoved.Add(chkbl.Items[i]);
                }

                for (int i = 0; i < toBeRemoved.Count; i++)
                {
                    chkbl.Items.Remove(toBeRemoved[i]);
                }
                if (chkbl.Items.Count == 0)
                {
                    ViewState["__tableInfo__"] = null;
                }
            }
            catch { }
        }

        private void AddCommerce()
        {
            try
            {
                DataTable dt = new DataTable();
                foreach (ListItem li in chkbl.Items)
                {
                    if (li.Selected)
                    {
                        string[] value = new string[2];
                        value[0] = li.Value;
                        value[1] = li.Text;
                        try
                        {
                            dt = (DataTable)ViewState["__tableInfoCommerce__"];
                            if (dt == null) dt = new DataTable();
                        }
                        catch { }

                        if (dt.Columns.Count == 0)
                        {
                            dt = new DataTable();
                            dt.Columns.Add("StudentId");
                            dt.Columns.Add("FullName");
                        }

                        dt.Rows.Add(value);
                        ViewState["__tableInfoCommerce__"] = dt;
                    }

                }
                ChkblCommerce.DataSource = dt;
                ChkblCommerce.DataTextField = "FullName";
                ChkblCommerce.DataValueField = "StudentId";
                ChkblCommerce.DataBind();
                List<ListItem> toBeRemoved = new List<ListItem>();
                for (int i = 0; i < chkbl.Items.Count; i++)
                {
                    if (chkbl.Items[i].Selected == true)
                        toBeRemoved.Add(chkbl.Items[i]);
                }

                for (int i = 0; i < toBeRemoved.Count; i++)
                {
                    chkbl.Items.Remove(toBeRemoved[i]);
                }
                if (chkbl.Items.Count == 0)
                {
                    ViewState["__tableInfo__"] = null;
                }
            }
            catch { }
        }
        private void AddArts()
        {
            try
            {
                DataTable dt = new DataTable();
                foreach (ListItem li in chkbl.Items)
                {
                    if (li.Selected)
                    {
                        string[] value = new string[2];
                        value[0] = li.Value;
                        value[1] = li.Text;
                        try
                        {
                            dt = (DataTable)ViewState["__tableInfoArts__"];
                            if (dt == null) dt = new DataTable();
                        }
                        catch { }

                        if (dt.Columns.Count == 0)
                        {
                            dt = new DataTable();
                            dt.Columns.Add("StudentId");
                            dt.Columns.Add("FullName");
                        }

                        dt.Rows.Add(value);
                        ViewState["__tableInfoArts__"] = dt;
                    }

                }
                ChkblArts.DataSource = dt;
                ChkblArts.DataTextField = "FullName";
                ChkblArts.DataValueField = "StudentId";
                ChkblArts.DataBind();
                //foreach (ListItem chkitem in ChkblArts.Items)
                //{
                //    chkitem.Selected = true;
                //}
                List<ListItem> toBeRemoved = new List<ListItem>();
                for (int i = 0; i < chkbl.Items.Count; i++)
                {
                    if (chkbl.Items[i].Selected == true)
                        toBeRemoved.Add(chkbl.Items[i]);
                }

                for (int i = 0; i < toBeRemoved.Count; i++)
                {
                    chkbl.Items.Remove(toBeRemoved[i]);
                }
                if (chkbl.Items.Count == 0)
                {
                    ViewState["__tableInfo__"] = null;
                }
            }
            catch { }
        }

        protected void btnCommerce_Click(object sender, EventArgs e)
        {
            if (chkbl.Items.Count == 0) return;
            AddCommerce();
        }

        protected void btnAtrs_Click(object sender, EventArgs e)
        {
            if (chkbl.Items.Count == 0) return;
            AddArts();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            BackFromScience();
            if (ChkblScience.Items.Count == 0)
            {
                ViewState["__tableInfoScience__"] = null;              
            }
            BackFromCommarce();
            if (ChkblCommerce.Items.Count == 0)
            {
                ViewState["__tableInfoCommerce__"] = null;
            }
            BackFromArts();
            if (ChkblArts.Items.Count == 0)
            {
                ViewState["__tableInfoArts__"] = null;
            }
        }

        private void BackFromScience()
        {
            try
            {
                int count = 0;
                DataTable dt = new DataTable();
                foreach (ListItem li in ChkblScience.Items)
                {
                    if (li.Selected)
                    {
                        count++;
                        string[] value = new string[2];
                        value[0] = li.Value;
                        value[1] = li.Text;
                        try
                        {
                            dt = (DataTable)ViewState["__tableInfo__"];
                            if (dt == null) dt = new DataTable();
                        }
                        catch { }

                        if (dt.Columns.Count == 0)
                        {
                            dt = new DataTable();
                            dt.Columns.Add("StudentId");
                            dt.Columns.Add("FullName");
                        }

                        dt.Rows.Add(value);
                        ViewState["__tableInfo__"] = dt;

                        chkbl.DataSource = dt;
                        chkbl.DataTextField = "FullName";
                        chkbl.DataValueField = "StudentId";
                        chkbl.DataBind();
                    }

                }
                if (count == 0) return;
               
                foreach (ListItem chkitem in chkbl.Items)
                {
                    chkitem.Selected = false;
                }
                List<ListItem> toBeRemoved = new List<ListItem>();
                for (int i = 0; i < ChkblScience.Items.Count; i++)
                {
                    if (ChkblScience.Items[i].Selected ==true)
                        toBeRemoved.Add(ChkblScience.Items[i]);
                }

                for (int i = 0; i < toBeRemoved.Count; i++)
                {
                    ChkblScience.Items.Remove(toBeRemoved[i]);
                }
                if (ChkblScience.Items.Count == 0)
                {
                    ViewState["__tableInfoScience__"] = null;
                }
            }
            catch { }
        }

        private void BackFromCommarce()
        {
            try
            {
                int count = 0;
                DataTable dt = new DataTable();
                foreach (ListItem li in ChkblCommerce.Items)
                {
                    if (li.Selected)
                    {
                        count++;
                        string[] value = new string[2];
                        value[0] = li.Value;
                        value[1] = li.Text;
                        try
                        {
                            dt = (DataTable)ViewState["__tableInfo__"];
                            if (dt == null) dt = new DataTable();
                        }
                        catch { }

                        if (dt.Columns.Count == 0)
                        {
                            dt = new DataTable();
                            dt.Columns.Add("StudentId");
                            dt.Columns.Add("FullName");
                        }

                        dt.Rows.Add(value);
                        ViewState["__tableInfo__"] = dt;

                        chkbl.DataSource = dt;
                        chkbl.DataTextField = "FullName";
                        chkbl.DataValueField = "StudentId";
                        chkbl.DataBind();
                    }

                }
                if (count == 0) return;

                foreach (ListItem chkitem in chkbl.Items)
                {
                    chkitem.Selected = false;
                }
                List<ListItem> toBeRemoved = new List<ListItem>();
                for (int i = 0; i < ChkblCommerce.Items.Count; i++)
                {
                    if (ChkblCommerce.Items[i].Selected == true)
                        toBeRemoved.Add(ChkblCommerce.Items[i]);
                }

                for (int i = 0; i < toBeRemoved.Count; i++)
                {
                    ChkblCommerce.Items.Remove(toBeRemoved[i]);
                }
                if (ChkblCommerce.Items.Count == 0)
                {
                    ViewState["__tableInfoCommerce__"] = null;
                }
            }
            catch { }
        }

        private void BackFromArts()
        {
            try
            {
                int count = 0;
                DataTable dt = new DataTable();
                foreach (ListItem li in ChkblArts.Items)
                {
                    if (li.Selected)
                    {
                        count++;
                        string[] value = new string[2];
                        value[0] = li.Value;
                        value[1] = li.Text;
                        try
                        {
                            dt = (DataTable)ViewState["__tableInfo__"];
                            if (dt == null) dt = new DataTable();
                        }
                        catch { }

                        if (dt.Columns.Count == 0)
                        {
                            dt = new DataTable();
                            dt.Columns.Add("StudentId");
                            dt.Columns.Add("FullName");
                        }

                        dt.Rows.Add(value);
                        ViewState["__tableInfo__"] = dt;

                        chkbl.DataSource = dt;
                        chkbl.DataTextField = "FullName";
                        chkbl.DataValueField = "StudentId";
                        chkbl.DataBind();
                    }

                }
                if (count == 0) return;

                foreach (ListItem chkitem in chkbl.Items)
                {
                    chkitem.Selected = false;
                }
                List<ListItem> toBeRemoved = new List<ListItem>();
                for (int i = 0; i < ChkblArts.Items.Count; i++)
                {
                    if (ChkblArts.Items[i].Selected == true)
                        toBeRemoved.Add(ChkblArts.Items[i]);
                }

                for (int i = 0; i < toBeRemoved.Count; i++)
                {
                    ChkblArts.Items.Remove(toBeRemoved[i]);
                }
                if (ChkblArts.Items.Count == 0)
                {
                    ViewState["__tableInfoArts__"] = null;
                }
            }
            catch { }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (UpdateClassNineStudent() == true)
            {
                if (chkbl.Items.Count != 0)
                {
                    foreach (ListItem li in chkbl.Items)
                    {
                        SqlCommand cmd = new SqlCommand("Update CurrentStudentInfo set SectionName=@SectionName where StudentId=" + li.Value + "", sqlDB.connection);
                        cmd.Parameters.AddWithValue("@SectionName", "");
                        cmd.ExecuteNonQuery();

                        cmd = new SqlCommand("Delete From OptionalSubjectInfo where StudentId=" + li.Value + "", sqlDB.connection);
                        cmd.ExecuteNonQuery();
                    }
                }
                lblScienceStudent.Text = "(" + ChkblScience.Items.Count + ")";
                lblCommerceStudent.Text = "(" + ChkblCommerce.Items.Count + ")";
                lblArtsStudent.Text = "(" + ChkblArts.Items.Count + ")";
                
            }
            loadClassNineStudent();
            lblMessage.InnerText = "success->Separation Successfully";
        }

        private Boolean UpdateClassNineStudent()
        {
            try
            {
                DataTable dtOpSub = new DataTable();
                sqlDB.fillDataTable("Select SubName,SubId From NewSubject Where IsOptional='1' ", dtOpSub);

                if (ChkblScience.Items.Count > 0)
                {
                    foreach (ListItem li in ChkblScience.Items)
                    {
                        dt = new DataTable();
                        sqlDB.fillDataTable("Select SectionName From CurrentStudentInfo where StudentId=" + li.Value + "", dt);
                        if (dt.Rows[0]["SectionName"].ToString().ToUpper() != "Science".ToUpper())
                        {
                            SqlCommand cmd1 = new SqlCommand("Delete From OptionalSubjectInfo where StudentId=" + li.Value + "", sqlDB.connection);
                            cmd1.ExecuteNonQuery();
                            SqlCommand cmd = new SqlCommand("Update CurrentStudentInfo set SectionName=@SectionName where StudentId=" + li.Value + "", sqlDB.connection);
                            cmd.Parameters.AddWithValue("@SectionName", "Science");
                            cmd.ExecuteNonQuery();

                            cmd = new SqlCommand("Insert Into OptionalSubjectInfo(StudentId,SubId,BatchName) values(@StudentId,@SubId,@BatchName)", sqlDB.connection);
                            cmd.Parameters.AddWithValue("@StudentId", li.Value);
                            cmd.Parameters.AddWithValue("@SubId", dtOpSub.Rows[0]["SubId"].ToString());
                            cmd.Parameters.AddWithValue("@BatchName", ddlBatchName.SelectedItem.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    List<ListItem> toBeRemoved = new List<ListItem>();
                    for (int i = 0; i < ChkblScience.Items.Count; i++)
                    {
                        toBeRemoved.Add(ChkblScience.Items[i]);
                    }

                    for (int i = 0; i < toBeRemoved.Count; i++)
                    {
                        ChkblScience.Items.Remove(toBeRemoved[i]);
                    }
                    if (ChkblScience.Items.Count == 0)
                    {
                        ViewState["__tableInfoScience__"] = null;
                    }
                }

                if (ChkblCommerce.Items.Count > 0)
                {
                    foreach (ListItem li in ChkblCommerce.Items)
                    {
                        dt = new DataTable();
                        sqlDB.fillDataTable("Select SectionName From CurrentStudentInfo where StudentId=" + li.Value + "", dt);
                        if (dt.Rows[0]["SectionName"].ToString().ToUpper() != "Commerce".ToUpper())
                        {
                            SqlCommand cmd1 = new SqlCommand("Delete From OptionalSubjectInfo where StudentId=" + li.Value + "", sqlDB.connection);
                            cmd1.ExecuteNonQuery();

                            SqlCommand cmd = new SqlCommand("Update CurrentStudentInfo set SectionName=@SectionName where StudentId=" + li.Value + "", sqlDB.connection);
                            cmd.Parameters.AddWithValue("@SectionName", "Commerce");
                            cmd.ExecuteNonQuery();

                            cmd = new SqlCommand("Insert Into OptionalSubjectInfo(StudentId,SubId,BatchName) values(@StudentId,@SubId,@BatchName)", sqlDB.connection);
                            cmd.Parameters.AddWithValue("@StudentId", li.Value);
                            cmd.Parameters.AddWithValue("@SubId", dtOpSub.Rows[0]["SubId"].ToString());
                            cmd.Parameters.AddWithValue("@BatchName", ddlBatchName.SelectedItem.Text);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    List<ListItem> toBeRemovedCo = new List<ListItem>();
                    for (int i = 0; i < ChkblCommerce.Items.Count; i++)
                    {
                        toBeRemovedCo.Add(ChkblCommerce.Items[i]);
                    }

                    for (int i = 0; i < toBeRemovedCo.Count; i++)
                    {
                        ChkblCommerce.Items.Remove(toBeRemovedCo[i]);
                    }
                    if (ChkblCommerce.Items.Count == 0)
                    {
                        ViewState["__tableInfoCommerce__"] = null;
                    }
                }

                if (ChkblArts.Items.Count > 0)
                {
                    foreach (ListItem li in ChkblArts.Items)
                    {
                        dt = new DataTable();
                        sqlDB.fillDataTable("Select SectionName From CurrentStudentInfo where StudentId=" + li.Value + "", dt);
                        if (dt.Rows[0]["SectionName"].ToString().ToUpper() != "Arts".ToUpper())
                        {
                            SqlCommand cmd1 = new SqlCommand("Delete From OptionalSubjectInfo where StudentId=" + li.Value + "", sqlDB.connection);
                            cmd1.ExecuteNonQuery();

                            SqlCommand cmd = new SqlCommand("Update CurrentStudentInfo set SectionName=@SectionName where StudentId=" + li.Value + "", sqlDB.connection);
                            cmd.Parameters.AddWithValue("@SectionName", "Arts");
                            cmd.ExecuteNonQuery();

                            cmd = new SqlCommand("Insert Into OptionalSubjectInfo(StudentId,SubId,BatchName) values(@StudentId,@SubId,@BatchName)", sqlDB.connection);
                            cmd.Parameters.AddWithValue("@StudentId", li.Value);
                            cmd.Parameters.AddWithValue("@SubId", dtOpSub.Rows[0]["SubId"].ToString());
                            cmd.Parameters.AddWithValue("@BatchName", ddlBatchName.SelectedItem.Text);
                            cmd.ExecuteNonQuery();
                        }
                        foreach (ListItem chkitem in chkbl.Items)
                        {
                            chkitem.Selected = false;
                        }
                    }
                        List<ListItem> toBeRemoved = new List<ListItem>();
                        for (int i = 0; i < ChkblArts.Items.Count; i++)
                        {
                            toBeRemoved.Add(ChkblArts.Items[i]);
                        }

                        for (int i = 0; i < toBeRemoved.Count; i++)
                        {
                            ChkblArts.Items.Remove(toBeRemoved[i]);
                        }
                        if (ChkblArts.Items.Count == 0)
                        {
                            ViewState["__tableInfoArts__"] = null;
                        }
                    
                }
                return true;
            }
            catch(Exception ex)
            { return false; }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlBatchName.Text == "...Select Batch...")
                {
                    chkbl.Items.Clear();
                    ChkblArts.Items.Clear();
                    ChkblCommerce.Items.Clear();
                    ChkblScience.Items.Clear();
                    btnSubmit.CssClass = "";
                    btnSubmit.Enabled = false;
                    btnScience.CssClass = "";
                    btnScience.Enabled = false;
                    btnCommerce.CssClass = "";
                    btnAtrs.CssClass = "";
                    btnBack.CssClass = "";
                    btnCommerce.Enabled = false;
                    btnAtrs.Enabled = false;
                    btnBack.Enabled = false;
                    ViewState["__tableInfoScience__"] = null;
                    ViewState["__tableInfoCommerce__"] = null;
                    ViewState["__tableInfoArts__"] = null;
                    ViewState["__tableInfo__"] = null;
                    lblArtsStudent.Text = "";
                    lblCommerceStudent.Text = "";
                    lblScienceStudent.Text = "";
                    lblMessage.InnerText = "warning->Please Select Batch";
                    return;
                }
                string cls = new String(ddlBatchName.Text.Where(Char.IsLetter).ToArray());

                if (cls == Session["__ClassNine__"].ToString())
                {
                    lblMessage.InnerText = "";
                    chkbl.Items.Clear();
                    ChkblArts.Items.Clear();
                    ChkblCommerce.Items.Clear();
                    ChkblScience.Items.Clear();
                    btnSubmit.CssClass = "btn btn-primary promotion_button";
                    btnSubmit.Enabled = true;
                    btnScience.CssClass = "promotion_button";
                    btnScience.Enabled = true;
                    btnCommerce.CssClass = "promotion_button";
                    btnAtrs.CssClass = "promotion_button";
                    btnBack.CssClass = "promotion_button";
                    btnCommerce.Enabled = true;
                    btnAtrs.Enabled = true;
                    btnBack.Enabled = true;
                    ViewState["__tableInfoScience__"] = null;
                    ViewState["__tableInfoCommerce__"] = null;
                    ViewState["__tableInfoArts__"] = null;
                    ViewState["__tableInfo__"] = null;
                    loadClassNineStudent();
                }
                else if (cls == Session["__ClassTen__"].ToString())
                {
                    lblMessage.InnerText = "";
                    chkbl.Items.Clear();
                    ChkblArts.Items.Clear();
                    ChkblCommerce.Items.Clear();
                    ChkblScience.Items.Clear();
                    btnSubmit.CssClass = "";
                    btnSubmit.Enabled = false;
                    btnScience.CssClass = "";
                    btnScience.Enabled = false;
                    btnCommerce.CssClass = "";
                    btnAtrs.CssClass = "";
                    btnBack.CssClass = "";
                    btnCommerce.Enabled = false;
                    btnAtrs.Enabled = false;
                    btnBack.Enabled = false;
                    ViewState["__tableInfoScience__"] = null;
                    ViewState["__tableInfoCommerce__"] = null;
                    ViewState["__tableInfoArts__"] = null;
                    ViewState["__tableInfo__"] = null;
                    LoadClassTenStudent();
                }
                lblScienceStudent.Text = "(" + ChkblScience.Items.Count + ")";
                lblCommerceStudent.Text = "(" + ChkblCommerce.Items.Count + ")";
                lblArtsStudent.Text = "(" + ChkblArts.Items.Count + ")";
            }
            catch { }
        }
        private void LoadClassTenStudent()
        {
            dt = new DataTable();
            sqlDB.fillDataTable("Select StudentId,FullName,SectionName from V_ClassNineStudent where BatchName='" + ddlBatchName.Text + "' and ClassName='" + Session["__ClassTen__"].ToString() + "'", dt);
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                if (dt.Rows[i]["SectionName"].ToString().ToUpper() == "Science".ToUpper())
                {
                    DataTable dt1 = new DataTable();
                    string[] value = new string[2];
                    value[0] = dt.Rows[i]["StudentId"].ToString();
                    value[1] = dt.Rows[i]["FullName"].ToString();


                    try
                    {
                        dt1 = (DataTable)ViewState["__tableInfoScience__"];
                        if (dt1 == null) dt1 = new DataTable();
                    }
                    catch { }

                    if (dt1.Columns.Count == 0)
                    {
                        dt1 = new DataTable();
                        dt1.Columns.Add("StudentId");
                        dt1.Columns.Add("FullName");
                    }

                    dt1.Rows.Add(value);
                    ViewState["__tableInfoScience__"] = dt1;
                    ChkblScience.DataSource = dt1;
                    ChkblScience.DataTextField = "FullName";
                    ChkblScience.DataValueField = "StudentId";
                    ChkblScience.DataBind();
                }
                else if (dt.Rows[i]["SectionName"].ToString().ToUpper() == "Commerce".ToUpper())
                {
                    DataTable dt1 = new DataTable();
                    string[] value = new string[2];
                    value[0] = dt.Rows[i]["StudentId"].ToString();
                    value[1] = dt.Rows[i]["FullName"].ToString();


                    try
                    {
                        dt1 = (DataTable)ViewState["__tableInfoCommerce__"];
                        if (dt1 == null) dt1 = new DataTable();
                    }
                    catch { }

                    if (dt1.Columns.Count == 0)
                    {
                        dt1 = new DataTable();
                        dt1.Columns.Add("StudentId");
                        dt1.Columns.Add("FullName");
                    }

                    dt1.Rows.Add(value);
                    ViewState["__tableInfoCommerce__"] = dt1;
                    ChkblCommerce.DataSource = dt1;
                    ChkblCommerce.DataTextField = "FullName";
                    ChkblCommerce.DataValueField = "StudentId";
                    ChkblCommerce.DataBind();
                }
                else if (dt.Rows[i]["SectionName"].ToString().ToUpper() == "Arts".ToUpper())
                {
                    DataTable dt1 = new DataTable();
                    string[] value = new string[2];
                    value[0] = dt.Rows[i]["StudentId"].ToString();
                    value[1] = dt.Rows[i]["FullName"].ToString();


                    try
                    {
                        dt1 = (DataTable)ViewState["__tableInfoArts__"];
                        if (dt1 == null) dt1 = new DataTable();
                    }
                    catch { }

                    if (dt1.Columns.Count == 0)
                    {
                        dt1 = new DataTable();
                        dt1.Columns.Add("StudentId");
                        dt1.Columns.Add("FullName");
                    }

                    dt1.Rows.Add(value);
                    ViewState["__tableInfoArts__"] = dt1;
                    ChkblArts.DataSource = dt1;
                    ChkblArts.DataTextField = "FullName";
                    ChkblArts.DataValueField = "StudentId";
                    ChkblArts.DataBind();
                }
            }
            foreach (ListItem chkitem in ChkblScience.Items)
            {
                chkitem.Selected = true;
            }
            foreach (ListItem chkitem in ChkblCommerce.Items)
            {
                chkitem.Selected = true;
            }
            foreach (ListItem chkitem in ChkblArts.Items)
            {
                chkitem.Selected = true;
            }
        }
    }
}