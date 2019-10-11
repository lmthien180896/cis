﻿using AutoMapper;
using CIS.Model.Models;
using CIS.Web.Models;

namespace CIS.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<UserGroup, UserGroupViewModel>();
            Mapper.CreateMap<UserGroup, UGroup>();
            Mapper.CreateMap<Request, RequestViewModel>();
            Mapper.CreateMap<RequestCategory, RequestCategoryViewModel>();
            Mapper.CreateMap<RequestReport, RequestReportViewModel>();
            Mapper.CreateMap<Role, RoleViewModel>();
            Mapper.CreateMap<Footer, FooterViewModel>();
            Mapper.CreateMap<Menu, MenuViewModel>();
            Mapper.CreateMap<ContactDetail, ContactDetailViewModel>();
            Mapper.CreateMap<Slide, SlideViewModel>();            
            Mapper.CreateMap<Job, JobViewModel>();            
            Mapper.CreateMap<Feedback, FeedbackViewModel>();                          
            Mapper.CreateMap<Applicant, ApplicantViewModel>();                          
            Mapper.CreateMap<InternalDetail, InternalDetailViewModel>();                          
        }
    }
}