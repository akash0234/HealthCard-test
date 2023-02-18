using HealthCard.BAL.Services;
using HealthCard.Entity.Models;
using HealthCard.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HealthCard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IUserService userService)
        {
            _logger = logger;
            _configuration = configuration;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult HealthCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateHealthCard(User user)
        {
            if(!ModelState.IsValid)
            {

                var dataModel = await _userService.CreateAppUserAsync(user);

                return View("HealthCard", dataModel);
            }
            return View("HealthCard");
           
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}