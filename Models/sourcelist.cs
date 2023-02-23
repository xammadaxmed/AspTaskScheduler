using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSchedular.Models
{
    public class sourcelist
    {
        [Key]
        public int Id { get; set; }
        public string? campaignname { get; set; }
        public string? sourcename { get; set; }
        public string? list { get; set; }
    }
}
