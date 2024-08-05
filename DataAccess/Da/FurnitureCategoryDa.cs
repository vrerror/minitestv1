using Course.Core.Models;
using DataAccess.Enities;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.Da
{
    public class FurnitureCategoryDa : IFurnitureCategoryDa
    {
        private CourseContext db;

        public FurnitureCategoryDa(CourseContext db)
        {
            this.db = db;
        }

        public async Task<GetCategoryDTRes> GetAll(GetCategoryDTReq req)
        {
            var raw = db.FurnitureCategory.Where(w => !w.IsDelete);

            if (!string.IsNullOrEmpty(req.Name))
                raw = raw.Where(w => w.Name.Contains(req.Name));

            int count = await raw.CountAsync();

            var data = await raw.Skip(req.Start).Take(req.Length)
                .OrderBy(w => w.Ranking)
                .Select(s => new CategoryDT
                {
                    Id = s.Id,
                    Name = s.Name,
                    Image = s.Image,
                    Ranking = s.Ranking,
                    IsActive = s.IsActive,
                    CreateBy = s.UpdateBy == null ? s.CreateBy : s.UpdateBy,
                    CreateDate = s.UpdateDate == null ? s.CreateDate : s.UpdateDate.Value
                }).ToListAsync();

            return new GetCategoryDTRes
            {
                data = data,
                draw = req.draw,
                recordsFiltered = count,
                recordsTotal = count
            };
        }

        public async Task<FurnitureCategory> GetById(int id)
        {
            return await db.FurnitureCategory.FindAsync(id);
        }

        public async Task Insert(FurnitureCategory data)
        {
            try
            {
                await db.AddAsync(data);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ex = ex;
            }

        }

        public async Task Update(FurnitureCategory data)
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
            var x = await db.FurnitureCategory.Where(w => !w.IsDelete).OrderByDescending(u => u.Ranking).Select(s => s.Ranking).FirstOrDefaultAsync();

            return x + 1;
        }

        public FurnitureCategory GetByName(string name) => db.FurnitureCategory.FirstOrDefault(f => f.Name == name && !f.IsDelete);


















































        //private readonly CourseContext db;

        //public FurnitureCategoryDa(CourseContext db)
        //{
        //    this.db = db;

        //}

        ////public List<Category> GetAll() => db.Category.ToList();

        //public async Task<List<FurnitureCategory>> GetAll()
        //{
        //    var data = await (from c in db.FurnitureCategory
        //                      where !c.IsDelete
        //                select c).ToListAsync();

        //    //var data =  await db.Category().ToList();

        //    return data;   
        //}

        //public FurnitureCategory GetById(int id)
        //{
        //    var data = (from c in db.FurnitureCategory
        //                select c).FirstOrDefault( wh => wh.Id == id);

        //    return data;
        //}

        //public void insert(FurnitureCategory req)
        //{
        //    //    var dataName = db.Category.FirstOrDefault(wh => wh.Name == req.Name && !wh.IsDelete);
        //    //    //var dataName = db.Category.Where(wh => wh.Name.Equals(req.Name)).ToList();

        //    //    //var dataName = db.Category.Any(wh => wh.Name == req.Name && !wh.IsDelete);

        //    //    if (dataName == null)
        //    //{
        //    db.Add(req);
        //    db.SaveChanges();
        //    //}

        //}

        //public async Task<FurnitureCategory> isExist(FurnitureCategory data) => db.FurnitureCategory.FirstOrDefault(wh => wh.Name == data.Name && !wh.IsDelete);


        //public FurnitureCategory update(FurnitureCategory data)
        //{
        //    var o = db.FurnitureCategory.FirstOrDefault(wh => wh.Id == data.Id);

        //    if (o != null)
        //    {
        //        o.Name = data.Name;
        //        o.Image = data.Image;
        //        o.IsActive = data.IsActive;
        //        o.UpdateBy = data.UpdateBy;
        //        o.UpdateDate = data.UpdateDate;
        //    }

        //    db.SaveChanges();

        //    return o;

        //}

        //public FurnitureCategory Delete(int id , string user)
        //{
        //    var o = db.FurnitureCategory.FirstOrDefault(wh => wh.Id == id);

        //    o.IsDelete = true;
        //    o.UpdateBy = user;
        //    o.UpdateDate = DateTime.Now;

        //    db.SaveChanges();

        //    return o;
        //}

        //public async Task<getCategoryDTRes> getDataCatDT(getCategoryDTReq req)
        //{
        //    var raw = db.FurnitureCategory.Where(w => !w.IsDelete);

        //    if (!string.IsNullOrEmpty(req.Name))
        //        raw = raw.Where(wh => wh.Name.Contains(req.Name));

        //    int count = await raw.CountAsync();

        //    if (req.OrderType == "asc")
        //    {
        //        if (req.OrderName == "Name")
        //            raw = raw.OrderBy(w => w.Name);
        //        else if (req.OrderName == "IsActive")
        //            raw = raw.OrderBy(w => w.IsActive);
        //    }
        //    else if (req.OrderType == "desc")
        //    {
        //        if (req.OrderName == "Name")
        //            raw = raw.OrderByDescending(w => w.Name);
        //    }

        //    var data = await raw.Skip(req.Start).Take(req.Length).Select(a => new CategoryDT
        //    {
        //        Id = a.Id,
        //        Name = a.Name,
        //        Image = a.Image,
        //        IsActive = a.IsActive,
        //        CreateBy = a.UpdateBy == null ? a.CreateBy : a.UpdateBy
        //      , CreateDate = a.UpdateDate == null ?  a.CreateDate :a.UpdateDate.Value
        //    }).ToListAsync();



        //    return new getCategoryDTRes
        //    {
        //        data = data,
        //        draw = req.draw,
        //        recordsFiltered = count,
        //        recordsTotal = count
        //    };
        //}

        //public FurnitureCategory GetByName(string name) => db.FurnitureCategory.FirstOrDefault(f => f.Name == name && !f.IsDelete);

    }
}
