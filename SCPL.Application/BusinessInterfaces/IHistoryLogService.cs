using SCPL.Core.DBEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCPL.Application.BusinessInterfaces
{
    public interface IHistoryLogService
    {
        Task<IEnumerable<HistoryLog>> GetLogsAsync(string token);
    }
}
