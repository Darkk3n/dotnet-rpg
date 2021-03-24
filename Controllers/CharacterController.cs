using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private static Character Knight = new Character();
        private static List<Character> Characters = new List<Character>
            {
                new Character(),
                new Character{Id=1,Name="Sam"}
            };

        [HttpGet("GetAll")]
        public IActionResult Get() => Ok(Characters);

        [HttpGet]
        public IActionResult GetSingle() => Ok(Characters.FirstOrDefault());

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return !Characters.Any(r => r.Id == id) ?
                NotFound() :
            Ok(Characters.FirstOrDefault(r => r.Id == id));
        }
        [HttpPost]
        public IActionResult AddCharacter(Character newCharacter)
        {
            Characters.Add(newCharacter);
            return Ok(Characters);
        }
    }
}