using BlogPessoal.src.data;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPessoalTeste.Tests.Data
{
    [TestClass]
    public class PersonalBlogContextTest
    {
        private PersonalBlogContext _context;

      [TestInitialize]
      public void Initialization()
      {
            DbContextOptions<PersonalBlogContext> options = new DbContextOptionsBuilder<PersonalBlogContext>()
                    .UseInMemoryDatabase(databaseName: "db_blogpessoal")
                    .Options;

            _context = new PersonalBlogContext(options);

        }


        [TestMethod]
        public void InsertNewUser()
        {
            UsersModel user = new UsersModel();
            user.Id = 0;
            user.Name = "Nickole Bueno";
            user.Email = "nickole@email.com";
            user.Password = "passwordtest123";
            user.Photo = "photolink...";
            _context.User.Add(user);
            _context.SaveChanges();
            Assert.IsNotNull(_context.User.FirstOrDefault(u => u.Email == "nickole@email.com"));
        }
    }
}
