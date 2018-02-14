using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Germaine.Models
{
    public class User
    {
        public int UserID {get; set;}
        
        public string Username {get; set;}

        public byte[] PasswordHash {get; set;}

        public byte[] PasswordSalt {get; set;}

        public int PersonID {get; set;}

        public string Modules {get; set;}

        public string Role {get; set;}

        public string Token {get; set;}

        public int PasswordReset { get; set; }

        public Person Person {get; set;}

        public string ProfilePic { get; set;}
        
        public DateTime CreatedAt {get; set;}

        public ICollection<Activity> Activity { get; set; }

        public User()
        {
            Activity = new Collection<Activity>();
        }
        
    }
}