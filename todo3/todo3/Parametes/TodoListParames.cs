using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo3.Parametes
{
    public class TodoListParames
    {
       public string name { get; set; }
      //public string order { get; set; }

       //public string? order_range { get; set; }
       public int? Minorder { get; set; }
       public int? Maxorder { get; set; }
       public bool? enable { get; set; }


        private string _order;
        public string order
        {
            get { return _order; }
            set
            {

                if (value.Contains('-'))
                {
                    Minorder = Int32.Parse(value.Split('-')[0]);
                    Maxorder = Int32.Parse(value.Split('-')[1]);
                }
                else
                {
                    _order = value;
                }
            }

        }

    }
}
