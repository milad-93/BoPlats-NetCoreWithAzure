using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoPlats.Models
{
    public class ApplicationDisplayViewModel
    {
        
        public IList<Apply> Applications { get; set; } // list of applications
       

        public SelectList ApartmentAdress { get; set; } // List of Listed Avaiable apartments

        public string ApartmentAdressSearch { get; set; } // manual search

        public string SearchString { get; set; } //search string

      
    }
}
