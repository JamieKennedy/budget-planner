using AutoMapper;

using Common.DataTransferObjects.Savings;
using Common.DataTransferObjects.SavingsBalance;
using Common.DataTransferObjects.User;
using Common.Models;

namespace Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<CreateUserDto, User>()
                .ForMember(user => user.UserName,
                    opt => opt.MapFrom(userRegistrationDto => userRegistrationDto.Email));
            CreateMap<User, UserDto>();

            // Savings
            CreateMap<CreateSavingsDto, Savings>();
            CreateMap<Savings, SavingsDto>();

            // Savings Balance
            CreateMap<CreateSavingsBalanceDto, SavingsBalance>();
            CreateMap<SavingsBalance, SavingsBalanceDto>();


        }

        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(configuration => configuration.AddProfile(new MappingProfile())).CreateMapper();
        }
    }
}
