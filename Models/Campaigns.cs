using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSchedular.Models
{
    public class Campaigns
    {
        [Key]
        public int Id { get; set; }
        public string? campaignname { get; set; }
        public string? sourcename { get; set; }
        public string? list { get; set; }
        public int? list_id { get; set; }
        public string? customer { get; set; }
        public string? auth_key { get; set; }
        public string? notes { get; set; }
        public string? totalleads { get; set; }
        public string? rate { get; set; }
        public string? PostponeDelivery { get; set; }
        public string? label { get; set; }
        public string? deliverythrottle { get; set; }
        public string? leadperday { get; set; }
        public string? validresponses { get; set; }
        public string? destination { get; set; }
        public string? exclusivity { get; set; }
        public string? deliverymethod { get; set; }
        public string? Rejectedtoday { get; set; }
        public string? asignedtoday { get; set; }
        public string? totalasign { get; set; }
        public string? status { get; set; }
        public string? totalremaining { get; set; }
        public string? datetime { get; set; }
        public string? todayremaining { get; set; }
        public string? esp { get; set; }
        public string? espa { get; set; }
        public string? espb { get; set; }
        public string? espc { get; set; }
        public string? espd { get; set; }
        public string? espe { get; set; }
        
      
    }
}
