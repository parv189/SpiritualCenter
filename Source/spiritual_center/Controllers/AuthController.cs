using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using spiritual_center.DTOs;
using spiritual_center.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace spiritual_center.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserContext _context;
        private readonly IConfiguration _config;
        public AuthController(UserContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;

        }
        [HttpPost("Register")]
        public async Task<IActionResult>register([FromBody] RegisterDTO registerDTO)
        {
            User user = new User();
            user.Email = registerDTO.Email;
            user.FirstName = registerDTO.FirstName;
            user.LastName = registerDTO.LastName;
            user.MidleName = registerDTO.MidleName;
            user.Initiation_Date = registerDTO.Initiation_Date;


            GenerateUserId(user.FirstName, user.LastName, user.Initiation_Date, out string id);
            user.User_Id = id;

            GeneratePassword(user.FirstName, user.LastName,out string Password);
            registerDTO.Passward = Password;

            CreatePasswordHash(registerDTO.Passward, out byte[] PasswordHash, out byte[] PasswordSalt);
            user.PasswardHash = PasswordHash;
            user.PasswardSalt = PasswordSalt;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }
        private void CreatePasswordHash(string Passward, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            var hamc = new HMACSHA512();
            PasswordSalt = hamc.Key;
            PasswordHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Passward));
        }
        private void GeneratePassword(string FirstName, string LastName, out string Password)
        {
            Password = FirstName.Substring(0,4)+"@"+LastName.Substring(0,4);
        }
        private void GenerateUserId(string FirstName, string LastName,DateTime Initiation_Date, out string id)
        {
            id = Initiation_Date.Year + FirstName.Substring(0,2) + LastName.Substring(0,2) + Initiation_Date.Month;
        }
        [EnableCors("Policy1")]
        [HttpPost("Login")]

        public async Task<IActionResult> login([FromBody] LoginDTO loginDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDTO.Email);
            if(user == null)
            {
                return Unauthorized("Invalid Email");
            }
            var hmac = new HMACSHA512(user.PasswardSalt);
            var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(loginDTO.Password));

            for(int i = 0; i< ComputedHash.Length; i++)
            {
                if (ComputedHash[i] != user.PasswardHash[i])
                {
                    return Unauthorized("Invalid Password");
                }
            }
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,
                          user.User_Id),
                new Claim(ClaimTypes.Email,
                          user.Email)
            };
            SymmetricSecurityKey Kay = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config["TokenKey"]));

            var cred = new SigningCredentials(Kay, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = cred
            };
            var tokenHeandler = new JwtSecurityTokenHandler();
            var token = tokenHeandler.CreateToken(tokenDescriptor);
            var resp = new
            {
                Status = "Login Successfull",
                Token = tokenHeandler.WriteToken(token),
            };
            return Ok(resp);
        }
    }   
}
