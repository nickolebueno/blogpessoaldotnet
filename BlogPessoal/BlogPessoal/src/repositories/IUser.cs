using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task CreateUserAsync(NewUserDTO userDTO);
        Task UpdateUserAsync(UpdateUserDTO userDTO);
        Task DeleteUserAsync(int id);
        Task<UsersModel> GetUserByIdAsync(int id);
        Task<UsersModel> GetUserByEmailAsync(string email);
        Task<UsersModel> GetUserByNameAsync(string name);
        Task<List<UsersModel>> GetUsersByNameAsync(string name);
    }
}
