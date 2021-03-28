using System.Threading.Tasks;
using dotnet_rpg.Dtos.CharacterSkills;
using dotnet_rpg.Services.CharacterSkillService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterSkillController : ControllerBase
    {
        private readonly ICharacterSkillService characterSkillService;

        public CharacterSkillController(ICharacterSkillService characterSkillService)
        {
            this.characterSkillService = characterSkillService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddCharacterSkill(AddCharacterSkillDto characterSkillDto)
        {
            return Ok(await characterSkillService.AddCharacterSkill(characterSkillDto));
        }
    }
}