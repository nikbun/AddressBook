﻿using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AddressBook.Controllers
{
    public class HomeController : Controller
    {
        AddressContext db = new AddressContext();
        
        public ActionResult Index()
        {
            return View(db.AddressesDB);
        }
    }
}