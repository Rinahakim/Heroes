using Azure.Core;
using Heroes.Data;
using Heroes.Models;
using Heroes.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroesRepository _heroesRepository;
        public HeroesController(IHeroesRepository heroesRepository) 
        {
            _heroesRepository = heroesRepository;
        }
        [HttpPost("")]
        public async Task<IActionResult> CreateHero([FromBody] NewHeroModel newHeroModel) { 
            var res = await _heroesRepository.CreateHero(newHeroModel);
            if (res == Guid.Empty) {
                return BadRequest();
            }
            return Ok(res);
        }

        [HttpGet("")]
        [Authorize]
        public async Task<IActionResult> GetAllAvailableHeroes() { 
            var res = await _heroesRepository.GetAllAvailableHeroes();
            if (res == null || res.Count == 0) { 
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost("add-to-trainer/{heroId}")]
        [Authorize]
        public async Task<IActionResult> AddHeroToTrainer([FromRoute] string heroId) {
            var userName = User.Identity.Name;
            if (!Guid.TryParse(heroId, out Guid heroGuid))
            {
                return BadRequest("Invalid Hero ID.");
            }
            var res = await _heroesRepository.AddHeroToTrainer(heroGuid, userName);
            if (!res)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost("user-heroes")]
        [Authorize]
        public async Task<IActionResult> GetAllHeroesByUserName([FromBody] string userName)
        {
            var res = await _heroesRepository.GetAllHeroesByUserName(userName);
            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }

        [HttpPost("train/{heroId:guid}")]
        [Authorize]
        public async Task<IActionResult> TrainHero([FromRoute] Guid heroId)
        {
            var userName = User.Identity.Name;
            if(userName == null) return BadRequest();
            
            var res = await _heroesRepository.TrainHero(heroId, userName);
            if (res == null)
            {
                return BadRequest();
            }

            return Ok(res);
        }
    }
}
