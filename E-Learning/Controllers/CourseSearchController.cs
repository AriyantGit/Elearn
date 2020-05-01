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

namespace E_Learning.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class CourseSearchController : Controller
    {
        private DbModel db = new DbModel();

        // GET: CourseSearch
        public ActionResult Index()
        {
            var studentCourseRegistrations = db.StudentCourseRegistrations.Include(s => s.Course).Include(s => s.Student).Include(s => s.TopicList);
            return View(studentCourseRegistrations.ToList());
        }

        // GET: CourseSearch/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCourseRegistration studentCourseRegistration = db.StudentCourseRegistrations.Find(id);
            if (studentCourseRegistration == null)
            {
                return HttpNotFound();
            }
            return View(studentCourseRegistration);
        }

        // GET: CourseSearch/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Cname");
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FName");
            ViewBag.TopicListId = new SelectList(db.TopicLists, "Id", "TopicName");
            return View();
        }

        // POST: CourseSearch/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentCourseRegistrationId,StudentId,CourseId,TopicListId,DateCreated,UserModified")] StudentCourseRegistration studentCourseRegistration)
        {
            if (ModelState.IsValid)
            {
                db.StudentCourseRegistrations.Add(studentCourseRegistration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Cname", studentCourseRegistration.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FName", studentCourseRegistration.StudentId);
            ViewBag.TopicListId = new SelectList(db.TopicLists, "Id", "TopicName", studentCourseRegistration.TopicListId);
            return View(studentCourseRegistration);
        }

        // GET: CourseSearch/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCourseRegistration studentCourseRegistration = db.StudentCourseRegistrations.Find(id);
            if (studentCourseRegistration == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Cname", studentCourseRegistration.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FName", studentCourseRegistration.StudentId);
            ViewBag.TopicListId = new SelectList(db.TopicLists, "Id", "TopicName", studentCourseRegistration.TopicListId);
            return View(studentCourseRegistration);
        }

        // POST: CourseSearch/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentCourseRegistrationId,StudentId,CourseId,TopicListId,DateCreated,UserModified")] StudentCourseRegistration studentCourseRegistration)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentCourseRegistration).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Cname", studentCourseRegistration.CourseId);
            ViewBag.StudentId = new SelectList(db.Students, "StudentId", "FName", studentCourseRegistration.StudentId);
            ViewBag.TopicListId = new SelectList(db.TopicLists, "Id", "TopicName", studentCourseRegistration.TopicListId);
            return View(studentCourseRegistration);
        }

        // GET: CourseSearch/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentCourseRegistration studentCourseRegistration = db.StudentCourseRegistrations.Find(id);
            if (studentCourseRegistration == null)
            {
                return HttpNotFound();
            }
            return View(studentCourseRegistration);
        }

        // POST: CourseSearch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentCourseRegistration studentCourseRegistration = db.StudentCourseRegistrations.Find(id);
            db.StudentCourseRegistrations.Remove(studentCourseRegistration);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
