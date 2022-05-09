using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        [HttpGet("id/{idTheme}")]
        public IActionResult GetThemeById([FromRoute] int idTheme)
        {
            ThemeModel themeModel = _repository.GetThemeById(idTheme);

            if (themeModel == null) return NotFound();

            return Ok(themeModel);
        }

        [HttpGet()]
        public IActionResult GetThemeByDescription([FromRoute] string descriptionTheme)
        {
            List<ThemeModel> themes = _repository.GetThemesByDescription(descriptionTheme);

            if (themes.Count < 1) return NoContent();

            return Ok(themes);
        }

        [HttpPost]
        public IActionResult NewTheme([FromBody] NewThemeDTO themeDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.NewTheme(themeDTO);
            return Created($"api/Themes/{themeDTO.Description}", themeDTO.Description);
        }

        [HttpPut]
        public IActionResult UpdateTheme([FromBody] UpdateThemeDTO themeDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.UpdateTheme(themeDTO);
            return Ok(themeDTO);
        }

        [HttpDelete("delete/{idTheme}")]
        public IActionResult DeleteTheme([FromRoute] int idTheme)
        {
            _repository.DeleteTheme(idTheme);
            return NoContent();
        }
        #endregion

    }
}
