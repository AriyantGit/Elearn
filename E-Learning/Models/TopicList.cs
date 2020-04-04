using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Learning.Models
{
    public class TopicList
    {
        public int Id { get; set; }
        [Required]
        public string TopicName { get; set; }
        [Required]
        public string Description { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
        [Required]
        public string VideoPath { get; set; }
        public byte[] PdfContent { get; set; }
        //public ICollection<QuestionSet> questionSets { get; set; }
        public ICollection<StudentCourseRegistration> StudentCourseRegistrations { get; set; }

    }
}