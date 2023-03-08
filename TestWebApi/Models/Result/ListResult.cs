using TestWebApi.Models.Enum;

namespace TestWebApi.Models.Result
{
    public class ListResult<T>
    {
        public ResultType? ResultType { get; set; }
        public IEnumerable<T>? Result { get; set; }
        public string? Message { get; set; }
    }
}
