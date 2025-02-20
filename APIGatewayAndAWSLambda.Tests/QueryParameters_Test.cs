using Amazon.Lambda.Annotations.APIGateway;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.TestUtilities;
using System.Net;
using Xunit;

namespace APIGatewayAndAWSLambda.Tests;

public class QueryParameters_Test
{
    public QueryParameters_Test()
    {
    }

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

    [Fact]
    public async Task TestGetMethod_QueryStrings()
    {
        var context = new TestLambdaContext();
        var functions = new Functions();

        Dictionary<string, string> queryParameters = new Dictionary<string, string>();
        queryParameters.Add("name", "Test Name");

        APIGatewayProxyRequest request = new APIGatewayProxyRequest()
        {
            HttpMethod = "Get",
            Path = "/",
            Body = "Denzel Awuah",
            QueryStringParameters = queryParameters
        };

        CustomResponse response = await functions.QueryParametersHandler(request, context);

        Assert.Equal("Test Name", response.data);
    }
}
