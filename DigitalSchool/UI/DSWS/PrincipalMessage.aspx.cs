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
    public partial class PrincipalMessage : System.Web.UI.Page
    {
        List<AddPresidentEntities> entitiesPnc;
        AddPresidentSpeechEntry EntrySpeech;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadPrincipalSpeech();
        }
        private void LoadPrincipalSpeech()
        {
            try
            {
                entitiesPnc = new List<AddPresidentEntities>();
                if (EntrySpeech == null)
                {
                    EntrySpeech = new AddPresidentSpeechEntry();
                }                
                entitiesPnc = EntrySpeech.getPrincipalSpeechData();
                hPrincipalName.InnerText = entitiesPnc[0].PresidentName;

                pSpeech.InnerText = entitiesPnc[0].Speech;

                string url = @"/Images/dsimages/" + entitiesPnc[0].ImgPath;
                imgPrincipal.ImageUrl = url;

                lblInstituteTitle.InnerText = Session["__InstituteTitle__"].ToString();
                lblPricipalDsg.InnerText = Session["__PrincipalDsg__"].ToString();
            }
            catch { }

        }
    }
}