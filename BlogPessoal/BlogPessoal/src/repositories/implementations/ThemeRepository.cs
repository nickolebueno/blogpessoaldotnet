using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.repositories.implementations
{
    public class ThemeRepository : ITheme
    {

        #region ATTRIBUTES
        private readonly PersonalBlogContext _context;
        #endregion


        #region CONSTRUCTOR
        public ThemeRepository(PersonalBlogContext context)
        {
            _context = context;
        }
        #endregion


        #region METHODS
        public void NewTheme(NewThemeDTO themeDTO)
        {
            _context.Theme.Add(new ThemeModel
            {
                Description = themeDTO.Description,
            });
            _context.SaveChanges();
        }

        public void UpdateTheme(UpdateThemeDTO themeDTO)
        {
            ThemeModel model = TakeThemeById(themeDTO.Id);
            model.Description = themeDTO.Description;
            _context.Update(model);
            _context.SaveChanges();
        }

        public void DeleteTheme(int id)
        {
            _context.Theme.Remove(TakeThemeById(id));
            _context.SaveChanges();
        }

        public List<ThemeModel> TakeAllThemes() 
        {
            return _context.Theme.ToList();
        }

        public List<ThemeModel> TakeThemeByDescription(string description)
        {
            return _context.Theme.Where(t => t.Description == description).ToList();
        }

        public ThemeModel TakeThemeById(int id)
        {
            return _context.Theme.FirstOrDefault(t => t.Id == id);
        }
        #endregion

    }
}
