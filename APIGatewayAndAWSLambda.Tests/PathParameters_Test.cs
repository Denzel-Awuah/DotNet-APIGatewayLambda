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
    /// <summary>
    /// Test Class for PathParameters Function
    /// </summary>
    public class PathParameters_Test
    {
        /// <summary>
        /// Constructor to initialize the PathParameters_Test class
        /// </summary>
        public PathParameters_Test()
        {
        }

        /// <summary>
        /// Test Method to check if the PathParameterHandler returns the correct response when the "name" Path Parameter is provided
        /// </summary>
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

        /// <summary>
        /// Test Method to check if the PathParameterHandler returns the default response when the "name" Path Parameter is not provided
        /// </summary>
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
