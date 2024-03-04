namespace LibraryApp.Core.Utilities.Result
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public T Data { get; set; }

        public DataResult(T data, bool isSuccess, string message = null) : base(isSuccess, message)
        {
            Data = data;
        }

        public DataResult(bool isSuccess, string message = null) : base(isSuccess, message)
        {

        }

        public DataResult(T data, bool isSuccess, List<string> messages) : base(isSuccess)
        {
            Data = data;
            messages = messages ?? new List<string>();
        }
    }
}
