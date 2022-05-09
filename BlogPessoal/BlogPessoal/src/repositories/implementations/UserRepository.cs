using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;

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
        public void CreateUser(NewUserDTO userDTO)
        {
            _context.User.Add(new UsersModel
            {
                Email = userDTO.Email,
                Name = userDTO.Name,
                Password = userDTO.Password,
                Photo = userDTO.Photo
            });
            _context.SaveChanges();
        }

        public void UpdateUser(UpdateUserDTO userDTO)
        {
            UsersModel user = GetUserById(userDTO.Id);
            user.Name = userDTO.Name;
            user.Password = userDTO.Password;
            user.Photo = userDTO.Photo;
            _context.User.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _context.User.Remove(GetUserById(id));
            _context.SaveChanges();
        }

        public UsersModel GetUserByEmail(string email)
        {
            return _context.User.FirstOrDefault(u => u.Email == email);
        }

        public UsersModel GetUserById(int id)
        {
            return _context.User.FirstOrDefault(u => u.Id == id);
        }

        public UsersModel GetUserByName(string name)
        {
            return _context.User.FirstOrDefault(u => u.Name == name);
        }

        public List<UsersModel> GetUsersByName(string name)
        {
            throw new System.NotImplementedException();
        }
        #endregion

    }
}
