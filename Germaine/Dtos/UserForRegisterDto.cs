using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Germaine.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username {get; set;}

        [Required]
        public string Password {get; set;}

        [Required]
        public string IPAddress {get; set;}

        // public int PersonID {get; set;}

        // public string Modules {get; set;}

        // public string Role {get; set;}

        // public string Token {get; set;}

        // [DataType(DataType.Date)]
        // public DateTime CreatedAt {get; set;}

    }
}