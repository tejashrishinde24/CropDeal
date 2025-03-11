using JWTMicroservice.Authentication;
using CropDealBackend.Interfaces;
using CropDealBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
//using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTMicroservice.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public AuthenticateController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService; // ✅ Ensure this is initialized correctly
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _context.UserDetails.FirstOrDefaultAsync(u => u.EmailId == model.EmailId);
            if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                return Unauthorized(new { message = "Invalid credentials" });

            var token = _jwtService.GenerateToken(user);
            return Ok(new { Token = token, Role = user.Role });
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDetailVM userObj)
        {
            UserDetail user = new UserDetail() {
                Name =  userObj.Name,
                Password = userObj.Password,
                LoginId = userObj.LoginId,
                EmailId = userObj.EmailId,
                Address = userObj.Address,
                Role = userObj.Role,
                PhoneNumber = userObj.PhoneNumber,
                IsActive = userObj.IsActive,
                CreatedAt = DateTime.UtcNow,
            };
            if (_context.UserDetails.Any(u => u.EmailId == user.EmailId))
                return BadRequest(new { message = "User already exists" });

            if (user.Role != "Farmer" && user.Role != "Dealer")
                return BadRequest(new { message = "Only Customer and Employee can be registered" });

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); // ✅ Secure password hashing
            _context.UserDetails.Add(user);
           await _context.SaveChangesAsync();

            return Ok(new { message = $"Registered successfully as {user.Role}" });
        }

    }
}
