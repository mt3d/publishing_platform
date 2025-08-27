using AutoMapper;
using backend.Data.Entities;
using backend.Logic.Users;

namespace backend.Infrastructure
{
	public class PlatformMapsProfile : Profile
	{
		public PlatformMapsProfile()
		{
			CreateMap<User, UserDto>(MemberList.None);
		}
	}
}
