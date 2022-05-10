using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.services
{
    public interface IAuthentication
    {
        string EncodePassword(string password);
        void CreateUserNoDuplicates(NewUserDTO userDTO);
        string GenerateToken(UsersModel user);
        AuthorizationDTO GetAuthorization(AuthenticateDTO authenticateDTO);
    }
}
