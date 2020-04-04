﻿using E_Learning.Models;
using E_Learning.Models.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Learning.Controllers
{
    public class TopicListController : Controller
    {
        _IAllRepository<TopicList> obj;
        _IAllRepository<Course> courseobj;
        DbModel db;
        public TopicListController()
        {
            this.db = new DbModel();
            this.obj = new AllRepository<TopicList>();
            this.courseobj = new AllRepository<Course>();
        }

        // GET: TopicList
        public ActionResult Index(string addquestion)
        {
            
            var courselist = courseobj.GetAll().Where(x => x.TutorDetail.Id ==  Convert.ToInt32(Session["TutorId"])).GroupBy(x => x.Cname).Select(x => x.FirstOrDefault());
            //.GroupBy(x => x.Cname ).Select(x => x.FirstOrDefault())
            //Convert.ToInt32(Session["TutorId"])
            ViewBag.droplist = courselist;
            if (addquestion != null)
            {
                TempData["addquestion"] = true;
                ViewBag.addquestion = "true";
            }
                
            return View(); 
        }
        public JsonResult GetLevel(string level)
        {
            
            db.Configuration.ProxyCreationEnabled = false;
            var tutorid = Convert.ToInt32(Session["TutorId"]);
            var levellist = db.Courses.Where(x => x.Cname == level && x.TutorDetail.Id == tutorid).ToList();
            //var levellist = courseobj.GetAll().Where(x => x.Cname==level && x.TutorDetail.Id== 1).ToList();
            return Json(levellist, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Index(Course course)
        {
            
            var coursedetails = courseobj.GetAll().Where(x => x.Cname == (course.Cname) && x.Clevel == course.Clevel &&x.TutorDetail.Id== Convert.ToInt32(Session["TutorId"])).FirstOrDefault();
            Session["Courseid"]= coursedetails.Id;
            Session["CourseName"] = coursedetails.Cname + "(" + coursedetails.Clevel + ")";
            return RedirectToAction("TopicList", "TopicList");
        }
        public ActionResult TopicList()
        {

            // var lists = courseobj.GetAll().GroupBy(x => x.Cname).Select(x => x.FirstOrDefault());
            //ViewBag.droplist = lists;
            ViewBag.courseNameLevel = Session["CourseName"];
            int id = Convert.ToInt32(Session["Courseid"]);
            return View(obj.GetAll().Where(x => x.CourseId == id).ToList());
        }
        public ActionResult AllTopic()
        {
            
            return View(obj.GetAll().Where(x=>x.CourseId == Convert.ToInt32(TempData["Courseid"])).ToList());
        }
        public ActionResult StuAllTopic(int id)
        {
            
            return View(obj.GetAll().Where(x => x.CourseId == id).ToList());
        }
        // GET: TopicList/Details/5
        public ActionResult Details(int id)
        {
            return View(obj.GetModelById(id));
        }

        // GET: TopicList/Create
        public ActionResult Create()
        {
            ViewBag.courseNameLevel = Session["CourseName"];
            return View();
        }

        // POST: TopicList/Create
        [HttpPost]
        public ActionResult Create(TopicList collection)
        {
            try
            {
                // TODO: Add insert logic here
                HttpPostedFileBase filevideo = Request.Files["customFile"];
                string FileExtension = filevideo.ContentType;
                
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filevideo.SaveAs(path + Path.GetFileName(filevideo.FileName));
                HttpPostedFileBase filepdf = Request.Files["pdffile"];
                string FileExtensionpdf = filepdf.ContentType;
                byte[] pdfeBytes = null;
                BinaryReader reader = new BinaryReader(filepdf.InputStream);
                pdfeBytes = reader.ReadBytes((int)filepdf.ContentLength);
                ViewBag.Message = "File uploaded successfully.";
                collection.VideoPath = "~/Uploads/" + Path.GetFileName(filevideo.FileName);
                collection.PdfContent = pdfeBytes;
                collection.CourseId = Convert.ToInt32(Session["Courseid"]);
                obj.InsertModel(collection);
                obj.Save();
                return RedirectToAction("TopicList");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: TopicList/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TopicList/Edit/5
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

        // GET: TopicList/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TopicList/Delete/5
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