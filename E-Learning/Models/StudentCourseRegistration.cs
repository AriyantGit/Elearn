using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Learning.Models
{
    public class StudentCourseRegistration : BaseEntity
    {

        [Key]
        public int StudentCourseRegistrationId { get; set; }
        [ForeignKey("Student")]
        [Required]
        public int StudentId { get; set; }
        public virtual Student Student{get; set;}
        [ForeignKey("Course")]
        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        [ForeignKey("TopicList")]
        
        public int? TopicListId { get; set; }
        public virtual TopicList TopicList { get; set; }



    }
}