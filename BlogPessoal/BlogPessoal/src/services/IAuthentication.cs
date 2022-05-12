using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Threading.Tasks;

namespace BlogPessoal.src.services
{
    public interface IAuthentication
    {
        string EncodePassword(string password);
        Task CreateUserNoDuplicatesAsync(NewUserDTO userDTO);
        string GenerateToken(UsersModel user);
        Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticateDTO authenticateDTO);
    }
}
