using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Germaine.Models
{
    public class Activity
    {
        public int ActivityID {get; set;}
        
        public string IPAddress {get; set;}
        
        public DateTime TimeLoggedIn {get; set;}

        public DateTime? TimeLoggedOut {get; set;}

        public int UserID {get; set;}   

        public User User {get; set;}         
    }
}