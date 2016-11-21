using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WAndCAssignment.Models;
using System.Web.Helpers;

namespace WAndCAssignment.Controllers
{
    public class CartController : Controller
    {
        static List<Product> cart = new List<Product>();
        DAO dao = new DAO();
        decimal totalPrice = 0;


        // GET: Cart
        public ActionResult Index()
        {
            List<Product> productsList = dao.AllProducts();
            return View(productsList);
        }

        public ActionResult AddItem(FormCollection form)
        {
            Product pr = new Product();
            List<Product> list = dao.AllProducts();
            bool found = false;

            if (Session["MemberName"] == null)
            {
                Console.WriteLine("Not Allowed");
            }
            else
            {
                for (int i = 0; i < list.Count && !found; i++)
                {
                    if (list[i].ProductID == form["id"].ToString())
                    {
                        pr = list[i];
                        pr.Quantity = int.Parse(form["quantity"]);
                        found = true;
                        cart.Add(list[i]);

                    }
                }
            }


            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            foreach (Product p in cart)
            {
                totalPrice += p.Quantity * p.ProductPrice;
                ViewData["price"] = totalPrice;
            }
         
            return View(cart);
        }

        public ActionResult CheckOut()
        {

            if (cart.Count == 0)
            {
                return RedirectToAction("ViewCart");
            }
            else
            {
                return View("CheckOut");
            }


        }

        [HttpPost]
        public ActionResult CheckOut(FormCollection form)
        {
            string credCardNum = form["cardNumber"];
            CreditCardValidation ccv = new CreditCardValidation();
            bool validCredCard = ccv.IsValidCreditCard(credCardNum);

            if (validCredCard) 
            {
                
                DAO dao = new DAO();
                string customerEmail = TempData["CustomerEmail"].ToString();

                //Encrypted Credit card number
                string encryptedCredCard = Crypto.HashPassword(credCardNum);

                bool successfulEntry = dao.InsertCustomerCreditCard(encryptedCredCard, customerEmail) > 0;
                if (successfulEntry)
                {
                    ViewData["credCardValidation"] = "Credit Card Validated & Inserted Successfully";
                    Session.Clear();
                    Session.Abandon();
                }
                else ViewData["credCardValidation"] = "Credit Card inserted Unsuccessfully";

            } 
            else ViewData["credCardValidation"] = "Credit Card Validation Unsuccessful";



            Customers customer = new Customers();
            customer.Email = TempData["CustomerEmail"].ToString();

            foreach (Product product in cart)
            {
                totalPrice += totalPrice + product.Quantity * product.ProductPrice;

            }

            int counter1 = dao.AddOrder(Session.SessionID, DateTime.Now, customer.Email, totalPrice);
            int counter2 = 0;

            foreach (Product product in cart)
            {
                counter2 = dao.AddLineItem(product.Quantity,Session.SessionID, product.ProductID);

               
            }
            if (counter1 > 0 && counter2 > 0)
            {
                Console.Write("Perfectly Working");

            }
            else
            {
                ViewData["message"] = "Error Please Fix ASAP";
            }

            return View();
        }


        public ActionResult Delete(FormCollection form)
        {
            Product pr = new Product();
            bool found = false;

            if (Session["MemberName"] == null)
            {
                Console.WriteLine("Not Allowed");
            }
            else
            {
                for (int i = 0; i < cart.Count && !found; i++)
                {
                    if (cart[i].ProductID == form["id"].ToString())
                    {
                        pr = cart[i];
                     
                        found = true;
                        cart.Remove(cart[i]);

                    }
                }
            }
            return RedirectToAction("ViewCart");
        }


    }
    }

