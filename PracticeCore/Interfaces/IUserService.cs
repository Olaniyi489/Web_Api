using PracticeCore.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeCore.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> AddUser(UserDto userDto);
        Task<List<UserDto>> GetAllUser();
        Task<UserDto> FindByEmail(string email);
        Task<List<EmailDto>> FindUserByEmail();
        Task<Tokens> Login(SignInDto signInDto);
        Task<bool> UpdatePassWord(PasswordDto passwordDto, string email);


    }
}
