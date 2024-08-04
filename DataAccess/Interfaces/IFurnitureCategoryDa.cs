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
        Task<List<FurnitureCategory>> GetAll();
        FurnitureCategory GetById(int id);
        void insert(FurnitureCategory req);
        Task<FurnitureCategory> isExist(FurnitureCategory data);
        FurnitureCategory update(FurnitureCategory data);
        FurnitureCategory Delete(int id, string user);
        Task<getCategoryDTRes> getDataCatDT(getCategoryDTReq req);
        FurnitureCategory GetByName(string name);
    }
}
