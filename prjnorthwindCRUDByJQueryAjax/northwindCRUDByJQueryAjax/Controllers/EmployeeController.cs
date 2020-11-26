using northwindCRUDByJQueryAjax.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace northwindCRUDByJQueryAjax.Controllers
{
    public class EmployeeController : Controller
    {
        NorthwindEntities db = new NorthwindEntities();
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAll()
        {
            return View(GetAllEmployee());
        }

        IEnumerable<Employees> GetAllEmployee()
        {
            var details = db.Employees.ToList<Employees>();
            return details;
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            Employees emp = new Employees();
            if (id != 0)
            { 
                emp = db.Employees.Where(a => a.EmployeeID == id).FirstOrDefault<Employees>();
            }
            return View(emp);
        }
        [HttpPost]
        public ActionResult AddOrEdit(Employees emp)
        {
            try
            {
                if (emp.fImageUpload != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(emp.fImageUpload.FileName);
                    string extension = Path.GetExtension(emp.fImageUpload.FileName);
                    //合併湊成檔名
                    filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
                    emp.PhotoPath = "~/AppFiles/images/" + filename;
                    emp.fImageUpload.SaveAs(Path.Combine(Server.MapPath("~/AppFiles/images/"), filename));
                }
                if (emp.EmployeeID == 0)
                {   //找不到，則新增
                    db.Employees.Add(emp);
                    db.SaveChanges();
                }
                else
                {   //有找到，就更新
                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = "成功新增!!"  }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false,message = e.Message },JsonRequestBehavior.AllowGet) ;
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                Employees emp = db.Employees.Where(a => a.EmployeeID == id).FirstOrDefault();
                db.Employees.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, html = GlobalClass.RenderRazorViewToString(this, "ViewAll", GetAllEmployee()), message = "成功刪除!!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}