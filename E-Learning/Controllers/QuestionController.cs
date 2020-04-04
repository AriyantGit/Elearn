using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Learning.Models;
using E_Learning.Models.DAL;

namespace E_Learning.Controllers
{
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
        public ActionResult Index( )
        {
           return View(obj.GetAll().Where(x => x.CourseId == Convert.ToInt32(Session["Courseid"])).ToList());
        }
        [HttpPost]
        public ActionResult Index(Course course)
        {
            var coursedetails = courseobj.GetAll().Where(x => x.Cname == (course.Cname) && x.Clevel == course.Clevel && x.TutorDetail.Id == Convert.ToInt32(Session["TutorId"])).FirstOrDefault();
            Session["Courseid"] = coursedetails.Id;
            return View(obj.GetAll().Where(x => x.CourseId == coursedetails.Id).ToList());
        }

        // GET: Question/Details/5
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
        public ActionResult Edit([Bind(Include = "QuestionSetId,QuestionDescription,Option1,Option2,Option3,Option4,CorrectAnswer,TopicId,Mark,DateCreated,UserModified")] QuestionSet questionSet)
        {
            if (ModelState.IsValid)
            {
                obj.UpdateModel(questionSet);
                obj.Save();
                return RedirectToAction("Index");
            }
            ViewBag.TopicId = new SelectList(courseobj.GetAll().ToList(), "Id", "cName", questionSet.CourseId);
            return View(questionSet);
        }

        // GET: Question/Delete/5
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
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionSet questionSet = obj.GetModelById(Convert.ToInt32(id)); ;
            obj.DeleteModel(id);
            obj.Save();
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
