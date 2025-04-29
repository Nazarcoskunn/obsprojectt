using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.IO;
using ObsBackend.Model;
using ObsBackend.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ObsBackend.Controllers
{
    public class InstructorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<InstructorController> _logger;

        public InstructorController(AppDbContext context, ILogger<InstructorController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Home action'ı
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult CourseLetterGrade()
        {
            return View();
        }

         public IActionResult newlettergrade()
        {
            return View();
        }


public IActionResult AnnounceDetails()
{
    var courses = _context.Courses.Include(c => c.Instructor).ToList();
    return View(courses);
}

 public IActionResult resitclasslist()
        {
            return View();
        } 
        public IActionResult resitexamtime()
        {
            return View();
        } 
          public IActionResult ressitannounce()
        {
            return View();
        } 
        


        // Upload Grade Request action
        [HttpPost]
        public async Task<IActionResult> CourseLetterGrade(IFormFile file)
        {
            // Dosya seçilmemişse hata mesajı göster
            if (file == null || file.Length == 0)
            {
                return View("Error", new { message = "No file selected!" });
            }

            // Dosya türü ve boyutunu kontrol et
            var allowedExtensions = new[] { ".pdf", ".docx", ".doc" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(fileExtension))
            {
                return View("Error", new { message = "Invalid file type! Allowed types: .pdf, .docx, .doc" });
            }

            if (file.Length > 25 * 1024 * 1024) // 25 MB limit
            {
                return View("Error", new { message = "File size exceeds the 25 MB limit!" });
            }

            // Yükleme dizini oluştur
            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            // Dosya adını al ve dosya yolunu oluştur
            var fileName = Path.GetFileName(file.FileName); // Dosya adını al
            var filePath = Path.Combine(uploadsPath, fileName);

            try
            {
                // Dosyayı sunucuya kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Dosya bilgilerini veritabanına kaydet
                var uploadGradeRequest = new UploadGradeRequest
                {
                    FileName = fileName,
                    FilePath = "/uploads/" + fileName // Web üzerinden erişilebilen yol
                };

                _context.UploadGradeRequests.Add(uploadGradeRequest);
                await _context.SaveChangesAsync();

                // Başarı mesajı ekle
                TempData["SuccessMessage"] = "File uploaded and saved successfully!";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error uploading file: {ex.Message}");
                return View("Error", new { message = "File upload failed!" });
            }

            // Yükleme başarılı, yönlendir
            return RedirectToAction("Home", "Instructor");
        }

        // Logout action
        public IActionResult Logout()
        {
            // Oturumu temizle
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

   [HttpGet("ViewResitExamSchedule")]
        public IActionResult ViewResitExamSchedule()
        {
            // Veritabanından en son yüklenen dosyayı alıyoruz
            var lastUploaded = _context.UploadExamSchedules
                                .OrderByDescending(x => x.Id)
                                .FirstOrDefault();

            if (lastUploaded == null)
            {
                TempData["ErrorMessage"] = "No resit exam schedule uploaded yet.";
                return View();
            }

            ViewBag.FilePath = lastUploaded.FilePath; // Dosya yolunu ViewBag ile gönderiyoruz
            return View();
        }

[HttpPost]
public async Task<IActionResult> PostExamAnnouncement(int ExamId, string Message)
{
    if (string.IsNullOrWhiteSpace(Message))
    {
        TempData["ErrorMessage"] = "Message cannot be empty.";
        return RedirectToAction("AnnounceDetails");
    }

    var announcement = new ExamAnnouncement
    {
        ExamId = ExamId,
        Message = Message
    };

    _context.ExamAnnouncements.Add(announcement);
    await _context.SaveChangesAsync();

    TempData["SuccessMessage"] = "Announcement shared successfully!";
    return RedirectToAction("AnnounceDetails");
}
public async Task AddExamAnnouncementAsync(ExamAnnouncement examAnnouncement, int examId)
{
    // ResitExam kaydının varlığını kontrol et
    var resitExamExists = await _context.ResitExams.AnyAsync(re => re.Id == examId);
    if (!resitExamExists)
    {
        throw new Exception("Eksik ResitExam kaydı.");
    }

    // Eğer ResitExam kaydı varsa, ExamAnnouncement kaydını ekle
    _context.ExamAnnouncements.Add(examAnnouncement);
    await _context.SaveChangesAsync();
}


        
    }
}