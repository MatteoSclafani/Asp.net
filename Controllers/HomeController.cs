using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using WAndCAssignment.Models;

namespace WAndCAssignment.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(FormCollection form, CustomerLogin cs)
        {
            DAO dao = new DAO();
            cs = new CustomerLogin();
            Customers customer = new Customers();

            if (ModelState.IsValid)
            {
               cs.Username = form["UserName"];
               cs.Password = form["Password"];
               customer.Username = cs.Username;
               customer.Password = cs.Password;

                customer = dao.CheckLogin(customer);
                /* 
                Once the username and password are confirmed, the associated email(primary key) is stored 
                in TempData so that it can used when inserting creditcard detail later should it be needed in the cart controller
                */
                TempData["CustomerEmail"] = customer.Email;
                Session.Add("MemberName", customer.FirstName);
                

                if (customer.FirstName != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["error"] = "Invalid username or password" + dao.message;
                    return View("Login");
                }
            }
            return View(customer);
        }


       

        //Terminate user Session
        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index");
        }


        //Create an About Us action
        public ActionResult AboutUs()
        {
            return View();
        }


        //Per image on click
        public ActionResult ViewMore()
        {

            return View();
        }


        [HttpGet]
        public ActionResult SearchItem()
        {
            return View();
        }


        [HttpPost]
        public ActionResult SearchItem(FormCollection form)
        {
            DAO dao = new DAO();
            Product prod = new Product();

            List<Product> list = dao.countFindItems(form["search"]);
            
            if(list.Count == 0)
            {
                ViewData["result"] = "No Items Found";
            }


            return View(list);
        }




    }
}