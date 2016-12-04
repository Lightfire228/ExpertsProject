using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ExpertsProject.Models;

namespace ExpertsProject.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _dbContext;

        public AdminController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdminDactivate()
        {
            var expert = _dbContext.Experts.ToList();

            return View(expert);
        }
        public ActionResult Verify(String id)
        {
            var expert = _dbContext.Experts.SingleOrDefault(v => v.Id == id);

            if (expert == null)
                return HttpNotFound();

            return View(expert);
        }
        public ActionResult Deactivate(ApplicationUser expert)
        {
            var expertInDb = _dbContext.Users.SingleOrDefault(v => v.Id == expert.Id);

            if (expertInDb == null)
                return HttpNotFound();
            expertInDb.ActiveStatus = expert.ActiveStatus;
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}