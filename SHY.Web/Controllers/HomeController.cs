using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SHY.Common;
using SHY.Model.Models;
using SHY.Service;
using SHY.Web.Models;

namespace SHY.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;
        IPostService _postService;
        ICommonService _commonService;

        public HomeController(IProductCategoryService productCategoryService,
            IProductService productService,
            IPostService postService,
            ICommonService commonService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;
            _postService = postService;
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Client)]
        public ActionResult Index()
        {
            var slideModel = _commonService.GetSlides();
            var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            var homeViewModel = new HomeViewModel();
            homeViewModel.Slides = slideView;

            var lastestProductModel = _productService.GetLastest(6);
            var topSaleProductModel = _productService.GetHotProduct(6);
            var lastestProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastestProductModel);
            var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
            homeViewModel.LastestProducts = lastestProductViewModel;
            homeViewModel.TopSaleProducts = topSaleProductViewModel;

            var listCategory = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listCategory);
            foreach (var item in listProductCategoryViewModel)
            {
                var topCateProductModel = _productService.GetProductByCategory(6, item.ID);

                item.Products = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topCateProductModel);
            }
            homeViewModel.ProductCategories = listProductCategoryViewModel;
            try
            {
                homeViewModel.Title = _commonService.GetSystemConfig(CommonConstants.HomeTitle).ValueString;
                homeViewModel.MetaKeyword = _commonService.GetSystemConfig(CommonConstants.HomeMetaKeyword).ValueString;
                homeViewModel.MetaDescription = _commonService.GetSystemConfig(CommonConstants.HomeMetaDescription).ValueString;
            }
            catch
            {
               
            }

            return View(homeViewModel);
        }


        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            var footerModel = _commonService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
            return PartialView(listProductCategoryViewModel);
        }

        [ChildActionOnly]
        public ActionResult HotProducts()
        {
            var topSaleProductModel = _productService.GetHotProduct(6);

            var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);

            return PartialView(topSaleProductViewModel);
        }

        [ChildActionOnly]
        public ActionResult GetAllProductsByCategory()
        {
            var listCategory = _productCategoryService.GetAll();
            var listProductCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(listCategory);
            foreach(var item in listProductCategoryViewModel)
            {
                var topSaleProductModel = _productService.GetProductByCategory(6,item.ID);

                var topSaleProductViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProductModel);
                item.Products = topSaleProductViewModel.ToList();
            }

            return PartialView(listProductCategoryViewModel);
        }

        [ChildActionOnly]
        public ActionResult NewPosts()
        {
            var topNewPostModel = _postService.GetNewPost(6);

            var topNewPostViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(topNewPostModel);

            return PartialView(topNewPostViewModel);
        }

    }
}