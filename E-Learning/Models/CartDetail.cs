using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Learning.Models
{
    public class CartDetail:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Course")]
        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        [ForeignKey("Student")]
        [Required]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}