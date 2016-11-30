using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExpertsProject.Models
{
    public class ExpertRegistryViewModel
    {
        public RegisterViewModel DefaultModel
        {
            get; set;
        }

        public string ExpertiseCatagory
        {
            get; set;
        }

        public string Keywords
        {
            get; set;
        }
    }
}