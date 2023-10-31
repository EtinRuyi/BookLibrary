
namespace BookLibrary.Model.Entities.Shared
{
    public class BaseResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<ErrorItem> Errors { get; set; }
        public int ResponseCode { get; set; }
    }

    public class ErrorItem
    {
        public string Key { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
