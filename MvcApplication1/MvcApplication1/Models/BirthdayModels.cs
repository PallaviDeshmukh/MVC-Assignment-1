using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Birthday.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel; 

namespace MvcApplication1.Models
{
    public class BirthdayModels
    {
        public int UserID { get; set; }
        [DisplayName("Name")]
        [StringLength (20)]
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(8)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? Birthdate { get; set; }
        

    }
}