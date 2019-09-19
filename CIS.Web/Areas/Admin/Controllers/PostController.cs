using AutoMapper;
using CIS.Common;
using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Infrastructure.Extensions;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        IPostService _postService;
        IPostCategoryService _postCategoryService;

        public PostController(IPostService postService, IPostCategoryService postCategoryService)
        {
            this._postService = postService;
            this._postCategoryService = postCategoryService;
        }

        public ActionResult Index()
        {
            var postModel = _postService.GetAll();
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            return View(postViewModel);
        }

        public ActionResult CreateView()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAlias(string title)
        {
            var alias = StringHelper.ToUnsignString(title);
            return Json(new
            {
                status = true,
                data = alias
            });
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                SetAlert("error", "Đăng bài không thành công.");
                return RedirectToAction("CreateView");
            }
            else
            {
                Post newPost = new Post();
                newPost.UpdatePost(postViewModel);
                newPost.CreatedDate = DateTime.Now;

                _postService.Add(newPost);
                _postService.SaveChanges();
                SetAlert("success", "Đăng bài thành công.");
                return RedirectToAction("Index");
            }
        }

        public JsonResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                SetAlert("error", "Đăng bài không thành công.");
                return Json(new
                {
                    status = false
                });
            }
            else
            {
                _postService.Delete(id);
                _postService.SaveChanges();
                SetAlert("success", "Đã xoá thành công.");
                return Json(new
                {
                    status = true
                });
            }
        }

        [HttpPost]
        public JsonResult DeleteAll(string listId)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var Ids = serializer.Deserialize<string>(listId);
            int countDelete = 0;
            foreach (var id in Ids.Split(new[] { "-" }, StringSplitOptions.None))
            {
                if (!string.IsNullOrEmpty(id))
                {
                    _postService.Delete(int.Parse(id));
                    countDelete++;
                }
            }
            _postService.SaveChanges();
            SetAlert("success", "Tổng cộng " + countDelete + " bản ghi đã được xoá.");
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            var post = _postService.GetById(id);
            post.Status = !post.Status;
            _postService.Update(post);
            _postService.SaveChanges();
            if (post.Status)
                SetAlert("success", "Kích hoạt đăng tin " + post.Name);
            else
                SetAlert("success", "Khoá tin " + post.Name);
            return Json(new
            {
                status = true
            });
        }

        public ActionResult EditView(int id)
        {
            var postModel = _postService.GetById(id);
            PostViewModel postViewModel = new PostViewModel();
            postViewModel = Mapper.Map<Post, PostViewModel>(postModel);
            return View(postViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(PostViewModel postViewModel)
        {
            if (!ModelState.IsValid)
            {
                SetAlert("error", "Chỉnh sửa không thành công.");
                return RedirectToAction("Index");
            }
            else
            {
                Post updatedPost = new Post();
                updatedPost.UpdatePost(postViewModel);
                updatedPost.UpdatedDate = DateTime.Now;

                _postService.Update(updatedPost);
                _postService.SaveChanges();
                SetAlert("success", updatedPost.Name + " đã được chỉnh sửa.");
                return RedirectToAction("Index");
            }
        }
    }