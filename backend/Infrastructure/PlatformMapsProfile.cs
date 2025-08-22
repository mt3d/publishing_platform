using AutoMapper;
using backend.Logic.Users;
using backend.Models;

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
