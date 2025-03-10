using Heroes.Models;

namespace Heroes.Repositories
{
    public interface IHeroesRepository
    {
        Task<Guid> CreateHero(NewHeroModel newHeroModel);
        Task<List<HeroModel>> GetAllAvailableHeroes();
        Task<bool> AddHeroToTrainer(Guid heroId, string userName);
        Task<List<HeroModel>> GetAllHeroesByUserName(string userName);
        Task<decimal?> TrainHero(Guid heroId, string userName);
    }
}