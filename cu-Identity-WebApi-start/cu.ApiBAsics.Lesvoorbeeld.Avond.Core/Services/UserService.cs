using cu.ApiBAsics.Lesvoorbeeld.Avond.Core.Entities;
using cu.ApiBAsics.Lesvoorbeeld.Avond.Core.Interfaces.Services;
using cu.ApiBAsics.Lesvoorbeeld.Avond.Core.Services.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace cu.ApiBAsics.Lesvoorbeeld.Avond.Core.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        public  IQueryable<ApplicationUser> GetUsers()
        {
            return  _userManager.Users;
        }

        public async Task<AuthenticateResultModel> Login(string username, string password)
        {
            //authenticate user
            //check credentials
            var user = await _userManager.FindByNameAsync(username);
            if (!await _userManager.CheckPasswordAsync(user, password))
            {
                return new AuthenticateResultModel {
                Messages = new List<string> { "Login failed!"}
                };
            }
            //user aythenticated => generate token
            //get the claims
            var claims = (List<Claim>)await _userManager.GetClaimsAsync(user);
            //generate the token
            var token = _jwtService.GenerateToken(claims);
            //serialize the token
            var serializedToken = _jwtService.SerializeToken(token);
            //return the token
            return new AuthenticateResultModel { Success = true, Messages = new List<string> { serializedToken } };
        }

        public async Task<AuthenticateResultModel> Register(string firstname, string lastname, string username, string password)
        {
            //create a user
            var applicationUser = new ApplicationUser 
            {
                Firstname = firstname,
                Lastname = lastname,
                UserName = username,
                Email = username
            };
            //store in database
            var result = await _userManager.CreateAsync(applicationUser,password);
            if (!result.Succeeded) 
            {
                return new AuthenticateResultModel 
                {
                    Messages = new List<string> { "Registration failed!"}
                };
            }
            await _userManager.AddClaimAsync(applicationUser,
                new Claim(ClaimTypes.Role, "customer"));
            return new AuthenticateResultModel
            {
                Success = true,
                Messages = new List<string> {"user registered!" }
            };
        }
    }
}
