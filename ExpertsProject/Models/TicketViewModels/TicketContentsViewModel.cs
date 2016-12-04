using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models.TicketViewModels {
	public class TicketContentsViewModel {

		public TicketContentsViewModel() {
			Deactivate = false;
		}

		public Ticket Ticket {
			get; set;
		}

		public int TicketID {
			get; set;
		}

		public IEnumerable<Message> Messages {
			get; set;
		}

		public string MessageToPost {
			get; set;
		}
		
		public IEnumerable<ApplicationUser> AllExperts {
			get; set;
		}

		public IEnumerable<ApplicationUser> AttachedExperts {
			get; set;
		}

		[Display(Name = "Remove me from Ticket?")]
		public bool Deactivate {
			get; set;
		}

	}

}