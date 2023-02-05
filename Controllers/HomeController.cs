using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models;
using System.Diagnostics;
using PhoneShop.Service;


namespace PhoneShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private PhoneService _service; 


        public HomeController(ILogger<HomeController> logger, PhoneService service) 
        {
            _logger = logger;
            _service= service;
        }

        public IActionResult Index() 
        {
            return View();
        }

        
        public IActionResult AddPhonePage()  
        {
            return View();
        }

        [HttpPost] 
        public IActionResult AddPhone(Phone phone)
        {
            _service.AddPhoneToList(phone);
            return RedirectToAction("Index"); 
        }


        public IActionResult SearchPhonePage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SearchPhone(string id)
        {
            var temp = _service.ShowPhoneById(id);
            if (temp == null)
            {
                return RedirectToAction("NotFoundPage");
            }
            else
            {

                return RedirectToAction("PhoneDitailsPage" + temp);
            }

        }

        public IActionResult PhoneDitailsPage()
        {
            return View(_service.ShowPhoneById);
        }


        public IActionResult AllPhonesPage()
        {     
            return View(_service.ShowPhones()); 
        }


        public IActionResult NotFoundPage()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] 
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult DeletePhonePage()  
        //{
        //    return View();
        //}

        //[HttpPost] 
        //public IActionResult DeletePhone(string id)
        //{
        //    _service.RemovePhone(id);
        //    return RedirectToAction("Index");
        //}

    }
}