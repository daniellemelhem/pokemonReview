using AutoMapper;
using PokemonReviewApp2.Dto;
using PokemonReviewApp2.Models;

namespace PokemonReviewApp2.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>();
            CreateMap<PokemonDto, Pokemon>();
            CreateMap <Category,CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<Country,CountryDto>();
            CreateMap<CountryDto, Country>();
            CreateMap<Owner,OwnerDto>();
            CreateMap<OwnerDto, Owner>();
            CreateMap<Review,ReviewDto>();
            CreateMap<ReviewDto,Review>();
            CreateMap<Reviewer, ReviewerDto>();
            CreateMap<ReviewerDto, Reviewer>();
        }
    }
}
