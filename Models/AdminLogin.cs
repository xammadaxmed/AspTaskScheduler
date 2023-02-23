using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Money_Finder.Models
{
    public class AdminLogin
    {
        [Key]
        public int AdminLoginId { get; set; }
      
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gmail { get; set; }
        public string Contact { get; set; }
        public string SystemRoll { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public string SkypeAddress { get; set; }
        public string DateTime { get; set; }
        public string TryCount { get; set; }
    }
}
