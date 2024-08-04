using Course.Core.Models;
using DataAccess.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IFurnitureCategoryDa
    {
        Task<GetCategoryDTRes> GetAll(GetCategoryDTReq req);
        Task<FurnitureCategory> GetById(int id);
        Task Insert(FurnitureCategory data);
        Task Update(FurnitureCategory data);
        Task Delete(int id, string user);
        Task<int> GetNextRanking();



    }
}
