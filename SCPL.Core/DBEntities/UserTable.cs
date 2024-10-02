using System.ComponentModel.DataAnnotations;

namespace SCPL.Core.DBEntities
{
    public class UserTable
    {
        [Key]
        public int UserId { get; set; }

        public required string UserEmail { get; set; }

        public required string UserPassword { get; set; }
    }

}
