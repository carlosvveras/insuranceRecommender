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

        [Fact]
        public void TestRecommendationController()
        {
            Person input = TestDefaultData();
            Recommendation reco = _sut.GetRecommendation(input);

            Recommendation expected = ExpectedReturn();

            Assert.Equal(expected.Auto, reco.Auto);
            Assert.Equal(expected.Disability, reco.Disability);
            Assert.Equal(expected.Home, reco.Home);
            Assert.Equal(expected.Life, reco.Life);
        }


        [Fact]
        public void TestAgeIneligibility()
        {
            Person input = TestAgeIneligibilityData();
            Recommendation reco = _sut.GetRecommendation(input);

            Recommendation expected = ExpectedAgeIneligibilityReturn();

            Assert.Equal(expected.Disability, reco.Disability);
            Assert.Equal(expected.Life, reco.Life);
        }


        [Fact]
        public void TestHomeIneligibility()
        {
            Person input = TestHomeIneligibilityData();
            Recommendation reco = _sut.GetRecommendation(input);

            Recommendation expected = ExpectedHomeIneligibilityReturn();

            Assert.Equal(expected.Home, reco.Home);
        }

        [Fact]
        public void TestDisabilityIneligibility()
        {
            Person input = TestDisabilityIneligibilityData();
            Recommendation reco = _sut.GetRecommendation(input);

            Recommendation expected = ExpectedDisabilityIneligibilityReturn();

            Assert.Equal(expected.Disability, reco.Disability);
        }


        [Fact]
        public void TestAutoIneligibility()
        {
            Person input = TestAutoIneligibilityData();
            Recommendation reco = _sut.GetRecommendation(input);

            Recommendation expected = ExpectedAutoIneligibilityReturn();

            Assert.Equal(expected.Auto, reco.Auto);
        }


        public static Person TestDefaultData()
        {
            return new Person
            {
                Age = 35,
                Dependents = 2,
                House = new House { OwnershipStatus = "owned" },
                Income = 0,
                Marital_status = "married",
                Risk_questions = new bool[] { false, true, false },
                Vehicle = new Vehicle { Year = 2018 }
            };
        }

        public static Person TestAgeIneligibilityData()
        {
            return new Person
            {
                Age = 61,
                Dependents = 2,
                House = new House { OwnershipStatus = "owned" },
                Income = 80000,
                Marital_status = "married",
                Risk_questions = new bool[] { true, true, false },
                Vehicle = new Vehicle { Year = 2019 }
            };
        }

        public static Person TestHomeIneligibilityData()
        {
            return new Person
            {
                Age = 35,
                Dependents = 2,
                House = new House { OwnershipStatus = "" },
                Income = 30000,
                Marital_status = "single",
                Risk_questions = new bool[] { false, true, false },
                Vehicle = new Vehicle { Year = 2012 }
            };
        }

        public static Person TestDisabilityIneligibilityData()
        {
            return new Person
            {
                Age = 35,
                Dependents = 0,
                House = new House { OwnershipStatus = "mortgaged" },
                Income = 0,
                Marital_status = "single",
                Risk_questions = new bool[] { false, true, true },
                Vehicle = new Vehicle { Year = 2012 }
            };
        }

        public static Person TestAutoIneligibilityData()
        {
            return new Person
            {
                Age = 35,
                Dependents = 2,
                House = new House { OwnershipStatus = "owned" },
                Income = 30000,
                Marital_status = "single",
                Risk_questions = new bool[] { false, true, false },
                Vehicle = null
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


        public static Recommendation ExpectedAgeIneligibilityReturn()
        {
            return new Recommendation
            {
                Disability = "ineligible",
                Life = "ineligible"
            };
        }

        public static Recommendation ExpectedHomeIneligibilityReturn()
        {
            return new Recommendation
            {
                Home = "ineligible"
            };
        }

        public static Recommendation ExpectedDisabilityIneligibilityReturn()
        {
            return new Recommendation
            {
                Disability = "ineligible"
            };
        }

        public static Recommendation ExpectedAutoIneligibilityReturn()
        {
            return new Recommendation
            {
                Auto = "ineligible"
            };
        }




    }

}
