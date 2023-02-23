using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskSchedular.Models
{
    public class sourcelead
    {
        [Key]
        public int Id { get; set; }
        public int? user_id { get; set; }
        public int? group_id { get; set; }
        public int? source_id { get; set; }
        public string? zip { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? url { get; set; }
        public string? esp { get; set; }
        public string? optin_date { get; set; }
        public string? timezone_dst_flag { get; set; }
        public string? exception { get; set; }
        public string? status { get; set; }
        public string? added_source { get; set; }
        public string? lu_carrier_code { get; set; }
        public string? lu_international_format { get; set; }
        public string? lu_local_format { get; set; }
        public string? lu_carrier_name { get; set; }
        public string? lu_carrier_error_code { get; set; }
        public string? created_at { get; set; }
        public string? updated_at { get; set; }
        
        public string? list { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? dob { get; set; }
        public string? age { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? datecreated { get; set; }
        public string? auth_key { get; set; }
        public string? asignto { get; set; }
        public string? archieve { get; set; }

    }
}
