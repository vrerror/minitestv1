using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ProductName { get; set; }

        [Column(TypeName = "nvarchar(120)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public string ShortDetail { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        public string Detail { get; set; }

        public int Ranking { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }

        [NotMapped]
        public string Rankings { get; set; }
    }
}
