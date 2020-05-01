using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning.Models
{
    public class TotalLike
    {
        public string courname { get; set; }
        public virtual Course Course { get; set; }

        public virtual TopicList TopicList { get; set; }
        public int totalLike { get; set; }
    }
}