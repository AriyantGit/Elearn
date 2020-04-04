using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Learning.Models
{
    public class AnswerDetails:BaseEntity
    {
        [Key]
        public int AnswerDetailsId { get; set; }
        public int MarksObtain { get; set; }
        public double percentage { get; set; }
        public int TickAnsr { get; set; }
        [ForeignKey("Student")]
        [Required]
        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        [ForeignKey("Course")]
        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public bool Reshcdule { get; set; }
    }
}