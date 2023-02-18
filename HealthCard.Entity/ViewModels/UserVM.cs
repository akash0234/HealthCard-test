using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCard.Entity.ViewModels
{
    public class UserVM
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; } 
        public string LastName { get; set; }
        public string ProfileImagePath { get; set; }
        public string HealthID { get; set; }
        public string PHRAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string QRCode { get; set; }

    }
   
}