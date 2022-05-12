using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogPessoal.src.services.implementations
{
    public class AuthenticateServices : IAuthentication
    {

        #region Attributes
        private readonly IUser _repository;
        public IConfiguration Configuration { get; }
        #endregion


        #region Constructors
        public AuthenticateServices(IUser repository, IConfiguration configuration)
        {
            _repository = repository;
            Configuration = configuration;
        }
        #endregion


        #region Methods
        public string EncodePassword(string password)
        {
            byte[] vs = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(vs);
        }

        public async Task CreateUserNoDuplicatesAsync (NewUserDTO userDTO)
        {
            UsersModel user = await _repository.GetUserByEmailAsync(userDTO.Email);
            if (user != null) throw new Exception("Este email já está sendo utilizado");
            userDTO.Password = EncodePassword(userDTO.Password) ;
            await _repository.CreateUserAsync(userDTO);
        }

        public string GenerateToken(UsersModel user)
        {
            var manipulationToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email.ToString()),
                new Claim(ClaimTypes.Role, user.UserType.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            )
            };
            var token = manipulationToken.CreateToken(tokenDescription);
            return manipulationToken.WriteToken(token);
        }

        public async Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticateDTO authenticateDTO)
        {
            var user = await _repository.GetUserByEmailAsync(authenticateDTO.Email);
            if (user == null) throw new Exception("User not found!");
            if (user.Password != EncodePassword(authenticateDTO.Password)) throw new Exception("Wrong password!");
            return new AuthorizationDTO(user.Id, user.Email, user.UserType, GenerateToken(user));
        }
        #endregion

    }
}
