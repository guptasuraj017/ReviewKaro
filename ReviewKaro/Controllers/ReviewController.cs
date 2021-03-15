using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ReviewKaro.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReviewController : Controller
    {

        private ReviewEntities context = new ReviewEntities();
        public ActionResult Index()
        {
            

            int organizationId = (int)TempData["organizationId"];
            TempData.Keep("organizationId");

          
            ViewBag.Name = context.Organizations.SingleOrDefault(x => x.Id == organizationId).Name;
            ViewBag.organizationId = organizationId;

            var review = context.Reviews.Where(r => r.OrganizationId == organizationId);
            return View(review.ToList());


        }

        [HttpGet]
        public ActionResult CreateReview()
        {


            int organizationId = (int)TempData["organizationId"];
            ViewBag.Name = context.Organizations.SingleOrDefault(x => x.Id == organizationId).Name;
            TempData.Keep("organizationId");
            return View();
        }

        [HttpPost]
        
        public ActionResult CreateReview(Review review)
        {
            int organizationId = (int)TempData["organizationId"];
            review.OrganizationId = organizationId;
            TempData.Keep("organizationId");
              
            if (ModelState.IsValid)
                {
                    context.Reviews.Add(review);
                    context.SaveChanges();
                    TempData["CreateReview"] = "Review Added Successfully";
                    return RedirectToAction("Edit", "Review", new { id = review.Id });
                }
             return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            int organizationId = (int)TempData["organizationId"];
            ViewBag.Name = context.Organizations.SingleOrDefault(x => x.Id == organizationId).Name;
            TempData.Keep("organizationId");
            ViewBag.CreateReview = TempData["CreateReview"];
            ViewBag.EditReview = TempData["EditReview"];
            var review = context.Reviews.Single(x => x.Id == id);
            return View(review);
        }

        [HttpPost]
        public ActionResult Edit(Review review)
        {
            TempData.Keep("organizationId");

            if (ModelState.IsValid)
            {
                context.Entry(review).State = EntityState.Modified;
               
                context.SaveChanges();
                TempData["EditReview"] = "Details Updated Successfully";
                return View(review);
            }
            return View(review);
        }

        public ActionResult Delete(int id)
        {
            TempData.Keep("organizationId");
            var review = context.Reviews.Single(x => x.Id == id);
            context.Reviews.Remove(review);
            context.SaveChanges();
            return RedirectToAction("Index", "Review");
        }


        [HttpGet]
        public ActionResult AssignReview(long reviewId)
        {
            var organizationId = (int)TempData["organizationId"];

           
            ViewBag.Name = context.Organizations.SingleOrDefault(x => x.Id == organizationId).Name;

            var review = context.Reviews.Single(x => x.Id == reviewId);
            ViewBag.Agenda = review.Agenda;
            ViewBag.CycleStartDate = review.ReviewCycleStartDate;
            ViewBag.CycleEndDate = review.ReviewCycleEndDate;

            ViewBag.EmployeeId = new MultiSelectList(context.Employees.Where(x => x.OrganizationId == organizationId), "Id", "FirstName");
            ViewBag.Reviewer = new MultiSelectList(context.Employees.Where(x => x.OrganizationId == organizationId), "Id", "FirstName");


            TempData.Keep("organizationId");
            TempData["reviewId"] = reviewId;
            return View();
        }

        [HttpPost]
        public JsonResult AssignReview(AssignedReviewDetail assignedReviewDetail)
        {
            ViewBag.EmployeeId = new MultiSelectList(context.Employees.Where(x => x.OrganizationId == assignedReviewDetail.OrganizationId), "Id", "FirstName");
            ViewBag.Reviewer = new MultiSelectList(context.Employees.Where(x => x.OrganizationId == assignedReviewDetail.OrganizationId), "Id", "FirstName");

            TempData.Keep("organizationId");
            TempData.Keep("reviewId");

            assignedReviewDetail.ReviewId = Convert.ToInt64(TempData["reviewId"]);
            assignedReviewDetail.OrganizationId = (int)TempData["organizationId"];
            assignedReviewDetail.Status = true;
            if (!context.AssignedReviewDetails.Any(x => x.ReviewId == assignedReviewDetail.ReviewId
                                                && x.EmployeeId == assignedReviewDetail.EmployeeId
                                                && x.Reviewer == assignedReviewDetail.Reviewer))
            {
                if (ModelState.IsValid)
                {
                    context.AssignedReviewDetails.Add(assignedReviewDetail);
                    context.SaveChanges();
                    ViewBag.AssignReviewMessage = "Review Assigned Successfully";

                }
            }
                


            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAssignedReviews(int id)
        {
            int organizationId = (int)TempData["organizationId"];
            
            ViewBag.Name = context.Organizations.SingleOrDefault(x => x.Id == organizationId).Name;

            TempData.Keep("organizationId");
            var assignedreviews = context.AssignedReviewDetails
                .Include(a => a.Employee)
                .Include(a => a.Employee1)
                .Include(a => a.Review).Where(x => x.OrganizationId == organizationId && x.ReviewId==id && x.Status==true);

            return View(assignedreviews);
        }

        public ActionResult GetSubmittedReviews(int id)
        {
            int organizationId = (int)TempData["organizationId"];

           
            ViewBag.Name = context.Organizations.SingleOrDefault(x => x.Id == organizationId).Name;
            TempData.Keep("organizationId");



            var submittedreviews = context.ReviewSubmissions
                                   .Include(s => s.Employee)
                                   .Include(s => s.Review)
                                   .Where(x => x.OrganizationId == organizationId && x.ReviewId==id);

            return View(submittedreviews);
        }

      


    }
}