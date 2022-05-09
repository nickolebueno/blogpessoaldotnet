using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public IActionResult GetPostById([FromRoute] int idPost)
        {
            PostModel post = _repository.GetPostById(idPost);

            if (post == null) return NotFound();

            return Ok(post);
        }

        [HttpGet]
        public IActionResult GetAllPosts()
        {
            List<PostModel> posts = _repository.GetAllPosts();

            if (posts.Count < 1) return NoContent();

            return Ok(posts);
        }

        [HttpGet]
        public IActionResult GetPostBySearch(
                [FromRoute] string title,
                [FromRoute] string description,
                [FromRoute] string creator
            )
        {
            List<PostModel> posts = _repository.GetPostBySearch(title, description, creator);

            if (posts.Count < 1) return NoContent();
            
            return Ok(posts);
        }

        [HttpPost]
        public IActionResult NewPost([FromBody] NewPostDTO postDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.NewPost(postDTO);
            return Created($"api/Posts/{postDTO.Title}", postDTO.Title);
        }

        [HttpPut]
        public IActionResult UpdatePost([FromBody] UpdatePostDTO postDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.UpdatePost(postDTO);
            return Ok(postDTO);
        }

        [HttpDelete("delete/{idPost}")]
        public IActionResult DeletePost([FromRoute] int idPost)
        {
            _repository.DeletePost(idPost);
            return NoContent();
        }
        #endregion

    }

}
