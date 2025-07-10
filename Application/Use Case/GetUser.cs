using Application.UserDtos;
using Domain.Entities;
using Domain.IReposotory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Use_Case
{
    public class GetUser
    {

        private readonly IUserRepository _userRepository;
        public GetUser(IUserRepository userRepository) {
            _userRepository = userRepository;
                
        }

        public async Task<UserDto?> GetUserById(Guid userId)
        {
            User user = await _userRepository.GetAsync(u => u.Id == userId);

            if (user is null)
            {
                return null;
            }

            UserDto dto = new()
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
            };

            return dto;
        }
    }
}
