using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CafeVesuviusApi.Entities
{
    [Table("UserRefreshToken")]
    public class UserRefreshToken
    {
        [Key]
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;
        
        public DateTime CreatedDate { get; set; }

        public DateTime ExpirationDate { get; set; }
        
        [NotMapped]
        public bool IsActive
        {
            get
            {
                return ExpirationDate > DateTime.UtcNow;
            }
        }
        
        public string IpAddress { get; set; } = null!;
        
        public bool IsInvalidated { get; set; }

        public int UserId { get; set; }
        
        [ForeignKey("UserID")]
        public virtual AccessUser User { get; set; } = null!;
    }
}
