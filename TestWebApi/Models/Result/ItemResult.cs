using TestWebApi.Models.Enum;

namespace TestWebApi.Models.Result
{
    public class ItemResult<T>
    {
        public ResultType? ResultType { get; set; }
        public T? Result { get; set; }
        public string? Message { get; set; }
    }
}
