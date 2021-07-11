using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using todo4.ValidationAttributes;

namespace todo4.Parametes
{
    public class todoListParametes2
    {
        //[TodoNameAttribute]
        //[StringLength(2, ErrorMessage = "錯誤")]
        public string Name { get; set; }
        public bool? Enable { get; set; }
        //public int? Orders { get; set; }

        public int? MinOrder { get; set; }
        public int? MaxOrder { get; set; }


        private string _order { get; set; }

        public string Order {

            get
            {
                return _order;
            }set
            {
                if(value.Contains('_'))
                {
                    MinOrder = Int32.Parse(value.Split('-')[0]);
                    MaxOrder = Int32.Parse(value.Split('-')[1]);
                }else
                {
                    _order = value;
                }
            }
        }


    }
}
