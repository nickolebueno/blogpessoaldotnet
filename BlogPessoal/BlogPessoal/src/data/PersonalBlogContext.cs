using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.data
{

    public class PersonalBlogContext : DbContext
    {
        
        public DbSet<UsersModel> User { get; set; }
        public DbSet<PostModel> Post { get; set; }
        public DbSet<ThemeModel> Theme { get; set;}

        public PersonalBlogContext(DbContextOptions<PersonalBlogContext> opt) : base(opt)
        {        

        }

    }

}
