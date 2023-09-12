using DS.BLL.GeneralSettings;
using DS.PropertyEntities.Model.GeneralSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.DSWS
{
    public partial class YearlyLeaveList : System.Web.UI.Page
    {
        OffdayEntry oday;
        List<OffdayEntities> odayEntities = new List<OffdayEntities>();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(!IsPostBack)
            {
                LoadYearlyLeave();
            }
        }
        private void LoadYearlyLeave()
        {
            try
            {
                if(oday==null)
                {
                    oday = new OffdayEntry();
                }
                odayEntities = oday.GetEntitiesData();
                List<OffdayEntities> DayEntities = new List<OffdayEntities>();

                DateTime begin = new DateTime(DateTime.Now.Year, 1, 1);
                DateTime end = new DateTime(DateTime.Now.Year, 12, 31);
                string div = "";
                while (begin <= end)
                {
                    div += "<div class='data_view month_div month_div"+begin.Month+"'>";
                    div += "<table cellspacing='1' class='month_table month_table" + begin.Month + "'>";
                    div += "<caption>"+begin.ToString("MMMM")+"</caption>";
                    div += "<tbody>";
                    div += "<tr><th>Day</th><th>event</th></tr>";
                    DateTime aftermonth = begin.AddMonths(1);
                    for (int b = begin.Day; begin < aftermonth; b++)
                    {
                        DayEntities = odayEntities.FindAll(c=>c.OffDate==begin);
                        if(DayEntities.Count>0)
                        {
                            div += "<tr>";
                            div += "<td>"+begin.ToString("dd")+"-"+begin.ToString("ddd")+"</td>";
                            div += "<td><div class='home_even home_holiday'>" + DayEntities[0].Purpose + "</div><a href='index.php?page=event&amp;y=2015&amp;m=5&amp;d=1&amp;eid=317' title='" + DayEntities[0].Purpose + "  দিবসটি  ফটো গ্যালারি, কমিটি ও তাৎপর্য দেখতে ক্লিক করুন'><div class='home_event home_event1'>" + DayEntities[0].Purpose + " </div></a></td>";
                            div += "</tr>";
                        }
                        else
                        {
                            div += "<tr>";
                            div += "<td>" + begin.ToString("dd") + "-" + begin.ToString("ddd") + "</td>";
                            div += "<td><div class='home_event homeclasscontinue'>Class</div></td>";
                            div += "</tr>";
                        }
                        begin = begin.AddDays(1);
                    }
                    div += "</tbody>";
                    div += "</table>";
                    div += "</div>";                                    
                }
                div += "<div class='cb'></div>";  
                divYearlyHoliday.Controls.Add(new LiteralControl(div));
            }
            catch { }
        }
    }
}