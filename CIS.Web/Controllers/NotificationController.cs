using AutoMapper;
using CIS.Common;
using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Infrastructure.Core;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Web.Controllers
{
    public class NotificationController : Controller
    {
        private IPostService _postService;

        public NotificationController(IPostService postService)
        {
            this._postService = postService;
        }

        public ActionResult Index(int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var postModel = _postService.GetListPostByCategoryIdPaging(CommonConstant.NotificationPostCategoryID, page, pageSize, out totalRow);
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

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

        public ActionResult ViewNotification(int id)
        {
            var post = _postService.GetById(id);
            if (post.CategoryID != CommonConstant.NotificationPostCategoryID)
            {
                return RedirectToAction("NotFound", "Error");
            }
            var postViewModel = Mapper.Map<Post, PostViewModel>(post);
            return View(postViewModel);
        }
    }
}