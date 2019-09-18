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
    }
}
