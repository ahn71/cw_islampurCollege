using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DS.BLL.DSWS
{
    public static class Common
    {
        public static void load_website_initialdata()
        {
            try {
                HttpContext.Current.Session["__InstituteTitle__"] = "ইসলামপুর কলেজ";
                HttpContext.Current.Session["__InstituteSlogan__"] = "শিক্ষা নিয়ে গড়ব দেশ শেখ হাসিনার বাংলাদেশ";
                HttpContext.Current.Session["__InstituePhone__"] = " +88 01962464700";
                HttpContext.Current.Session["__InstitueEmail__"] = "islampurcollege@yahoo.com";
                HttpContext.Current.Session["__InstitueWeb__"] = "http://islampurcollege.edu.bd";
                HttpContext.Current.Session["__InstitueGoogleMapSrc__"] ="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d5903.717876255933!2d89.77854286742634!3d25.093960487564125!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x39fd721f2edb5259%3A0x7626096e590eeb6a!2sIslampur+Upazila!5e0!3m2!1sen!2sbd!4v1547027597366";
                HttpContext.Current.Session["__InstitueCode__"] = "5008";
                HttpContext.Current.Session["__InstitueEIIN__"] = "109857";
                HttpContext.Current.Session["__InstitueAddress__"] = "ইসলামপুর কলেজ <br/>ইসলামপুর,জামালপুর";

                HttpContext.Current.Session["__PresidentMessageHeader__"] = "CHAIRMAN MESSAGE";
                HttpContext.Current.Session["__PresidentDsg__"] = "Chairman";
                HttpContext.Current.Session["__NoticeBoardHeader__"] = "Notice Board";
                HttpContext.Current.Session["__PrincipalMessageHeader__"] = "PRINCIPAL MESSAGE";
                HttpContext.Current.Session["__PrincipalDsg__"] = "Principal";               

            } catch (Exception ex) { }
        }

       
    }
}
