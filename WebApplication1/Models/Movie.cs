using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models;



public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tytuł jest wymagany")]
    [StringLength(100, ErrorMessage = "Tytuł może mieć maksymalnie 100 znaków")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Gatunek jest wymagany")]
    public string Genre { get; set; }

    [Range(1900, 2100, ErrorMessage = "Rok musi być w zakresie 1900-2100")]
    public int Year { get; set; }
}
