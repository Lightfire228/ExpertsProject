using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models.TicketViewModels {
	public class NewTicketViewModel {

		[Display(Name = "Title of Ticket")]
		public string Title {
			get; set;
		}

		[Display(Name = "Description of Ticket")]
		public string BodyText {
			get; set;
		}
	}
}