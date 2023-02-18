using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCard.Entity.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(40)]
        public string FirstName { get; set; }

        public string MiddleName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; }

        [Required]

        public string ProfileImagePath { get; set; }

        [NotMapped]
        public IFormFile? ProfileImage { get; set; }

        [Required]
        public string PHRAddress { get; set; }

        [Required]
        [MinLength(10), MaxLength(10)]
        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]

        public string QRCode { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime UpdatedDate { get; set;}
      






    }
   
}