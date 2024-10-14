using EC.DataAccess.Data;
using EC.DataAccess.Repository.IRepository;
using EC.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EC.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == obj.Id);
            if (product != null)
            {
                product.Title = obj.Title;
                product.Description = obj.Description;
                product.CategoryId = obj.CategoryId;
                product.Price = obj.Price;
                product.ListPrice = obj.ListPrice;
                product.Price50 = obj.Price50;
                product.Price100 = obj.Price100;
                product.Author = obj.Author;
                product.ISBN = obj.ISBN;
                if(obj.ImageUrl != null)
                {
                    product.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
