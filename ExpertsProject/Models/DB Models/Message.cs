using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models {
	public class Message {

		[Key][Column(Order = 1)]
		public int ID {
			get; set;
		}

		// The sole perpose of this is to ensure a message
		// has a user
		[Key][Column(Order = 2)]
		[ForeignKey("User")]
		public string UserID {
			get; set;	
		}

		public string BodyText {
			get; set;
		}

		public DateTime Date {
			get; set;
		}

		public Ticket IsPartOf {
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