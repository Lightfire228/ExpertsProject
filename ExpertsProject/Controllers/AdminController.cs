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
        public ActionResult Verify(String id)
        {
            var expert = _dbContext.Experts.SingleOrDefault(v => v.Id == id);

            if (expert == null)
                return HttpNotFound();

            return View(expert);
        }
    }
}