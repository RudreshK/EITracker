// -----------------------------------------------------------------------------
// <copyright file="ErrorResponse.cs" company="ecoInsight, Inc.">
//	Copyright © ecoInsight, Inc. All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------

namespace ODataDemo.Services
{
    public class ErrorResponse
    {
        public Error? error { get; set; }
    }

    public class Error
    {
        public Error(string code, string message)
        {
            this.code = code;
            this.message = message;
        }

        public string code { get; set; }
        public string message { get; set; }
        public InnerError? innererror { get; set; }

    }

    public class InnerError
    {
        public InnerError(string? message, string? type, string? stacktrace)
        {
            this.message = message;
            this.type = type;
            this.stacktrace = stacktrace;
        }

        public string? message { get; set; }
        public string? type { get; set; }
        public string? stacktrace { get; set; }
    }
}
