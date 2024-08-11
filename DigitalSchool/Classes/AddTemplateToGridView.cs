using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace DigitalSchool.Classes
{
    public class AddTemplateToGridView
    {
        ListItemType _type;

        string _colName;

        public AddTemplateToGridView(ListItemType type, string colname)
        {

            _type = type;

            _colName = colname;
        }
    }
}