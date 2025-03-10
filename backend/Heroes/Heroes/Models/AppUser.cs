using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Heroes.Models
{
    public class AppUser: IdentityUser
    {
        public List<HeroModel>? Heroes { get; set; }
    }
}
