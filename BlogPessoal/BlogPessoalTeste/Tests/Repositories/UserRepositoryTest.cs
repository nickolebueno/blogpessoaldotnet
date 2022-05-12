using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implementations;
using BlogPessoal.src.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoal.Tests.Repositories
{

    [TestClass]
    public class UserRepositoryTeste
    {

        private PersonalBlogContext _context;
        private IUser _repository;

        [TestMethod]
        public async Task CreateUsersReturnsUsersCountComparison()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            await _repository.CreateUserAsync(
            new NewUserDTO("testname", "testemail@email.com", "testpassword", "pictureUrl", UserType.NORMAL));

            await _repository.CreateUserAsync(
            new NewUserDTO("testname2", "testemail2@email.com", "testpassword2", "pictureUrl2", UserType.ADMIN));

            Assert.AreEqual(2, _context.User.Count());
        }

        [TestMethod]
        public async Task GetUserByEmailReturnsNotNull()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            await _repository.CreateUserAsync(
            new NewUserDTO("testname", "testemail@email.com", "testpassword", "pictureUrl", UserType.NORMAL));

            var user = await
            _repository.GetUserByEmailAsync("testemail@email.com");

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task GetUserByIdReturnNotNullAndUserName()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            await _repository.CreateUserAsync(
            new NewUserDTO("testname", "testemail@email.com", "testpassword", "pictureUrl", UserType.NORMAL));

            var user = await _repository.GetUserByIdAsync(1);

            Assert.IsNotNull(user);

            Assert.AreEqual("testname", user.Name);
        }

        [TestMethod]
        public async Task UpdateUserReturnUserUpdated()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);

            await _repository.CreateUserAsync(
            new NewUserDTO("testname", "testemail@email.com", "testpassword", "pictureUrl", UserType.NORMAL));

            await _repository.UpdateUserAsync(
            new UpdateUserDTO("testname", "testpassword", "pictureUrl"));

             var older = await
            _repository.GetUserByEmailAsync("testemail@email.com");
            Assert.AreEqual("testname", _context.User.FirstOrDefault(u => u.Id == older.Id).Name);

            Assert.AreEqual("testpassword", _context.User.FirstOrDefault(u => u.Id == older.Id).Password);
        }
    }
}