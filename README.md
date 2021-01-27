# insuranceRecommender

Abstract: An API which offers its users an insurance package personalized to their specific needs without requiring the user to understand anything about insurance.

1. Instructions

  1.1 Running the Code
  - Download and run the InsuranceRecommender Project it in Visual Studio 2019
  - The project will redirect you to https://localhost:5001/swagger/ui/index.html, which is the swagger (a tool for sending the json file to the API)
  - Open the GET /api/Recommendation function and click in "Try it Out" to allow to input data
  - After filling the inputs, click on execute

  PS: The Swagger can not limitate the amount of data on Risk_questions field, so please consider making it an array with 3 elements.
  
  1.2 Running Tests
  - Open the Test Explorer (View > Test Explorer) in Visual Studio 19
  - All the test will be listed and you can Run or Debug then in the inner menu
  
  
2. Technical Decisions
  - I've decided to use .NET Core framework because is the one that I am more familiar with
  - The decision to use Swagger tool was made to make the input of data easier to make tests and see the API working
  
 

