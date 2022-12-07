using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ImageGallery.Models
{
    public class Movie
    {   
        public string? Id { get; set; }

        [Display(Name = "Movie Name"), DataType(DataType.Text)]
        public string? MovieName { get; set; }

        [Display(Name = "File Name"), DataType(DataType.Text)]
        public string? FileName { get; set; }

        [Display(Name = "Upload Date"), DataType(DataType.Date)]
        public string? UploadDate { get; set; }
    }
}
