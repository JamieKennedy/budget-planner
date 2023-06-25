using AutoMapper;

using Common.Models.Saving;
using Common.Models.Saving.Dto;
using Common.Models.SavingBalance;
using Common.Models.SavingBalance.Dto;
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

            // Saving
            CreateMap<CreateSavingDto, SavingModel>();

            // Saving Balance
            CreateMap<CreateSavingBalanceDto, SavingBalanceModel>();
        }

        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(configuration => configuration.AddProfile(new MappingProfile())).CreateMapper();
        }
    }
}
