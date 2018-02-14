using System.Linq;
using Germaine.Dtos;
using Germaine.Models;
using AutoMapper;

namespace Germaine.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForDetailDtos>()
            .ForMember(dest => dest.Expiry, opt =>
                {
                    opt.MapFrom(d => d.PasswordReset);
                });
                // .ForMember(dest => dest.PhotoUrl, opt => 
                // {
                //     opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                // })
                // .ForMember(dest => dest.Age, opt =>
                // {
                //     opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
                // });
            CreateMap<User, UserForListDtos>()
                .ForMember(dest => dest.Expiry, opt =>
                {
                    opt.MapFrom(d => d.PasswordReset);
                });
            //     .ForMember(dest => dest.PhotoUrl, opt => 
            //     {
            //         opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            //     })
            //     .ForMember(dest => dest.Age, opt =>
            //     {
            //         opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            //     });

                
            CreateMap<Activity, ActivityForListDtos>();

            CreateMap<Person, PersonForListDtos>();

            //  CreateMap<UserForPasswordUpdateDtos, User>();
        }
    }
}