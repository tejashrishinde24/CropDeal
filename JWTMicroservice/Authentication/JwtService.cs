using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JWTMicroservice.Models;
using Microsoft.IdentityModel.Tokens;

public class JwtService
{
    private readonly string _secretKey;
    private readonly string _issuer = "CropDeal";
    private readonly string _audience = "CropDealUsers";

    public JwtService(string secretKey)
    {
        _secretKey = secretKey;
    }

    public string GenerateToken(UserDetail user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.EmailId),
            new Claim(ClaimTypes.Role, user.Role ?? "Customer") // Default role if null
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(5), // Token valid for 5 hours
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
