using System.ComponentModel.DataAnnotations;

namespace BooksLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [MaxLength(100)] public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        [MaxLength(100)] public string Author { get; set; } = "";
        [MaxLength(100)] public string Genre { get; set; } = "";
        public DateTime AddedAt { get; set; }

    }
}
