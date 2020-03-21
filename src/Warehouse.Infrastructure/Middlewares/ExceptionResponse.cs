using System.Net;

namespace Warehouse.Infrastructure.Middlewares
{
    public class ExceptionResponse
    {
        public object Response { get; }

        public HttpStatusCode StatusCode { get; }

        public ExceptionResponse(object response, HttpStatusCode statusCode)
        {
            Response = response;
            StatusCode = statusCode;
        }
    }
}
