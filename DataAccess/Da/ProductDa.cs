using Course.Core.Models;
using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;
namespace DataAccess.Da
{
    public class ProductDa : IProductDa
    {
        private readonly CourseContext db;
        public ProductDa(CourseContext db)
        {
            this.db = db;
        }
        public async Task<Product> GetById(int id)
        {
            return await db.Product.FindAsync(id);
        }
        public async Task<ProductImage> GetImageById(int id) => await db.ProductImage.FindAsync(id);
        public async Task<List<ProductImage>> GetImage(int id) => await db.ProductImage.Where(w => !w.IsDelete && w.ProductId == id).ToListAsync();
        public async Task<GetProductDtRes> GetDt(GetProductDtReq req)
        {
            var raw = db.Product.Where(w => !w.IsDelete);
            var rawType = db.ProductType.Where(w => !w.IsDelete);

            if (req.TypeId != 0)
                rawType = rawType.Where(w => w.Id == req.TypeId);

            if (!string.IsNullOrEmpty(req.Name))
                raw = raw.Where(w => w.Name.Contains(req.Name) || w.Title.Contains(req.Name));

            int count = await raw.CountAsync();

            var data = await (from r in raw
                              join t in rawType on r.ProductTypeId equals t.Id
                              orderby t.Ranking, r.Ranking
                              select new GetProductDtRes2
                              {
                                  Id = r.Id,
                                  Name = r.Name,
                                  Ranking = r.Ranking,
                                  Title = r.Title,
                                  TypeName = t.Name,
                                  IsActive = t.IsActive,
                                  CreateBy = r.UpdateBy == null ? r.CreateBy : r.UpdateBy,
                                  CreateDate = r.UpdateDate == null ? r.CreateDate : r.UpdateDate.Value
                              })
                       .Skip(req.Start).Take(req.Length)
                       .ToListAsync();

            if (data.Any())
            {
                var ids = data.Select(s => s.Id);
                var images = await db.ProductImage.Where(w => !w.IsDelete && ids.Contains(w.ProductId)).ToListAsync();

                foreach (var d in data)
                {
                    d.Image = images.OrderBy(o => o.Ranking).FirstOrDefault(f => f.ProductId == d.Id)?.Image;
                }
            }

            return new GetProductDtRes
            {
                data = data,
                draw = req.Draw,
                recordsFiltered = count,
                recordsTotal = count
            };
        }
        public async Task<GetProductImageDtRes> GetImageDt(GetProductImageDtReq req)
        {
            var raw = db.ProductImage.Where(w => !w.IsDelete && w.ProductId == req.ProductId).OrderBy(o => o.Ranking);

            int count = await raw.CountAsync();

            var data = await raw.Skip(req.Start).Take(req.Length)
                .Select(s => new GetProductImageDtRes2
                {
                    Id = s.Id,
                    Image = s.Image,
                    Ranking = s.Ranking,
                    CreateBy = s.UpdateBy == null ? s.CreateBy : s.UpdateBy,
                    CreateDate = s.UpdateDate == null ? s.CreateDate : s.UpdateDate.Value
                }).ToListAsync();

            return new GetProductImageDtRes
            {
                data = data,
                draw = req.Draw,
                recordsFiltered = count,
                recordsTotal = count
            };
        }
        public async Task<List<Ddl>> GetTypeDdl() => await db.ProductType.Where(w => !w.IsDelete && w.IsActive)
                .OrderBy(o => o.Ranking)
                .Select(s => new Ddl
                {
                    Name = s.Name,
                    Value = s.Id
                })
                .ToListAsync();
        public async Task<int> GetNextRanking(int typeId)
        {
            var x = await db.Product.Where(w => !w.IsDelete && w.ProductTypeId == typeId).OrderByDescending(u => u.Ranking).Select(s => s.Ranking).FirstOrDefaultAsync();

            return x + 1;
        }
        public async Task<List<GetProductWebRes>> GetWeb()
        {
            var data = await (from r in db.Product
                              where !r.IsDelete && r.IsActive
                              orderby r.Ranking
                              select new GetProductWebRes
                              {
                                  Id = r.Id,
                                  ProductTypeId = r.ProductTypeId,
                                  Name = r.Name,
                                  Title = r.Title,
                                  ShortDetail = r.ShortDetail,
                              })
                       .ToListAsync();

            if (data.Any())
            {
                var ids = data.Select(s => s.Id);
                var images = await db.ProductImage.Where(w => !w.IsDelete && ids.Contains(w.ProductId)).ToListAsync();

                foreach (var d in data)
                {
                    d.Image = images.OrderBy(o => o.Ranking).FirstOrDefault(f => f.ProductId == d.Id)?.Image;
                }
            }

            return data;
        }
        public async Task<GetProductDetailRes> GetDetail(int id)
        {
            var r = await GetById(id);

            var t = await db.ProductType.FindAsync(r.ProductTypeId);

            var data = new GetProductDetailRes
            {
                Name = r.Name,
                Title = r.Title,
                Detail = r.Detail,
                ShortDetail = r.ShortDetail,
                Type = t.Name,
                Images = await db.ProductImage.Where(w => !w.IsDelete && w.ProductId == id).Select(s => s.Image).ToListAsync()
            };

            return data;
        }
        public async Task Insert(Product data, List<ProductImage> images)
        {
            await db.AddAsync(data);
            await db.SaveChangesAsync();

            await InsertImage(data, images);
        }
        public async Task Update(Product data, List<ProductImage> images, List<ProductImage> rankings)
        {
            var o = await GetById(data.Id);

            if (o.Name != data.Name || o.Title != data.Title || o.ShortDetail != data.ShortDetail 
                || o.Detail != data.Detail || o.IsActive != data.IsActive
                || o.Ranking != data.Ranking || o.ProductTypeId != data.ProductTypeId)
            {
                o.Name = data.Name;
                o.Title = data.Title;
                o.ShortDetail = data.ShortDetail;
                o.Detail = data.Detail;
                o.IsActive = data.IsActive;
                o.Ranking = data.Ranking;
                o.ProductTypeId = data.ProductTypeId;
                o.UpdateBy = data.UpdateBy;
                o.UpdateDate = data.UpdateDate;
            }

            var existingImgs = await GetImage(data.Id);
            foreach (var x in existingImgs)
            {
                var r = rankings.FirstOrDefault(f => f.Id == x.Id);
                if (r == null)
                    continue;

                if (x.Ranking != r.Ranking)
                {
                    x.Ranking = r.Ranking;
                    x.UpdateBy = data.UpdateBy;
                    x.UpdateDate = data.UpdateDate;
                }
            }

            await db.SaveChangesAsync();

            await InsertImage(data, images);
        }
        public async Task Delete(int id, string user)
        {
            var o = await GetById(id);
            o.IsDelete = true;
            o.UpdateDate = DateTime.Now;
            o.UpdateBy = user;

            await db.SaveChangesAsync();
        }
        public async Task DeleteImage(int id, string user)
        {
            var o = await db.ProductImage.FindAsync(id);

            o.IsDelete = true;
            o.UpdateDate = DateTime.Now;
            o.UpdateBy = user;

            await db.SaveChangesAsync();
        }
        private async Task InsertImage(Product data, List<ProductImage> images)
        {
            if (images.Any())
            {
                var x = images.Select(s => new ProductImage
                {
                    CreateBy = data.CreateBy,
                    CreateDate = data.CreateDate,
                    Image = s.Image,
                    Ranking = s.Ranking,
                    ProductId = data.Id
                });

                await db.AddRangeAsync(x);
                await db.SaveChangesAsync();
            }
        }



























    }
}
