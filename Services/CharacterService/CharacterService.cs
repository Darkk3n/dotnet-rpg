using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Controllers;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper mapper;
        private readonly DataContext context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }


        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var newCharacter = mapper.Map<Character>(character);

            await context.Characters.AddAsync(newCharacter);
            await context.SaveChangesAsync();
            response.Data = context.Characters.Select(r => mapper.Map<GetCharacterDto>(r)).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var characterToRemove = await context.Characters.FirstAsync(r => r.Id == id);
                context.Characters.Remove(characterToRemove);
                await context.SaveChangesAsync();
                response.Data = context.Characters.Select(r => mapper.Map<GetCharacterDto>(r)).ToList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int userId)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbData = await context.Characters.Where(r => r.User.Id == userId).ToListAsync();
            response.Data = dbData.Select(r => mapper.Map<GetCharacterDto>(r)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await context.Characters.FirstOrDefaultAsync(r => r.Id == id);
            try
            {
                response.Data = mapper.Map<GetCharacterDto>(dbCharacter);
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var currentCharacter = await context.Characters.FirstOrDefaultAsync(r => r.Id == updatedCharacter.Id);
                currentCharacter.Name = updatedCharacter.Name;
                currentCharacter.Class = updatedCharacter.Class;
                currentCharacter.Defense = updatedCharacter.Defense;
                currentCharacter.HitPoints = updatedCharacter.HitPoints;
                currentCharacter.Intelligence = updatedCharacter.Intelligence;
                currentCharacter.Strength = updatedCharacter.Strength;

                context.Characters.Update(currentCharacter);
                await context.SaveChangesAsync();
                response.Data = mapper.Map<GetCharacterDto>(currentCharacter);
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