using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User";

        //public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
