using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.PropertyEntities.Model.DSWS
{
   public  class AddNoticeEntities
    {
     public int SL { get; set; }
     public   int NSL { get; set; }
     public string NSubject { get; set; }
     public string NDetails { get; set; }
     public DateTime NEntryDate { get; set; }
     public DateTime NPublishedDate { get; set; }
     public int NOrdering { get;set; }
     public bool IsActive { get; set; }
     public bool IsImportantNews { get; set; }
     public string NSummary { get; set; }
     public string Type { get; set; }
     public string FileName { get; set; }
    }
}
