using Microsoft.AspNetCore.Mvc;
using TestAAI.Interfaces;
using TestAAI.Models;

namespace TestAAI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int id)
        {
            var user = _userRepo.GetUserById(id);
            if (user == null)
            {
                ViewBag.Message = "Usuario no encontrado.";
                return View();
            }
            return View(user);
        }
    }
}
