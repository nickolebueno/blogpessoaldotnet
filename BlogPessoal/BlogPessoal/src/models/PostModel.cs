using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.src.models
{
    [Table("tb_posts")]
    public class PostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Title { get; set; }

        [Required, StringLength(100)]
        public string Description { get; set; }

        [ForeignKey("FK_User")]
        public UsersModel Creator { get; set; }

        [ForeignKey("FK_Theme")]
        public ThemeModel Theme { get; set; }
    }
}