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
    }
}