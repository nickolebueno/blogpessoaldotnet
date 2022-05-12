using BlogPessoal.src.data;
using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task NewThemeAsync(NewThemeDTO themeDTO)
        {
            await _context.Theme.AddAsync(new ThemeModel
            {
                Description = themeDTO.Description,
            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdateThemeAsync(UpdateThemeDTO themeDTO)
        {
            ThemeModel model = await GetThemeByIdAsync(themeDTO.Id);
            model.Description = themeDTO.Description;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteThemeAsync(int id)
        {
            _context.Theme.Remove(await GetThemeByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<ThemeModel>> TakeAllThemesAsync() 
        {
            return await _context.Theme.ToListAsync();
        }

        public async Task<List<ThemeModel>> GetThemesByDescriptionAsync(string description)
        {
            return await _context.Theme.Where(t => t.Description == description).ToListAsync();
        }

        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
            return await _context.Theme.FirstOrDefaultAsync(t => t.Id == id);
        }
        #endregion

    }
}
