using Microsoft.AspNetCore.Mvc;
using ObsBackend.Data;
using ObsBackend.Model;

namespace ObsBackend.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Giriş başarılıysa Session veya Cookie atayabilirsin
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserRole", user.Role);

                // Role göre yönlendirme
                string redirectUrl = user.Role switch
                {
                    "student" => "/Student/Home",
                    "instructor" => "/instructor/Home",
                    "secretary" => "/Secretary/Home",
                    _ => "/home" // Varsayılan yönlendirme
                };

                return Redirect(redirectUrl); // Yönlendirme
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid email or password.";
                return View();
            }
        }
    }
}
