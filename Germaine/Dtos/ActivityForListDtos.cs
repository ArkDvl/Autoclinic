using System;
using System.ComponentModel.DataAnnotations;
using Germaine.Models;

namespace Germaine.Dtos
{
    public class ActivityForListDtos
    {
        public int ActivityID {get; set;}
        
        public string IPAddress {get; set;}
        
        public DateTime TimeLoggedIn {get; set;}

        public DateTime? TimeLoggedOut {get; set;}

    }
}