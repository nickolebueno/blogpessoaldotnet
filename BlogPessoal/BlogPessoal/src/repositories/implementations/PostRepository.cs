using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;
using BlogPessoal.src.data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.repositories.implementations
{
    public class PostRepository : IPost
    {

        #region ATTRIBUTE
        private readonly PersonalBlogContext _context;
        #endregion


        #region CONSTRUCTOR
        public PostRepository(PersonalBlogContext context)
        {
            _context = context;
        }
        #endregion


        #region METHODS
        public async Task NewPostAsync(NewPostDTO post)
        {
            await _context.Post.AddAsync(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Photo = post.Photo,
            });
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(UpdatePostDTO post)
        {
            PostModel model = await GetPostByIdAsync(post.Id);
            model.Title = post.Title;
            model.Description = post.Description;
            model.Photo = post.Photo;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            _context.Post.Remove(await GetPostByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<PostModel>> GetAllPostsAsync()
        {
            return await _context.Post
                .Include(c => c.Creator)
                .Include(t => t.Theme)
                .ToListAsync();
        }

        public async Task<PostModel> GetPostByIdAsync(int id)
        {
            return await _context.Post
                .Include(c => c.Creator)
                .Include(t => t.Theme)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PostModel>> GetPostBySearchAsync(string title, string description, string creator)
        {
            switch (title, description, creator)
            {
                case (null, null, null):
                    return await GetAllPostsAsync();

                case (null, null, _):
                    return await _context.Post
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(creator))
                    .ToListAsync();

                case (null, _, null):
                    return await _context.Post
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(description))
                    .ToListAsync();

                case (_, null, null):
                    return await _context.Post
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToListAsync();

                case (_, _, null):
                    return await _context.Post
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title) & p.Theme.Description.Contains(description))
                    .ToListAsync();

                case (null, _, _):
                    return await _context.Post
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(description) & p.Creator.Name.Contains(creator))
                    .ToListAsync();

                case (_, null, _):
                    return await _context.Post
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title) & p.Creator.Name.Contains(creator))
                    .ToListAsync();

                case (_, _, _):
                    return await _context.Post
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title) | p.Theme.Description.Contains(description) | p.Creator.Name.Contains(creator))
                    .ToListAsync();
            }
        }
    }
    #endregion

}

