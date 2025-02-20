using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIGatewayAndAWSLambda
{
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
}
