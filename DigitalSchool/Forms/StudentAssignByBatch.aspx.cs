using DS.DAL.AdviitDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DS.Forms
{
    public partial class StudentAssignByBatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Classes.commonTask.loadPreviousBatch(dlPreviousBatch);
            Classes.commonTask.loadBatch(dlCurrentBatch);
        }
    }
}