﻿using Course.Core.Models;
using DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
namespace DataAccess.Da
{
    public class ProductTypeDa : ICategoryDa
    {
        private readonly CourseContext db;

        public ProductTypeDa(CourseContext db)
        {
            this.db = db;
        }

        public async Task<ProductType> GetById(int id)
        {
            return await db.ProductType.FindAsync(id);
        }

        public async Task<GetCategoryDtRes> GetAll(GetCategoryDtReq req)
        {
            var raw = db.ProductType.Where(w => !w.IsDelete);

            if (!string.IsNullOrEmpty(req.Name))
                raw = raw.Where(w => w.Name.Contains(req.Name));

            int count = await raw.CountAsync();

            var data = await raw.Skip(req.Start).Take(req.Length)
                .OrderBy(w => w.Ranking)
                .Select(s => new GetCategoryDtRes2
                {
                    Id = s.Id,
                    Name = s.Name,
                    Image = s.Image,
                    Ranking = s.Ranking,
                    IsActive = s.IsActive,
                    CreateBy = s.UpdateBy == null ? s.CreateBy : s.UpdateBy,
                    CreateDate = s.UpdateDate == null ? s.CreateDate : s.UpdateDate.Value
                }).ToListAsync();

            return new GetCategoryDtRes
            {
                data = data,
                draw = req.Draw,
                recordsFiltered = count,
                recordsTotal = count
            };
        }

      

        public async Task Insert(ProductType data)
        {


            await db.AddAsync(data);
            await db.SaveChangesAsync();

        }

        public async Task Update(ProductType data)
        {
            var o = await GetById(data.Id);
            o.Ranking = data.Ranking;
            o.Name = data.Name;
            o.IsActive = data.IsActive;
            o.UpdateBy = data.UpdateBy;
            o.UpdateDate = data.UpdateDate;

            if (!string.IsNullOrEmpty(data.Image))
                o.Image = data.Image;

            await db.SaveChangesAsync();
        }
        public async Task Delete(int id, string user)
        {
            var o = await GetById(id);
            o.IsDelete = true;
            o.UpdateDate = DateTime.Now;
            o.UpdateBy = user;

            await db.SaveChangesAsync();
        }

        public async Task<int> GetNextRanking()
        {
            var x = await db.ProductType.Where(w => !w.IsDelete).OrderByDescending(u => u.Ranking).Select(s => s.Ranking).FirstOrDefaultAsync();

            return x + 1;
        }

        public ProductType GetByName(string name) => db.ProductType.FirstOrDefault(f => f.Name == name && !f.IsDelete);









    }
}
