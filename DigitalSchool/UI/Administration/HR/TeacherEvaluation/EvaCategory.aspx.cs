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
    public partial class EvaCategory : System.Web.UI.Page
    {
        private CategoryEntities Entities;
        private CategoryEntry Entry;
        private List<CategoryEntities> CategoryList;
        protected void Page_Load(object sender, EventArgs e)
        {
            lblMessage.InnerText = "";
            if (!IsPostBack)
            {
                if (!PrivilegeOperation.SetPrivilegeControl(int.Parse(Session["__UserTypeId__"].ToString()), "AddDepartment.aspx", btnSave)) Response.Redirect(Request.UrlReferrer.ToString() + "?hasperm=no");
                LoadCategory();
            }
        }
        private void LoadCategory()
        {
            CategoryList = new List<CategoryEntities>();
            if (Entry == null)
                Entry = new CategoryEntry();
            CategoryList = Entry.GetEntitiesData();
            int totalRows = (CategoryList==null)?0:CategoryList.Count;
            string divInfo = "";

            divInfo = " <table id='tblClassList' class='table table-striped table-bordered dt-responsive nowrap'cellspacing='0' Width='100%' > ";
            divInfo += "<thead>";
            divInfo += "<tr>";
            divInfo += "<th>Category Name</th>";
            divInfo += "<th>Ordering</th>";            
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

            for (int x = 0; x < CategoryList.Count; x++)
            {
                id = CategoryList[x].CategoryID.ToString();
                divInfo += "<tr id='r_" + id + "'>";
                divInfo += "<td >" + CategoryList[x].Category.ToString() +"</td>";
                divInfo += "<td>" + CategoryList[x].Ordering.ToString() + "</td>";                
                if (Session["__Update__"].ToString().Equals("true"))
                    divInfo += "<td class='numeric_control' >" + "<img src='/Images/gridImages/edit.png' class='editImg'   onclick='editCategory(" + id + ");'  />";
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
                Entry = new CategoryEntry();
            }
            Entry.AddEntities = Entities;
            if (Entry.Insert())
            {
                LoadCategory();
                AllClear();
                lblMessage.InnerText = "success-> Successfully Saved ";
            }
        }
        private void AllClear() 
        {
            lblCategoryId.Value = "";
            txtCategory.Text = "";
            txtOrdering.Text = "";
            btnSave.Text = "Save";
        }
        private void update() 
        {
            getCotrolValue();
            if (Entry == null)
            {
                Entry = new CategoryEntry();
            }
            Entry.AddEntities = Entities;
            if (Entry.Update())
            {
                LoadCategory();
                AllClear();
                lblMessage.InnerText = "success-> Successfully Update ";
            }
        }
        private void getCotrolValue()
        {
            Entities = new CategoryEntities();
            if (lblCategoryId.Value != "")
            Entities.CategoryID = int.Parse(lblCategoryId.Value);
            Entities.Category = txtCategory.Text;
            Entities.Ordering = int.Parse(txtOrdering.Text.Trim());
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (lblCategoryId.Value=="")
                save();
            else 
                update();
               
        }
    }
}