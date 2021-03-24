using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            var response = new ServiceResponse<List<AddCharacterDto>>();
            Characters.Add(character);
            response.Data = Characters;
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            response.Data = Characters;
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            if (!Characters.Any(r => r.Id == id))
                return null;
            response.Data = Characters.FirstOrDefault(r => r.Id == id);
            return response;
        }
    }
}