using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class AddressModel
    {
        public int AddressId { get; set; }
        
        [Required] // model data annotations, used to validate model when entering controller action
        [Range(1, int.MaxValue)]
        public int CustomerId { get; set; }
        
        [Required]
        [StringLength(255)]
        public required string Street { get; set; }
        
        [Required]
        [StringLength(255)]
        public required string City { get; set; }
        
        [Required]
        [StringLength(255)]
        public required string State { get; set; }
        
        [Required]
        [Range(501, 99950)] // us zip code range
        public required int PostalCode { get; set; }
        
        [StringLength(255)]
        public string? Country { get; set; }
        
        [Required]
        [StringLength(255)]
        public required string AddressType { get; set; }
    }
}
