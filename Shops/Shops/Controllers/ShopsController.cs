using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using BLL.Services;
using Shops.Models;

namespace Shops.Controllers
{
    public class ShopsController : Controller
    {
        IShopService shopService;

        public ShopsController(IShopService service)
        {
            shopService = service;
        }

        /// <summary>
        /// Get shops by page
        /// </summary>
        /// <param name="id">Page</param>
        /// <returns></returns>
        public ActionResult Index(int? id)
        {

            int page = id ?? 1;

            int pageSize = Convert.ToInt32(ConfigurationManager.AppSettings["ShopsPageSize"]);
            var pagesCountOnPage = Convert.ToInt32(ConfigurationManager.AppSettings["ShopPageNumbersCount"]);
            var itemsToSkip = (page - 1) * pageSize;

            var shopDtos = shopService.GetShops();

            Mapper.Initialize(cfg => cfg.CreateMap<ShopDTO, ShopViewModel>());
            var shops = Mapper.Map<IEnumerable<ShopDTO>, List<ShopViewModel>>(shopDtos);
            var numberOfPages = Convert.ToInt32(Math.Ceiling((double) shops.Count()/pageSize));
            var startSectionNumber = ((page - 1)/pagesCountOnPage)*pagesCountOnPage; // start page number for current paging
            var endSectionNumber = (startSectionNumber + pagesCountOnPage) <= numberOfPages
                ? (startSectionNumber + pagesCountOnPage)
                : numberOfPages; // end page number for current paging
            
            var pagedData = new PagedData<ShopViewModel>()
            {
                Data = shops.Skip(itemsToSkip).Take(pageSize),
                CurrentPage = page,
                NumberOfPages = numberOfPages,
                StartSectionNumber = startSectionNumber,
                EndSectionNumber = endSectionNumber

            };
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Shops", pagedData);
            }
            return View(pagedData);
        }

        protected override void Dispose(bool disposing)
        {
            shopService.Dispose();
            base.Dispose(disposing);
        }
    }
}