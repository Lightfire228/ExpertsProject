using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models.UserViewModels {
	public class SearchStringViewModel {

		[Display(Name = "Search Keywords")]
		public string SearchText {
			get; set;
		}
	}
}