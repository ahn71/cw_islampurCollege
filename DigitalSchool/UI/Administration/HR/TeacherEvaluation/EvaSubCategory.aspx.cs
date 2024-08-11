using DS.BLL.ControlPanel;
using DS.BLL.TeacherEvaluation;
using DS.PropertyEntities.Model.TeacherEvaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.HR.TeacherEvaluation
{
    public partial class EvaSubCategory : System.Web.UI.Page
    {
        private SubCategoryEntities Entities;
        private SubCategoryEntry Entry;
        private CategoryEntry CatEntry;
        private List<SubCategoryEntities> SubCategoryList;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddDepartment.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                LoadSubCategory();
                if (CatEntry == null)
                    CatEntry = new CategoryEntry();
                CatEntry.GetDropdownlist(ddlCategory);
            }
        }
        private void LoadSubCategory()
        {
            SubCategoryList = new List<SubCategoryEntities>();
            if (Entry == null)
                Entry = new SubCategoryEntry();
            SubCategoryList = Entry.GetEntitiesData();
            int totalRows = (SubCategoryList == null) ? 0 : SubCategoryList.Count;
            string divInfo = "";

            divInfo = " <table id='tblClassList' class='table table-striped table-bordered dt-responsive nowrap'cellspacing='0' Width='100%' > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Sub Category Name</th>";
            divInfo += "<th>Category Name</th>";
            divInfo += "<th>Ordering</th>";
            divInfo += "<th>Status</th>";
            if (Session["__Update__"].ToString().Equals("true"))
                divInfo += "<th>Edit</th>";
            divInfo += "</tr>";

            divInfo += "</thead>";

            divInfo += "<tbody>";
            if (totalRows == 0)
            {
                divInfo += "</tbody></table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divCategoryList.Controls.Add(new LiteralControl(divInfo));
                return;
            }
            string id = "";
            string cid = "";
            string status = "";
            for (int x = 0; x < SubCategoryList.Count; x++)
            {
                id = SubCategoryList[x].SubCategoryID.ToString();
                cid = SubCategoryList[x].Category.CategoryID.ToString();
                status = (SubCategoryList[x].Status) ? "Active" : "Inactive";
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + SubCategoryList[x].SubCategory.ToString() + "</td>";
                divInfo += "<td >" + SubCategoryList[x].Category.Category.ToString() + "</td>";
                divInfo += "<td>" + SubCategoryList[x].Ordering.ToString() + "</td>";
                divInfo += "<td>" + status+ "</td>";
                if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editCategory(" + id + "," + cid + ");'  />";
            }

            divInfo += "</tbody>";
            divInfo += "<tfoot>";

            divInfo += "</table>";
            divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";

            divCategoryList.Controls.Add(new LiteralControl(divInfo));
        }
        private void save()
        {
            //Entities=new CategoryEntities();
            getCotrolValue();
            if (Entry == null)
            {
                Entry = new SubCategoryEntry();
            }
            Entry.AddEntities = Entities;
            if (Entry.Insert())
            {
                LoadSubCategory();
                AllClear();
                lblMessage.InnerText = "success-> Successfully Saved ";
            }
        }
        private void AllClear()
        {
            lblSubCategoryId.Value = "";
            txtSubCategory.Text = "";
            txtOrdering.Text = "";
            btnSave.Text = "Save";
        }
        private void update()
        {
            getCotrolValue();
            if (Entry == null)
            {
                Entry = new SubCategoryEntry();
            }
            Entry.AddEntities = Entities;
            if (Entry.Update())
            {
                LoadSubCategory();
                AllClear();
                lblMessage.InnerText = "success-> Successfully Update ";
            }
        }
        private void getCotrolValue()
        {
            Entities = new SubCategoryEntities();
            if (lblSubCategoryId.Value != "")
                Entities.SubCategoryID = int.Parse(lblSubCategoryId.Value);
            Entities.SubCategory = txtSubCategory.Text;
            Entities.Category=new CategoryEntities();
            Entities.Category.CategoryID =int.Parse( ddlCategory.SelectedValue);
            Entities.Ordering = int.Parse(txtOrdering.Text.Trim());
            Entities.Status = ckbStatus.Checked;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblSubCategoryId.Value == "")
                save();
            else
                update();

        }
    }
}