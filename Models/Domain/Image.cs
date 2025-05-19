using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalksAPI.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }
        [NotMapped]
        public required IFormFile File { get; set; } //File holds the in-memory file.
        public required string FileName { get; set; }
        public string? FileDescription { get; set; }
        public required string FileExtension { get; set; } //like jpg , png
        public required long FileSizeInBytes { get; set; }
        public required string FilePath { get; set; }

    }
}