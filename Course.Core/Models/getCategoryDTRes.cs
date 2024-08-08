using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Core.Models
{

    public class GetCategoryDtRes : DtRes
    {
        public List<GetCategoryDtRes2> data { get; set; }
    }

       public class GetCategoryDtRes2
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Keywork { get; set; }
        public string Image { get; set; }
        public int Ranking { get; set; }
        public bool IsActive { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
