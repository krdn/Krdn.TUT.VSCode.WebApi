using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class AppUser : IdentityUser
    {
        // Portfolio 모델과 1:N 관계
        public List<Portfolio> Portfolios { get; set; } = [];
    }
}
