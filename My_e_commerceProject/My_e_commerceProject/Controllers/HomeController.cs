using My_e_commerceProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace My_e_commerceProject.Controllers
{
    public class HomeController : Controller
    {
        private storeEntities1 db = new storeEntities1();
        public ActionResult Index()
        {

            return View(db.categories.ToList());
        }


        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "email,password")] user user, string confirmpassword)
        {
            
            if (ModelState.IsValid & user.password == confirmpassword)
            {
                var uSer = new user()
                {
                    name = " ",
                    email=user.email,
                    password= user.password,
                    role_id=1 
                };
                db.users.Add(uSer);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }



        public ActionResult Login()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user uSER)
        {
            var checkInputs = db.users.Where(model => model.email == uSER.email && model.password == uSER.password).FirstOrDefault();

            Session["UserID"] = checkInputs.id;


            if (checkInputs != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["UserID"] = null;
            return RedirectToAction("Index");
        }



        public ActionResult category()
        {
            return View();
        }
        public ActionResult ShopByCategory(int categoryId)
        {
            var products = db.products.Where(p => p.category_id == categoryId).ToList();
            if (products == null || !products.Any())
            {
                ViewBag.Message = "No products found in this category.";
                return View("Shop", new List<product>()); // Return an empty product list if no products found
            }
            return View("Shop", products); // Pass the filtered products to the shop view
        }


        public ActionResult shop()
        {
            var product = db.products.ToList();
            return View(product);
        }



        public ActionResult product_details( int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var user_id = (int?)Session["UserID"];

            if (user_id == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var userCart = db.carts.FirstOrDefault(c => c.user_id == user_id);
            if (userCart == null)
            {
                userCart = new cart { user_id = user_id.Value };
                db.carts.Add(userCart);
                db.SaveChanges();
            }

            var existingCartItem = userCart.cart_item.FirstOrDefault(ci => ci.product_id == product.id);
            if (existingCartItem != null)
            {
                existingCartItem.quantity += quantity;
            }
            else
            {
                cart_item new_product = new cart_item
                {
                    product_id = product.id,
                    quantity = quantity,
                    cart_id = userCart.id,
                };

                userCart.cart_item.Add(new_product);
            }

            db.SaveChanges();

            Session["CartItems"] = userCart.cart_item.Sum(ci => ci.quantity);


            // Pass the cart item to the view
            var addedItem = userCart.cart_item.FirstOrDefault(ci => ci.product_id == product.id);
            return RedirectToAction("AddToCart", addedItem);// Return the AddToCart view
        }


        public ActionResult Cart()
        {
            var user_id = (int?)Session["UserID"];

            if (user_id == null)
            {
                return RedirectToAction("Login", "Home");
            }

            var userCart = db.carts.Include("cart_item.product").FirstOrDefault(c => c.user_id == user_id);

            if (userCart == null || !userCart.cart_item.Any())
            {
                ViewBag.Message = "Your cart is empty.";
                return View();
            }

            return View(userCart);
        }

        public ActionResult UpdateQuantity(int id, int change)
        {
            var cartItem = db.cart_item.FirstOrDefault(ci => ci.id == id);
            if (cartItem != null)
            {
                cartItem.quantity += change;

                // If the quantity goes below 1, remove the item from the cart
                if (cartItem.quantity <= 0)
                {
                    db.cart_item.Remove(cartItem);
                }

                db.SaveChanges();

                var userCart = db.carts.Include("cart_item").FirstOrDefault(c => c.id == cartItem.cart_id);
                Session["CartItems"] = userCart.cart_item.Sum(ci => ci.quantity);
            }

            return RedirectToAction("Cart");
        }

        public ActionResult RemoveFromCart(int id)
        {
            var cartItem = db.cart_item.FirstOrDefault(ci => ci.id == id);
            if (cartItem != null)
            {
                db.cart_item.Remove(cartItem);
                db.SaveChanges();

                var userCart = db.carts.Include("cart_item").FirstOrDefault(c => c.id == cartItem.cart_id);
                Session["CartItems"] = userCart.cart_item.Sum(ci => ci.quantity);
            }

            return RedirectToAction("Cart");
        }

        public ActionResult Details()
        {

            var userID = (int?)Session["UserID"];

            if (userID == null)
            {
                return RedirectToAction("Login");
            }

            user user = db.users.Find(userID);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        public ActionResult Edit()
        {

            var userID = (int?)Session["UserID"];
            if (userID == null)
            {
                return RedirectToAction("Login");
            }
            user user = db.users.Find(userID);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(user user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.users.Find(user.id);
                if (existingUser == null)
                {
                    return HttpNotFound(); // إذا لم يكن المستخدم موجوداً
                }

                // تحديث الحقول التي ترغب في تعديلها
                existingUser.email = user.email;
                existingUser.password = user.password;
                existingUser.role_id =1;
                existingUser.name = user.name;

                db.Entry(existingUser).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Details");
            }
            return View(user);
        }


        public ActionResult EditPassword()
        {

            return View();
        }

        [HttpPost]
        public ActionResult EditPassword(string oldPassword, string newPassword, string repeatPassword)
        {
            var userID = (int?)Session["UserID"];

            if (userID == null)
            {
                return RedirectToAction("Login");
            }

            user user = db.users.Find(userID);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (string.IsNullOrWhiteSpace(oldPassword) || string.IsNullOrWhiteSpace(newPassword) || string.IsNullOrWhiteSpace(repeatPassword))
            {
                ViewBag.ErrorMessage = "Please fill in all the fields.";
                return View();
            }

            if (user.password != oldPassword)
            {
                ViewBag.ErrorMessage = "Incorrect old password.";
                return View();
            }

            if (newPassword != repeatPassword)
            {
                ViewBag.ErrorMessage = "New password and repeat password do not match.";
                return View();
            }

            user.password = newPassword;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();

            ViewBag.SuccessMessage = "Password successfully changed.";
            return View("Index");
        }


    }
}