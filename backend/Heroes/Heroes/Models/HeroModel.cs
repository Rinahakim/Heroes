using Heroes.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Heroes.Models
{
    public class HeroModel
    {
        [Key] //id is primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //the value for id be automatic by DB
        public Guid Id { get; set; }
        [Required]
        public string UrlImg { get; set; }

        [Required]
        public string Name { get; set; }  
        
        [Required]
        public HeroAbility HeroAbility { get; set; }
        public DateTime? StartedTraining { get; set; }

        [Required]
        public string SuitColor { get; set; }

        [Required]
        [Precision(38,2)] //allowing 38 charecters and allowing 2 places after the point
        public decimal StartingPower { get; set; }
        public DateTime? AddToCartDate { get; set; }

        [Precision(38, 2)]
        public decimal CurrentPower { get; set; }

        public string? TrainerId { get; set; }
        public AppUser? Trainer { get; set; }

        public DateTime? LastTrainingDate { get; set; }

        public int? DailyTrainingCount { get; set; }
    }
}
