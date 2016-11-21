using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WAndCAssignment.Models;

namespace WAndCAssignment.Controllers
{
    public class ShoppingCartController : Controller
    {



        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult OrderNow(string ProductId)
        {
            DAO dao = new DAO();

            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(dao.GetProductById(ProductId), 1));
                Session["cart"] = cart;
            }
            return View("cart");
        }

        public ActionResult Login()
        {

            return RedirectToAction("Login", "Home");
        }

    }
}