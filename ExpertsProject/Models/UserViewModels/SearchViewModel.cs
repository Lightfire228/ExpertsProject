using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models.UserViewModels {
	public class SearchViewModel {

		public string Name {
			get; set;
		}

		public string Expertise {
			get; set;
		}

		public string Id {
			get; set;
		}
	}
}