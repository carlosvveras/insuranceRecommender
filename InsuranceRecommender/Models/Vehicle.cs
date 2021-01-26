using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRecommender.Models
{
    public class Vehicle
    {
        [Required]
        public int? Year { get; set; }
    }
}
