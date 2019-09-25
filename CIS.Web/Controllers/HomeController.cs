using AutoMapper;
using CIS.Common;
using CIS.Model.Models;
using CIS.Service;
using CIS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CIS.Web.Controllers
{
    public class HomeController : Controller
    {
        private IFooterService _footerService;
        private IPostService _postService;
        private IMenuService _menuService;
        private ISlideService _slideService;

        public HomeController(IFooterService footerService, IMenuService menuService, ISlideService slideService, IPostService postService)
        {
            this._footerService = footerService;
            this._menuService = menuService;
            this._slideService = slideService;
            this._postService = postService;
        }

        public ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            var hotNews = _postService.GetTwoHotNews();
            var listNews = _postService.GetThreeNews();
            homeViewModel.HotNews = hotNews;
            homeViewModel.ListNews = listNews;
            return View(homeViewModel);
        }

        public ActionResult AboutUs()
        {
            var aboutUsPost = _postService.GetById(CommonConstant.AboutUsPostID);
            PostViewModel postViewModel = Mapper.Map<Post, PostViewModel>(aboutUsPost); 
            return View(postViewModel);
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            IEnumerable<Menu> listMenu = _menuService.GetAll(CommonConstant.MainMenuId);
            var listMenuViewModel = Mapper.Map<IEnumerable<Menu>, IEnumerable<MenuViewModel>>(listMenu);
            return PartialView(listMenuViewModel);
        }

        [ChildActionOnly]
        public ActionResult Slide()
        {
            IEnumerable<Slide> listSlide = _slideService.GetAll();
            var listSlideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(listSlide);
            return PartialView(listSlideViewModel);
        }

        [ChildActionOnly]
        public ActionResult QuickAccessFooter()
        {
            var footer = _footerService.GetByName(CommonConstant.QAFooter);
            FooterViewModel footerViewModel = Mapper.Map<Footer, FooterViewModel>(footer);
            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult LicenseFooter()
        {
            var footer = _footerService.GetByName(CommonConstant.LFooter);
            FooterViewModel footerViewModel = Mapper.Map<Footer, FooterViewModel>(footer);
            return PartialView(footerViewModel);
        }
    }
}