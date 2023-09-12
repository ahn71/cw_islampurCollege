using DS.DAL;
using System;

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.BLL.DSWS
{
    public class QuickLinkEntry
    {
        WSQuickLink _Entities;
        List<WSQuickLink> _list;
        bool result;      

        public WSQuickLink SetEntities
        {
            set
            {
                _Entities = value;
            }

        }
        public bool save()
        {
            try {
                using (cw_islampurCollegeDB db = new cw_islampurCollegeDB())
                {
                    //WSQuickLink wSQuickLink = new WSQuickLink {
                    //    Title = " আন্তঃশিক্ষা বোর্ড",
                    //    Url = "http://www.educationboard.gov.bd/",
                    //    IsActive=true,
                    //    Ordering=2
                    //};
                    db.WSQuickLinks.Add(_Entities);
                    db.SaveChanges();
                }

                return true;
            }
            catch(Exception ex) { return false; }
        }
        public bool update()
        {
            try {
                using (cw_islampurCollegeDB db = new cw_islampurCollegeDB())
                {                              
                    db.Entry(_Entities).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return true;
            }
            catch(Exception ex) { return false; }
        }
        public bool delete(int sl)
        {
            try {
                using (cw_islampurCollegeDB db = new cw_islampurCollegeDB())
                {
                   
                    WSQuickLink quickLink = new WSQuickLink()
                    {
                       SL=sl
                    };
                    db.WSQuickLinks.Attach(quickLink);
                    db.WSQuickLinks.Remove(quickLink);
                    db.SaveChanges();
                }

                return true;
            }
            catch(Exception ex) { return false; }
        }
       
        public List<WSQuickLink> list()
        {
            try
            {
               
                using (cw_islampurCollegeDB db = new cw_islampurCollegeDB())
                {
                    _list = new List<WSQuickLink>();
                    _list = db.WSQuickLinks.ToList<WSQuickLink>().OrderBy(p => p.Ordering).ToList();
                    return _list;
                }
            }
            catch (Exception ex) { return null; }
        }
        public List<WSQuickLink> listActive()
        {
            try
            {
               
                using (cw_islampurCollegeDB db = new cw_islampurCollegeDB())
                {
                    _list = new List<WSQuickLink>();
                    _list = db.WSQuickLinks.Where(a=>a.IsActive==true).ToList<WSQuickLink>().OrderBy(p => p.Ordering).ToList();
                    return _list;
                }
            }
            catch (Exception ex) { return null; }
        }
        
    }
}
