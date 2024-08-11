using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.DSWS
{
   public  class AddSecialDescriptionEntities
    {
        public int SL { get; set; }
        public int DSL { get; set; }
        public string Subject { get; set; }
        public string Details { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
    }
}
