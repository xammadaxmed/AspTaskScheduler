using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSchedular.Models
{
    public class source
    {

        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? auth_key { get; set; }
        public string? datecreated { get; set; }
        public string? dupe_check { get; set; }
        public string? dupcount { get; set; }
        public string? dupcounttoday { get; set; }
        public string? totalleads { get; set; }
        public string? rate { get; set; }
        public string? status { get; set; }
        public string? sourcecompanyname { get; set; }
        public string? asignto { get; set; }



    }
}
