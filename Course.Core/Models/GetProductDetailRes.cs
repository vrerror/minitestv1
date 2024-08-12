using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Core.Models
{

    public class GetProductDetailRes 
    {
        public string Type { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string ShortDetail { get; set; }
        public string Detail { get; set; }
        public List<string> Images { get; set; }

    }
}
