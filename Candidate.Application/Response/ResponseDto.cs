﻿namespace Candidate.Application.Response
{
    public class ResponseDto<T>
    {
        public int StatusCode { get; set; }

        public string Message { get; set; }

        public T? Result { get; set; }
    }
}
