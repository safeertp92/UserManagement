using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Liwapoi.Api.Models.ResponseModel
{
    public class ErrorResponse
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public ErrorResponse(Exception ex)
        {
            Message = string.Empty;
            Type = ex.GetType().Name;

#if DEBUG
            Message = ex.Message;
#else
            Message = string.Empty;
#endif
            StackTrace = ex.ToString();
        }
    }
}
