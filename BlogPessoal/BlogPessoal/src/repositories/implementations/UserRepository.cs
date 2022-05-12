using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories.implementations
{
    public class UserRepository : IUser
    {

        #region ATTRIBUTES
        private readonly PersonalBlogContext _context;
        #endregion


        #region CONSTRUCTORS
        public UserRepository(PersonalBlogContext context)
        {
            _context = context;
        }
        #endregion


        #region METHODS
        public async Task CreateUserAsync(NewUserDTO userDTO)
        {
            await _context.User.AddAsync(new UsersModel
            {
                Email = userDTO.Email,
                Name = userDTO.Name,
                Password = userDTO.Password,
                Photo = userDTO.Photo,
                UserType = userDTO.UserType,
            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(UpdateUserDTO userDTO)
        {
            UsersModel user = await GetUserByIdAsync(userDTO.Id);
            user.Name = userDTO.Name;
            user.Password = userDTO.Password;
            user.Photo = userDTO.Photo;
            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            _context.User.Remove(await GetUserByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<UsersModel> GetUserByEmailAsync(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<UsersModel> GetUserByIdAsync(int id)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UsersModel> GetUserByNameAsync(string name)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<List<UsersModel>> GetUsersByNameAsync(string name)
        {
            return await _context.User
                .Where(u => u.Name.Contains(name))
                .ToListAsync();
        }
        #endregion

    }
}
