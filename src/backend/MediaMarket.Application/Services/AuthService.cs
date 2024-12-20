﻿using AutoMapper;
using MediaMarket.Application.Bases;
using MediaMarket.Application.Configs;
using MediaMarket.Application.Contracts.Common;
using MediaMarket.Application.Contracts.Services;
using MediaMarket.Application.DTO.Request.Auth;
using MediaMarket.Application.DTO.Response.Auth;
using MediaMarket.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MediaMarket.Application.Services
{
    public class AuthService : BaseResponseHandler, IAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtConfig _jwtConfig;
        private readonly string _algorithm = SecurityAlgorithms.HmacSha256Signature;
        private readonly IMapper _mapper;
        private readonly IUser _user;

        public AuthService(
            UserManager<User> userManager,
            IOptionsMonitor<JwtConfig> jwtConfig,
            IMapper mapper,
            IUser user
        )
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig.CurrentValue;
            _mapper = mapper;
            _user = user;
        }

        public async Task<BaseResponse<RegisterResponse>> Register(RegisterRequest request)
        {
            var existsUser = await _userManager.FindByEmailAsync(request.Email);
            if (existsUser == null)
            {
                var user = new User
                {
                    Name = request.Name,
                    UserName = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    Email = request.Email,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    var userResponse = _mapper.Map<UserResponse>(user);
                    return Created(new RegisterResponse
                    {
                        Token = GenerateToken(user),
                        User = userResponse,
                    });
                }
                else
                {
                    return BadRequest<RegisterResponse>(string.Join("", result.Errors.Select(a => a.Description).ToList()));
                }
            }
            else
            {
                return BadRequest<RegisterResponse>("Email da ton tai");
            }
        }

        public async Task<BaseResponse<LoginResponse>> Login(LoginRequest request)
        {
            User? user;
            if (request.Email != null)
            {
                user = await _userManager.FindByEmailAsync(request.Email);
            }
            else
            {
                user = await _userManager.FindByNameAsync(request.UserName!);
            }

            if (user == null || !(await _userManager.CheckPasswordAsync(user, request.Password)))
            {
                return NotFound<LoginResponse>("Tai khoan hoac mat khau khong chinh xac");
            }

            string token = GenerateToken(user);

            var userResponse = _mapper.Map<UserResponse>(user);
            return Success(new LoginResponse
            {
                Token = token,
                User = userResponse,
            });
        }

        private string GenerateToken(User user)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();

            Byte[] accessSecretKey = Encoding.ASCII.GetBytes(_jwtConfig.SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, user.Name),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email!)
                }),
                Expires = DateTime.Now.AddMinutes(_jwtConfig.ExpirationMinutes),
                SigningCredentials = new(new SymmetricSecurityKey(accessSecretKey), _algorithm),
                Audience = _jwtConfig.Audience,
                Issuer = _jwtConfig.Issuer,
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }

        public async Task<BaseResponse<UserResponse>> GetProfile()
        {
            var currentUser = await _userManager.FindByIdAsync(_user.Id.ToString()!);
            return Success(_mapper.Map<UserResponse>(currentUser));
        }
    }
}
