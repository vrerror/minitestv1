using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Core.Models
{
    public class GetProductReportRes
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int OrderQty { get; set; }
        public double Amount { get; set; }
        public double TotalAmount
        {
            get
            {
                return Amount * OrderQty;
            }
        }
    }
}
