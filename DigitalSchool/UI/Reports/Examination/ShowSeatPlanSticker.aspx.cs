using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Reports.Examination
{
    public partial class ShowSeatPlanSticker : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showSticker();
            }
           
        }
        private void showSticker()
        {
            try {
                DataTable dt = new DataTable();
                dt = (DataTable)Session["__SeatPlanStricker__"];
                string divInfo = "<table style='width:100%'>";
                int count = 0;
                for (int r=0;r<dt.Rows.Count;r++)
                {
                    
                    if (r == 0 || count==3)
                    {
                        divInfo += @"<tr>";
                        count = 0;
                    }
                    int Year =int.Parse(dt.Rows[r]["BatchName"].ToString().Substring(Math.Max(0, dt.Rows[r]["BatchName"].ToString().Length - 4))); 
                    
                    divInfo += @"<td>
                    <div class='sticker-card-box' style='width: 250px;border: 2px solid #000;padding: 3px;margin: 5px;'>
                        <table style='width: 100%; font-size: 16px;'>
                            <tr>
                                <td>
                                    <img height='60px'
                                        src='https://islampurcollege.edu.bd/Images/Logo/256333333333333333333.jpg'>
                                </td>
                                <td colspan='2' style='text-align: left;'>
                                    <h1 style='font-size: 18px;margin: 0;'>Govt. Islampur College</h1>
                                    <p style='margin: 0;'>Islampur, Jamalpur</p>
                                </td>
                            </tr>
                            <tr>
                                <td colspan='3' style='text-align: center;'><h2 style='text-align: center;border: 1px dashed #000;padding: 2px;font-size: 16px;margin: 0;'>" + dt.Rows[r]["ExName"].ToString() + @"</h2></td>
                            </tr>
                            <tr>
                                <td>Name</td>
                                <td>: </td>
                                <td style='font-size:12px;white-space:nowrap;'>" + dt.Rows[r]["FullName"].ToString() + @"</td>
                            </tr>
                            <tr>
                                <td>Session</td>
                                <td>: </td>
                                <td >" + Year.ToString()+"-"+ (Year+1).ToString() + @"</td>
                            </tr>
                            <tr>
                                <td>Group</td>
                                <td>: </td>
                                <td>" + dt.Rows[r]["GroupName"].ToString() + @"</td>
                            </tr>
                            <tr>
                                <td>Roll</td>
                                <td>: </td>
                                <td><strong>" + dt.Rows[r]["RollNo"].ToString() + @"</strong></td>
                            </tr>
                        </table>
                    </div>
                </td>";
                    count++;
                    if (count == 3)
                        divInfo += "</tr>";
                }
                if(count>0)
                    divInfo += "</tr>";
                divInfo += "</table>";
                divShow.Controls.Add(new LiteralControl(divInfo));
            } catch(Exception ex) { }
        }
    }
}