using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace APIGatewayAndAWSLambda.Tests
{
    public class PathParameters_Test
    {
        public PathParameters_Test()
        {
        }

        [Fact]
        public async Task PathParameterHandler_ReturnsCorrectResponse_WhenNameIsProvided()
        {
            // Arrange
            var request = new APIGatewayProxyRequest
            {
                PathParameters = new Dictionary<string, string> { { "name", "John Doe" } }
            };
            var context = new TestLambdaContext();
            var functions = new Functions();

            // Act
            CustomResponse response = await functions.PathParameterHandler(request, "John Doe", context);

            // Assert
            Assert.Equal("John Doe", response.data);
            Assert.Equal(200, response.statusCode);
        }

        [Fact]
        public async Task PathParameterHandler_ReturnsDefaultResponse_WhenNameIsNotProvided()
        {
            // Arrange
            var request = new APIGatewayProxyRequest
            {
                PathParameters = new Dictionary<string, string>()
            };
            var context = new TestLambdaContext();
            var functions = new Functions();

            // Act
            CustomResponse response = await functions.PathParameterHandler(request, "", context);

            // Assert
            Assert.Equal("Test Name", response.data);
            Assert.Equal(200, response.statusCode);
        }
    }
}
