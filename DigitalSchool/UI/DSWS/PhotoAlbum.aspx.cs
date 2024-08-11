using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.BLL.DSWS;
using DS.PropertyEntities.Model.DSWS;

namespace DS.UI.DSWS
{
    public partial class PhotoAlbum : System.Web.UI.Page
    {
        string divInfo="";
        bool isDetails = false;
        AlbumInfoDetails album = new AlbumInfoDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            divPhotoAlbum.InnerHtml = null;
            if (!IsPostBack) 
            {
                try
                {
                    if (Request.QueryString["PASL"].ToString() != null)
                    {
                        loadAlbumDetails(Request.QueryString["PASL"].ToString());
                        isDetails = true;
                    }
                }
                catch { }
                if(!isDetails)
                loadPhotoAlbum();
            }
        }

        private void loadPhotoAlbum() 
        {
            List<AlbumDetailsEntities> albumSummary = new List<AlbumDetailsEntities>();
            albumSummary = album.getAlbumSummary();
            if (albumSummary != null) {              
                for (int r=0; r < albumSummary.Count;r++ )
                    divInfo += "<div class='col-sm-6 col-md-3 no-margin'>" +
                            " <div class='thumbnail'>" +
                              "<a href='/UI/DSWS/PhotoAlbum.aspx?PASL=" + albumSummary[r].PASL + "'><img alt='...' src='../../" + albumSummary[r].imgLocation.Remove(0, 2) + "' ></a>" +
                              " <div class='caption'>" +
                                " <h5><a href='/UI/DSWS/PhotoAlbum.aspx?PASL=" + albumSummary[r].PASL + "'>" + albumSummary[r].AlbumName + "</a></h5>" +
                                " </div>" +
                             "</div>" +
                          "</div> ";
            }
            divPhotoAlbum.Controls.Add(new LiteralControl(divInfo));

        }
        private void loadAlbumDetails(string PASL)
        {
            List<AlbumDetailsEntities> albumDetails = new List<AlbumDetailsEntities>();
            albumDetails = album.getAlbumDetails(PASL);
            if (albumDetails != null)
            {
                for (int r = 0; r < albumDetails.Count; r++)
                    divInfo += "<div class='col-sm-6 col-md-3 no-margin'>" +
                            " <div class='thumbnail'>" +
                              " <a class='fancybox' rel='gallery1' href='../../" + albumDetails[r].imgLocation.Remove(0, 2) + "' ><img id='photo_" + r + "' alt='...' src='../../" + albumDetails[r].imgLocation.Remove(0, 2) + "' ></a>" +
                             "</div>" +
                          "</div> ";
            }
            divPhotoAlbum.Controls.Add(new LiteralControl(divInfo)); 


           
        }       
    }
}