using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JWTMicroservice.Controllers;
using JWTMicroservice.Authentication;
using CropDealBackend.Models;

namespace AuthenticateControllerTests
{
    [TestClass]
    public class AuthenticateControllerTests
    {
        private AuthenticateController _controller;
        private ApplicationDbContext _context;
        private JwtService _jwtService;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _jwtService = new JwtService("your-secret-key"); // Adjust based on your implementation

            _controller = new AuthenticateController(_context, _jwtService);
        }

      
        [TestMethod]
        public async Task Register_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var newUser = new UserDetailVM
            {
                Name = "Test User",
                Password = "Test@1234",
                LoginId = "testuser",
                EmailId = "test@example.com",
                Address = "Test Address",
                Role = "Farmer",
                PhoneNumber = "1234567890",
                IsActive = true
            };

            // Act
            var result = await _controller.Register(newUser);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }

       
        [TestMethod]
        public async Task Register_DuplicateEmail_ReturnsBadRequest()
        {
            // Arrange
            var existingUser = new UserDetail
            {
                Name = "Existing User",
                Password = BCrypt.Net.BCrypt.HashPassword("Test@1234"),
                LoginId = "existinguser",
                EmailId = "existing@example.com",
                Address = "Existing Address",
                Role = "Farmer",
                PhoneNumber = "1234567890",
                IsActive = true,
                CreatedAt = System.DateTime.UtcNow
            };

            _context.UserDetails.Add(existingUser);
            await _context.SaveChangesAsync();

            var newUser = new UserDetailVM
            {
                Name = "New User",
                Password = "Test@1234",
                LoginId = "newuser",
                EmailId = "existing@example.com", // Same email as existing user
                Address = "New Address",
                Role = "Farmer",
                PhoneNumber = "0987654321",
                IsActive = true
            };

            // Act
            var result = await _controller.Register(newUser) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("User already exists", result.Value.GetType().GetProperty("message")?.GetValue(result.Value, null));
        }

       
        [TestMethod]
        public async Task Register_InvalidRole_ReturnsBadRequest()
        {
            // Arrange
            var newUser = new UserDetailVM
            {
                Name = "Invalid Role User",
                Password = "Test@1234",
                LoginId = "invalidroleuser",
                EmailId = "invalidrole@example.com",
                Address = "Invalid Address",
                Role = "Admin", // Invalid role
                PhoneNumber = "1234567890",
                IsActive = true
            };

            // Act
            var result = await _controller.Register(newUser) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("Only Farmer and Dealer can be registered", result.Value.GetType().GetProperty("message")?.GetValue(result.Value, null));
        }

        /// <summary>
        /// ✅ Test Case: Valid login should return OkResult with a token.
        /// </summary>
        [TestMethod]
        public async Task Login_ValidUser_ReturnsOkResult()
        {
            // Arrange
            var user = new UserDetail
            {
                Name = "Login User",
                Password = BCrypt.Net.BCrypt.HashPassword("Test@1234"),
                LoginId = "loginuser",
                EmailId = "login@example.com",
                Address = "Login Address",
                Role = "Farmer",
                PhoneNumber = "1234567890",
                IsActive = true,
                CreatedAt = System.DateTime.UtcNow
            };

            _context.UserDetails.Add(user);
            await _context.SaveChangesAsync();

            var loginModel = new LoginModel
            {
                EmailId = "login@example.com",
                Username = "loginuser",
                Password = "TestUser123"
            };

            // Act
            var result = await _controller.Login(loginModel) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsTrue(result.Value.GetType().GetProperty("Token") != null);
        }

        /// <summary>
        /// ❌ Test Case: Invalid login should return Unauthorized.
        /// </summary>
        [TestMethod]
        public async Task Login_InvalidUser_ReturnsUnauthorized()
        {
            // Arrange
            var loginModel = new LoginModel
            {
                EmailId = "nonexistent@example.com",
                Password = "WrongPassword"
            };

            // Act
            var result = await _controller.Login(loginModel) as UnauthorizedObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(401, result.StatusCode);
            Assert.AreEqual("Invalid credentials", result.Value.GetType().GetProperty("message")?.GetValue(result.Value, null));
        }
    }
}
