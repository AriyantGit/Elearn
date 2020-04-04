using E_Learning.Models;
using E_Learning.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Learning.Controllers
{
    public class StuHomeController : Controller
    {
        _IAllRepository<TopicList> obj;
        _IAllRepository<Course> courseobj;
        _IAllRepository<StudentCourseRegistration> regobj;
        _IAllRepository<QuestionSet> quesobj;
        _IAllRepository<AnswerDetails> ansrobj;
        public StuHomeController()
        {
            this.obj = new AllRepository<TopicList>();
            this.courseobj = new AllRepository<Course>();
            this.regobj = new AllRepository<StudentCourseRegistration>();
            this.quesobj = new AllRepository<QuestionSet>();
            this.ansrobj = new AllRepository<AnswerDetails>();
        }
        // GET: StuHome
        public ActionResult Index()
        {
            return View();
        }

        // GET: StuHome/Details/5
        public ActionResult Mycourse()
        {
            return View(regobj.GetAll().Where(x=>x.StudentId==1).ToList());
        }

        // GET: StuHome/Create
        public ActionResult SearchCourse()
        {
            ViewBag.TopicId = new SelectList(courseobj.GetAll().ToList());
            return View(courseobj.GetAll().ToList()); ;
        }
        public ActionResult ListCourse()
        {
            return PartialView(courseobj.GetAll().ToList());
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
        public ActionResult Registration(int id)
        {
            return View(courseobj.GetModelById(id));
        }
        [HttpPost]
        public ActionResult Registration(Course course,int id)
        {
                StudentCourseRegistration st = new StudentCourseRegistration();
                st.CourseId = id;
                st.StudentId = 6;

                regobj.InsertModel(st);
                regobj.Save();
                return RedirectToAction("Index");
            
        }
        public ActionResult MyExam()
        {
           //var b= (from c in ansrobj.GetAll()
           //  select c.CourseId).Distinct();
            var b = ansrobj.GetAll().Distinct().ToList(); // Distinct(x=>x);
            ViewData["ansr"] = b;
            var a = regobj.GetAll().Where(x => x.StudentId == 1).ToList(); 
   
            return View(a);
        }

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
