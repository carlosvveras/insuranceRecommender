using Microsoft.AspNetCore.Mvc;
using InsuranceRecommender.Models;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Threading.Tasks;

namespace InsuranceRecommender.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        [HttpGet]
        public Recommendation GetRecommendation([FromQuery] Person person)
        {
            Recommendation reco = new Recommendation();

            //RISK SCORES
            int base_score = 0;
            int autoRS = 0;
            int disabilityRS = 0;
            int homeRS = 0;
            int lifeRS = 0;

            foreach(bool riskQuestion in person.Risk_questions)
            {
                if(riskQuestion)
                {
                    base_score += 1;
                }
            }

            reco = SearchForIneligibility(person);

            //If the user is under 30 years old, deduct 2 risk points from all lines of insurance.If she is between 30 and 40 years old, deduct 1.
            if(person.Age < 30)
            {
                autoRS -= 2;
                disabilityRS -= 2;
                homeRS -= 2;
                lifeRS -= 2;
            }
            else if(person.Age >= 30 && person.Age <= 40)
            {
                autoRS -= 1;
                disabilityRS -= 1;
                homeRS -= 1;
                lifeRS -= 1;
            }

            //If her income is above $200k, deduct 1 risk point from all lines of insurance.
            if (person.Income > 200000)
            {
                autoRS -= 1;
                disabilityRS -= 1;
                homeRS -= 1;
                lifeRS -= 1;
            }

            //If the user's house is mortgaged, add 1 risk point to her home score and add 1 risk point to her disability score.
            if(person.House != null && person.House.OwnershipStatus == "mortgaged")
            {
                disabilityRS += 1;
                homeRS += 1;
            }

            //If the user has dependents, add 1 risk point to both the disability and life scores.
            if(person.Dependents > 0)
            {
                disabilityRS += 1;
                lifeRS += 1;
            }

            //If the user is married, add 1 risk point to the life score and remove 1 risk point from disability.
            if (person.Marital_status == "married")
            {
                lifeRS += 1;
                disabilityRS -= 1;
            }

            //If the user's vehicle was produced in the last 5 years, add 1 risk point to that vehicle’s score.
            if(person.Vehicle != null && person.Vehicle.Year >= (DateTime.Now.Year - 5))
            {
                autoRS += 1;
            }

            if(reco.Auto != "ineligible")
            {
                reco.Auto = CalculateRiskScore(autoRS + base_score);
            }
            if (reco.Disability != "ineligible")
            {
                reco.Disability = CalculateRiskScore(disabilityRS + base_score);
            }
            if (reco.Home != "ineligible")
            {
                reco.Home = CalculateRiskScore(homeRS + base_score);
            }
            if (reco.Life != "ineligible")
            {
                reco.Life = CalculateRiskScore(lifeRS + base_score);
            }

            return reco;
        }

        private Recommendation SearchForIneligibility(Person person)
        {
            Recommendation reco = new Recommendation();

            //If the user doesn’t have income, vehicles or houses, she is ineligible for disability, auto, and home insurance, respectively.
            if (person.Income == 0)
            {
                reco.Disability = "ineligible";
            }
            if (person.Vehicle == null || person.Vehicle.Year == 0)
            {
                reco.Auto = "ineligible";
            }
            if (person.House == null || String.IsNullOrEmpty(person.House.OwnershipStatus))
            {
                reco.Home = "ineligible";
            }
            //If the user is over 60 years old, she is ineligible for disability and life insurance.
            if (person.Age > 60)
            {
                reco.Disability = "ineligible";
                reco.Life = "ineligible";
            }

            return reco;
        }

//        This algorithm results in a final score for each line of insurance, which should be processed using the following ranges:
//          0 and below maps to “economic”.
//          1 and 2 maps to “regular”.
//          3 and above maps to “responsible”.
        private string CalculateRiskScore(int riskScore)
        {
            string result = null;
            if(riskScore <= 0)
            {
                result = "economic";
            }
            else if(riskScore >= 1 && riskScore < 3)
            {
                result = "regular";
            }
            else
            {
                result = "responsible";
            }

            return result;
        }


       
    }

}