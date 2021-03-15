using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ReviewKaro.Controllers
{
    [Authorize(Roles ="SuperUser")]
    public class HomeController : Controller
    {
        private ReviewEntities context = new ReviewEntities();
        public ActionResult Index()
        {
            ViewBag.Active = "Active";
            ViewBag.Inactive = "Inactive";
            return View(context.Organizations.Include(c => c.Country).Include(s => s.State).Include(c => c.City).ToList());
           
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Countries = new SelectList(context.Countries, "CountryId", "CountryName");
            ViewBag.States = new SelectList(context.States, "StateId", "StateName");
            ViewBag.Cities = new SelectList(context.Cities, "CityId", "CityName");
            return View();
        }


        [HttpPost]
        public ActionResult Create(Organization organization)
        {

            ViewBag.Countries = new SelectList(context.Countries, "CountryId", "CountryName");
            ViewBag.States = new SelectList(context.States, "StateId", "StateName");
            ViewBag.Cities = new SelectList(context.Cities, "CityId", "CityName");
            organization.Role = "Admin";
            if (ModelState.IsValid)
            {
                context.Organizations.Add(organization);
                context.SaveChanges();
                TempData["CreateOrganization"]="Organization Added Successfully";
                return RedirectToAction("Edit", new { id = organization.Id });
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Countries = new SelectList(context.Countries, "CountryId", "CountryName");
            ViewBag.States = new SelectList(context.States, "StateId", "StateName");
            ViewBag.Cities = new SelectList(context.Cities, "CityId", "CityName");

            ViewBag.CreateOrganization = TempData["CreateOrganization"];
            ViewBag.EditOrganization = TempData["EditOrganization"];
            var organization = context.Organizations.Single(o => o.Id == id);
            return View(organization);
        }

        [HttpPost]
        public ActionResult Edit(Organization organization)
        {
            if (ModelState.IsValid)
            {
                context.Entry(organization).State = EntityState.Modified;
                context.SaveChanges();
                TempData["EditOrganization"] = "Details Updated Successfully";
                return RedirectToAction("Edit", new { id = organization.Id });
            }
            else
            {
                return View();
            }

        }
        public ActionResult Delete(int id)
        {
            var organization = context.Organizations.Single(o => o.Id == id);
            organization.Status = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public ActionResult GetStateList(int id)
        {
            var states = context.States.Where(s => s.CountryId == id).ToList();
            return PartialView("_GetStateList", states);
        }
        [AllowAnonymous]
        public ActionResult GetCityList(int id)
        {
            var cities = context.Cities.Where(s => s.StateId == id).ToList();
            return PartialView("GetCityList", cities);
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        public JsonResult IsUserNameAvailable(string UserId)
        {
            return Json(!(context.Organizations.Any(user => user.UserId == UserId)|| context.Employees.Any(u => u.Username == UserId)), JsonRequestBehavior.AllowGet);
        }

    }
}