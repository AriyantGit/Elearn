using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Learning.Models
{
    public class BaseEntity
    {
        public DateTime? DateCreated { get;
            set; }
       public DateTime? UserModified
        {
            get;
            set;
        }
    }
}