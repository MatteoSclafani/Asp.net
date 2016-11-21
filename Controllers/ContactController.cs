using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WAndCAssignment.Models;

namespace WAndCAssignment.Controllers
{
    public class ContactController : Controller
    {

        static DataSet ds;
        static DataTable dt;

        // GET: Contact
        public ActionResult Index()
        {
            if (System.IO.File.Exists(Server.MapPath(@"~/comments.xml")))
            {

                ds = new DataSet();

                ds.ReadXml(Server.MapPath(@"~/comments.xml"));

                dt = ds.Tables[0];

            }
            else
            {
                ds = new DataSet("user_comments");
                dt = new DataTable("user_comment");
                dt.Columns.Add("name");
                dt.Columns.Add("email");
                dt.Columns.Add("comments");
                ds.Tables.Add(dt);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Comments(FormCollection form, ContactModel contact)
        {
            contact = new ContactModel();

            if (ModelState.IsValid)
            {
                DataRow row = dt.NewRow();

                row["name"] = form["Name"];
                row["email"] = form["Email"];
                row["comments"] = form["Comments"];

                dt.Rows.Add(row);
                ds.WriteXml(Server.MapPath(@"~/comments.xml"));

                return View();
            }
            else return View("Index", contact);
        }

        public ActionResult ShowFeedback()
        {

            List<ContactModel> list = new List<ContactModel>();

            if (System.IO.File.Exists(Server.MapPath(@"~/comments.xml")))
            {

                DataSet ds = new DataSet();

                ds.ReadXml(Server.MapPath(@"~/comments.xml"));

                DataTable dt = ds.Tables["user_comment"];


                foreach (DataRow row in dt.Rows)
                {
                    ContactModel contact = new ContactModel();
                    contact.Name = row["name"].ToString();
                    contact.Email = row["email"].ToString();
                    contact.Comments = row["comments"].ToString();
                    list.Add(contact);
                }

            }
            return View(list);
        }




    }
}