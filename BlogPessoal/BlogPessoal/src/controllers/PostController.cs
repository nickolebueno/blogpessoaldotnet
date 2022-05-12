using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.controllers
{

    [ApiController]
    [Route("api/Posts")]
    [Produces("application/json")]
    public class PostController : ControllerBase
    {

        #region Attributes
        private readonly IPost _repository;
        #endregion


        #region Constructors
        public PostController(IPost postRepository)
        {
            _repository = postRepository;
        }
        #endregion


        #region Methods
        [HttpGet("id/{idPost}")]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int idPost)
        {
            PostModel post = await _repository.GetPostByIdAsync(idPost);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            List<PostModel> posts = await _repository.GetAllPostsAsync();

            if (posts.Count < 1) return NoContent();

            return Ok(posts);
        }

        [HttpGet]
        public async Task<ActionResult> GetPostBySearchAsync([FromRoute] string title, [FromRoute] string description, [FromRoute] string creator)
        {
            List<PostModel> posts = await _repository.GetPostBySearchAsync(title, description, creator);

            if (posts.Count < 1) return NoContent();
            
            return Ok(posts);
        }

        [HttpPost]
        public async Task<ActionResult> NewPostAsync([FromBody] NewPostDTO postDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewPostAsync(postDTO);
            return Created($"api/Posts/{postDTO.Title}", postDTO.Title);
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePostAsync([FromBody] UpdatePostDTO postDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdatePostAsync(postDTO);
            return Ok(postDTO);
        }

        [HttpDelete("delete/{idPost}")]
        public async Task<ActionResult> DeletePostAsync([FromRoute] int idPost)
        {
            await _repository.DeletePostAsync(idPost);
            return NoContent();
        }
        #endregion

    }

}
