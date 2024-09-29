using abdp.Service.IServices;
using abdp.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace abdp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeService _service;

        public HomeController(IEmployeeService employeeService)
        {
            _service = employeeService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            List<string> listEmployees = _service.GetAllEmployee();
            ViewBag.ListEmployees = listEmployees;

            return View();
        }
    }
}