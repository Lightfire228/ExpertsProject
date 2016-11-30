using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models {
	public class Expert {
		
		[Key]
		[ForeignKey("User")]
		public string Id {
			get; set;
		}

		public bool Validated {
			get; set;
		}

		public string ExpertiseCatagory {
			get; set;
		}

		public string Keywords {
			get; set;
		}

		public virtual ApplicationUser User { 
			get; set;
		}

	}
}