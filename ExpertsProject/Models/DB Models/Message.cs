using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models {
	public class Message {
	/*
	 *	FOR SOME FLIPPING REASON UNBEKNOWNST TO MANKIND,
	 *	THE IDENTITY PROPERTY WAS NOT SET FOR 'ID'.
	 *	THIS MEANS THAT WHEN A MESSAGE WAS POSTED, IT WOULD
	 *	USE THE SAME KEY VALUE BECAUSE IT WASN'T BEING GENERATED.
	 *	THIS MEANS THAT THE DATABASE INSERT WOULD FAIL BECAUSE IT 
	 *	VIOLATED THE PRINCIPLE OF UNIQUE IDS
	 *	
	 *	TO FIX THIS I HAD TO GO TO THE TABLE PROPERTIES THEMSELVES
	 *	AND EDIT THE KEY VALUE DIRECTLY AND SET IT TO 'IDENTITY = TRUE'
	 *	
	 *	TWO HOURS TO FIGURE THAT OUT
	 *		
	 *	ADLKFHBGASDLFHBGASLJDKFHGAS;LDIFHVGL;KSDNFGV';ASDKNFGADGAS'FDG'
	 *	AFDG
	 *	OANDFG'OADNFG'LADKNFG
	 *	A;FDGHFDNG';SAEFDJGKSHDFG;LJSDFKLGSAD
	 *	
	 */

		[Key][Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Not necessary but for reasons above, don't want to bother
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

		[Required]
		[ForeignKey("Ticket")]
		public int IsPartOf {
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