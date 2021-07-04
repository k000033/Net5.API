using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Todo.Parameters
{
    public class TodoSelectParameter
    {
        public string name { get; set; }
        public bool? enable { get; set; }
        public DateTime? InsertTime { get; set; }
        public int? minOrder { get; set; }
        public int? maxOrder { get; set; }

        private string _order;
        public string Order
        {
            get { return _order; }
            set
            {
                //2-3
                Regex regex = new Regex(@"^\d*-\d$");
                if (regex.Match(value).Success)
                {
                    minOrder =Int32.Parse(value.Split('-')[0]);
                    maxOrder = Int32.Parse(value.Split('-')[1]);
                }
                _order = value;
            }
        }
    }
}
