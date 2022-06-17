using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TonVinhHienMau.Data;
using TonVinhHienMau.Data.Models;
using WebGhiChu.Data;

namespace TonVinhHienMau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationSettings _appSettings;

        public AccountController(
             AppDbContext context,
             UserManager<ApplicationUser> userManager,
             IOptions<ApplicationSettings> appSettings
             )
        {
            _context = context;
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.FindByNameAsync(loginRequest.Username);
                        
            if (user != null && await _userManager.CheckPasswordAsync(user, loginRequest.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString()),

                    }),
                    Expires = DateTime.UtcNow.AddHours(2),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), 
                        SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new LoginResult()
                {
                    Message = "Đăng nhập thành công!",
                    Success = true,
                    Token = token,
                }) ;
            }
            else
                return BadRequest(new { message = "Tài khoản hoặc mật khẩu không đúng." });
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest register)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName = register.Username,
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, register.Password);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
