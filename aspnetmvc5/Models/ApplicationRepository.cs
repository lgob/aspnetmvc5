using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspnetmvc5.Models
{
    public class ApplicationRepository
    {
        public static ApplicationUser GetUser(string id) {

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);

            return user;
        }

        public static string GetUserName(string id)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(ApplicationDbContext.Create()));
            ApplicationUser user = userManager.FindById(id);

            return user.UserName;
        }

       


    }
}