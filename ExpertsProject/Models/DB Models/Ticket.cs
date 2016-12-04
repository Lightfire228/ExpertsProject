using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models {
	public class Ticket {

		public int ID {
			get; set;
		}

		public string Title {
			get; set;
		}

		public DateTime Created {
			get; set;
		}

		public TicketStatus Status {
			get; set;
		}

		[ForeignKey("User")]
		public string CreatedBy {
			get; set;
		}

		public virtual ApplicationUser User {
			get; set;
		}
	}

	public enum TicketStatus {
		OPEN,
		CLOSED,
	}
}