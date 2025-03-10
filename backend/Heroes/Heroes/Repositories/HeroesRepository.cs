using Heroes.Data;
using Heroes.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Heroes.Repositories
{
    public class HeroesRepository: IHeroesRepository
    {
        private readonly HeroesContext _heroesContext;
        public HeroesRepository(HeroesContext heroesContext) 
        {
            _heroesContext = heroesContext;
        }
        public async Task<Guid> CreateHero(NewHeroModel newHeroModel) {
            HeroModel heroModel = new()
            {
                Name = newHeroModel.Name,
                UrlImg = newHeroModel.UrlImg,
                CurrentPower = newHeroModel.StartingPower,
                StartingPower = newHeroModel.StartingPower,
                HeroAbility = newHeroModel.HeroAbility,
                SuitColor = newHeroModel.SuitColor,
            };
            _heroesContext.Heroes.Add(heroModel);
            var res = await _heroesContext.SaveChangesAsync();
            if (res != 0) //if changes have been made
            { 
                return heroModel.Id;
            }
            return Guid.Empty;
        }

        public async Task<List<HeroModel>> GetAllAvailableHeroes() {
            var heroesToUpdate = await _heroesContext.Heroes
               .Where(h => h.AddToCartDate != null && h.AddToCartDate.Value < DateTime.Today.Date)
               .ToListAsync();

            foreach (var hero in heroesToUpdate)
            {
                hero.LastTrainingDate = null;
                hero.TrainerId = null;
                hero.StartedTraining = null;
                hero.LastTrainingDate = null;
                hero.AddToCartDate = null;
                hero.CurrentPower = 0;
                hero.DailyTrainingCount = 0;
            }

            if (heroesToUpdate.Any())
            {
                await _heroesContext.SaveChangesAsync();
            }

            var heroes = await _heroesContext.Heroes.Include(h => h.Trainer).Where(h => h.Trainer == null).ToListAsync();
            return heroes;
        }

        public async Task<bool> AddHeroToTrainer(Guid heroId, string userName) {
            var user = await GetUser(userName);
            if (user == null) return false;

            var hero = await _heroesContext.Heroes.FirstOrDefaultAsync(h => h.Id == heroId);
            if (hero == null) return false;

            if (user.Heroes != null && user.Heroes.Contains(hero)) return false;

            hero.Trainer = user;
            hero.AddToCartDate = DateTime.Today.Date;
            if (user.Heroes == null) {
                user.Heroes = new List<HeroModel>();
            }
            user.Heroes.Add(hero);

            var result = await _heroesContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<HeroModel>> GetAllHeroesByUserName(string userName)
        {
            var user = await GetUser(userName);
            if (user == null) {
                return null; 
            }
            return await _heroesContext.Heroes.Where(h => h.TrainerId == user.Id).ToListAsync(); 
        }

        public async Task<decimal?> TrainHero(Guid heroId, string userName)
        {
            var user = await GetUser(userName);
            if (user == null) {
                return null;
            }
            var hero = await _heroesContext.Heroes.FindAsync(heroId);
            if (hero == null || !user.Heroes.Contains(hero)) { 
                return null;
            }
            if (!hero.LastTrainingDate.HasValue) { //if this hero has none value - hero didnt training
                TrainHeroHendler(ref hero, false);
            }
            else if (hero.LastTrainingDate.HasValue && hero.LastTrainingDate.Value.Date < DateTime.Today.Date) {
                TrainHeroHendler(ref hero, false);
            }
            else if (hero.DailyTrainingCount.HasValue && hero.DailyTrainingCount.Value < 5)
            {
                TrainHeroHendler(ref hero, true);
            }
            var res = await _heroesContext.SaveChangesAsync();
            if (res != 0) {
                return hero.CurrentPower;
            }
            return null;
        }

        private async Task<AppUser> GetUser(string userName) {
            return await _heroesContext.Users.FirstOrDefaultAsync(u => u.Email == userName);
        }

        private void TrainHeroHendler(ref HeroModel hero, bool sameDay) {
            hero.CurrentPower = TrainHeroResult(hero.CurrentPower);
            hero.LastTrainingDate = DateTime.Today.Date;
            hero.StartedTraining = (hero.StartedTraining == null)? DateTime.Today: hero.StartedTraining;
            hero.DailyTrainingCount = sameDay ? hero.DailyTrainingCount + 1 : 1;
        }

        private decimal TrainHeroResult(decimal currentPower) {
            Random random = new();
            int rand = random.Next(100,110);
            decimal newPower = (currentPower == 0 ? 1 : currentPower) * rand / 100;
            return newPower;
        }


    }
}
