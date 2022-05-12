using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{
    public interface ITheme
    {

        Task NewThemeAsync(NewThemeDTO themeDTO);
        Task UpdateThemeAsync(UpdateThemeDTO themeDTO);
        Task DeleteThemeAsync(int id);
        Task<ThemeModel> GetThemeByIdAsync(int id);
        Task<List<ThemeModel>> GetThemesByDescriptionAsync(string description);

    }
}
