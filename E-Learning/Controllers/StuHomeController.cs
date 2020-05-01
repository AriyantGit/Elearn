using E_Learning.Filter;
using E_Learning.Models;
using E_Learning.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace E_Learning.Controllers
{
    //[Authorize]
    //[SessionTimeout]

    public class StuHomeController : Controller
    {
        _IAllRepository<TopicList> obj;
        _IAllRepository<Course> courseobj;
        _IAllRepository<StudentCourseRegistration> regobj;
        _IAllRepository<QuestionSet> quesobj;
        _IAllRepository<AnswerDetails> ansrobj;
        _IAllRepository<TopicLike> topiclikeobj;

        public StuHomeController()
        {
            this.obj = new AllRepository<TopicList>();
            this.courseobj = new AllRepository<Course>();
            this.regobj = new AllRepository<StudentCourseRegistration>();
            this.quesobj = new AllRepository<QuestionSet>();
            this.ansrobj = new AllRepository<AnswerDetails>();
            this.topiclikeobj = new AllRepository<TopicLike>();
        }
        // GET: StuHome
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Index()
        {
            return View();
        }

        // GET: StuHome/Details/5
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Mycourse()
        {
            return View(regobj.GetAll().Where(x=>x.StudentId==Convert.ToInt32(Session["StudentId"])).ToList());
        }
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult MycoursePartial()
        {
            return PartialView(regobj.GetAll().Where(x => x.StudentId == Convert.ToInt32(Session["StudentId"])).OrderByDescending(x=>x.DateCreated).Take(3).ToList());
        }

        // GET: StuHome/Create
        [Authorize(Roles = "Tutor,Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult SearchCourse()
        {
            //ViewBag.TopicId = new SelectList(courseobj.GetAll().ToList());
            return View();// courseobj.GetAll().OrderByDescending(x=>x.Id).ToList()); ;
        }
        public ActionResult ListCourse()
        {
            return PartialView(courseobj.GetAll().OrderByDescending(x=>x.Id).Take(5).ToList());
        }
        public ActionResult test()
        {
            return PartialView(courseobj.GetAll().OrderByDescending(x => x.Id).Take(5).ToList());
        }

        public JsonResult CourseAll(string term)
        {

            var students = courseobj.GetAll().Where(s => s.Cname.ToLower()
                            .StartsWith(term.ToLower())).Select(x => new { x.Cname }).Distinct().ToList();

            //var students = courseobj.GetAll().Where(s => s.Cname.ToLower()
            //                .StartsWith(term.ToLower())).Select(x => x.Cname.ToUpper() ).Distinct().ToList();
            return Json(students, JsonRequestBehavior.AllowGet);
            
        }
        public ActionResult LikeCourse()
        {
            var a = (from s in courseobj.GetAll()
                            select new TotalLike
                            {
                                Course = s,
                                totalLike = Convert.ToInt32(obj.GetAll().Where(x => x.CourseId == s.Id).Sum(x => x.Views))
                            }
                     ).Take(5).ToList();
            return PartialView(a);
        }
       // public ActionResult LikeCourse()
       //{
       //     //Model1 db = new Model1();



       //     //var filteredFoos = obj.GetAll().GroupBy(x => x.CourseId)
       //     //           .Select(p => new
       //     //           {
       //     //              courseid= p.Key,
       //     //               views = p.Sum(x => x.Views)
       //     //           }).ToList();

       //     //var a = (from s in courseobj.GetAll()
       //     //         join st in obj.GetAll()
       //     //         on s.Id equals st.CourseId
       //     //         select s
       //     //         ).Take(5).ToList();
            
       //     var result = (from emp in obj.GetAll()
       //                   group emp by emp.CourseId into empTemp
       //                   select new
       //                   {
       //                       Dept = empTemp,
       //                       TotalSalary = empTemp.Sum(e => e.Views)
       //                   }).ToList();
       //     ViewBag.list =result;
       //     List <Course> a=new List<Course>();
       //     foreach (var i in result)
       //     {
       //         a.Add(courseobj.GetAll().Where(x=>x.Id== i.Dept.Key).FirstOrDefault());
                
       //     }

       //     //var innerJoin = (from s in obj.GetAll().Where(x=>x.Views>10) // outer sequence
       //     //                    join st in courseobj.GetAll() //inner sequence 
       //     //                    on s.CourseId equals st.Id // key selector 
       //     //                    select new { 
       //     //                    de=s.Id,
       //     //                    a=s
       //     //                    }).ToList();
       //     return PartialView(a);
       //     //return PartialView(courseobj.GetAll().OrderByDescending(x => x.Id).Take(5).ToList());
       // }
        //Count Total like Topic of each course
        public  int countlike(int id)
        {
            
            var a =  (obj.GetAll().Where(cx => cx.CourseId == id).Sum(x => x.Views));
            return  Convert.ToInt32(a);
        }
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult MyAllliketopic()
        {

            var rslt = topiclikeobj.GetAll().Where(x => x.StudentId == Convert.ToInt32(Session["studentid"])).ToList();
            return View(rslt);
        }
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Myliketopic()
        {
            
            var rslt = topiclikeobj.GetAll().Where(x => x.StudentId == Convert.ToInt32(Session["studentid"])).Take(5).ToList();
            return PartialView(rslt);
        }
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult LastWatchTopicVideo()
        {
           
            var rslt = regobj.GetAll().Where(x => x.StudentId == Convert.ToInt32(Session["studentid"]) && x.TopicListId!=null).OrderByDescending(x => x.UserModified).Select(x => x.TopicListId).Take(1).FirstOrDefault();
            // ViewBag.videourl = obj.GetAll().Where(x => x.CourseId == coursedetails).Select(x => x.VideoPath).FirstOrDefault();
            var rslt1 = obj.GetAll().Where(x=>x.Id==rslt).FirstOrDefault();
            return PartialView(rslt1);
        }
        public ActionResult LastCourseBuy()
        {

            var rslt = regobj.GetAll().Where(x => x.StudentId == Convert.ToInt32(Session["studentid"])).OrderByDescending(x => x.DateCreated).Select(x => x.CourseId).Take(1).FirstOrDefault();
            // ViewBag.videourl = obj.GetAll().Where(x => x.CourseId == coursedetails).Select(x => x.VideoPath).FirstOrDefault();
            var rslt1 = courseobj.GetAll().Where(x => x.Id == rslt).FirstOrDefault();
            return PartialView(rslt1);
        }

        // POST: StuHome/Create
        [HttpPost]
        public ActionResult SearchCourse(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StuHome/Edit/5
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Registration(int id)
        {
            var stuid = Convert.ToInt32(Session["StudentId"]);
            ViewBag.reg = regobj.GetAll().Where(x => x.StudentId == stuid && x.CourseId == id).FirstOrDefault();
            ViewBag.video = obj.GetAll().Where(x => x.CourseId == id).Select(x => x.VideoPath).FirstOrDefault();
            var a = (from s in courseobj.GetAll().Where(x=>x.Id==id)
                     select new TotalLike
                     {
                         Course = s,
                         totalLike = Convert.ToInt32(obj.GetAll().Where(x => x.CourseId == s.Id).Count())
                     }).ToList();
            return View(a);
        }
        [HttpPost]
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Registration(Course course,int id)
        {
                StudentCourseRegistration st = new StudentCourseRegistration();
                st.CourseId = id;
                st.StudentId =Convert.ToInt32(Session["StudentId"]);

                regobj.InsertModel(st);
                regobj.Save();
                return RedirectToAction("Index");
            
        }
        [HttpGet]
        [Authorize(Roles = "Tutor,Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult searchcoursedetials(string searchtext)
        {
            var a = courseobj.GetAll().ToList();
            a = a.Where(x => x.Cname.ToLower().Contains(searchtext.ToLower())).ToList();
            return View(a);
        }
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult MyExam()
        {
           //var b= (from c in ansrobj.GetAll()
           //  select c.CourseId).Distinct();
            var b = ansrobj.GetAll().Where(x=>x.StudentId==Convert.ToInt32(Session["StudentId"])).ToList(); // Distinct(x=>x);
            ViewData["ansr"] = b;
            var a = regobj.GetAll().Where(x => x.StudentId == Convert.ToInt32(Session["StudentId"])).ToList(); 
   
            return View(a);
        }
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult QuestionSet(int id)
        {
            return View(quesobj.GetAll().Where(x=>x.CourseId==id).Distinct().ToList());
        }



        // POST: StuHome/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: StuHome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StuHome/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
