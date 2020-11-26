using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YarnsAndMobile.Models;

namespace YarnsAndMobile.Data
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<Member> userManager, string userId = null)
        {
            if (userManager.FindByEmailAsync("jb@yarnsandmobile.com").Result == null)
            {
                var user = new Member
                {
                    UserName = "jb@yarnsandmobile.com",
                    Email = "jb@yarnsandmobile.com",
                    AccountNumber = "FLSO00000001",
                    FirstName = "Jester",
                    LastName = "Bozo",
                    Id = "75c82e22-c4fc-4de8-87e5-da3c964a88a6"
                };

                IdentityResult result = userManager.CreateAsync(user, "Dwindle!2020").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}
