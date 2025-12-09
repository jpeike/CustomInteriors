using Core;
using System.ComponentModel.DataAnnotations;

public class CreateQuoteRequestModel
{

    [Required]
    [Range(1, int.MaxValue)]
    public int JobId { get; set; }

    [StringLength(10000)]
    public string? DescriptionOfWork { get; set; }

    [Range(typeof(decimal), "0.00", "999999999.99")]
    public decimal? EstimatedPrice { get; set; }
}