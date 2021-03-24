using System.Collections.Generic;
using System.Linq;
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

        public List<Character> AddCharacter(Character character)
        {
            Characters.Add(character);
            return Characters;
        }

        public List<Character> GetAllCharacters() => Characters;

        public Character GetCharacterById(int id)
        {
            return !Characters.Any(r => r.Id == id) ?
                null :
            Characters.FirstOrDefault(r => r.Id == id);
        }
    }
}