using Microsoft.IdentityModel.Tokens;
using SCPL.Application.BusinessInterfaces;
using SCPL.Core;
using SCPL.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPL.Application.BusinessServices
{
    public class HistoryLogService : IHistoryLogService
    {
        private readonly InboxContext _dbContext;

        public HistoryLogService(InboxContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<HistoryLog>> GetLogsAsync(string token)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken;
            try
            {
                // Validate and read the token
                jwtToken = jwtHandler.ReadJwtToken(token.Replace("Bearer ", ""));
                if (jwtToken == null)
                {
                    throw new SecurityTokenException("Invalid token");
                }
            }
            catch
            {
                throw new SecurityTokenException("Error reading token");
            }

            // Fetch all logs from the database (you can filter by user if needed)
            var logs = _dbContext.HistoryLog_5254.ToList();

            return await Task.FromResult(logs);
        }
    }
}
