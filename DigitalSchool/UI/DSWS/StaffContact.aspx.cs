using DS.BLL.HR;
using DS.PropertyEntities.Model.HR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class StaffContact : System.Web.UI.Page
    {
        string divInfo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            loadTeacherContacts();
        }
        private void loadTeacherContacts()
        {
            EmployeeEntry emp = new EmployeeEntry();
            List<Employee> ListEntities = new List<Employee>();
            ListEntities = emp.getStaffCotactList_WS();
            if (ListEntities != null)
            {
                divInfo = "<h2>কর্মচারী পরিচিতি</h2> <div class='row'>";
                for (int r = 0; r < ListEntities.Count; r++)
                {
                    string PicName = (ListEntities[r].PicName == "") ? "noProfileImage.jpg" : ListEntities[r].PicName;
                    divInfo += "<div class='col-md-5'>" +
                        "   <div class='TContact'>" +
                         "  <div class='row'>" +
                           "   <div class='col-md-4'> " +
                             "  <img class='img-responsive' src='../../Images/teacherProfileImage/" + PicName + "' alt='...' />" +
                                "  </div>" +
                             "  <div class='col-md-8'>" +
                                  " <h4 class='media-heading'>" + ListEntities[r].EmpName + "</h4>" +
                                  "  কার্ড নং:  " + ListEntities[r].EmpCardNo + "" +
                                   "     <br />" +
                                  "  পদবীঃ  " + ListEntities[r].Designation + "" +
                                   "     <br />" +
                                 "   মোবাইলঃ " + ListEntities[r].Mobile + "" +
                                  " </div>  " +
                          " </div> " +
                               "</div>   " +
                           "</div>";

                    if ((r + 1) % 2 == 0)
                    {
                        divInfo += "</div><br/> <div class='row'>";
                    }
                }
                divInfo += " </div>";
                divTeacherContacts.Controls.Add(new LiteralControl(divInfo));
            }
        }
    }
}