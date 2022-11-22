using Microsoft.AspNetCore.Identity;
using Practice.Data;
using PracticeCore.Dto;
using PracticeCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCore.Services
{
    public class UserService: IUserService
    {
        private UserManager<PracticeUser> _userManager;
        private SignInManager<PracticeUser> _signInManager;
        private IJwtService _jwtService;
        private PracticeIdentityDbContext _db;

        public UserService(UserManager<PracticeUser> userManager, SignInManager<PracticeUser> signInManager, IJwtService jwtService, PracticeIdentityDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _db = db;
        }

        public static List<UserDto> Users { get; set; } = new List<UserDto>();
        public async Task<UserDto> AddUser(UserDto userDto)
        {
             var user = new PracticeUser()
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
            };

            var result = await _userManager.CreateAsync(user, "P@ssword123");

            if (result.Succeeded)
            {
                return userDto;
            }

            return null;
        }

        public async Task<List<UserDto>> GetAllUser()
        {
            return _db.Users.Select(x => new UserDto() 
            {
                FirstName = x.FirstName,
                LastName= x.LastName,
                Email = x.Email

            }).ToList();
        }

        public async Task<UserDto> FindByEmail(string email)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var userDto = new UserDto() { Email = user.Email, FirstName = user?.FirstName, LastName = user?.LastName };

                return userDto;
            }

                return null;

            //return Users.Where(x => x.Email.ToLower().Trim() == email.ToLower().Trim()).FirstOrDefault();
        }

        public async Task<List<EmailDto>> FindUserByEmail()
        {
            return Users.Select(x => new EmailDto()
            {
                Email = x.Email
            }).ToList();
        }

        public async Task<Tokens> Login(SignInDto signInDto)
        {
          var result = await  _signInManager.PasswordSignInAsync(signInDto.Email, signInDto.Password,false,false);
           var user = await _userManager.FindByEmailAsync(signInDto.Email);

            if (result.Succeeded)
            {
                return _jwtService.Authenticate(user);
            }
            return null;
        }

        public async Task<bool> UpdatePassWord(PasswordDto passwordDto, string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
            {
                return false;
            }

           var result = await _userManager.ChangePasswordAsync(user, "P@ssword123", passwordDto.Password);

           //var result = await _userManager.ResetPasswordAsync(user,token,passwordDto.Password);

            if (!result.Succeeded)
            {
                return false;
            }
            return true;
        }
    }
}
