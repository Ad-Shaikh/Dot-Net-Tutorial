﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeForm.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string submit)
        {
            ViewBag.Message = "Employee Deleted Succesfully";
            return View();
        }
    }
}
