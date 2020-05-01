using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_Learning.Models
{
    public class Course:BaseEntity
    {[Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Course Name")]
        public string Cname { get; set; }
        [Required]
        [Display(Name = "Course Level")]
        public Clevel Clevel { get; set; }
        [Required]
        public bool Version { get; set; }
        [Display(Name = "Course Fee")]
        public int Fee { get; set; }
        
        public int TutorId { get; set; }
        [ForeignKey("TutorId")]
        public  virtual TutorDetails TutorDetail { get; set; }
        public ICollection<TopicList> TopicList { get; set; }
        public ICollection<QuestionSet> QuestionSets { get; set; }

        public ICollection<StudentCourseRegistration> StudentCourseRegistrations { get; set; }
        public ICollection<CartDetail> CartDetail { get; set; }
        public ICollection<ExamCheck> ExamChecks { get; set; }
    }
    public enum Clevel
    {
        Beginer,
        Ammature,
        Expert,
        Others
    }
}