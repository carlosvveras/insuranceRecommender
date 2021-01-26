namespace InsuranceRecommender.Models
{
    public class Recommendation
    {
        // "auto": "regular",
        // "disability": "ineligible",
        // "home": "economic",
        // "life": "regular"
        public string Auto { get; set; }
        public string Disability { get; set; }
        public string Home { get; set; }
        public string Life { get; set; }
    }
}