using AutoMapper;
using SHY.Common;
using SHY.Model.Models;
using SHY.Service;
using SHY.Web.Infrastructure.Core;
using SHY.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace SHY.Web.Controllers
{
    public class PostController : Controller
    {
        IPostService _postService;
        IPostCategoryService _postCategoryService;
        public PostController(IPostService productService, IPostCategoryService postCategoryService)
        {
            this._postService = productService;
            this._postCategoryService = postCategoryService;
        }
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string keyword = null, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var postModel = _postService.Search(keyword, page, pageSize, sort, out totalRow);
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Keyword = keyword;
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postViewModel,
                MaxPage = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            return View(paginationSet);
        }

        public ActionResult Detail(int postId)
        {
            var postModel = _postService.GetById(postId);
            var viewModel = Mapper.Map<Post, PostViewModel>(postModel);
            return View(viewModel);
        }

    }
}