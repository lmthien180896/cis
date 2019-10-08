﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CIS.Model.Models;
using CIS.Web.Models;

namespace CIS.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        //User
        public static void UpdateUser(this User user, UserViewModel userVm)
        {
            user.ID = userVm.ID;
            user.Username = userVm.Username;
            user.Password = userVm.Password;
            user.Fullname = userVm.Fullname;
            user.Phone = userVm.Phone;
            user.Address = userVm.Address;
            user.GroupID = userVm.GroupID;                       
            user.Status = userVm.Status;
        }


        //Post
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVm)
        {
            postCategory.ID = postCategoryVm.ID;
            postCategory.Name = postCategoryVm.Name;
            postCategory.Description = postCategoryVm.Description;
            postCategory.Alias = postCategoryVm.Alias;
            postCategory.ParentID = postCategoryVm.ParentID;
            postCategory.DisplayOrder = postCategoryVm.DisplayOrder;
            postCategory.Image = postCategoryVm.Image;
            postCategory.HomeFlag = postCategoryVm.HomeFlag;
            
            postCategory.UpdatedDate = postCategoryVm.UpdatedDate;
            postCategory.UpdatedBy = postCategoryVm.UpdatedBy;
            postCategory.MetaKeyword = postCategoryVm.MetaKeyword;
            postCategory.MetaDescription = postCategoryVm.MetaDescription;
            postCategory.Status = postCategoryVm.Status;

        }

        public static void UpdatePost(this Post post, PostViewModel postVm)
        {
            post.ID = postVm.ID;
            post.Name = postVm.Name;
            post.Description = postVm.Description;
            post.Alias = postVm.Alias;
            post.CategoryID = postVm.CategoryID;
            post.Content = postVm.Content;
            post.ReferenceUrl = postVm.ReferenceUrl;
            post.Image = postVm.Image;
            post.HomeFlag = postVm.HomeFlag;
            post.HotFlag = postVm.HotFlag;
            post.ViewCount = postVm.ViewCount;
            post.Tags = postVm.Tags;
            
            post.UpdatedDate = postVm.UpdatedDate;
            post.UpdatedBy = postVm.UpdatedBy;
            post.MetaKeyword = postVm.MetaKeyword;
            post.MetaDescription = postVm.MetaDescription;
            post.Status = postVm.Status;
        }

        //Request
        public static void UpdateRequestCategory(this RequestCategory requestCategory, RequestCategoryViewModel requestCategoryVm)
        {
            requestCategory.ID = requestCategoryVm.ID;
            requestCategory.Name = requestCategoryVm.Name;
            requestCategory.Requests = requestCategoryVm.Requests;
         
            requestCategory.UpdatedDate = requestCategoryVm.UpdatedDate;
            requestCategory.UpdatedBy = requestCategoryVm.UpdatedBy;
            requestCategory.MetaKeyword = requestCategoryVm.MetaKeyword;
            requestCategory.MetaDescription = requestCategoryVm.MetaDescription;
            requestCategory.Status = requestCategoryVm.Status;

        }

        public static void UpdateRequest(this Request request, RequestViewModel requestVm)
        {
            request.ID = requestVm.ID;
            request.CategoryID = requestVm.CategoryID;
            request.SenderName = requestVm.SenderName;
            request.Email = requestVm.Email;
            request.Phone = requestVm.Phone;
            request.Detail = requestVm.Detail;
            request.Place = requestVm.Place;
            request.Files = requestVm.Files;
            request.Code = requestVm.Code;
            request.ClosedDate = requestVm.ClosedDate;
            request.Progress = requestVm.Progress;

            request.CreatedDate = requestVm.CreatedDate;
            request.CreatedBy = requestVm.CreatedBy;
            request.UpdatedDate = requestVm.UpdatedDate;
            request.UpdatedBy = requestVm.UpdatedBy;
            request.MetaKeyword = requestVm.MetaKeyword;
            request.MetaDescription = requestVm.MetaDescription;
            request.Status = requestVm.Status;
        }

        public static void UpdateRequestReport(this RequestReport requestReport, RequestReportViewModel requestReportVm)
        {
            requestReport.ID = requestReportVm.ID;
            requestReport.RequestID = requestReportVm.RequestID;
            requestReport.SupporterID = requestReportVm.SupporterID;
            requestReport.Note = requestReportVm.Note;

            requestReport.UpdatedDate = requestReportVm.UpdatedDate;
            requestReport.UpdatedBy = requestReportVm.UpdatedBy;
            requestReport.MetaKeyword = requestReportVm.MetaKeyword;
            requestReport.MetaDescription = requestReportVm.MetaDescription;
            requestReport.Status = requestReportVm.Status;

        }

        //Footer
        public static void UpdateFooter(this Footer footer, FooterViewModel footerVm)
        {
            footer.ID = footerVm.ID;      
            footer.Name = footerVm.Name;      
            footer.Content = footerVm.Content;            
        }

        //Slide
        public static void UpdateSlide(this Slide slide, SlideViewModel slideVm)
        {
            slide.ID = slideVm.ID;
            slide.Name = slideVm.Name;
            slide.Image = slideVm.Image;
        }

        //Job
        public static void UpdateJob(this Job job, JobViewModel jobVm)
        {
            job.ID = jobVm.ID;
            job.Name = jobVm.Name;
            job.Description = jobVm.Description;           
            job.Status = jobVm.Status;
        }

        //Feedback
        public static void UpdateFeedback(this Feedback feedback, FeedbackViewModel feedbackVm)
        {
            feedback.ID = feedbackVm.ID;
            feedback.Name = feedbackVm.Name;
            feedback.Email = feedbackVm.Email;
            feedback.Phone = feedbackVm.Phone;
            feedback.Unit = feedbackVm.Unit;
            feedback.Message = feedbackVm.Message;          
            feedback.Status = feedbackVm.Status;
        }

        //Applicant
        public static void UpdateApplicant(this Applicant applicant, ApplicantViewModel applicantVm)
        {
            applicant.ID = applicantVm.ID;
            applicant.Fullname = applicantVm.Fullname;
            applicant.Email = applicantVm.Email;
            applicant.Phone = applicantVm.Phone;
            applicant.CreatedDate = applicantVm.CreatedDate;
            applicant.JobID = applicantVm.JobID;            
        }
    }
}