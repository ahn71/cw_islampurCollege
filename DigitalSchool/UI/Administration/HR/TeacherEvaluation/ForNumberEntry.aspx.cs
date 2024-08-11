using DS.BLL.TeacherEvaluation;
using DS.DAL.ComplexScripting;
using DS.PropertyEntities.Model.TeacherEvaluation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.UI.Administration.HR.TeacherEvaluation
{
    public partial class ForNumberEntry : System.Web.UI.Page
    {
        DataTable dt;
        string getValue;
        string getTblData;
        SqlCommand cmd;
        SqlDataAdapter da;
        NumberEntry entry;
        NumberSheetEntities entities;
        bool result;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                getTblData = Request.QueryString["tbldata"];
                getValue = Request.QueryString["val"];

                /* value splited for set parameter in every update function*/

                string[] getTableName = getTblData.Split(',');

                /* call function for by using parameters*/
                EvaluationNumberEntry(getTableName, getValue);
            }
            catch { }
        }
        private void EvaluationNumberEntry(string[] getNeededInfo, string getMarks)   // marks enterd in marksheet
        {
            try
            {
                // frist time compare marks are acurade
                if (float.Parse(getMarks) > float.Parse(getNeededInfo[4])) { Response.Write("Not Save"); return; }
                // for entered subject mark of examinations

                entities = new NumberSheetEntities();
                entities.EID = int.Parse(getNeededInfo[0]);
                entities.SessionID = int.Parse(getNeededInfo[1]);
                entities.MemberID = int.Parse(getNeededInfo[2]);
                entities.SubCategoryID = int.Parse(getNeededInfo[3]);
                entities.ObtainNumber = float.Parse(getMarks);
               
                if (entry == null)
                    entry = new NumberEntry();
                entry.AddEntities = entities;
             if(entry.Insert())
                Response.Write("Yes Save");
                else
                 Response.Write("Not Save");
            }
            catch { Response.Write("Not Save"); }
        }
    }
}