using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WAndCAssignment.Models;

namespace WAndCAssignment.Controllers
{
    public class ProductsController : Controller
    {
  
          public ActionResult Index()
        {

            //create a list to store values from dao class
            List<Product> list;
            DAO dao = new DAO();
            list = dao.AllProducts();

            return View(list);
        }

        public ActionResult Laptop()
        {

            List<Product> list;
            DAO dao = new DAO();
            list = dao.AllLaptop();
            return View(list);

        }

        public ActionResult Electronic()
        {

            List<Product> list;
            DAO dao = new DAO();
            list = dao.Allelectronic();
            return View(list);

        }

        public ActionResult Smartphone()
        {

            List<Product> list;
            DAO dao = new DAO();
            list = dao.AllPhones();
            return View(list);

        }


    }
}