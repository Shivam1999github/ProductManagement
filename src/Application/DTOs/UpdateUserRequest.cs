using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateUserRequest
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
