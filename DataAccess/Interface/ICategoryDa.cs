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
        Task<CategoryET> GetById(int id);
        Task Insert(CategoryET data);
        Task Update(CategoryET data);
        Task Delete(int id, string user);
        Task<int> GetNextRanking();
        CategoryET GetByName(string name);









    }
}
