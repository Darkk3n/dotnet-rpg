using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Character>> AddCharacter(Character character)
        {
            Characters.Add(character);
            return Characters;
        }

        public async Task<List<Character>> GetAllCharacters() => Characters;

        public async Task<Character> GetCharacterById(int id)
        {
            return !Characters.Any(r => r.Id == id) ?
                null :
            Characters.FirstOrDefault(r => r.Id == id);
        }
    }
}