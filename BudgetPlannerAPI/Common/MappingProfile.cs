using AutoMapper;
using Common.DataTransferObjects.Saving;
using Common.DataTransferObjects.SavingBalance;
using Common.DataTransferObjects.User;
using Common.Models;

namespace Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User
            CreateMap<CreateUserDto, User>();

            // Saving
            CreateMap<CreateSavingDto, Saving>();

            // Saving Balance
            CreateMap<CreateSavingBalanceDto, SavingBalance>();
        }

        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(configuration => configuration.AddProfile(new MappingProfile())).CreateMapper();
        }
    }
}
