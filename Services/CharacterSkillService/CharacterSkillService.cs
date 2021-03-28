using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Controllers;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.CharacterSkills;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterSkillService
{
    public class CharacterSkillService : ICharacterSkillService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;
        private readonly IHttpContextAccessor httpContext;

        public CharacterSkillService(DataContext context, IHttpContextAccessor httpContext, IMapper mapper)
        {
            this.httpContext = httpContext;
            this.context = context;
            this.mapper = mapper;
        }

        private int GetUserId() => int.Parse(httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto addCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await context.Characters
                .Include(r => r.Weapon)
                .Include(r => r.CharacterSkills)
                    .ThenInclude(r => r.Skill)
                .FirstOrDefaultAsync(r => r.Id == addCharacterSkill.CharacterId && r.User.Id == GetUserId());
                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }
                var skill = await context.Skills.FirstOrDefaultAsync(r => r.Id == addCharacterSkill.SkillId);
                if (skill == null)
                {
                    response.Success = false;
                    response.Message = "Skill not found";
                    return response;
                }
                var characterSkill = new CharacterSkill
                {
                    Character = character,
                    Skill = skill
                };
                await context.CharacterSkills.AddAsync(characterSkill);
                await context.SaveChangesAsync();
                response.Data = mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}