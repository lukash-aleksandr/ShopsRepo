using System.Collections;
using System.Collections.Generic;
using BLL.DTO;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        void AddProductToShop(ProductDTO productDTO);
        IEnumerable<ProductDTO> GetShopProducts(int? shopId);

        void Dispose();
    }
}