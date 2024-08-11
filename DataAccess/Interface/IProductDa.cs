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
        Task<Product> GetById(int id);
        Task<ProductImage> GetImageById(int id);
        Task<List<ProductImage>> GetImage(int id);
        Task<GetProductDtRes> GetDt(GetProductDtReq req);
        Task<GetProductImageDtRes> GetImageDt(GetProductImageDtReq req);
        Task<List<Ddl>> GetTypeDdl();
        Task<int> GetNextRanking(int typeId);
        Task<List<GetProductWebRes>> GetWeb();
        Task<GetProductDetailRes> GetDetail(int id);
        Task Insert(Product data, List<ProductImage> images);
        Task Update(Product data, List<ProductImage> images, List<ProductImage> rankings);
        Task Delete(int id, string user);
        Task DeleteImage(int id, string user);



    }
}
