using System.Collections;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IShopService
    {
        IEnumerable<ShopDTO> GetShops();
        ShopDTO GetShop(int id);
        void CreateShop(ShopDTO shopDTO);
        void Dispose();
    }
}