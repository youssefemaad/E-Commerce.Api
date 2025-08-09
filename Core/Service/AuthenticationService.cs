using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Exceptions;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityModuleDto;

namespace Service
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager, IConfiguration _configuration, IMapper mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                return false;
            return true;
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string Email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                                               .FirstOrDefaultAsync(u => u.Email == Email) ?? throw new UserNotFound(Email);

            if (user.Address is null)
                throw new AddressNotFoundException(Email);
            return mapper.Map<Address, AddressDto>(user.Address);
        }

        public async Task<UserDto> GetCurrentUserAsync(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email) ?? throw new UserNotFound(Email);
            return new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email!,
                Token = await CreateTokenAsync(user),
            };
        }


        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User is null)
                throw new UserNotFound(loginDto.Email);
            var result = await _userManager.CheckPasswordAsync(User, loginDto.Password);
            if (result)
            {
                return new UserDto
                {
                    DisplayName = User.DisplayName,
                    Email = User.Email!,
                    Token = await CreateTokenAsync(User),
                };
            }
            else
            {
                throw new UnAuthorizedException();
            }
        }

        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                                               .FirstOrDefaultAsync(u => u.Email == email) ?? throw new UserNotFound(email);

            if (user.Address is not null)
            {
                user.Address.City = addressDto.City;
                user.Address.Street = addressDto.Street;
                user.Address.Country = addressDto.Country;
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
            }
            else
            {
                user.Address = mapper.Map<AddressDto, Address>(addressDto);
            }

            await _userManager.UpdateAsync(user);
            return mapper.Map<AddressDto>(user.Address);
        }


        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                DisplayName = registerDto.DisplayName
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    DisplayName = user.DisplayName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user),
                };
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new BadRequestException(errors);
            }
        }



        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!),
            };
            var Roles = await _userManager.GetRolesAsync(user);

            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var SecurityKey = _configuration.GetSection("JwtOptions")["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JwtOptions")["Issuer"],
                audience: _configuration.GetSection("JwtOptions")["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}