using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSchedular.Models
{
    public class sendoncosmolead
    {
        [Key]
        public int Id { get; set; }
        public string first_name { get; set; }
        public string dob { get; set; }
        public string age { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string datecreated { get; set; }
        public string auth_key { get; set; }

    }
}
