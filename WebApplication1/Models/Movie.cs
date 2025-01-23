using System.ComponentModel.DataAnnotations;


namespace WebApplication1.Models;



public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tytuł jest wymagany")]
    [StringLength(100)]
    public string Title { get; set; }

    [Required(ErrorMessage = "Reżyser jest wymagany")]
    [StringLength(100)]
    public string Director { get; set; }

    [Range(1900, 2100)]
    public int Year { get; set; }

    public string Genre { get; set; }
}