using E_Learning.Filter;
using E_Learning.Models;
using E_Learning.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Learning.Controllers
{
    [Authorize]
    [SessionTimeout]
    public class TutorHomeController : Controller
    {
        _IAllRepository<TutorDetails> tutorobj;
        _IAllRepository<Course> courseobj;
        _IAllRepository<TotalLike> totallikeobj;
        _IAllRepository<TopicLike> topiclikeobj;
        _IAllRepository<TopicList> topicobj;
        public TutorHomeController()
        {
            this.tutorobj = new AllRepository<TutorDetails>();
            this.courseobj = new AllRepository<Course>();
            this.totallikeobj = new AllRepository<TotalLike>();
            this.topiclikeobj = new AllRepository<TopicLike>();
            this.topicobj = new AllRepository<TopicList>();
        }
        // GET: TutorHome
        [Authorize(Roles ="Tutor,Admin")]
        [CustomAuthenticationFilter]
       
        public ActionResult Index()
        {
            ViewBag.coursecount=courseobj.GetAll().Where(x=>x.TutorId== Convert.ToInt32(Session["tutorid"])).Count();
            return View();
        }
        [AllowAnonymous]
        public ActionResult partialIndex()
        {
            var a = courseobj.GetAll().Where(x => x.TutorId == Convert.ToInt32(Session["tutorid"])).OrderByDescending(x=>x.DateCreated).Take(3).ToList();
            return View(a);
        }
        public ActionResult partialLike()
        {
          
            var courseid=(courseobj.GetAll().Where(a => a.TutorId == Convert.ToInt32(Session["Tutorid"]))).ToList();
            var topicidlist = (from s in topicobj.GetAll()
                               join o in courseid
                               on s.CourseId equals o.Id
                               select new TotalLike { TopicList = s }).ToList();


            //topicobj.GetAll().Where(x => x.CourseId == courseid).ToList();
            //ViewBag.Like =topiclikeobj.GetAll().Where(x => x.TopicId ==topicidlist);

            var rslt = (from s in topiclikeobj.GetAll()
                     join o in topicidlist
                     on s.TopicId equals o.TopicList.Id
                     select new TotalLike
                     {
                         TopicList = o.TopicList,
                         totalLike = Convert.ToInt32(topiclikeobj.GetAll().Where(x => x.TopicId == o.TopicList.Id).Count())

                     }).Distinct().OrderByDescending(x => x.totalLike).Take(3).ToList();


            return PartialView(rslt);

        }
        public ActionResult TotalTopicLike()
        {
            var rslt = (from s in courseobj.GetAll().Where(x => x.TutorId == Convert.ToInt32(Session["Tutorid"]))
                        join o in topicobj.GetAll()
                     on s.Id equals o.CourseId
                        select new TotalLike
                        {
                            Course = s,
                            TopicList=o
                           
                        }).OrderByDescending(x=>x.totalLike).Take(3).ToList();

            return PartialView(rslt);
        }
        // GET: TutorHome/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TutorHome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TutorHome/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: TutorHome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TutorHome/Edit/5
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

        // GET: TutorHome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TutorHome/Delete/5
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
