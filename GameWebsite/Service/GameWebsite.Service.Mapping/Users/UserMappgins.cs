using GameWebsite.Data.Models;
using GameWebsite.Service.Models.Users;

namespace GameWebsite.Service.Mapping.Users
{
    public static class UserMappgins
    {
        public static GameWebsiteUser ToEntity(this GameWebsiteUserDto gameWebsiteUserDto)
        {
            return new GameWebsiteUser
            {
                Id = gameWebsiteUserDto.Id,
                UserName = gameWebsiteUserDto.UserName,
                Email = gameWebsiteUserDto.Email,
            };
        }

        public static GameWebsiteUserDto ToDto(this GameWebsiteUser gameWebsiteUser)
        {
            return new GameWebsiteUserDto
            {
                Id = gameWebsiteUser.Id,
                UserName = gameWebsiteUser.UserName,
                Email = gameWebsiteUser.Email,
            };
        }
    }
}
