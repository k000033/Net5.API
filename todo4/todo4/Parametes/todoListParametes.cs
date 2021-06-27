using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo4.Parametes
{
    public class todoListParametes
    {

        public string Name { get; set; }
        public bool? Enable { get; set; }
        //public int? Orders { get; set; }
        public int? MinOrder { get; set; }
        public int? MaxOrder { get; set; }

        private string _order { get; set; }
        public string order {

            get { return _order; }
            set
            {
                if (value.Contains('-'))
                {
                    MinOrder = Int32.Parse(value.Split('-')[0]);
                    MaxOrder = Int32.Parse(value.Split('-')[1]);
                }
                else
                {
                    _order = value;
                };
            }
        }
    }
}
