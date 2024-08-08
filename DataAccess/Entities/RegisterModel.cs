using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class RegisterModel
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CreateBy { get; set; }
        public string ?CreateDate { get; set; }
        public string IsActive { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UpdateBy { get; set; }
        public string ?UpdateDate { get; set; }
    }
}
