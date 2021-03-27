using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Controllers;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly IMapper mapper;
        private readonly DataContext context;

        public WeaponService(DataContext context, IMapper mapper, IHttpContextAccessor httpContext)
        {
            this.context = context;
            this.mapper = mapper;
            this.httpContext = httpContext;

        }

        private int GetUserId() => int.Parse(httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto weaponDto)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await context.Characters
                .FirstOrDefaultAsync(r => r.Id == weaponDto.CharacterId && r.User.Id == GetUserId());
                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }
                var weapon = mapper.Map<Weapon>(weaponDto);
                await context.Weapons.AddAsync(weapon);
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