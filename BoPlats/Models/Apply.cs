using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoPlats.Models
{
    public class Apply
    {
        [DisplayName("Case Number")]
        public int Id { get; set; }

        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Lastname")]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Phonenumber")]
        public string PhoneNumber { get; set; }
        [Required]
        [DisplayName("Email")]
        public string email { get; set; }
        [Required]
        [DisplayName("Salary")]
        public string Salary { get; set; }
        [Required]
        [DisplayName("Social security number")]
        public string socSecNum { get; set; }




        //Relation to apartments
        [Required]
        [DisplayName("[Apartment-ID]")]
        public  int ApartmentForeginKey { get; set; }       
        public Apartment Apartment { get; set; }

    }
}
