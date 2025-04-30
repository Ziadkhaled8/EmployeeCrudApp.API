
using System.Net;


namespace EmployeeCrudApp.Application.Dtos
{
    public class ResponseDto<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? Error { get; set; }
        public T? Result { get; set; }
    }
}
