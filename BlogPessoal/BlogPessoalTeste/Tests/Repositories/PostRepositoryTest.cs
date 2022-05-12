using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implementations;
using BlogPessoal.src.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoalTeste.Tests.repositories
{

    [TestClass]
    public class PostagemRepositorioTeste
    {
        private PersonalBlogContext _context;
        private IUser _userRepo;
        private ITheme _themeRepo;
        private IPost _postRepo;

        [TestMethod]
        public async Task CreateThreePostsReturnsCountComparison()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _userRepo = new UserRepository(_context);
            _themeRepo = new ThemeRepository(_context);
            _postRepo = new PostRepository(_context);

            await _userRepo.CreateUserAsync(
            new NewUserDTO("testname", "testemail@email.com", "testpassword", "pictureUrl", UserType.NORMAL));

            await _userRepo.CreateUserAsync(
            new NewUserDTO("testname", "testemail@email.com", "testpassword", "pictureUrl", UserType.NORMAL));

            await _themeRepo.NewThemeAsync(new NewThemeDTO("C#"));
            await _themeRepo.NewThemeAsync(new NewThemeDTO("Java"));

            await _postRepo.NewPostAsync(
            new NewPostDTO(
            "TestTitle",
            "TestDescription",
            "PhotoURL",
            "teste@email.com",
            "TestThemeDescription"
            )
            );
            await _postRepo.NewPostAsync(
            new NewPostDTO(
            "TestTitle",
            "TestDescription",
            "PhotoURL",
            "teste@email.com",
            "TestThemeDescription"
            )
            );
            await _postRepo.NewPostAsync(
            new NewPostDTO(
            "TestTitle",
            "TestDescription",
            "PhotoURL",
            "teste@email.com",
            "TestThemeDescription"
            )
            );

            var postagens = await _postRepo.GetAllPostsAsync();

            Assert.AreEqual(3, postagens.Count());
        }

        [TestMethod]
        public async Task UpdatePostReturnsPostUpdated()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _userRepo = new UserRepository(_context);
            _themeRepo = new ThemeRepository(_context);
            _postRepo = new PostRepository(_context);

            await _userRepo.CreateUserAsync(
            new NewUserDTO("testname", "testemail@email.com", "testpassword", "pictureUrl", UserType.NORMAL));

            await _themeRepo.NewThemeAsync(new NewThemeDTO("COBOL"));
            await _themeRepo.NewThemeAsync(new NewThemeDTO("C#"));

            await _postRepo.NewPostAsync(
            new NewPostDTO(
            "TestTitle",
            "TestDescription",
            "PhotoURL",
            "teste@email.com",
            "TestThemeDescription"
            )
            );

            await _postRepo.UpdatePostAsync(
            new UpdatePostDTO(
            "TestTitle",
            "TestDescription",
            "PhotoURL",
            "TestThemeDescription"
            )
            );
            var postagem = await _postRepo.GetPostByIdAsync(1);

            Assert.AreEqual("TestTitle", postagem.Title);
            Assert.AreEqual("TestDescription",
            postagem.Description);
            Assert.AreEqual("PhotoURLUpdated", postagem.Photo);
            Assert.AreEqual("TestThemeDescription", postagem.Theme.Description);
        }

        [TestMethod]
        public async Task GetPostsBySearchReturnsCountComparison()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _userRepo = new UserRepository(_context);
            _themeRepo = new ThemeRepository(_context);
            _postRepo = new PostRepository(_context);

            await _userRepo.CreateUserAsync(
            new NewUserDTO("testname", "testemail@email.com", "testpassword", "pictureUrl", UserType.NORMAL));

            await _userRepo.CreateUserAsync(
            new NewUserDTO("testname2", "testemail@email.com2", "testpassword2", "pictureUrl2", UserType.ADMIN));

            await _themeRepo.NewThemeAsync(new NewThemeDTO("C#"));
            await _themeRepo.NewThemeAsync(new NewThemeDTO("Java"));

            await _postRepo.NewPostAsync(
            new NewPostDTO(
            "TestTitle",
            "TestDescription",
            "PhotoURL",
            "teste@email.com",
            "TestThemeDescription"
            )
            );
            await _postRepo.NewPostAsync(
            new NewPostDTO(
            "TestTitle",
            "TestDescription",
            "PhotoURL",
            "teste@email.com",
            "TestThemeDescription"
            )
            );
            await _postRepo.NewPostAsync(
            new NewPostDTO(
            "TestTitle",
            "TestDescription",
            "PhotoURL",
            "teste@email.com",
            "TestThemeDescription"
            )
            );

            var postagensTeste1 = await
            _postRepo.GetPostBySearchAsync("TestTitle", null, null);
            var postagensTeste2 = await
            _postRepo.GetPostBySearchAsync(null, "TestDescription", null);
            var postagensTeste3 = await
            _postRepo.GetPostBySearchAsync(null, null, "testname");

            Assert.AreEqual(2, postagensTeste1.Count);
            Assert.AreEqual(2, postagensTeste2.Count);
            Assert.AreEqual(2, postagensTeste3.Count);
        }
    }
}