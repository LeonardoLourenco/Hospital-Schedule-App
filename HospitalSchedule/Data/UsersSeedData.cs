using HospitalSchedule.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalSchedule.Data
{
    public class UsersSeedData
    {
        // Criar Utilizadores (exemplo Admin)
        public static async Task EnsurePopulatedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string adminName = "admin@admin.pt";
            const string adminPass = "Secret123$";

            const string nurseName = "asd@admin.pt";
            const string nursePass = adminPass;

          
            if (!await roleManager.RoleExistsAsync("Administrador"))
            {
                await roleManager.CreateAsync(new IdentityRole("Administrador"));
            }


            //----------------------------------------------------------------------------------------------------------------------------------------------------

            if (!await roleManager.RoleExistsAsync("nurse"))
            {
                await roleManager.CreateAsync(new IdentityRole("nurse"));
            }

           


            ApplicationUser admin = await userManager.FindByNameAsync(adminName);

            if (admin == null)
            {
                admin = new ApplicationUser { UserName = adminName };
                await userManager.CreateAsync(admin, adminPass);
            }

            if (!await userManager.IsInRoleAsync(admin, "Administrator"))
            {
                await userManager.AddToRoleAsync(admin, "Administrator");
            }

           


            ApplicationUser nurse = await userManager.FindByNameAsync(nurseName);
            if (nurse == null)
            {
                nurse = new ApplicationUser { UserName = nurseName };
                await userManager.CreateAsync(nurse, nursePass);
            }

            if (!await userManager.IsInRoleAsync(nurse, "nurse"))
            {
                await userManager.AddToRoleAsync(nurse, "nurse");
            }


           
        }
    }
}
