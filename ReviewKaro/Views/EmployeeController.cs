using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ReviewKaro.Models;
using Newtonsoft.Json;

namespace ReviewKaro.Views
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private ReviewEntities context = new ReviewEntities();

        public ActionResult Index()
        {
                var employeeId = Convert.ToInt64(TempData["employeeId"]);
                TempData.Keep("employeeId");
                var FirstName= context.Employees.SingleOrDefault(x => x.Id == employeeId).FirstName;
                var LastName = context.Employees.SingleOrDefault(x => x.Id == employeeId).Last_Name;
                ViewBag.Name = FirstName + " " + LastName;
                ViewBag.ReviewSubmittedMessage = TempData["ReviewSubmittedMessage"];

            return View();
           


        }

        

        

        [HttpGet]
        public ActionResult SubmitReview(long id)
        {
            var employeeId = Convert.ToInt64(TempData["employeeId"]);
            var assignedreview = context.AssignedReviewDetails.Single(d => d.Id == id);
            TempData.Keep("employeeId");
            var FirstName = context.Employees.SingleOrDefault(x => x.Id == employeeId).FirstName;
            var LastName = context.Employees.SingleOrDefault(x => x.Id == employeeId).Last_Name;
            ViewBag.Name = FirstName + " " + LastName;
            ViewBag.Agenda = assignedreview.Review.Agenda;
            ViewBag.MinRate = assignedreview.Review.MinRate;
            ViewBag.MaxRate = assignedreview.Review.MaxRate;
            ViewBag.ReviewCycleEndDate = assignedreview.Review.ReviewCycleEndDate;
            ViewBag.ReviewCycleStartDate = assignedreview.Review.ReviewCycleStartDate;

            ViewBag.EmployeeName = assignedreview.Employee.FirstName + " " + assignedreview.Employee.Last_Name;
            TempData["assignedReviewId"] = assignedreview.Id;
            
            return View();
        }

        [HttpPost]
        public ActionResult SubmitReview(ReviewSubmission reviewSubmission)
        {
            
            TempData.Keep("employeeId");
            var assignedreviewId = Convert.ToInt64(TempData["assignedReviewId"]);
            var assignedreview = context.AssignedReviewDetails.Single(d => d.Id == assignedreviewId);

            var employee = context.Employees.Single(x => x.Id == assignedreview.EmployeeId);

            reviewSubmission.EmployeeId = assignedreview.EmployeeId;
            reviewSubmission.OrganizationId = employee.OrganizationId;
            reviewSubmission.ReviewId = assignedreview.ReviewId;
            reviewSubmission.ReviewerId = Convert.ToInt64(TempData["employeeId"]);

            if (ModelState.IsValid)
            {
                context.ReviewSubmissions.Add(reviewSubmission);
                context.SaveChanges();
                TempData["ReviewSubmittedMessage"] = "Submitted Successfully";

               return  RedirectToAction("DeleteSubmittedAssignedReview",new {id=assignedreviewId });
                
            }
            return View();
        }

        public ActionResult DeleteSubmittedAssignedReview(long id)
        {
            TempData.Keep("employeeId");
            TempData.Keep("ReviewSubmittedMessage");
            var assignedreview = context.AssignedReviewDetails.Single(x => x.Id == id);
            assignedreview.Status = false;
            context.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }

        public ActionResult SubmittedReviewsByReviewer()
        {
            var ReviewerId = Convert.ToInt64(TempData["employeeId"]);
            TempData.Keep("employeeId");
            var submittedReviewsByReviewer = context.ReviewSubmissions.Where(x => x.ReviewerId == ReviewerId).ToList();
            return PartialView("SubmittedReviewsByReviewer",submittedReviewsByReviewer);
        }
        public ActionResult AssignedReviewsToSubmit()
        {
            var ReviewerId = Convert.ToInt64(TempData["employeeId"]);
            TempData.Keep("employeeId");
            var assignedreviewstosubmit = context.AssignedReviewDetails.Where(x => x.Reviewer == ReviewerId && x.Status==true).ToList();
            return PartialView("AssignedReviewsToSubmit", assignedreviewstosubmit);
        }

        public JsonResult RecievedReviewsJson()
        {
            var ReviewerId = Convert.ToInt64(TempData["employeeId"]);
            TempData.Keep("employeeId");
            var result = (from reviewSubmission in context.ReviewSubmissions
                         where reviewSubmission.Employee.Id == ReviewerId
                         group reviewSubmission by reviewSubmission.Review.Agenda into review
                         select new RecievedReview { ReviewAgenda = review.Key, TotalRating = review.Sum(r => r.Rating), MaximumRating = review.Sum(r => r.Review.MaxRate) }).ToList();
            string json = JsonConvert.SerializeObject(result, Formatting.Indented);
            return Json(json, JsonRequestBehavior.AllowGet);

        }

        public ActionResult RecievedReviews()
        {
            var ReviewerId = Convert.ToInt64(TempData["employeeId"]);
            TempData.Keep("employeeId");
            var result = from reviewSubmission in context.ReviewSubmissions
                         where reviewSubmission.Employee.Id == ReviewerId
                         group reviewSubmission by reviewSubmission.Review.Agenda into review
                         select new RecievedReview { ReviewAgenda = review.Key, TotalRating = review.Sum(r => r.Rating), MaximumRating = review.Sum(r => r.Review.MaxRate) };
            return PartialView("RecievedReviews",result);
        }

        
    }
}