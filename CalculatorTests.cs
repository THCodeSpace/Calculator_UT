using CalculatorApi;
using CalculatorApi.Controllers;
using CalculatorApi.Models;
using CalculatorApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Calculator_UT
{
    [TestClass]
    public class CalculatorTests
    {
        CalculatorApi.Controllers.AuthorizationApiController _authorizationApiController;
        IAuthorizationService _authorizationService;

        CalculatorApiController _calculatorApiController;
        ICalculatorService _calculatorService;

        public CalculatorTests()
        {
            var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

            IConfiguration configuration = builder.Build();

            _authorizationService = new AuthorizationService(configuration);
            _authorizationApiController = new AuthorizationApiController(_authorizationService);

            _calculatorService = new CalculatorService();
            _calculatorApiController = new CalculatorApiController(_calculatorService);
        }

        [TestMethod]
        public void AuthorizationTokenPostTest()
        {
            AutenticationRequest autenticationRequest = new AutenticationRequest() { AuthenticationValue = "0542214579" };

            var autenticationResponce = _authorizationApiController.AuthorizationTokenPost(autenticationRequest);

            Assert.IsNotNull(autenticationResponce);
        }


        [TestMethod]
        public void CalculatorAddPostTest()
        {
            CalculatorRequest calculatorRequest = new CalculatorRequest()
            {
                Num1 = 1,
                Num2 = 2
            };

            var calculatorAddResponse = _calculatorApiController.CalculatorAddPost(calculatorRequest);

            if (calculatorAddResponse != null)
            {
                CalculatorResponce result = (CalculatorResponce)((ObjectResult)calculatorAddResponse).Value!;

                Assert.IsTrue((result.Result == calculatorRequest.Num1 + calculatorRequest.Num2));
            }
        }

        [TestMethod]
        public void CalculatorDividePostTest()
        {
            AutenticationRequest autenticationRequest = new AutenticationRequest() { AuthenticationValue = "0542214579" };

            var autenticationResponce = _authorizationApiController.AuthorizationTokenPost(autenticationRequest);

            CalculatorRequest calculatorRequest = new CalculatorRequest()
            {
                Num1 = 1,
                Num2 = 0
            };

            var calculatorAddResponse = _calculatorApiController.CalculatorDividePost(calculatorRequest);

            if (((Microsoft.AspNetCore.Mvc.ObjectResult)calculatorAddResponse).StatusCode != 400)
            {
                CalculatorResponce result = (CalculatorResponce)((ObjectResult)calculatorAddResponse).Value!;

                Assert.IsTrue((result.Result == calculatorRequest.Num1 / calculatorRequest.Num2));
            }
            else
            {
                Error err = (Error)((ObjectResult)calculatorAddResponse).Value!;

                Assert.IsTrue(err.Code == 500 && err.Message == "It is impossible to divide by 0");
            }
        }

        [TestMethod]
        public void CalculatorMultiplyPostTest()
        {
            AutenticationRequest autenticationRequest = new AutenticationRequest() { AuthenticationValue = "0542214579" };

            var autenticationResponce = _authorizationApiController.AuthorizationTokenPost(autenticationRequest);

            CalculatorRequest calculatorRequest = new CalculatorRequest()
            {
                Num1 = 1,
                Num2 = 0
            };

            var calculatorAddResponse = _calculatorApiController.CalculatorMultiplyPost(calculatorRequest);

            if (calculatorAddResponse != null)
            {
                CalculatorResponce result = (CalculatorResponce)((ObjectResult)calculatorAddResponse).Value!;

                Assert.IsTrue((result.Result == calculatorRequest.Num1 * calculatorRequest.Num2));
            }
        }


        [TestMethod]
        public void CalculatorSubtractPostTest()
        {
            AutenticationRequest autenticationRequest = new AutenticationRequest() { AuthenticationValue = "0542214579" };

            var autenticationResponce = _authorizationApiController.AuthorizationTokenPost(autenticationRequest);

            CalculatorRequest calculatorRequest = new CalculatorRequest()
            {
                Num1 = 12.3m,
                Num2 = 12
            };

            var calculatorAddResponse = _calculatorApiController.CalculatorSubtractPost(calculatorRequest);

            if (calculatorAddResponse != null)
            {
                CalculatorResponce result = (CalculatorResponce)((ObjectResult)calculatorAddResponse).Value!;

                Assert.IsTrue((result.Result == calculatorRequest.Num1 - calculatorRequest.Num2));
            }
        }
    }
}