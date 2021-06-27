using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo3.Parametes
{
    public class todolistparames2
    {
        public string name { get; set; }
        //public int order { get; set; }
        public bool? enable { get; set; }

        public int? minOrder { get; set; }
        public int? maxOrder { get; set; }
        private string _order { get; set; }

        public string? order
        {
            get { return _order; }
            set
            {
                if( value.Contains('-'))
                {
                    minOrder = Convert.ToInt32(value.Split('-')[0]);
                    maxOrder = Int32.Parse(value.Split('-')[1]);
                }else
                {
                    _order =value;
                }
            }
        }

    }
}
