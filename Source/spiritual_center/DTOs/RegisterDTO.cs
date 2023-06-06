using System.Text.Json.Serialization;

namespace spiritual_center.DTOs
{
    public class RegisterDTO
    {
        public string FirstName { get; set; } = string.Empty;
    
        public string MidleName { get; set; } = string.Empty;
       
        public string LastName { get; set; } = string.Empty;
       
        public string Email { get; set; } = string.Empty;

        public DateTime Initiation_Date { get; set; }

        [JsonIgnore]
        public string ? Passward { get; set; }

    }
}
