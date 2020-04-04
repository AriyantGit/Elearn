using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Learning.Models
{
    public class TutorDetails:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Tutor name.")]
        public string Fname { get; set; }

        public string Lname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MaxLength(10)]
        public string PhoneNo { get; set; }
        [Required]
        public string Password { get; set; }
        //[Required]
        public byte[] Imageurl { get; set; }
        public ICollection<Course> courses { get; set; }

    }
}