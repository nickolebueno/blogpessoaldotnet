using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.dtos
{

    /// <summary>
    /// <para> DTO for create a new user </para>
    /// <para>Criado por: Nickole Bueno</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class NewUserDTO
    {
        public NewUserDTO(string name, string email, string password, string photo)
        {
            Name = name;
            Email = email;
            Password = password;
            Photo = photo;
        }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(40)]
        public string Email { get; set; }

        [Required, StringLength(20)]
        public string Password { get; set; }

        public string Photo { get; set; }
    }

    /// <summary>
    /// <para> DTO for update a new user </para>
    /// <para>Criado por: Nickole Bueno</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class UpdateUserDTO
    {
        public UpdateUserDTO(string name, string password, string photo)
        {
            Name = name;
            Password = password;
            Photo = photo;
        }

        [Required]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(20)]
        public string Password { get; set; }

        public string Photo { get; set; }
    }

    /// <summary>
    /// <para> DTO for delete a new user </para>
    /// <para>Criado por: Nickole Bueno</para>
    /// <para>Versão: 1.0</para>
    /// <para>Data: 29/04/2022</para>
    /// </summary>
    public class DeleteUserDTO
    {

    }

}
