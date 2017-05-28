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
    public class ShopService : IShopService
    {
        IUnitOfWork Database { get; set; }

        public ShopService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<ShopDTO> GetShops()
        {
            
            Mapper.Initialize(m => m.CreateMap<Shop, ShopDTO>());
            return Mapper.Map<IEnumerable<Shop>, List<ShopDTO>>(Database.Shops.GetAll());
        }

        public ShopDTO GetShop(int id)
        {
            Mapper.Initialize(m => m.CreateMap<Shop, ShopDTO>());
            return Mapper.Map<Shop, ShopDTO>(Database.Shops.GetItem(id));
        }

        // for the future
        public void CreateShop(ShopDTO shopDTO)
        {
            // validation
            if (string.IsNullOrEmpty(shopDTO.Name))
            {
                throw new ValidationException("The field Name is not filled", "Name");
            }
            if (string.IsNullOrEmpty(shopDTO.Address))
            {
                throw new ValidationException("The field Address is not filled", "Address");
            }
            if (string.IsNullOrEmpty(shopDTO.TimeSchedule))
            {
                throw new ValidationException("The field TimeSchedule is not filled", "TimeSchedule");
            }

            var shop = new Shop()
            {
                Name = shopDTO.Name,
                Address = shopDTO.Address,
                TimeSchedule = shopDTO.TimeSchedule
            };

            Database.Shops.Create(shop);
            Database.Save();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
