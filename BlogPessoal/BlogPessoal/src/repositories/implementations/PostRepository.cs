using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;
using BlogPessoal.src.data;

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
        public void NewPost(NewPostDTO post)
        {
            _context.Post.Add(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Photo = post.Photo,
            });
            _context.SaveChanges();
        }

        public void UpdatePost(UpdatePostDTO post)
        {
            PostModel model = GetPostById(post.Id);
            model.Title = post.Title;
            model.Description = post.Description;
            model.Photo = post.Photo;
            _context.Update(model);
            _context.SaveChanges();
        }

        public void DeletePost(int id)
        {
            _context.Post.Remove(GetPostById(id));
            _context.SaveChanges();
        }

        public List<PostModel> GetAllPosts()
        {
            return _context.Post.ToList();
        }

        public PostModel GetPostById(int id)
        {
            return _context.Post.FirstOrDefault(p => p.Id == id);
        }

        public List<PostModel> GetPostBySearch(string title, string description, string creator)
        {
            // TODO: Wait the implementation...
            return null;
        }
        #endregion

    }
}
