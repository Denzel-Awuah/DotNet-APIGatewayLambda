using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Annotations;
using Amazon.Lambda.Annotations.APIGateway;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace APIGatewayAndAWSLambda;

public class Functions
{
    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Functions()
    {
    }


    /// <summary>
    /// A Lambda function to respond to HTTP Get methods from API Gateway
    /// </summary>
    /// <remarks>
    /// This uses the <see href="https://github.com/aws/aws-lambda-dotnet/blob/master/Libraries/src/Amazon.Lambda.Annotations/README.md">Lambda Annotations</see> 
    /// programming model to bridge the gap between the Lambda programming model and a more idiomatic .NET model.
    /// 
    /// This automatically handles reading parameters from an APIGatewayProxyRequest
    /// as well as syncing the function definitions to serverless.template each time you build.
    /// 
    /// If you do not wish to use this model and need to manipulate the API Gateway 
    /// objects directly, see the accompanying Readme.md for instructions.
    /// </remarks>
    /// <param name="context">Information about the invocation, function, and execution environment</param>
    /// <returns>The response as an implicit <see cref="APIGatewayProxyResponse"/></returns>
    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/")]
    public async Task<CustomResponse> QueryParametersHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var name = request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey("name")
            ? request.QueryStringParameters["name"]
            : "Test Name";

        CustomResponse response = new CustomResponse(name, 200);
        Console.WriteLine($"The request data {JsonSerializer.Serialize(request)}");
        Console.WriteLine($"The response data: {JsonSerializer.Serialize(response)}");

        return response;
    }

    [LambdaFunction]
    [RestApi(LambdaHttpMethod.Get, "/{name}")]
    public async Task<CustomResponse> PathParameterHandler(APIGatewayProxyRequest request, String name, ILambdaContext context)
    {
        var routeParam = request.PathParameters != null && request.PathParameters.ContainsKey("name")
            ? request.PathParameters["name"]
            : "Test Name";

        Console.WriteLine($"The name route parameter: {name}");

        routeParam = routeParam.Replace("%20", " ");

        CustomResponse response = new CustomResponse(routeParam, 200);
        Console.WriteLine($"The APIProxyRequest data {JsonSerializer.Serialize(request)}");
        Console.WriteLine($"The Custom Response data: {JsonSerializer.Serialize(response)}");

        return response;
    }
}

public class CustomResponse
{
    public string data { get; set; }
    public int statusCode { get; set; }

    public CustomResponse(string data, int statusCode)
    {
        this.data = data;
        this.statusCode = statusCode;
    }
}

