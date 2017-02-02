using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace dotnet_vsr.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Repeat password")]
        public string Password2 { get; set; }
    }
}