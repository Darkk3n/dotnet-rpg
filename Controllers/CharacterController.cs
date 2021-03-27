using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
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
            return characterFound.Success ? Ok(characterFound) : NotFound();
        }
        [HttpPost("Add")]
        public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter) => Ok(await characterService.AddCharacter(newCharacter));

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
            var response = await characterService.UpdateCharacter(updateCharacterDto);
            return response.Success ? Ok(response.Data) : NotFound();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCharacter(int id)
        {
            var response = await characterService.DeleteCharacter(id);
            return response.Success ? Ok(response.Data) : NotFound();
        }
    }
}