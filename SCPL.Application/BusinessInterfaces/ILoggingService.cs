using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SCPL.Application.BusinessInterfaces
{
    public interface ILoggingService
    {
        Task LogLoginAttemptAsync(string email, bool isSuccess, HttpContext context);
    }
}
