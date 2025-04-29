using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Http;

namespace ObsBackend.Model
{
    public class UploadGradeRequest
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [NotMapped]  // Bu, `IFormFile`'ın veritabanına kaydedilmeyeceğini belirtir.
        public IFormFile File { get; set; } = null!; // Yalnızca dosya yüklemek için kullanılan property

        // Dosyanın adını ve yolunu saklayacak veritabanı özellikleri
        public string FileName { get; set; } = null!;  // Dosya adı
        public string FilePath { get; set; } = null!;  // Dosyanın sunucudaki yolu
    }
}

