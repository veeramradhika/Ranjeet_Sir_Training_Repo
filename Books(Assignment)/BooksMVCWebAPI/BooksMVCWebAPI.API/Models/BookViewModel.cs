using System.ComponentModel.DataAnnotations;

namespace BooksMVCWebAPI.API.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string BookZoner { get; set; }
        [DataType(DataType.Date)]
        public DateTime RelaseDate { get; set; }
        public int Cost { get; set; }
        [Display(Name ="Image")]
        public string BookImage { get; set; }
    }
}
