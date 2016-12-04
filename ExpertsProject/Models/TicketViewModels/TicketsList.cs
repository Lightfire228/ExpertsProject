using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models.TicketViewModels {
	public class TicketsList {

		public IEnumerable<Ticket> Tickets {
			get; set;	
		}

		public bool IsMe {
			get; set;
		}


	}

}