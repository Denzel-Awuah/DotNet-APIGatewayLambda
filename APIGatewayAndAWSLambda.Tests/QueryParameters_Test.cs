using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using System.Net;
using Xunit;

namespace APIGatewayAndAWSLambda.Tests;

public class QueryParameters_Test
{
    /// <summary>
    /// Constructor to initialize the QueryParameters_Test class
    /// </summary>
    public QueryParameters_Test()
    {
    }

    /// <summary>
    /// Test Method to check if the QueryParametersHandler returns the correct response when the "name" Query Parameter is provided
    /// </summary>
    [Fact]
    public async Task TestGetMethod_ReturnData()
    {

        //Arrange
        var context = new TestLambdaContext();
        var functions = new Functions();

        //Act
        APIGatewayProxyRequest request = new APIGatewayProxyRequest()
        {
            HttpMethod = "GET",
            Path = "/",
            Body = "Denzel Awuah"
        };

        CustomResponse response = await functions.QueryParametersHandler(request, context);

        //Assert
        Assert.Equal("Test Name", response.data);
        Assert.Equal((int)HttpStatusCode.OK, response.statusCode);
    }

    /// <summary>
    /// Test Method to check if the QueryParametersHandler returns the correct response when the "name" Query Parameter is provided
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task TestGetMethod_QueryStrings()
    {
        //Arrange
        var context = new TestLambdaContext();
        var functions = new Functions();

        //Act
        Dictionary<string, string> queryParameters = new Dictionary<string, string>();
        queryParameters.Add("name", "Test Name");

        APIGatewayProxyRequest request = new APIGatewayProxyRequest()
        {
            HttpMethod = "Get",
            Path = "/",
            Body = "Denzel Awuah",
            QueryStringParameters = queryParameters
        };

        //Assert
        CustomResponse response = await functions.QueryParametersHandler(request, context);
        Assert.Equal("Test Name", response.data);
    }
}
