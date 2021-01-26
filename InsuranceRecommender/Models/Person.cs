using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceRecommender.Models
{
    public class Person
    {
  //      "age": 35,
  //"dependents": 2,
  //"house": {"ownership_status": "owned"},
  //"income": 0,
  //"marital_status": "married",
  //"risk_questions": [0, 1, 0],
  //"vehicle": {"year": 2018}
        [Required]
        public int Age { get; set; }
        [Required]
        public int Dependents { get; set; }
        public House House { get; set; }
        [Required]
        public float Income { get; set; }
        [Required]
        public string Marital_status { get; set; }
        [Required]
        public bool[] Risk_questions { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
