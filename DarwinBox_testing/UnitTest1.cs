using System.Threading.Tasks;
using gm_safety_thirdparty.Models;
using gm_safety_thirdparty.Options;
using gm_safety_thirdparty.Services;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Xunit;
using System.Net.Http;
using System.Net;
using System.Threading;

namespace DarwinBox_testing
{
    public class DarwinBoxClientTests
    {
        [Fact]
        public async Task FetchEmployeesAsync_ReturnsEmployees_WhenApiReturnsSuccess()
        {
            // Arrange
            var expectedJson = "{\"employee_data\":[{\"first_name\":\"John\",\"last_name\":\"Doe\",\"company_email_id\":\"john.doe@example.com\",\"date_of_joining\":\"2023-01-01\"}]}";
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
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
            Assert.NotNull(result);
            Assert.NotNull(result.employee_data);
            Assert.Equal("John", result.employee_data[0].FirstName);
            Assert.Equal("Doe", result.employee_data[0].LastName);
        }
    }
}
