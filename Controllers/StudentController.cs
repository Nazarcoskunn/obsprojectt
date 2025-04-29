using Microsoft.AspNetCore.Mvc;

namespace obsproject.Controllers
{
    [Route("Student")]
    public class StudentController : Controller
    {
        [HttpGet("Home")]
        public IActionResult Home()
        {
            return View();
        }

[HttpGet("attendresitexam")]
         public IActionResult attendresitexam()
        {
            return View();
        }
[HttpGet("courselettergrade")]
         public IActionResult courselettergrade()
        {
            return View();
        }
[HttpGet("newlettergrade")]
         public IActionResult newlettergrade()
        {
            return View();
        }
[HttpGet("resitexamdetails")]
         public IActionResult resitexamdetails()
        {
            return View();
        }
        [HttpGet("resitexamtime")]
         public IActionResult resitexamtime()
        {
            return View();
        }

      
    }
}