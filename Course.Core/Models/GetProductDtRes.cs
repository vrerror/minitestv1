using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Core.Models
{

    public class GetProductDtRes : DtRes
    {
        public List<GetProductDtRes2> data { get; set; }
    }

       public class GetProductDtRes2
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string ProductName { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Ranking { get; set; }
        public bool IsActive { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
