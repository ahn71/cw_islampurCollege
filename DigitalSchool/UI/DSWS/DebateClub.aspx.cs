using DS.BLL.DSWS;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class DebateClub : System.Web.UI.Page
    {
        List<AddPageContentEntities> entities;
        AddPageContentEntry entry;
        string divInfo = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                loadContent();
            }
        }
        private void loadContent()
        {
            if (entry == null)
                entry = new AddPageContentEntry();
            entities = new List<AddPageContentEntities>();
            entities = entry.getIndividualPageData("DebateClub");
            if (entities != null)
                divInfo = "<div class='page-title'><h3>" + entities[0].Title + "</h3></div>" +
                    "<div class='page-fimg'>" +
                                "<img width = '100%' src='PageImages/" + entities[0].Image + "'/> " +
                           "</div>" +
                            "<div class='page-prgp'>" +
                                "<p>" + entities[0].TextContent + "</p>" +
                            "</div>";

            else
                divInfo = "<div class='page-title'> <h3></h3></div >" +

                                       "<div class='page-prgp'>" +
                                           "<p>Content will be Published soon dc</p>" +
                                       "</div>";
            divContent.Controls.Add(new LiteralControl(divInfo));
        }
    }
}