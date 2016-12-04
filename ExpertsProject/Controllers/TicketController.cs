using ExpertsProject.Models;
using ExpertsProject.Models.TicketViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExpertsProject.Controllers
{
    public class TicketController : Controller
    {
		private ApplicationDbContext _dbContext = new ApplicationDbContext();

		// GET: Ticket
		public ActionResult Index() {

			if (!isLoggedIn()) {
				return RedirectToAction("Login", "Account");
			}
			else if (isExpert()) {
				return RedirectToAction("ExpertIndex");
			}
			
            return RedirectToAction("UserIndex");
        }

		public ActionResult UserIndex() {

			if (!isLoggedIn()) {
				return RedirectToAction("Login", "Account");
			}

			IEnumerable<Ticket> modelTickets;
			IEnumerable<Ticket> tickets = _dbContext.Ticket.ToList();

			ApplicationUser user = _dbContext.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());

			modelTickets = from ticket in tickets
						   where ticket.CreatedBy == user.Id
						   select ticket;

			TicketsList model = new TicketsList();
			model.Tickets = modelTickets;
			model.IsMe = true;

			return View(model);
		}

		[HttpGet]
		public ActionResult New() {
		
			if (!isLoggedIn())
				return RedirectToAction("Login", "Account");

			return View();
		}

		[HttpPost]
		public ActionResult New(NewTicketViewModel model) {

			if (!isLoggedIn())
				return RedirectToAction("Oops");

			Ticket ticket        = new Ticket();
			Message message      = new Message();
			ApplicationUser user = _dbContext.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());

			ticket.Title = model.Title;
			ticket.Status = TicketStatus.OPEN;
			ticket.User = user;
			ticket.Created = System.DateTime.Now;

			message.BodyText = model.BodyText;
			message.Date = ticket.Created;
			message.Ticket = ticket;
			message.User = user;

			_dbContext.Ticket.Add(ticket);
			_dbContext.Messages.Add(message);
			_dbContext.SaveChanges();

			return RedirectToAction("UserIndex");
		}

		public ActionResult ExpertIndex() {

			return View();
		}
		
		public bool isExpert(ApplicationUser user) {
			return _dbContext.Experts.Find(user.Id) != null;
		}

		public bool isExpert() {
			String Id = System.Web.HttpContext.Current.User.Identity.GetUserId();
			return isExpert(_dbContext.Users.Find(Id));
		}

		public bool isLoggedIn() {
			return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
		}
    }
}