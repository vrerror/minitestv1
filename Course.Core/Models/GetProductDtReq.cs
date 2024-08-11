using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Core.Models
{
    public class GetProductDtReq : DtReq
    {

        public int TypeId { get; set; }
        public string Name { get; set; }

    }
}
