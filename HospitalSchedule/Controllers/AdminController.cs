using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalSchedule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HospitalSchedule.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
            // GET: /<controller>/
            public IActionResult Index()
            {
                return View(userManager.Users);
            }
        }
    }
