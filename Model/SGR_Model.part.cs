using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRSalary.Model
{
    public partial class BI_Personel
    {
        public string compony {get { return BI_Company?.Name; }}
        public string project { get { return BI_Project?.Name; } }
        public string city { get { return BI_City?.Name; } }

    }
}
