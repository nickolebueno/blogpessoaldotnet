using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        /// <summary>
        /// Get posts by as async.
        /// </summary>
        /// <returns> ActionResult </returns>
        /// <response code = "200">Post object</response>
        /// <response code = "204">Post empty</response>
        [HttpGet("id/{idPost}")]
        [Authorize]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int idPost)
        {
            PostModel post = await _repository.GetPostByIdAsync(idPost);

            if (post == null) return NotFound();

            return Ok(post);
        }

        /// <summary>
        /// Get all posts
        /// </summary>
        /// <returns>ActionResult </returns>
        /// <response code="200">Posts list</response>
        /// <response code="204">Post empty list</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            List<PostModel> posts = await _repository.GetAllPostsAsync();

            if (posts.Count < 1) return NoContent();

            return Ok(posts);
        }

        /// <summary>
        /// Get post list by search
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return posts list</response>
        /// <response code="204">Posts not found for this search</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult> GetPostBySearchAsync([FromRoute] string title, [FromRoute] string description, [FromRoute] string creator)
        {
            List<PostModel> posts = await _repository.GetPostBySearchAsync(title, description, creator);

            if (posts.Count < 1) return NoContent();
            
            return Ok(posts);
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="201">Retorna created post</response>
        /// <response code="400">Request error</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NewPostAsync([FromBody] NewPostDTO postDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewPostAsync(postDTO);
            return Created($"api/Posts/{postDTO.Title}", postDTO.Title);
        }

        /// <summary>
        /// Update post
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the updated post</response>
        /// <response code="400">Request error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> UpdatePostAsync([FromBody] UpdatePostDTO postDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdatePostAsync(postDTO);
            return Ok(postDTO);
        }

        /// <summary>
        /// Delete the post by id.
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="204">Post deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idPost}")]
        [Authorize]
        public async Task<ActionResult> DeletePostAsync([FromRoute] int idPost)
        {
            await _repository.DeletePostAsync(idPost);
            return NoContent();
        }
        #endregion

    }

}
