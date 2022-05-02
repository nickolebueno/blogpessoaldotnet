using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{

    /// <summary>
    /// <para> DTO for create a new post </para>
    /// <para>Criado por: Nickole Bueno</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NewPostDTO
    {
        public NewPostDTO(string title, string description, string photo, string creatorEmail, string themeDescription)
        {
            Title = title;
            Description = description;
            Photo = photo;
            CreatorEmail = creatorEmail;
            ThemeDescription = themeDescription;
        }

        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        public string Photo { get; set; }

        [Required, StringLength(30)]
        public string CreatorEmail { get; set; }

        [Required]
        public string ThemeDescription { get; set; }

    }

    /// <summary>
    /// <para> DTO for update a new post </para>
    /// <para>Criado por: Nickole Bueno</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpdatePostDTO
    {
        public UpdatePostDTO(string title, string description, string photo, string themeDescription)
        {
            Title = title;
            Description = description;
            Photo = photo;
            ThemeDescription = themeDescription;
        }

        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        public string Photo { get; set; }

        [Required]
        public string ThemeDescription { get; set; }
    }

}
