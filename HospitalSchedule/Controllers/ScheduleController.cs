﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HospitalSchedule.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult ScheduleCreation()
        {
            return View();
        }
    }
}