using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Learning.Models
{
    public class Coupon:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [Range(1, 100)]
        public int Discount { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Coupon Code Till Date")]
        public DateTime Validity { get; set; }
        [DefaultValue("No")]
        public string Disable { get; set; }
        public ICollection<StudentCourseRegistration> studentCourseRegistrations { get; set; }
    }
}