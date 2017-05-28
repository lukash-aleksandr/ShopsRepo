using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using Shops.Models;

namespace Shops.Controllers
{
    public class ProductController : Controller
    {
        IProductService productService;
        private IShopService shopService;

        public ProductController(IProductService productServ, IShopService shopServ)
        {
            productService = productServ;
            shopService = shopServ;
        }
        
        /// <summary>
        /// Get list of products for the shop by page
        /// </summary>
        /// <param name="shopId">Id of shop</param>
        /// <param name="id">Page</param>
        /// <returns></returns>
        public ActionResult Index(int? shopId, int? id)
        {
            ViewBag.ShopId = shopId;
            ViewBag.ShopName = string.Empty;
            if (shopId == null)
            {
                return null;
            }

            var shopDto = shopService.GetShop(shopId.Value);
            if (shopDto != null)
            {
                Mapper.Initialize(m => m.CreateMap<ShopDTO, ShopViewModel>());
                var shop = Mapper.Map<ShopDTO, ShopViewModel>(shopDto);
                ViewBag.ShopName = shop.Name;
            }
            
            int page = id ?? 1;

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ProductsPageSize"]);
            var pagesCountOnPage = Convert.ToInt32(ConfigurationManager.AppSettings["ProductPageNumbersCount"]);
            var itemsToSkip = (page - 1) * pageSize;

            var productDtos = productService.GetShopProducts(shopId);
            Mapper.Initialize(cfg => cfg.CreateMap<ProductDTO, ProductViewModel>());
            var products = Mapper.Map<IEnumerable<ProductDTO>, List<ProductViewModel>>(productDtos);
            var numberOfPages = Convert.ToInt32(Math.Ceiling((double) products.Count() / pageSize));
            var startSectionNumber = ((page - 1) / pagesCountOnPage) * pagesCountOnPage; // start page number for current paging
            var endSectionNumber = (startSectionNumber + pagesCountOnPage) <= numberOfPages
                ? (startSectionNumber + pagesCountOnPage)
                : numberOfPages; // end page number for current paging

            var pagedData = new PagedData<ProductViewModel>()
            {
                Data = products.Skip(itemsToSkip).Take(pageSize),
                CurrentPage = page,
                NumberOfPages = numberOfPages,
                StartSectionNumber = startSectionNumber,
                EndSectionNumber = endSectionNumber

            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Products", pagedData);
            }
            return null;
        }
    }
}