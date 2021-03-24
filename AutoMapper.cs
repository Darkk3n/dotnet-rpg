using AutoMapper;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<GetCharacterDto, Character>();
            CreateMap<AddCharacterDto, Character>();
        }
    }
}