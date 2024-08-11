using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.DSWS
{
   public  class AddPageContentEntities
    {
        public int SL { get; set; }
        public string PageID { get; set; }
        public string Page { get; set; }
        public string Image { get; set; }
        public string TextContent { get; set; }
        public string Title { get; set; }
        public bool Status { get; set; }
        public DateTime EntryTime { get; set; }
    }
}
