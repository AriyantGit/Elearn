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
    public class StudentsController : Controller
    {
        //private DbModel db = new DbModel();
        _IAllRepository<Student> stuobj;
        public StudentsController()
        {
            this.stuobj = new AllRepository<Student>();
        }

        // GET: Students
        public ActionResult Index()
        {
            return View(stuobj.GetAll().ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = stuobj.GetModelById(Convert.ToInt32(id));
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FName,LName,Email,Mobile,Image,Password,DateCreated,UserModified")] Student student)
        {
            if (ModelState.IsValid)
            {
                
                stuobj.InsertModel(student);
                stuobj.Save();
                
                return RedirectToAction("Index");
            }

            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = stuobj.GetModelById(Convert.ToInt32(id));
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FName,LName,Email,Mobile,Image,Password,DateCreated,UserModified")] Student student)
        {
            if (ModelState.IsValid)
            {
                stuobj.UpdateModel(student);
                stuobj.Save();
                //db.Entry(student).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = stuobj.GetModelById(Convert.ToInt32(id));
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = stuobj.GetModelById(id);
            stuobj.DeleteModel(id);
            stuobj.Save();
            //db.Students.Remove(student);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                stuobj.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
