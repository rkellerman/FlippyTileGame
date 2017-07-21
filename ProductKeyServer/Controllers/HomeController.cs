using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductKeyServer.ViewModels;

namespace ProductKeyServer.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            var vm = new KeyViewModel();
            vm.HandleRequest();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Index(KeyViewModel vm)
        {
            vm.IsValid = ModelState.IsValid;

            vm.HandleRequest();

            if (vm.IsValid)
            {
                ModelState.Clear();
            }
            else
            {
                foreach (var item in vm.ValidationErrors)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }


            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}