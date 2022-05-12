using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpGet("id/{idUser}")]
        public async Task<ActionResult> GetUserByIdAsync([FromRoute] int idUser)
        {
            UsersModel user = await _repository.GetUserByIdAsync(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersByNameAsync([FromQuery] string name)
        {
            List<UsersModel> users = await _repository.GetUsersByNameAsync(name);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        [HttpGet("email/{emailUser}")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string emailUser)
        {
            UsersModel user = await _repository.GetUserByEmailAsync(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

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

        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMIN")]
        public async Task<ActionResult> UpdateUserAsync([FromBody] UpdateUserDTO userDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            userDTO.Password = _services.EncodePassword(userDTO.Password);

            await _repository.UpdateUserAsync(userDTO);
            return Ok(userDTO);
        }

        [HttpDelete("delete/{idUsuario}")]
        public async Task<ActionResult> DeleteUserAsync([FromRoute] int idUser)
        {
            await _repository.DeleteUserAsync(idUser);
            return NoContent();
        }
        #endregion

    }
}
