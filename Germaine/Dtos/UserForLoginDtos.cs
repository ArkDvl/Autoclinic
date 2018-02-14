using System.ComponentModel.DataAnnotations;

namespace Germaine.Dtos
{
    public class UserForLoginDtos
    {
        [Required]
        public string Username {get; set;}

        [Required]
        public string Password {get; set;}

        [Required]
        public string IPAddress {get; set;}

    }
}