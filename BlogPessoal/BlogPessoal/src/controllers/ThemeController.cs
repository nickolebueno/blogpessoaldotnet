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
    [Route("api/Themes")]
    [Produces("application/json")]
    public class ThemeController : ControllerBase
    {

        #region Attributes
        private readonly ITheme _repository;
        #endregion


        #region Constructors
        public ThemeController(ITheme themeRepository)
        {
            _repository = themeRepository;
        }
        #endregion


        #region Methods

        /// <summary>
        /// Get all themes
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Themes list</response>
        /// <response code="204">List empty</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> PegarTodosTemasAsync()
        {
            List<ThemeModel> themes = await _repository.GetAllThemesAsync();

            if (themes.Count < 1) return NoContent();

            return Ok(themes);
        }

        /// <summary>
        /// Get theme by Id
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the theme</response>
        /// <response code="404">Theme does not exist</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idTheme}")]
        [Authorize]
        public async Task<ActionResult> GetThemeByIdAsync([FromRoute] int idTheme)
        {
            ThemeModel themeModel = await _repository.GetThemeByIdAsync(idTheme);

            if (themeModel == null) return NotFound();

            return Ok(themeModel);
        }

        /// <summary>
        /// Get theme by Description
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the themes</response>
        /// <response code="204">List themes empty</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult> GetThemeByDescriptionAsync([FromRoute] string descriptionTheme)
        {
            List<ThemeModel> themes = await _repository.GetThemesByDescriptionAsync(descriptionTheme);

            if (themes.Count < 1) return NoContent();

            return Ok(themes);
        }

        /// <summary>
        ///Create a new theme
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="201">Return theme created</response>
        /// <response code="400">Request error</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> NewThemeAsync([FromBody] NewThemeDTO themeDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewThemeAsync(themeDTO);
            return Created($"api/Themes/{themeDTO.Description}", themeDTO.Description);
        }

        /// <summary>
        /// Update the theme
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the updated theme</response>
        /// <response code="400">Request error</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ThemeModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> UpdateThemeAsync([FromBody] UpdateThemeDTO themeDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdateThemeAsync(themeDTO);
            return Ok(themeDTO);
        }

        /// <summary>
        /// Delete the theme by Id
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="204">Theme deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("deletar/{idTheme}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<ActionResult> DeleteThemeAsync([FromRoute] int idTheme)
        {
            await _repository.DeleteThemeAsync(idTheme);
            return NoContent();
        }
        #endregion

    }
}
