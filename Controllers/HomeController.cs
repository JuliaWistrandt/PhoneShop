using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models;
using System.Diagnostics;
using PhoneShop.Service;


namespace PhoneShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private PhoneService _service; // в папке PhoneService лежит класс отражающий нашу бизнес логику, см описание к коду отдельно где детально написано что к чему


        public HomeController(ILogger<HomeController> logger, PhoneService service) // соединяем в том числе и PhoneService в конструкторе контроллера
        {
            _logger = logger;
            _service= service;
        }

        public IActionResult Index() // добавляем вкладку странички по умолчанию
        {
            return View();
        }

        
        public IActionResult AddPhonePage()  // добавляем вкладку AddPhonePage, внутри которой будет приходить отправка данных о новом телефоне в базу данных, см следующий блок кода
        {
            return View();
        }

        [HttpPost] // этот тэг очень важен, без него код не знает что запрос придет от html
        public IActionResult AddPhone(Phone phone)
        {
            _service.AddPhoneToList(phone);
            return RedirectToAction("Index"); // после добавления нового телефона переключимся на страничку по умолчанию чтобы юзеру было ясно что телефон добавился
        }

        public IActionResult DeletePhonePage()  // добавляем вкладку AddPhonePage, внутри которой будет приходить отправка данных о новом телефоне в базу данных, см следующий блок кода
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchPhone()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchPhone(string id)
        {
            var temp = _service.ShowPhoneById(id);
            if (temp == null)
            {
                return RedirectToAction("NotFoundPage");
            }
            else
            {

                return View("PhoneDitails" + temp);
            }
            
        }

        [HttpPost] 
        public IActionResult DeletePhone(string id)
        {
            _service.RemovePhone(id);
            return RedirectToAction("Index");


        }


        public IActionResult AllPhonesPage()
        {
            
            return View(_service.ShowPhones()); // _service описывает нашу бизнес-логику, внутри которой лежит метод ShowPhones см отдельный файл с объяснением кода
        }

        public IActionResult NotFoundPage()
        {

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // это по умолчанию тут было, сообщение об ошибке
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}