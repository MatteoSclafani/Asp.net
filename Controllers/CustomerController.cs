using System.Web.Mvc;
using WAndCAssignment.Models;
using System.Web.Helpers;

namespace WAndCAssignment.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(FormCollection form, Customers member)
        {
            int count = 0;
            DAO dao = new DAO();
            member = new Customers();

            if (ModelState.IsValid)
            {
                member.FirstName = form["FirstName"];
                member.LastName = form["LastName"];
                member.Email = form["Email"];
                member.Username = form["Username"];
                member.Password = form["Password"];

                string confirmPass = form["ConfirmPassword"];
                if (member.Password != confirmPass)
                {
                    ViewData["error"] = "Password Do Not Match";
                }
                else
                {

                    count = dao.Insert(member);
                    if (count > 0)
                    {
                        ViewData["error"] = "Your Account Has Been Created Successfully You May Now Login";
                    }
                    else
                    ViewData["error"] = "Error: " + dao.message;
                }
              
            }
            return View();

        }



    }
}