﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models {
	public class Expert {
		
		[Key]
		[ForeignKey("User")]
		public string ID {
			get; set;
		}

		public bool Validated {
			get; set;
		}

		public string ExpertiseCatagory {
			get; set;
		}

		[ForeignKey("Keyword")]
		public int KeywordsID {
			get; set;
		}

		public virtual ApplicationUser User { 
			get; set;
		}

		public virtual Keywords Keyword {
			get; set;
		}
	}
}