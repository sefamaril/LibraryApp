
namespace LibraryApp.Core.Utilities.Result
{
    public class Result : IResult
    {
        public bool IsSuccess { get; set; }
        public List<string> Messages { get; set; }

        public Result()
        {
            Messages = new List<string>();
        }

        public Result(bool isSuccess) : this()
        {
            IsSuccess = isSuccess;
        }

        public Result(bool isSuccess, string message) : this(isSuccess)
        {
            Messages.Add(message);
        }

        public void AddMessage(string message)
        {
            Messages.Add(message);
        }
    }
}
