using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models.UserViewModels {
	public class SearchViewModel {

		public string SearchText {
			get; set;
		}

		public IEnumerable<Expert> Experts {
			get; set;
		}
	}
}