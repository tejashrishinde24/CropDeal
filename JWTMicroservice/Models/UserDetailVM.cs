namespace JWTMicroservice.Models
{
    public class UserDetailVM
    {

            public int Id { get; set; }

            public string Name { get; set; } = null!;

            public string? Role { get; set; }

            public string? Address { get; set; }

            public string EmailId { get; set; } = null!;

            public string PhoneNumber { get; set; } = null!;

            public string LoginId { get; set; } = null!;

            public string Password { get; set; } = null!;

            public bool? IsActive { get; set; }

        }
    }

