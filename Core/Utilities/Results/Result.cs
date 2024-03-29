﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message) : this(success) // this(success)--> parametre(leri/si) => success olan constructorı çalıştır
        {
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}
