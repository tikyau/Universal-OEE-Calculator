using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OEEAPI.Models
{
    public class APIResponse
    {
        public string Status;

        public string Message;

        public APIResponse(string status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}
