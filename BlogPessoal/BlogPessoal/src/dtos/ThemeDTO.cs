using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{

    /// <summary>
    /// <para> DTO for create a new theme </para>
    /// <para>Criado por: Nickole Bueno</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NewThemeDTO
    {
        public NewThemeDTO(string description)
        {
            Description = description;
        }

        [Required, StringLength(150)]
        public string Description { get; set; }
    }

    /// <summary>
    /// <para> DTO for update the theme </para>
    /// <para>Criado por: Nickole Bueno</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpdateThemeDTO
    {
        public UpdateThemeDTO(string description)
        {
            Description = description;
        }

        [Required, StringLength(150)]
        public string Description { get; set; }
    }

}
