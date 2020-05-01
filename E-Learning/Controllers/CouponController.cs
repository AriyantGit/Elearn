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
    public class CouponController : Controller
    {
        _IAllRepository<Coupon> couponobj;
        public CouponController()
        {
            this.couponobj = new AllRepository<Coupon>();
        }
        // GET: Coupon
        public ActionResult Index()
        {
            return View(couponobj.GetAll().ToList());
        }
        public ActionResult CouponCode()
        {
            var a = couponobj.GetAll().Where(x => x.Validity >= DateTime.Today).ToList();
            return PartialView(a);
            //return View(couponobj.GetAll().ToList());
        }
      
        public ActionResult CouponCodeChk(string couponcode)
        {
           
            try
            {
                var a = couponobj.GetAll().Where(x=>x.Code==couponcode && x.Validity>=DateTime.Today).ToList();
                var b = a.Select(x => x.Discount);
                if(a.Count()!=0)
                {
                    return Json(new{msg = "yes",discount=b});
                }
               else
                    return Json(new { msg = "no" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult CouponValid(string couponcode)
        {

            try
            {
                if((couponcode.Trim().Length)>0)
                {
                    var a = couponobj.GetAll().Where(x => x.Code == couponcode).ToList();
                    var b = a.Select(x => x.Discount);
                    if (a.Count() > 0)
                    {
                        return Json(new { msg = "alreadyExits" });
                    }
                    else
                        return Json(new { msg = "available" });
                }
                else
                    return Json(new { msg = "alreadyExits" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // GET: Coupon/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Coupon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Coupon/Create
        [HttpPost]
        public ActionResult Create(Coupon collection)
        {
            try
            {
                var a = couponobj.GetAll().Where(x => x.Code == collection.Code).ToList();
                var b = a.Select(x => x.Discount);
                if (a.Count() > 0)
                {
                    ViewBag.coupon = "already exits";
                    return View();
                }
                else
                {
                    couponobj.InsertModel(collection);
                    couponobj.Save();

                    return RedirectToAction("Index");
                }
            
                // TODO: Add insert logic here
                
            }
            catch
            {
                return View();
            }
        }

        // GET: Coupon/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Coupon/Edit/5
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

        // GET: Coupon/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var a = couponobj.GetAll().Where(x => x.Id == id).FirstOrDefault();
                couponobj.DeleteModel(a.Id);
                couponobj.Save();
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        // POST: Coupon/Delete/5
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
