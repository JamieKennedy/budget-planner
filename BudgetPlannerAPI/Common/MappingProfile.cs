using AutoMapper;

using Common.DataTransferObjects.Group;
using Common.DataTransferObjects.GroupMember;
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
            CreateMap<CreateUserDto, User>();

            // Savings
            CreateMap<CreateSavingsDto, Savings>();
            CreateMap<Savings, SavingsDto>();

            // Savings Balance
            CreateMap<CreateSavingsBalanceDto, SavingsBalance>();
            CreateMap<SavingsBalance, SavingsBalanceDto>();

            // Group
            CreateMap<CreateGroupDto, Group>();
            CreateMap<Group, GroupDto>();

            // GroupMember
            CreateMap<CreateGroupMemberDto, GroupMember>();
            CreateMap<GroupMember, GroupMemberDto>();
        }

        public static IMapper CreateMapper()
        {
            return new MapperConfiguration(configuration => configuration.AddProfile(new MappingProfile())).CreateMapper();
        }
    }
}
