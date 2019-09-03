using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp.Models;



namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        List<ClassTask> TaskList;
        DataStore ds;

        public HomeController()
        {
            ds = new DataStore();
        }        

        public IActionResult Index()
        {            
            ViewBag.Tasks = ds.GetTaskList();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CreateNewTask(string tName)
        {
            if (!ds.TaskExists(tName))
            {
                ClassTask tsk = new ClassTask(tName);
                ds.SaveTask(tsk);
                if (ModelState.IsValid)
                {
                    ModelState.Clear();
                }
                
                ViewBag.Tasks = ds.GetTaskList();
            }            
            return View("~/Views/Home/Index.cshtml");
        }

        
        public IActionResult ShowTaskList(string viewMode)
        {
            ViewBag.ViewMode = viewMode;                      
            ViewBag.Tasks = ds.GetTaskList(viewMode); ;
            return View("~/Views/Home/Index.cshtml");
        }
                       

        public IActionResult CheckAsExecute(string taskName)
        {
            ds.ChangeTaskStatus(taskName);
            ViewBag.Tasks = ds.GetTaskList() ;
            return View("~/Views/Home/Index.cshtml");
        }

        public IActionResult DeleteTask(string tName)
        {
            ds.DeleteTask(tName);           
            ViewBag.Tasks = ds.GetTaskList();
            return View("~/Views/Home/Index.cshtml");
        }

    }
}
