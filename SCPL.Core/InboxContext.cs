
using Microsoft.EntityFrameworkCore;
using SCPL.Core.DBEntities;

namespace SCPL.Core
{
    public class InboxContext(DbContextOptions<InboxContext>options) : DbContext(options)
    {
        public DbSet<UserTable> UserTable_5254 { get; set; }
        public DbSet<HistoryLog> HistoryLog_5254 { get; set; }
    }
}
