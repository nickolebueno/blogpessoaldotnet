using System.Linq;
using System.Threading.Tasks;
using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.repositories;
using BlogPessoal.src.repositories.implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlogPessoal.Tests.Repositories
{

    [TestClass]
    public class ThemeRepositoryTeste
    {

        private PersonalBlogContext _context;
        private ITheme _repository;

        [TestMethod]
        public async Task CreateThemesReturnsThemesCountComparison()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            await _repository.NewThemeAsync(new NewThemeDTO("C#"));
            await _repository.NewThemeAsync(new NewThemeDTO("Java"));
            await _repository.NewThemeAsync(new NewThemeDTO("Python"));
            await _repository.NewThemeAsync(new NewThemeDTO("JavaScript"));
            var themes = await _repository.GetAllThemesAsync();

            Assert.AreEqual(4, themes.Count);
        }

        [TestMethod]
        public async Task GetThemeByIdReturnsThemeDescriptionComparison()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            await _repository.NewThemeAsync(new NewThemeDTO("C#"));

            var theme = await _repository.GetThemeByIdAsync(1);

            Assert.AreEqual("C#", theme.Description);
        }

        [TestMethod]
        public async Task GetThemesByDescriptionReturnsCountComparison()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            await _repository.NewThemeAsync(new NewThemeDTO("Java"));

            await _repository.NewThemeAsync(new NewThemeDTO("JavaScript"));

            var theme = await _repository.GetThemesByDescriptionAsync("Java");

            Assert.AreEqual(2, theme.Count);
        }

        [TestMethod]
        public async Task ChangePythonThemeReturnsCobol()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            await _repository.NewThemeAsync(new NewThemeDTO("Python"));

            await _repository.UpdateThemeAsync(new UpdateThemeDTO(
            "COBOL"));
            var theme = await _repository.GetThemeByIdAsync(1);

            Assert.AreEqual("COBOL", theme.Description);
        }

        [TestMethod]
        public async Task DeleteThemeReturnsNull()
        {

            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_personalblog")
            .Options;
            _context = new PersonalBlogContext(opt);
            _repository = new ThemeRepository(_context);

            await _repository.NewThemeAsync(new NewThemeDTO("C#"));

            await _repository.DeleteThemeAsync(1);

            Assert.IsNull(await _repository.GetThemeByIdAsync(1));
        }
    }
}