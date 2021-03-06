﻿using ExpertsProject.Models;
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
				return RedirectToAction("Expert");
			}
			
            return RedirectToAction("UserIndex");
        }

		public ActionResult UserIndex() {

			if (!isLoggedIn()) {
				return RedirectToAction("Login", "Account");
			}

			IEnumerable<Ticket> modelTickets;
			IEnumerable<Ticket> tickets = _dbContext.Ticket.ToList();

			ApplicationUser user = getUser();

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

			if (!getUser().ActiveStatus)
				return View("Deactivated");


			return View();
		}

		[HttpPost]
		public ActionResult New(NewTicketViewModel model) {

			if (!isLoggedIn())
				return RedirectToAction("Oops");

			if (!getUser().ActiveStatus)
				return View("Deactivated");

			Ticket ticket        = new Ticket();
			Message message      = new Message();
			ApplicationUser user = getUser();

			ticket.Title = model.Title;
			ticket.Status = TicketStatus.OPEN;
			ticket.User = user;
			ticket.Created = System.DateTime.Now;

			message.BodyText = model.BodyText;
			message.Date = ticket.Created;
			message.Ticket = ticket;
			message.User = user;

			_dbContext.Ticket.Add(ticket);
			_dbContext.SaveChanges();
			_dbContext.Messages.Add(message);
			_dbContext.SaveChanges();

			return RedirectToAction("UserIndex");
		}

		public ActionResult ViewTicket(Ticket ticket) {

			if (!isLoggedIn())
				return RedirectToAction("Login", "Account");

			IEnumerable < Message > messages;

			messages = from message in _dbContext.Messages.ToList()
					where message.IsPartOf == ticket.ID
					select message;

			TicketContentsViewModel model = new TicketContentsViewModel();
			model.Messages = messages;
			// Weird stuff happens if you don't do this
			model.Ticket = _dbContext.Ticket.Find(ticket.ID);
			model.TicketID = model.Ticket.ID;

			IEnumerable<Expert> experts;
			IEnumerable<Expert> preAddedExperts;

			experts = from expert in _dbContext.Experts.ToList()
					  where expert.Validated && expert.Id != getUser().Id // So experts don't add themselves
					  select expert;

			preAddedExperts = from expert in experts
							  from attached in _dbContext.AttachedUsers.ToList()
							  where expert.Id == attached.UserID && attached.TicketID ==
							        model.TicketID
							  select expert;
						
			experts = experts.Except(preAddedExperts);

			IEnumerable<ApplicationUser> users = from expert in experts
												 from user in _dbContext.Users.ToList()
												 where user.Id == expert.Id
												 select user;

			IEnumerable<ApplicationUser> preAddedUsers = from expert in preAddedExperts
														 from user in _dbContext.Users.ToList()
														 where user.Id == expert.Id
														 select user;

			model.AllExperts = users;
			model.AttachedExperts = preAddedUsers;

			if (!isTicketCreator(model.Ticket))
				return View("ExpertTicketView", model);

			return View("UserTicketView", model);
		}

		[HttpPost]
		public ActionResult PostResponse(TicketContentsViewModel model) {

			if (!isLoggedIn())
				return RedirectToAction("Oops");

			if (!getUser().ActiveStatus)
				return View("Deactivated");

			Ticket ticket = _dbContext.Ticket.Find(model.TicketID);
			Message message = new Message();

			message.BodyText = model.MessageToPost;
			message.Date = System.DateTime.Now;
			message.User = getUser();
			message.Ticket = ticket;

			if (model.Deactivate) {
				
				message.BodyText = "Expert " + getUser().Name + " has removed themselves from this ticket. Reason: \n" + message.BodyText;
				_dbContext.Messages.Add(message);
				_dbContext.SaveChanges();
	
				AttachedUsers attached = _dbContext.AttachedUsers.Find(getUser().Id, ticket.ID);

				_dbContext.AttachedUsers.Remove(attached);
				_dbContext.SaveChanges();

				return RedirectToAction("Index");

			}

			_dbContext.Messages.Add(message);
			_dbContext.SaveChanges();

			return RedirectToAction("ViewTicket", ticket);
		}

		public ActionResult AttachExperts(AttachViewModel model) {

			if (!isLoggedIn())
				return RedirectToAction("Oops");

			if (!getUser().ActiveStatus)
				return View("Deactivated");

			Ticket ticket = _dbContext.Ticket.Find(model.TicketID);
			ApplicationUser user = _dbContext.Users.Find(model.UserID);

			AttachedUsers users = new AttachedUsers();

			users.User = user;
			users.Ticket = ticket;

			_dbContext.AttachedUsers.Add(users);
			_dbContext.SaveChanges();

			return RedirectToAction("ViewTicket", ticket);
			
		}

		public ActionResult Expert() {

			return RedirectToAction("ExpertIndex", new SortViewModel());

		}

		public ActionResult ExpertIndex(SortViewModel sortModel) {

			if (!isLoggedIn()) {
				return RedirectToAction("Login", "Account");
			}

			if (!isExpert())
				return RedirectToAction("UserIndex");

			IEnumerable<Ticket> modelTickets;
			IEnumerable<Ticket> tickets = _dbContext.Ticket.ToList();

			ApplicationUser user = getUser();

			modelTickets = from ticket in tickets
						   from attached in _dbContext.AttachedUsers.ToList()
						   where ticket.ID == attached.TicketID && user.Id == attached.UserID
						   select ticket;

			TicketsList model = new TicketsList();
			model.Tickets = sort(sortModel, modelTickets);
			model.IsMe = false;

			return View(model);
		}

		public bool isExpert(ApplicationUser user) {
			return _dbContext.Experts.Find(user.Id) != null;
		}

		public bool isExpert() {
			return isExpert(getUser());
		}

		public bool isLoggedIn() {
			return System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
		}

		public ApplicationUser getUser() {
			return _dbContext.Users.Find(System.Web.HttpContext.Current.User.Identity.GetUserId());
		}

		public bool isTicketCreator(Ticket ticket) {
			return ticket.User.Id.Equals(getUser().Id);
		}

		public IEnumerable<Ticket> sort(SortViewModel model, IEnumerable<Ticket> tickets) {

			IEnumerable<Ticket> sorted = tickets;

			switch (model.Selection) {
				case SortBy.LAST_RESPONSE_DATE:
					break;

				case SortBy.POST_DATE:

					sorted = from ticket in tickets
							 orderby ticket.Created
							 select ticket;
					break;

				case SortBy.SUBJECT:

					sorted = from ticket in tickets
							 orderby ticket.Title
							 select ticket;
					break;

				case SortBy.USERNAME:

					sorted = from ticket in tickets
							 orderby ticket.User.Name
							 select ticket;
					break;

				case SortBy.NONE:
					break;

			}

			return sorted;

		}

	}
}