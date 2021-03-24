using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.Controllers;
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

        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character character)
        {
            var response = new ServiceResponse<List<Character>>();
            Characters.Add(character);
            response.Data = Characters;
            return response;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<Character>>();
            response.Data = Characters;
            return response;
        }

        public async Task<ServiceResponse<Character>> GetCharacterById(int id)
        {
            var response = new ServiceResponse<Character>();
            if (!Characters.Any(r => r.Id == id))
                return null;
            response.Data = Characters.FirstOrDefault(r => r.Id == id);
            return response;
        }
    }
}