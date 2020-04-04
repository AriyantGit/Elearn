using E_Learning.Models;
using E_Learning.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Learning.Controllers
{
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
        public ActionResult Index(int id)
        {
            //Random r=new Random();
            //
            TempData["CourseId"] = id;
            var a = quesobj.GetAll().Where(x => x.CourseId == id).ToList();
            return View(a);
        }
        [HttpPost]
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
           

            double a =marks>0 && totalmark>0?((double)marks/totalmark)*100 :0;
            model.percentage = a;
            model.StudentId = 1;
            model.CourseId =Convert.ToInt32( TempData["CourseId"]);
            TempData["CourseId"] = model.CourseId;
            model.MarksObtain = marks;
           // model.TickAnsr =  list.Count()!=0?list.Count():0;
            ansrobj.InsertModel(model);
            ansrobj.Save();
            //return View(a);
            return RedirectToAction("MarksDetail", new { id = model.AnswerDetailsId });
            
        }

        // GET: Exam/Details/5
        public ActionResult MarksDetail(int id)
        {
            var course =  Convert.ToInt32(TempData["CourseId"])!=null? id: Convert.ToInt32(TempData["CourseId"]);
            ViewBag.total = quesobj.GetAll().Where(x => x.CourseId == course).Select(x =>x.Mark).ToList().Sum();//.to
            return View(ansrobj.GetAll().Where(x=>x.CourseId==course && x.StudentId==1).FirstOrDefault());
        }

        // GET: Exam/Create
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
