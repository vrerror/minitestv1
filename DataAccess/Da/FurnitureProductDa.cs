using Course.Core.Models;
using DataAccess.Enities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataAccess.Da
{
    public class FurnitureProductDa
    {
        private readonly CourseContext db;

        public FurnitureProductDa(CourseContext db )
        {
            this.db = db;
        }

        public List<FurnitureProduct> GetAll(string name)
        {
            //var data = (from c in db.Products
            //            where !c.IsDelete
            //            select c).ToList();

            var data = db.FurnitureProduct.Where(wh => !wh.IsDelete);

            if (!string.IsNullOrEmpty(name))
            {
                data = data.Where(wh => wh.Name.Contains(name));
            }

            return data.ToList();
        }

        public List<getProductsModel> getProducts(string name)
        {
            var data = from a in db.FurnitureProduct
                       join b in db.FurnitureCategory on a.CategoryId equals b.Id
                       where !a.IsDelete && !b.IsDelete
                       select new getProductsModel
                       {
                           Id = a.Id
                           , Name = a.Name
                           , CategoryName = b.Name
                           , Price = a.Price
                           , Image = a.Image
                           , IsActive = a.IsActive
                           , UpdateBy = a.UpdateBy == null ? a.CreateBy : a.UpdateBy
                           , UpdateDate = a.UpdateDate == null ?  a.CreateDate :a.UpdateDate.Value
                       };

            if (!string.IsNullOrEmpty(name))
                data = data.Where(wh => wh.Name.Contains(name));


            return data.ToList();
        }

        public FurnitureProduct GetById(int id)
        {
            var data = (from c in db.FurnitureProduct
                        select c).FirstOrDefault(wh => wh.Id == id);

            return data;
        }

        public void insert(FurnitureProduct data)
        {
            db.Add(data);
            db.SaveChanges();

        }

        public FurnitureProduct update(FurnitureProduct data)
        {
            var o = db.FurnitureProduct.FirstOrDefault(wh => wh.Id == data.Id);

            o.Name = data.Name;
            o.CategoryId = data.CategoryId;
            o.Price = data.Price;
            o.Image = data.Image;
            o.IsActive = data.IsActive;
            o.UpdateBy = data.UpdateBy;
            o.UpdateDate = data.UpdateDate;

            db.SaveChanges();

            return o;
        }

        public FurnitureProduct Delete(int id, string user)
        {
            var o = db.FurnitureProduct.FirstOrDefault(wh => wh.Id == id);

            o.IsDelete = true;
            o.UpdateBy = user;
            o.UpdateDate = DateTime.Now;

            db.SaveChanges();

            return o;

        }
    }
}
