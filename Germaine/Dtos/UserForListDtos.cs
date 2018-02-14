using System;
using System.ComponentModel.DataAnnotations;
using Germaine.Models;
using System.Collections.Generic;

namespace Germaine.Dtos
{
    public class UserForListDtos
    {
        public int UserID {get; set;}
        
        public string Username {get; set;}

        public string Modules {get; set;}

        public string Role {get; set;}
        
        public DateTime CreatedAt {get; set;}

        public int Expiry { get; set; }

        public string ProfilePic { get; set; }

        public PersonForListDtos Person{ get; set; }

        public ICollection<ActivityForListDtos> Activity { get; set; }

    }
}