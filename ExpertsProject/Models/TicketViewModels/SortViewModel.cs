using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models.TicketViewModels {
	public class SortViewModel {

		public SortViewModel() {
			Selection = SortBy.NONE;
		}

		public SortBy Selection {
			get; set;
		} 
		
	}

	public enum SortBy {
		USERNAME,
		POST_DATE,
		LAST_RESPONSE_DATE,
		SUBJECT,
		NONE
	}
}