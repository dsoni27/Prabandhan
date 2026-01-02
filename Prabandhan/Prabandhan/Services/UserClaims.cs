using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Parbandhan.Services
{
    public class UserClaims
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime Expiry { get; set; }
        public List<JobPermission> Permissions { get; set; }
    }
}
