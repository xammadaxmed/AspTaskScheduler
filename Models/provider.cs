using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSchedular.Models
{
    public class provider
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string auth_key { get; set; }
        public string notes { get; set; }
        public string contact { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string zip { get; set; }
        public string address { get; set; }
        public string state { get; set; }

    
      
    }
}
