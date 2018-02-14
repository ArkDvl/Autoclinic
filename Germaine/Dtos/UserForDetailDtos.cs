using System;
using System.ComponentModel.DataAnnotations;
using Germaine.Models;
using System.Collections.Generic;

namespace Germaine.Dtos
{
    public class UserForDetailDtos
    {
        public int UserID {get; set;}
        
        public string Username {get; set;}

        public string Modules {get; set;}

        public string Role {get; set;}

        public int Expiry { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public string ProfilePic { get; set; }

        public Person Person{ get; set; }

        public ICollection<ActivityForListDtos> Activity { get; set; }

    }
}