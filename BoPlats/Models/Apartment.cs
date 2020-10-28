using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BoPlats.Models
{
    public class Apartment
    {
        [DisplayName("[Apartment ID]")]
        public int Id { get; set; }

        [Required,StringLength(50),DisplayName("Adress")]    
        public string Adress { get; set; }
                
        [DisplayName("Elevator")]
        public bool Elevator { get; set; }
      
        [StringLength(10)]
        [Required]
        [DisplayName("Rooms")]
        public string NumberOfRooms { get; set; }
       
        [DisplayName("Balcony")]
        public bool Balcony { get; set; }
       
        [Required]
        [DisplayName("Squaremeter")]
        public string SquareMeter { get; set; }
        [Required]
        [DisplayName("Floor")]
        public string Floor { get; set; }

        [Required]
        [DisplayName("Monthly Cost")]
        public string Rent { get; set; }
        
      //time 
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        private DateTime _date = DateTime.Now;

        [DisplayName("Time/Date")]
        [DataType(DataType.DateTime)]
        public DateTime RegisterTime
        {                          
            get { return _date; }
            set { _date = value; }
        }

        
        //relation

        public IList<Apply> Apply { get; set; }

    }
}
