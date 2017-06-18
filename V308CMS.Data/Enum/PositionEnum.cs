using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace V308CMS.Data.Enum
{
    public enum PositionEnum
    {
        [Description("Trên")]
        Top =1,
        [Description("Giữa")]
        Center = 2,
        [Description("Dưới")]
        Bottom =3
      
    }
}