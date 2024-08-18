using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using My_e_commerceProject.Models;

namespace My_e_commerceProject.Controllers
{
    public class cart_itemController : Controller
    {
        private storeEntities1 db = new storeEntities1();

        // GET: cart_item
        public ActionResult Index()
        {
            var cart_item = db.cart_item.Include(c => c.cart).Include(c => c.product);
            return View(cart_item.ToList());
        }

        // GET: cart_item/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart_item cart_item = db.cart_item.Find(id);
            if (cart_item == null)
            {
                return HttpNotFound();
            }
            return View(cart_item);
        }

        // GET: cart_item/Create
        public ActionResult Create()
        {
            ViewBag.cart_id = new SelectList(db.carts, "id", "id");
            ViewBag.product_id = new SelectList(db.products, "id", "name");
            return View();
        }

        // POST: cart_item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,product_id,quantity,cart_id")] cart_item cart_item)
        {
            if (ModelState.IsValid)
            {
                db.cart_item.Add(cart_item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cart_id = new SelectList(db.carts, "id", "id", cart_item.cart_id);
            ViewBag.product_id = new SelectList(db.products, "id", "name", cart_item.product_id);
            return View(cart_item);
        }

        // GET: cart_item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart_item cart_item = db.cart_item.Find(id);
            if (cart_item == null)
            {
                return HttpNotFound();
            }
            ViewBag.cart_id = new SelectList(db.carts, "id", "id", cart_item.cart_id);
            ViewBag.product_id = new SelectList(db.products, "id", "name", cart_item.product_id);
            return View(cart_item);
        }

        // POST: cart_item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,product_id,quantity,cart_id")] cart_item cart_item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cart_item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cart_id = new SelectList(db.carts, "id", "id", cart_item.cart_id);
            ViewBag.product_id = new SelectList(db.products, "id", "name", cart_item.product_id);
            return View(cart_item);
        }

        // GET: cart_item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cart_item cart_item = db.cart_item.Find(id);
            if (cart_item == null)
            {
                return HttpNotFound();
            }
            return View(cart_item);
        }

        // POST: cart_item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            cart_item cart_item = db.cart_item.Find(id);
            db.cart_item.Remove(cart_item);
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
