using System.Collections.Generic;
using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;

namespace dotnet_rpg.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public RpgClass Class { get; set; }

        public GetWeaponDto Weapon { get; set; }

        public List<GetSkillDto> Skills { get; set; }
    }
}