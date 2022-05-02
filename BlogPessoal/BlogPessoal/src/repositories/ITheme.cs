using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

namespace BlogPessoal.src.repositories
{
    public interface ITheme
    {

        void NewTheme(NewThemeDTO themeDTO);
        void UpdateTheme(UpdateThemeDTO themeDTO);
        void DeleteTheme(int id);
        ThemeModel TakeThemeById(int id);
        List<ThemeModel> TakeThemeByDescription(string description);

    }
}
