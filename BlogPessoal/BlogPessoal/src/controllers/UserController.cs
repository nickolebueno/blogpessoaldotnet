using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.controllers
{

    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {

        #region Attributes
        private readonly IUser _repository;
        private readonly IAuthentication _services;
        #endregion


        #region Constructors
        public UserController(IUser userRepository, IAuthentication services)
        {
            _repository = userRepository;
            _services = services;
        }
        #endregion


        #region Methods
        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">User does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idUser}")]
        [Authorize(Roles = "NORMAL,ADMIN")]
        public async Task<ActionResult> GetUserByIdAsync([FromRoute] int idUser)
        {
            UsersModel user = await _repository.GetUserByIdAsync(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Get user by name
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">User does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{nameUser}")]
        [Authorize(Roles = "NORMAL,ADMIN")]
        public async Task<ActionResult> GetUsersByNameAsync([FromQuery] string name)
        {
            List<UsersModel> users = await _repository.GetUsersByNameAsync(name);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">User does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsersModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "NORMAL,ADMIN")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string emailUser)
        {
            UsersModel user = await _repository.GetUserByEmailAsync(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="201">Returns the created user</response>
        /// <response code="400">Request error</response>
        /// <response code="401">Email already registered</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsersModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> NewUserAsync([FromBody] NewUserDTO userDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                await _services.CreateUserNoDuplicatesAsync(userDTO);
                return Created($"api/Users/email/{userDTO.Email}", userDTO.Email);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        /// <summary>
        /// Update the user
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return user updated</response>
        /// <response code="400">Request error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMIN")]
        public async Task<ActionResult> UpdateUserAsync([FromBody] UpdateUserDTO userDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            userDTO.Password = _services.EncodePassword(userDTO.Password);

            await _repository.UpdateUserAsync(userDTO);
            return Ok(userDTO);
        }

        /// <summary>
        /// Delete the user by the Id
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="204">User deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idUser}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int idUser)
        {
            await _repository.DeleteUserAsync(idUser);
            return NoContent();
        }
        #endregion


    }
}
