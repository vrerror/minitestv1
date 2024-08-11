using Course.Core.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interface
{
    public interface ICategoryDa
    {

        Task<GetCategoryDtRes> GetAll(GetCategoryDtReq req);
        Task<ProductType> GetById(int id);
        Task Insert(ProductType data);
        Task Update(ProductType data);
        Task Delete(int id, string user);
        Task<int> GetNextRanking();
        ProductType GetByName(string name);









    }
}
