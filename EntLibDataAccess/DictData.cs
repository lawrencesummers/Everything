using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntLibDataAccess
{
   public class DictData
    {
       public string ID{get;set;}
       public string DictType_ID { get; set; }
       public string Name { get; set; }
       public string Value { get; set; }
       public string Remark { get; set; }
       public string Seq { get; set; }
       public string Editor { get; set; }
       public DateTime LastUpdated { get; set; }
    }
}
