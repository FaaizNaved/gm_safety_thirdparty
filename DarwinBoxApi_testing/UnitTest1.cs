using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using gm_safety_thirdparty.Models;
using gm_safety_thirdparty.Options;
using gm_safety_thirdparty.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace DarwinBoxApi_testing
{
    [TestClass]
    public class DarwinBoxClientTests
    {
        [TestMethod]
        public async Task FetchEmployeesAsync_ReturnsEmployees_WhenApiReturnsSuccess()
        {
            // Arrange
            var expectedJson = "{\"employee_data\":[{\"first_name\":\"John\",\"last_name\":\"Doe\",\"company_email_id\":\"john.doe@example.com\",\"date_of_joining\":\"2023-01-01\"}]}";
            var handlerMock = new Mock<HttpMessageHandler>();
            // Replace ItExpr with It from Moq
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    It.IsAny<HttpRequestMessage>(),
                    It.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedJson),
                });


            var httpClient = new HttpClient(handlerMock.Object);
            var options = Options.Create(new DarwinBoxOptions
            {
                ApiUrl = "https://fakeapi.com",
                ApiKey = "testkey",
                Username = "user",
                Password = "pass",
                DatasetKey = "dataset"
            });

            var client = new DarwinBoxClient(httpClient, options);

            // Act
            var result = await client.FetchEmployeesAsync(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.employee_data);
            Assert.AreEqual("John", result.employee_data[0].FirstName);
            Assert.AreEqual("Doe", result.employee_data[0].LastName);
        }
    }
}
