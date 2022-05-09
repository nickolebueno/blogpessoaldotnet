using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogPessoal.src.controllers
{

    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {

        #region Attributes
        private readonly IUser _repository;
        #endregion


        #region Constructors
        public UserController(IUser userRepository)
        {
            _repository = userRepository;
        }
        #endregion


        #region Methods
        [HttpGet("id/{idUser}")]
        public IActionResult GetUserById([FromRoute] int idUser)
        {
            UsersModel user = _repository.GetUserById(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetUsersByName([FromQuery] string name)
        {
            List<UsersModel> users = _repository.GetUsersByName(name);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        [HttpGet("email/{emailUser}")]
        public IActionResult GetUserByEmail([FromRoute] string emailUser)
        {
            UsersModel user = _repository.GetUserByEmail(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public IActionResult NewUser ([FromBody] NewUserDTO userDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.CreateUser(userDTO);
            return Created($"api/Users/{userDTO.Email}", userDTO.Email);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO userDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.UpdateUser(userDTO);
            return Ok(userDTO);
        }

        [HttpDelete("delete/{idUsuario}")]
        public IActionResult DeleteUser([FromRoute] int idUser)
        {
            _repository.DeleteUser(idUser);
            return NoContent();
        }
        #endregion

    }
}
