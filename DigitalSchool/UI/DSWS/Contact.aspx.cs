using ComplexScriptingSystem;
using DS.BLL.DSWS;
using DS.DAL;
using DS.PropertyEntities.Model.DSWS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace DS.UI.DSWS
{
    public partial class Contact : System.Web.UI.Page
    {
        ContactEntry Entry;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
          
             loadContact();
            }
        }       
        private void loadContact()
        {
            try {
                iframeGoogleMap.Attributes["src"] = Session["__InstitueGoogleMapSrc__"].ToString();
                pAddress.InnerHtml = Session["__InstitueAddress__"].ToString();
                aWeb.InnerText = Session["__InstitueWeb__"].ToString();
                aWeb.HRef = Session["__InstitueWeb__"].ToString();
                lblEmail.InnerText = Session["__InstitueEmail__"].ToString();
                lblPhone.InnerText = Session["__InstituePhone__"].ToString();
            }
            catch(Exception ex) { }
        }
        private void save()
        {
            ContactEntities entitis = getCotrolValue();
            if (Entry == null)
            {
                Entry = new ContactEntry();
            }
            Entry.AddEntities = entitis;
            if (Entry.Insert())
            {
                AllClear();
                lblMessage.InnerText = "success-> Successfully Send ";
            }
        }
        private ContactEntities getCotrolValue()
        {
            ContactEntities entities = new ContactEntities();            
            entities.Name = txtName.Text;
            entities.Email = txtEmail.Text;
            entities.PhoneNumber = txtPhoneNumber.Text;
            entities.Comments = txtComments.Text;            
            entities.SendDate = DateTime.Now;
            return entities;
        }
        private void AllClear()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtComments.Text = "";
            txtPhoneNumber.Text = "";
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            save();
        }       
    }
}