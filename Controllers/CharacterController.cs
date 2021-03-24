using System.Threading.Tasks;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService characterService;

        public CharacterController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get() => Ok(await characterService.GetAllCharacters());

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var characterFound = await characterService.GetCharacterById(id);
            return characterFound == null ?
            NotFound() :
            Ok(characterFound);
        }
        [HttpPost]
        public async Task<IActionResult> AddCharacter(Character newCharacter) => Ok(await characterService.AddCharacter(newCharacter));
    }
}