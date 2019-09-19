namespace CIS.Data.Migrations
{
    using CIS.Common;
    using CIS.Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CIS.Data.CISDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CIS.Data.CISDbContext context)
        {
            CreateUserGroupSample(context);
            CreateUserSample(context);           
            CreatePostCategorySample(context);           
            CreatePostSample(context);           
        }

        private void CreateUserGroupSample(CISDbContext context)
        {
            if (context.UserGroups.Count() == 0)
            {
                List<UserGroup> listUserGroup = new List<UserGroup>()
            {
                new UserGroup() { Name = "admin", DisplayName = "Quản trị"  },
                new UserGroup() { Name = "intmod", DisplayName = "Quản trị mạng"  },
                new UserGroup() { Name = "webmod", DisplayName = "Quản trị web"  },
                new UserGroup() { Name = "member", DisplayName = "Thành viên"  }
            };
                context.UserGroups.AddRange(listUserGroup);
                context.SaveChanges();
            }

        }

        private void CreateUserSample(CISDbContext context)
        {
            if (context.Users.Count() == 0)
            {
                List<User> listUser = new List<User>()
            {
                new User() { Username="lmthien",Password="123456", GroupID=CommonConstant.AdminId, Status=true }
            };
                context.Users.AddRange(listUser);
                context.SaveChanges();
            }

        }

        private void CreatePostCategorySample(CISDbContext context)
        {
            if (context.PostCategories.Count() == 0)
            {
                List<PostCategory> listPostCategory = new List<PostCategory>()
            {
                new PostCategory() { Name="tin tức", Alias="tin-tuc", Status=true},
                new PostCategory() { Name="thông báo", Alias="thong-bao", Status=false},                
            };
                context.PostCategories.AddRange(listPostCategory);
                context.SaveChanges();
            }

        }

        private void CreatePostSample(CISDbContext context)
        {
            if (context.Posts.Count() == 0)
            {
                List<Post> listPost = new List<Post>()
            {
                new Post() { Name="tin tức 1", Alias="tin-tuc-1", CategoryID=1, Status=true},
                new Post() { Name="tin tức 2", Alias="tin-tuc-2", CategoryID=1, Status=false},
            };
                context.Posts.AddRange(listPost);
                context.SaveChanges();
            }

        }
    }
}
