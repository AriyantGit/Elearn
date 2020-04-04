using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace E_Learning.Models
{
    public class DbModel : DbContext
    {
        public DbModel() : base("conn")
        {


        }
       
  
        /// <summary>  
        /// Overriding Save Changes  
        /// </summary>  
        /// <returns></returns>  
        public override int SaveChanges()  
        {  
            var selectedEntityList = ChangeTracker.Entries()  
                                    .Where(x => x.Entity is BaseEntity &&  
                                    (x.State == EntityState.Added || x.State == EntityState.Modified));

            //Gt user Name from  session or other authentication   
            //var userName = "MUKESH";  
            //selectedEntityList.
            foreach (var entity in selectedEntityList)  
            {

                ((BaseEntity)entity.Entity).UserModified = DateTime.Now;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateCreated = DateTime.Now;
                }
            }  
  
            return base.SaveChanges();  
        }
        public override async Task<int> SaveChangesAsync()
        {
            TrackChanges();
            return await base.SaveChangesAsync();
        }
        private void TrackChanges()
        {
            var selectedEntityList = ChangeTracker.Entries()
                                    .Where(x => x.Entity is BaseEntity &&
                                    (x.State == EntityState.Added || x.State == EntityState.Modified));

            //Gt user Name from  session or other authentication   
            //var userName = "MUKESH";  
            //selectedEntityList.
            foreach (var entity in selectedEntityList)
            {

                ((BaseEntity)entity.Entity).UserModified = DateTime.Now;

                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).DateCreated = DateTime.Now;
                }
            }
        }
        

        public virtual DbSet<TutorDetails> TutorDetails { get; set; }
        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentCourseRegistration> StudentCourseRegistrations { get; set; }

        public System.Data.Entity.DbSet<E_Learning.Models.TopicList> TopicLists { get; set; }

        public System.Data.Entity.DbSet<E_Learning.Models.QuestionSet> QuestionSets { get; set; }

        public System.Data.Entity.DbSet<E_Learning.Models.AnswerDetails> AnswerDetails { get; set; }
    }
}