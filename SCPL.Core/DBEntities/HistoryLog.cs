using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCPL.Core.DBEntities
{
    public class HistoryLog
    {
        [Key]
        public int LogId { get; set; }

        [ForeignKey("UserTable")]
        public int UserId { get; set; }
        public bool LoginStatus { get; set; }
        public DateTime LogDate { get; set; }
        public string? LoginIp { get; set; }
        public string? BrowserDetails { get; set; }

    }
}
