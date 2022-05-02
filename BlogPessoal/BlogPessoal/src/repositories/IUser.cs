using BlogPessoal.src.dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.repositories
{

    /// <summary>
    /// <para>  </para>
    /// <para>Criado por: Nickole Bueno</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public interface IUser
    {
        void CreateUser(NewUserDTO userDTO);
        void UpdateUser(UpdateUserDTO userDTO);
        void DeleteUser(int id);
        UsersModel GetUserById(int id);
        UsersModel GetUserByEmail(string email);
        UsersModel GetUserByName(string name);
    }
}
