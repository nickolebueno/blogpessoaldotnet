using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

namespace BlogPessoal.src.repositories
{
    public interface IPost
    {
        void NewPost(NewPostDTO post);
        void UpdatePost(UpdatePostDTO post);
        void DeletePost(int id);
        PostModel GetPostById(int id);
        List<PostModel> GetAllPosts();
        List<PostModel> GetPostBySearch(string title, string description, string creator);
    }
}
