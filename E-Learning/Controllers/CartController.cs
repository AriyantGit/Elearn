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
    public class CartController : Controller
    {
        _IAllRepository<TopicList> topicobj;
        _IAllRepository<Course> courseobj;
        _IAllRepository<StudentCourseRegistration> regobj;
        _IAllRepository<QuestionSet> quesobj;
        _IAllRepository<AnswerDetails> ansrobj;
        _IAllRepository<CartDetail> cartobj;
        _IAllRepository<Coupon> couponobj;
        public CartController()
        {
            this.couponobj = new AllRepository<Coupon>();
            this.topicobj = new AllRepository<TopicList>();
            this.courseobj = new AllRepository<Course>();
            this.regobj = new AllRepository<StudentCourseRegistration>();
            this.quesobj = new AllRepository<QuestionSet>();
            this.ansrobj = new AllRepository<AnswerDetails>();
            this.cartobj= new AllRepository<CartDetail>();
        }
        // GET: Cart
        public ActionResult Index()
        {
            var cartlist = cartobj.GetAll().Where(x => x.StudentId == Convert.ToInt32(Session["Studentid"])).ToList();

            return View(cartlist);
        }

        // GET: Cart/Details/5
        public ActionResult Details(int id)
        {

            return View();
        }
        public ActionResult RegistrationIndex( int id)
        {
            var cartdtls =new CartDetail();
            cartdtls.CourseId = id;
            var courseexit = cartobj.GetAll().Where(x => x.CourseId == id && x.StudentId == Convert.ToInt32(Session["Studentid"])).FirstOrDefault();
            if (courseexit == null)
            {
                cartdtls.StudentId = Convert.ToInt32(Session["Studentid"]);
                cartobj.InsertModel(cartdtls);
                cartobj.Save();

            }
            else
                ViewBag.message = "Course Already in Your Cart";
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Registration(Int32[] list,string Coupontext)
        {
            var coupondtls = couponobj.GetAll().Where(x => x.Code== Coupontext).FirstOrDefault();//.Select(x=>x.Id);
            //var id1 = couponobj.GetAll().Where(x => x.Code == Coupontext).Select(x => x.Id);
            if (coupondtls!=null)
            {
                foreach (int a in list)
                {
                    

                    StudentCourseRegistration st = new StudentCourseRegistration();
                    st.CourseId = a;

                    st.CouponId = coupondtls.Id;
                    st.StudentId = Convert.ToInt32(Session["Studentid"]);
                    regobj.InsertModel(st);
                    regobj.Save();
                    var cartdtls = cartobj.GetAll().Where(x => x.CourseId == a && x.StudentId == Convert.ToInt32(Session["Studentid"])).FirstOrDefault();
                    cartobj.DeleteModel(cartdtls.Id);
                    cartobj.Save();
                }
            }
            else
            {
                foreach (int a in list)
                {
                    StudentCourseRegistration st = new StudentCourseRegistration();
                    st.CourseId = a;
                    st.StudentId = Convert.ToInt32(Session["Studentid"]);
                    regobj.InsertModel(st);
                    regobj.Save();
                    var cartdtls = cartobj.GetAll().Where(x => x.CourseId == a && x.StudentId == Convert.ToInt32(Session["Studentid"])).FirstOrDefault();
                    cartobj.DeleteModel(cartdtls.Id);
                    cartobj.Save();
                }
            }
            
            
           
            return RedirectToAction("Index");

        }
        // GET: Cart/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cart/Create
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

        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
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

        // GET: Cart/Delete/5
        public ActionResult Delete(int id)
        {
             cartobj.DeleteModel(id);
            cartobj.Save();
            return RedirectToAction("Index");
        }

        // POST: Cart/Delete/5
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
