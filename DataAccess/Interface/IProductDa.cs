using Course.Core.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface IProductDa
    {
        Task<List<Ddl>> GetTypePd();
        Task<Product> GetById(int id);







    }
}
