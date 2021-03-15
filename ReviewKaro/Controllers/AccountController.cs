using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ReviewKaro.Models;

namespace ReviewKaro.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(User user)
        {
            using (var context=new ReviewEntities())
            {
               
                
                    FormsAuthentication.SetAuthCookie(user.Username, false);

                    if(user.Username=="bala123" && user.Password == "Welcome123")
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    else if(context.Organizations.Any(o=>o.UserId==user.Username && o.Password == user.Password))
                    {
                        var organization = context.Organizations.Single(o => o.UserId == user.Username && o.Password == user.Password);
                        
                        TempData["organizationId"] = organization.Id;
                        return RedirectToAction("Index", "Employees");
                    }
                    else if(context.Employees.Any(e=>e.Username==user.Username && e.Password == user.Password))
                    {
                        var employee = context.Employees.Single(e => e.Username == user.Username && e.Password == user.Password);
                        
                        TempData["employeeId"] = employee.Id;
                        return RedirectToAction("Index", "Employee");
                    }
                   
               

                ModelState.AddModelError("", "Invalid username and password");
                return View();
            }
               
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }
    }
}