using NorthwindDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthwindDemo.Controllers
{
    public class EmployeeController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        // GET: Employee
        public ActionResult Index()
        {
            var detail = db.Employees.ToList();
            return View(detail);
        }

        public ActionResult Details(int id)
        {
            var detail = db.Employees.Where(a=>a.EmployeeID == id).FirstOrDefault();
            return View(detail);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}