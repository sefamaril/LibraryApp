﻿namespace LibraryApp.Core.Utilities.Result
{
    public interface IDataResult<T> : IResult
    {
        T Data { get; set; }
    }
}
