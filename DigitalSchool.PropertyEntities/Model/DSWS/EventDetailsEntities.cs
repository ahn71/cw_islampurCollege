using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.DSWS
{
   public  class EventDetailsEntities
    {
        public int slNo { get; set; }
        public int SL { get; set; }
        public int ESL { get; set; }
        public string Title { get; set; }
        public string imgLocation { get; set; }
        public string Description { get; set; }
        public string EventName { get; set; }
        public DateTime? EventDate { get; set; }
    }
}
