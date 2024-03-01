namespace LibraryApp.Core.Utilities.Result
{
    public interface IResult
    {
        bool IsSuccess { get; set; }

        List<string> Messages { get; set; }
    }
}
