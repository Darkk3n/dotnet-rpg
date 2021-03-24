using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_rpg.Controllers;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static Character Knight = new Character();
        private static List<Character> Characters = new List<Character>
            {
                new Character(),
                new Character{Id=1,Name="Sam"}
            };
        private readonly IMapper mapper;

        public CharacterService(IMapper mapper)
        {
            this.mapper = mapper;
        }


        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var newCharacter = mapper.Map<Character>(character);
            newCharacter.Id = Characters.Max(r => r.Id) + 1;
            Characters.Add(newCharacter);
            response.Data = Characters.Select(r => mapper.Map<GetCharacterDto>(r)).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            response.Data = Characters.Select(r => mapper.Map<GetCharacterDto>(r)).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                response.Data = mapper.Map<GetCharacterDto>(Characters.First(r => r.Id == id));
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
                var currentCharacter = Characters.First(r => r.Id == updatedCharacter.Id);
                currentCharacter.Name = updatedCharacter.Name;
                currentCharacter.Class = updatedCharacter.Class;
                currentCharacter.Defense = updatedCharacter.Defense;
                currentCharacter.HitPoints = updatedCharacter.HitPoints;
                currentCharacter.Intelligence = updatedCharacter.Intelligence;
                currentCharacter.Strength = updatedCharacter.Strength;

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