using InsuranceRecommender.Controllers;
using InsuranceRecommender.Models;
using Xunit;

namespace InsuranceRecommender.TestAnalyzers
{
    public class RecommendationTest
    {
        private readonly RecommendationController _sut;

        public RecommendationTest()
        {
            _sut = new RecommendationController();
        }

        public static Person TestData()
        {
            return new Person
            {
                Age = 35,
                Dependents = 2,
                House = new House { OwnershipStatus = "owned" },
                Income = 0,
                Marital_status = "married",
                Risk_questions = new int[] { 0, 1, 0 },
                Vehicle = new Vehicle { Year = 2018 }
            };
        }

        public static Recommendation ExpectedReturn()
        {
            return new Recommendation
            {
                Auto = "regular",
                Disability = "ineligible",
                Home = "economic",
                Life = "regular"
            };
        }

        [Fact]
        public async void TestRecommendationController()
        {
            Person input = TestData();
            Recommendation reco = await _sut.GetRecommendation(input);

            Recommendation expected = ExpectedReturn();

            Assert.Equal(expected.Auto, reco.Auto);
            Assert.Equal(expected.Disability, reco.Disability);
            Assert.Equal(expected.Home, reco.Home);
            Assert.Equal(expected.Life, reco.Life);
        }
    }
}
