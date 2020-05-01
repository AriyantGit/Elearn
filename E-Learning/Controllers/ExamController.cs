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
    public class ExamController : Controller
    {
        _IAllRepository<Course> courseobj;
        _IAllRepository<StudentCourseRegistration> regobj;
        _IAllRepository<QuestionSet> quesobj;
        _IAllRepository<AnswerDetails> ansrobj;
        public ExamController()
        {
            this.courseobj =new AllRepository<Course>();
            this.quesobj = new AllRepository<QuestionSet>();
            this.ansrobj = new AllRepository<AnswerDetails>();
        }
        // GET: Exam
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Index(int? id)
        {
            //Random r=new Random();
            //
            //id = 1;
            TempData["CourseId"] = id;
            var a = quesobj.GetAll().Where(x => x.CourseId ==Convert.ToInt32(id)).ToList();
            return View(a);
        }
        //[HttpPost]
        //public ActionResult Index(string[] list)
        //{
        //    //Random r=new Random();
        //    int marks = 0, totalmark = 0;
        //    var model = new AnswerDetails();
        //    if (list != null)
        //    {
        //        foreach (var listitem in list)
        //        {

        //            var quesId = listitem.ToString().Substring(0, listitem.ToString().LastIndexOf("."));
        //            var ansr = listitem.ToString().Substring(listitem.ToString().LastIndexOf(".") + 1);
        //            var quesdtls = quesobj.GetModelById(Convert.ToInt32(quesId));
        //            totalmark += quesdtls.Mark;
        //            if (quesdtls.CorrectAnswer == Convert.ToInt32(ansr))
        //            {
        //                marks += quesdtls.Mark;
        //            }

        //        }
        //        model.TickAnsr = list.Count();
        //    }
        //    else
        //        model.TickAnsr = 0;


        //    double a =marks>0 && totalmark>0?((double)marks/totalmark)*100 :0;
        //    model.percentage = a;
        //    model.StudentId = 1;
        //    model.CourseId =Convert.ToInt32( TempData["CourseId"]);
        //    TempData["CourseId"] = model.CourseId;
        //    model.MarksObtain = marks;
        //   // model.TickAnsr =  list.Count()!=0?list.Count():0;
        //    ansrobj.InsertModel(model);
        //    ansrobj.Save();
        //    //return View(a);
        //    return RedirectToAction("MarksDetail", new { id = model.AnswerDetailsId });

        //}

        //// GET: Exam/Details/5
        //public ActionResult MarksDetail(int id)
        //{
        //    var course =  Convert.ToInt32(TempData["CourseId"])!=null? id: Convert.ToInt32(TempData["CourseId"]);
        //    ViewBag.total = quesobj.GetAll().Where(x => x.CourseId == course).Select(x =>x.Mark).ToList().Sum();//.to
        //    var a = ansrobj.GetModelById(course);
        //        //GetAll().Where(x => x.CourseId == course && x.StudentId == 1).FirstOrDefault();
        //    return View(a);
        //}

        // GET: Exam/Create
        [HttpPost]
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult Index(string[] list)
        {
            //Random r=new Random();
            int marks = 0, totalmark = 0;
            var model = new AnswerDetails();
            if (list != null)
            {
                foreach (var listitem in list)
                {

                    var quesId = listitem.ToString().Substring(0, listitem.ToString().LastIndexOf("."));
                    var ansr = listitem.ToString().Substring(listitem.ToString().LastIndexOf(".") + 1);
                    var quesdtls = quesobj.GetModelById(Convert.ToInt32(quesId));
                    totalmark += quesdtls.Mark;
                    if (quesdtls.CorrectAnswer == Convert.ToInt32(ansr))
                    {
                        marks += quesdtls.Mark;
                    }

                }
                model.TickAnsr = list.Count();
            }
            else
                model.TickAnsr = 0;
            double a = marks > 0 && totalmark > 0 ? ((double)marks / totalmark) * 100 : 0;
        model.percentage = a;
            model.StudentId =Convert.ToInt32(Session["StudentId"]);
            model.CourseId =Convert.ToInt32(TempData["CourseId"]);
            TempData["CourseId"] = model.CourseId;
            model.MarksObtain = marks;
           // model.TickAnsr =  list.Count()!=0?list.Count():0;
            ansrobj.InsertModel(model);
            ansrobj.Save();
            //return View(a);
            return RedirectToAction("MarksDetail", new { ansrid = model.AnswerDetailsId
    });
            
        }

        // GET: Exam/Details/5
        [Authorize(Roles = "Student,Admin")]
        [CustomAuthenticationFilter]
        public ActionResult MarksDetail(int? id,int? ansrid)
{
            if (ansrid == null)
            {
                //logic for courseid
                var course = Convert.ToInt32(TempData["CourseId"]) != null ? Convert.ToInt32(id) : Convert.ToInt32(TempData["CourseId"]);
                ViewBag.total = quesobj.GetAll().Where(x => x.CourseId == course).Select(x => x.Mark).ToList().Sum();//.to
                var a = ansrobj.GetAll().Where(x => x.CourseId == course && x.StudentId == Convert.ToInt32(Session["StudentId"])).FirstOrDefault();
                return View(a);
            }
            else
            {
                //logic for absrid
               var course = ansrobj.GetModelById(Convert.ToInt32(ansrid));
                ViewBag.total = quesobj.GetAll().Where(x => x.CourseId == course.CourseId).Select(x => x.Mark).ToList().Sum();//.to
                var a = ansrobj.GetAll().Where(x => x.AnswerDetailsId == ansrid && x.StudentId == Convert.ToInt32(Session["StudentId"])).FirstOrDefault();
                return View(a);
            }
    
}
public ActionResult Create()
        {
            return View();
        }

        // POST: Exam/Create
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

        // GET: Exam/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Exam/Edit/5
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

        // GET: Exam/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Exam/Delete/5
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
