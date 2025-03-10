using Heroes.Entities;
using System.ComponentModel.DataAnnotations;

namespace Heroes.Models
{
    public class NewHeroModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string UrlImg { get; set; }
        [Required]
        public HeroAbility HeroAbility { get; set; }
        [Required] 
        public string SuitColor { get; set; }
        [Required]
        public decimal StartingPower { get; set; }
    }
}
