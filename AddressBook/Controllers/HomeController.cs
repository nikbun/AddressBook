﻿using AddressBook.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AddressBook.Controllers
{
    public class HomeController : Controller
    {
        AddressContext db = new AddressContext();
        
        public ActionResult Index()
        {
            List<Address> addressList = new List<Address>();
            addressList.Add(new Address());
            foreach (Address adrs in db.AddressesDB)
            {
                addressList.Add(adrs);
            }
            return View(addressList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAdresses(string country, string city, string street, string houseNumber)
        {
            List<Address> addressList = new List<Address>();
            Address address = new Address();
            address.Country = country;
            address.City = city;
            address.Street = street;
            address.HouseNumber = int.Parse(houseNumber);
            address.Date = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                bool isExist = false;
                addressList.Add(new Address());
                foreach (Address adrs in db.AddressesDB)
                {
                    addressList.Add(adrs);
                    if (adrs.Country == address.Country &&
                        adrs.City == address.City &&
                        adrs.Street == address.Street &&
                        adrs.HouseNumber == address.HouseNumber)
                    {
                        isExist = true;
                    }
                }
                if (isExist)
                    return PartialView("ExistValidationView", addressList);

                addressList.Add(address);
                db.AddressesDB.Add(address);
                
                await db.SaveChangesAsync();
                return PartialView(addressList);
            }
            addressList.Add(address);
            foreach (Address adrs in db.AddressesDB)
            {
                addressList.Add(adrs);
            }
            return PartialView("ValidationView", addressList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }    
}