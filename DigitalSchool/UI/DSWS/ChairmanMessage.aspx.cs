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
    public partial class ChairmanMessage : System.Web.UI.Page
    {
        List<AddPresidentEntities> entitiesPr;
        AddPresidentSpeechEntry EntrySpeech;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadPresidentSpeech();
        }
        private void LoadPresidentSpeech()
        {
            try
            {
                entitiesPr = new List<AddPresidentEntities>();
                
                if (EntrySpeech == null)
                {
                    EntrySpeech = new AddPresidentSpeechEntry();
                }
                entitiesPr = EntrySpeech.getPresidentSpeechData();
                hPresidentName.InnerText = entitiesPr[0].PresidentName;
                pSpeech.InnerText= entitiesPr[0].Speech;
                string url = @"/Images/dsimages/" + entitiesPr[0].ImgPath;
                imgPresident.ImageUrl = url;
                lblInstituteTitle.InnerText = Session["__InstituteTitle__"].ToString();                
                lblPresidentDsg.InnerText = Session["__PresidentDsg__"].ToString();

            }
            catch { }

        }
    }
}