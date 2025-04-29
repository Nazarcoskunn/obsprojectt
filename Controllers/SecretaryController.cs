using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using ObsBackend.Model; // UploadExamSchedule modelini kullanmak için
using ObsBackend.Data;

using System;


namespace obsproject.Controllers
{
    [Route("Secretary")]
    public class SecretaryController : Controller
    {
        private readonly AppDbContext _context; // DbContext örneği

        public SecretaryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Home")]
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet("ResitExamTime")]
        public IActionResult ResitExamTime()
        {
            return View();
        }

[HttpPost("ResitExamTime")]
public async Task<IActionResult> UploadResitExam(IFormFile file)
{
    if (file == null || file.Length == 0)
    {
        TempData["ErrorMessage"] = "No file selected.";
        return RedirectToAction("ResitExamTime");
    }

    var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
    if (!Directory.Exists(uploadsPath))
    {
        Directory.CreateDirectory(uploadsPath);
    }

    var fileName = Path.GetFileName(file.FileName);
    var filePath = Path.Combine(uploadsPath, fileName);

    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    var uploadExamSchedule = new UploadExamSchedule
    {
        FileName = fileName,
        FilePath = "/uploads/" + fileName
    };

    _context.UploadExamSchedules.Add(uploadExamSchedule);
    await _context.SaveChangesAsync();

    TempData["UploadedFilePath"] = uploadExamSchedule.FilePath; // Burası eklendi
    return RedirectToAction("ResitExamSucces");
}

        [HttpGet("ResitExamSucces")]
        public IActionResult ResitExamSucces()
        {
            return View();
        }
    }
}
