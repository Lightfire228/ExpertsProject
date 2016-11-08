using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models {
	public class AttachedUsers {

		[Key][Column(Order = 1)]
		[ForeignKey("User")]
		public string UserID {
			get; set;
		}

		[Key][Column(Order = 2)]
		[ForeignKey("Ticket")]
		public int TicketID {
			get; set;
		}

		public virtual ApplicationUser User {
			get; set;
		}

		public virtual Ticket Ticket {
			get; set;
		}
	}
}