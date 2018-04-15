namespace chatService.Migrations
{
    using chatService.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<chatService.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(chatService.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            if (!context.Roles.Any(rol => rol.Name == "Admin"))
            {
                var result = roleManager.Create(new IdentityRole { Name = "Admin" });
            }
            if (!context.Roles.Any(rol => rol.Name == "Member"))
            {
                var result = roleManager.Create(new IdentityRole { Name = "Member" });
            }
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            if (!context.Users.Any(rol => rol.UserName == "jahangir@gmail.com"))
            {
                var user = new ApplicationUser { UserName = "jahangir@gmail.com", PhoneNumber = "1912948563" };
                var result = UserManager.Create(user, "Jahangir@123");
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }
            List<UserAccess> lst = new List<UserAccess>
            { 
                new UserAccess {RoleName = "Admin",ActionName = "Create",ControllerName = "GroupInfoes",MenuItem = "Add New Group"},
                new UserAccess {RoleName = "Admin",ActionName = "Index",ControllerName = "GroupInfoes",MenuItem = "View Group"},
                new UserAccess {RoleName = "Member",ActionName = "Chat",ControllerName = "Home",MenuItem = "Start Chat"},
                new UserAccess {RoleName = "Admin",ActionName = "PendingRequest",ControllerName = "RequestInfoes",MenuItem = "Pending Request"},
            };
            lst.ForEach(s => context.UserAccesss.Add(s));
            context.SaveChanges();
        }
    }
}
