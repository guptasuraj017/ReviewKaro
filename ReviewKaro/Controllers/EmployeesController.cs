using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ReviewKaro;


namespace ReviewKaro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {
        private ReviewEntities db = new ReviewEntities();
        
        
        public ActionResult Index()
        {

            int organizationId = (int)TempData["organizationId"];
            ViewBag.organizationId = organizationId;
            TempData.Keep("organizationId");

            ViewBag.Name = db.Organizations.SingleOrDefault(x => x.Id == organizationId).Name;

            var employees = db.Employees.Include(e => e.City).Include(e => e.Country).Include(e => e.EmployeeType).Include(e => e.Gender).Include(e => e.Organization).Include(e => e.State).Where(x=>x.OrganizationId== organizationId);
            return View(employees.ToList());
        }

       
        

       

       
        public ActionResult Create()
        {
            TempData.Keep("organizationId");
            int organizationId = (int)TempData["organizationId"];
            ViewBag.Name = db.Organizations.SingleOrDefault(x => x.Id == organizationId).Name;

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName");
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName");
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "Id", "EmployeeType1");
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1");
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            
            employee.OrganizationId = (int)TempData["organizationId"];

            
            employee.Role = "User";
            
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                
                db.SaveChanges();
                TempData["CreateMessage"] = "Employee Added Successfully";
                return RedirectToAction("Edit","Employees",new {id= employee.Id });
            }

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", employee.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", employee.CountryId);
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "Id", "EmployeeType1", employee.EmployeeTypeId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1", employee.GenderId);
           
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", employee.StateId);
            return View(employee);
        }

       
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            int organizationId = (int)TempData["organizationId"];
            TempData.Keep("organizationId");
            ViewBag.CreateMessage = TempData["CreateMessage"];
            ViewBag.EditEmployee = TempData["EditEmployee"];
            ViewBag.Name = db.Organizations.SingleOrDefault(x => x.Id == organizationId).Name;

            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", employee.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", employee.CountryId);
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "Id", "EmployeeType1", employee.EmployeeTypeId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1", employee.GenderId);
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", employee.StateId);
  

            return View(employee);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            TempData.Keep("organizationId");
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                TempData["EditEmployee"] = "Details Updated Successfully";
                return RedirectToAction("Edit","Employees",new { id=employee.Id});
            }
            ViewBag.CityId = new SelectList(db.Cities, "CityId", "CityName", employee.CityId);
            ViewBag.CountryId = new SelectList(db.Countries, "CountryId", "CountryName", employee.CountryId);
            ViewBag.EmployeeTypeId = new SelectList(db.EmployeeTypes, "Id", "EmployeeType1", employee.EmployeeTypeId);
            ViewBag.GenderId = new SelectList(db.Genders, "Id", "Gender1", employee.GenderId);
           
            ViewBag.StateId = new SelectList(db.States, "StateId", "StateName", employee.StateId);
            return View(employee);
        }

      
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            employee.Status = false;
            db.SaveChanges();
            if (employee == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index","Employees");
        }
        
        public JsonResult IsUserNameAvailable(string Username)
        {
            return Json(!(db.Employees.Any(user => user.Username == Username ||db.Organizations.Any(u=>u.UserId==Username))), JsonRequestBehavior.AllowGet);
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
