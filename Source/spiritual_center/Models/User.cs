using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace spiritual_center.Models
{
    public class User
    {
        [Key]
        public string User_Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string MidleName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string ? Photo_URL { get; set; } 
        public int ? Flat_Number { get; set; } 
        public string ? Area { get; set; } 
        public string ? State { get; set; } 
        public string ? City { get; set; } 
        public int ? PinCode { get; set; }
        [Required]
        public DateTime  Initiation_Date { get; set; }
        [Required]
        public byte[]  PasswardHash { get; set; }
        [Required]
        public byte[]  PasswardSalt { get; set; }
        [JsonIgnore]
        public virtual  ICollection<Payments>? Payments { get; set; }

    }
}
