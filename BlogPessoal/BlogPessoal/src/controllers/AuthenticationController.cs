using BlogPessoal.src.dtos;
using BlogPessoal.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Authentication")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {

        #region Attributes
        private readonly IAuthentication _services;
        #endregion


        #region Contructors
        private AuthenticationController (IAuthentication services)
        {
            _services = services;
        }
        #endregion


        #region Methods
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Autenticar([FromBody] AuthenticateDTO authenticationDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var autorizacao = _services.GetAuthorization(authenticationDTO);
                return Ok(autorizacao);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
                #endregion

            }
        }
    }
}
