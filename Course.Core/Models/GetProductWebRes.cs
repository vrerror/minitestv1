using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Core.Models
{

    public class GetProductWebRes 
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ShortDetail { get; set; }
        public string Image { get; set; }

    }
}
