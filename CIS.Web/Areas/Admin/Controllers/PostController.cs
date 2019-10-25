using AutoMapper;
using CIS.Common;
using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Infrastructure.Extensions;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CIS.Web.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        private IPostService _postService;
        private IPostCategoryService _postCategoryService;

        public PostController(IPostService postService, IPostCategoryService postCategoryService)
        {
            this._postService = postService;
            this._postCategoryService = postCategoryService;
        }

        [HasCredential(RoleID = "R_POST")]
        public ActionResult Index(int filterCategoryId = 0)
        {
            IEnumerable<Post> postModel;
            if (filterCategoryId != 0)
                postModel = _postService.GetAll(filterCategoryId);
            else
                postModel = _postService.GetAll();
            var postViewModel = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            ViewBag.PostCategories = _postCategoryService.GetAll();
            ViewBag.SelectedCategoryID = filterCategoryId;
            return View(postViewModel);
        }

        [HasCredential(RoleID = "CUD_POST")]
        public ActionResult CreateView()
        {
            PostViewModel postViewModel = new PostViewModel();
            postViewModel.PostCategories = _postCategoryService.GetAll();
            return View(postViewModel);
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
        [HasCredential(RoleID = "CUD_POST")]
        public ActionResult Create(PostViewModel postViewModel)
        {

            if (postViewModel.CategoryID == CommonConstant.ChildPageCategoryID)
            {
                postViewModel.Status = true;
            }
            Post newPost = new Post();
            newPost.UpdatePost(postViewModel);
            newPost.CreatedDate = DateTime.Now;
            newPost.CreatedBy = currentUserName;
            TryValidateModel(newPost);
            if (!ModelState.IsValid)
            {
                SetAlert("error", "ModelState is not valid.");
                return RedirectToAction("CreateView");
            }
            else
            {
                _postService.Add(newPost);
                _postService.SaveChanges();
                SetAlert("success", "Đăng bài thành công.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [HasCredential(RoleID = "CUD_POST")]
        public JsonResult Delete(int id)
        {
            _postService.Delete(id);
            _postService.SaveChanges();
            SetAlert("success", "Đã xoá thành công.");
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        [HasCredential(RoleID = "CUD_POST")]
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
        [HasCredential(RoleID = "CUD_POST")]
        public JsonResult ChangeStatus(int id)
        {
            var post = _postService.GetById(id);
            if (post.CategoryID == CommonConstant.ChildPageCategoryID)
            {
                SetAlert("error", "Bài đăng này không được khoá");
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                post.Status = !post.Status;
                post.UpdatedDate = DateTime.Now;
                post.UpdatedBy = currentUserName;
                _postService.Update(post);
                _postService.SaveChanges();
                if (post.Status)
                    SetAlert("success", "Kích hoạt đăng tin " + post.Name);
                else
                    SetAlert("warning", "Khoá tin " + post.Name);
                return Json(new
                {
                    status = true
                });
            }
        }

        [HasCredential(RoleID = "CUD_POST")]
        public ActionResult EditView(int id)
        {
            var postModel = _postService.GetById(id);
            PostViewModel postViewModel = new PostViewModel();
            postViewModel = Mapper.Map<Post, PostViewModel>(postModel);
            postViewModel.PostCategories = _postCategoryService.GetAll();
            return View(postViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [HasCredential(RoleID = "CUD_POST")]
        public ActionResult Update(PostViewModel postViewModel)
        {
            Post updatedPost = _postService.GetById(postViewModel.ID);
            updatedPost.UpdatePost(postViewModel);
            updatedPost.UpdatedDate = DateTime.Now;
            updatedPost.UpdatedBy = currentUserName;
            TryValidateModel(updatedPost);
            if (!ModelState.IsValid)
            {
                SetAlert("error", "Chỉnh sửa không thành công.");
                return RedirectToAction("Index");
            }
            else
            {
                _postService.Update(updatedPost);
                _postService.SaveChanges();
                SetAlert("success", updatedPost.Name + " đã được chỉnh sửa.");
                return RedirectToAction("Index");
            }
        }
    }
}