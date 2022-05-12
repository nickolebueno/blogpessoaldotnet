using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.repositories
{
    public interface IPost
    {
        Task NewPostAsync(NewPostDTO post);
        Task UpdatePostAsync(UpdatePostDTO post);
        Task DeletePostAsync(int id);
        Task<PostModel> GetPostByIdAsync(int id);
        Task<List<PostModel>> GetAllPostsAsync();
        Task<List<PostModel>> GetPostBySearchAsync(string title, string description, string creator);
    }
}
