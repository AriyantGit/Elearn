using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Learning.Filter;
using E_Learning.Models;
using E_Learning.Models.DAL;

namespace E_Learning.Controllers
{
    [Authorize]
    //[SessionTimeout]
    public class QuestionController : Controller
    {
        _IAllRepository<QuestionSet> obj;
        _IAllRepository<Course> courseobj;
        public QuestionController()
        {
            this.obj = new AllRepository<QuestionSet>();
            this.courseobj = new AllRepository<Course>();
        }
        //private DbModel db = new DbModel();

        // GET: Question
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Index( )
        {
            var courseid =  Convert.ToInt32(Session["Courseid"]);
           // ViewBag.coursename=
            return View(obj.GetAll().Where(x=>x.CourseId==courseid).ToList());//.Where(x => x.CourseId == courseid).ToList());
        }
        [HttpPost]
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Index(Course course)
        {
            var coursedetails = courseobj.GetAll().Where(x => x.Cname == (course.Cname) && x.Clevel == course.Clevel && x.TutorDetail.Id == Convert.ToInt32(Session["TutorId"])).FirstOrDefault();
            Session["Courseid"] = coursedetails.Id;
            return View(obj.GetAll().Where(x => x.CourseId == coursedetails.Id).ToList());
        }

        // GET: Question/Details/5
        [Authorize(Roles = "Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionSet questionSet = obj.GetModelById(Convert.ToInt32(id));
            if (questionSet == null)
            {
                return HttpNotFound();
            }
            return View(questionSet);
        }
        public ActionResult AnsrDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //QuestionSet questionSet = obj.GetAll().Where(x=>x.(Convert.ToInt32(id));
            var a = obj.GetAll().Where(x => x.CourseId == id).ToList();
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        // GET: Question/Create
        public ActionResult Create()
        {
            ViewBag.TopicId = new SelectList(courseobj.GetAll().ToList(), "Id", "Cname");
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Create([Bind(Include = "QuestionSetId,QuestionDescription,Option1,Option2,Option3,Option4,CorrectAnswer,CourseId,Mark,DateCreated,UserModified")] QuestionSet questionSet)
        {
            if (ModelState.IsValid)
            {
                questionSet.CourseId = Convert.ToInt32( Session["Courseid"]);
                obj.InsertModel(questionSet);
                obj.Save();
                return RedirectToAction("Index");
            }

            ViewBag.TopicId = new SelectList(courseobj.GetAll().ToList(), "Id", "CName", questionSet.CourseId);
            return View(questionSet);
        }

        // GET: Question/Edit/5
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionSet questionSet = obj.GetModelById(Convert.ToInt32(id));
            if (questionSet == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicId = new SelectList(courseobj.GetAll().ToList(), "Id", "cName", questionSet.CourseId);
            return View(questionSet);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Edit([Bind(Include = "QuestionSetId,QuestionDescription,Option1,Option2,Option3,Option4,CorrectAnswer,TopicId,Mark,DateCreated,UserModified")] QuestionSet questionSet)
        {
            if (ModelState.IsValid)
            {
                var quesdtls = obj.GetModelById(questionSet.QuestionSetId);
                quesdtls.QuestionDescription = questionSet.QuestionDescription;
                quesdtls.Option1 = questionSet.Option1;
                quesdtls.Option2 = questionSet.Option2;
                quesdtls.Option3 = questionSet.Option3;
                quesdtls.Option4 = questionSet.Option4;
                quesdtls.Mark = questionSet.Mark;
                quesdtls.CorrectAnswer = questionSet.CorrectAnswer;
                obj.UpdateModel(quesdtls);
                obj.Save();
                return RedirectToAction("Index");
            }
            ViewBag.TopicId = new SelectList(courseobj.GetAll().ToList(), "Id", "cName", questionSet.CourseId);
            return View(questionSet);
        }

        // GET: Question/Delete/5
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionSet questionSet = obj.GetModelById(Convert.ToInt32(id));
            if (questionSet == null)
            {
                return HttpNotFound();
            }
            return View(questionSet);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Tutor,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionSet questionSet = obj.GetModelById(Convert.ToInt32(id)); 
            obj.DeleteModel(id);
            obj.Save();
            //return View();
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                obj.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
