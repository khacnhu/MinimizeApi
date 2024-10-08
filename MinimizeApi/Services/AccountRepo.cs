﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MinimizeApi.Data;
using MinimizeApi.Models.Dtos;
using MinimizeApi.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimizeApi.Services
{
    public class AccountRepo(AppDbContext appDbContext, IMapper mapper, IConfiguration configuration) : IAccountRepo
    {
        public async Task<LoginResponse> Login(LoginDTO loginDTO)
        {
            var user = await FindUserByEmail(loginDTO.Email);

            if (user != null) 
            { 
                bool verifyPassword = BCrypt.Net.BCrypt.Verify(loginDTO.Password, user.Password);
                if (!verifyPassword) 
                {
                    return new LoginResponse(false, null, "Invalid credentials");
                }

                string token = GenerateToken(user);
                return new LoginResponse(true, token, "Userlogin successfully");
            }

            throw new NotImplementedException();
        }

        public async Task<Response> Register(RegisterDTO registerDTO)
        {
            var user = await FindUserByEmail(registerDTO.Email);
            if ( user != null)
            {
                return new Response(false, "User have already registered");
            }
            
            var addUser = mapper.Map<User>(registerDTO);
            
            addUser.Password = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password);
            appDbContext.Users.Add(addUser);
            await appDbContext.SaveChangesAsync();
            return new Response(true, "Register account succesfully");

            

        }

        private async Task<User> FindUserByEmail(string email)
        {
            var strEmail = email.ToLower();
            return await appDbContext.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == strEmail);
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim("FULLNAME", user.Name),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var token = new JwtSecurityToken(
                    issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims:userClaims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials

               );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

      


    }
}
