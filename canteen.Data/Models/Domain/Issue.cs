using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace canteen.Data.Models.Domain
{
    public class Issue
    {
        public int Id { get; set; }
        public string item_number { get; set; }
        public string description { get; set; }
        public DateTime rect_date { get; set; }
        public decimal rate { get; set; }
        public decimal quantity { get; set; }
        public DateTime issd_date { get; set; }
        public string category { get; set; }

        public string menuitem { get; set; }
        public bool isChecked { get; set; }
    }
}

