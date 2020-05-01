using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_Learning.Models
{
    public class Student:BaseEntity
    {
        [Key]
        public int StudentId { get; set; }
        [Required]
        public string FName { get; set; }
        public string LName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Mobile { get; set; }
        public byte[] Image { get; set; }
        
        public string Password { get; set; }

        public ICollection<StudentCourseRegistration> StudentCourseRegistrations { get; set; }
        public ICollection<TopicLike> TopicLikes { get; set; }

        public ICollection<AnswerDetails> AnswerDetails { get; set; }
        public ICollection<CartDetail> CartDetails { get; set; }
        [DefaultValue("No")]
        public string Disable { get; set; }
        public ICollection<ExamCheck> ExamChecks { get; set; }
        
        [DefaultValue("Student")]
        public string Role { get; set; }

    }
}