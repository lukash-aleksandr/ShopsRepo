using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        IUnitOfWork Database { get; set; }

        public ProductService(IUnitOfWork uow)
        {
            Database = uow;
        }

        // for the future
        public void AddProductToShop(ProductDTO productDTO)
        {
            Shop shop = Database.Shops.GetItem(productDTO.ShopId);
            if (shop == null)
            {
                throw new ValidationException("Shop is not found", "");
            }
            if (string.IsNullOrEmpty(productDTO.Name))
            {
                throw new ValidationException("The field Name is not filled", "Name");
            }

            var product = new Product()
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                ShopId = shop.Id
            };

            Database.Products.Create(product);
            Database.Save();
        }

        public IEnumerable<ProductDTO> GetShopProducts(int? shopId)
        {
            if (shopId == null)
            {
                throw new ValidationException("Id of shop is not set", "");
            }
            Mapper.Initialize(m => m.CreateMap<Product, ProductDTO>());
            return Mapper.Map<IEnumerable<Product>, List<ProductDTO>>(Database.Products.Find(p => p.ShopId == shopId.Value));
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
