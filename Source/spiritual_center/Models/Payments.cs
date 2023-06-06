using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace spiritual_center.Models
{
    public class Payments
    {
        [Key]
        public int Payment_Id { get; set; }
        [Required]
        public int Month { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Amount { get; set; }
        [ForeignKey("User")]
        public string ? User_Id { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }
    }
}
