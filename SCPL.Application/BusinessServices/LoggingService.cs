using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SCPL.Application.BusinessInterfaces;
using SCPL.Core;
using SCPL.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPL.Application.BusinessServices
{
    public class LoggingService : ILoggingService
    {
        private readonly InboxContext _dbContext;

        public LoggingService(InboxContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LogLoginAttemptAsync(string email, bool isSuccess, HttpContext context)
        {
            var historyLog = new HistoryLog
            {
                UserId = (await _dbContext.UserTable_5254.SingleOrDefaultAsync(u => u.UserEmail == email))?.UserId ?? 0,
                LoginStatus = isSuccess,
                LogDate = DateTime.UtcNow,
                LoginIp = context.Connection.RemoteIpAddress?.ToString() ?? "UNKNOWN",
                //BrowserDetails = context.Request.Headers.UserAgent.ToString()
            };

            await _dbContext.HistoryLog_5254.AddAsync(historyLog);
            await _dbContext.SaveChangesAsync();
        }
    }
}
