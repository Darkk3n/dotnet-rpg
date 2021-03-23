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
        public IActionResult Get()
        {
            return Ok(Characters);
        }

        [HttpGet]
        public IActionResult GetSingle()
        {
            return Ok(Characters.FirstOrDefault());
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            if(!Characters.Any(r=>r.Id==id))
            {
                return NotFound();
            }
            return Ok(Characters.FirstOrDefault(r=>r.Id==id));
        }
    }
}