using AutoMapper;

using Common.Models.Savings;
using Common.Models.Savings.Dto;
using Common.Models.SavingsBalance;
using Common.Models.SavingsBalance.Dto;
using Common.Models.User;
using Common.Models.User.Dto;

namespace Common.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<CreateUserDto, UserModel>();

            // Savings
            CreateMap<CreateSavingsDto, SavingsModel>();

            // Savings Balance
            CreateMap<CreateSavingsBalanceDto, SavingsBalanceModel>();
        }

        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(configuration => configuration.AddProfile(new MappingProfile())).CreateMapper();
        }
    }
}
