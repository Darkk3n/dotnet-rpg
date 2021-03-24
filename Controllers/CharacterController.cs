using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Get() => Ok(characterService.GetAllCharacters());

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var characterFound = characterService.GetCharacterById(id);
            return characterFound == null ?
            NotFound() :
            Ok(characterFound);
        }
        [HttpPost]
        public IActionResult AddCharacter(Character newCharacter) => Ok(characterService.AddCharacter(newCharacter));
    }
}