using BlogPessoal.src.dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.repositories;
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
        [HttpGet("id/{idTheme}")]
        public async Task<ActionResult> GetThemeByIdAsync([FromRoute] int idTheme)
        {
            ThemeModel themeModel = await _repository.GetThemeByIdAsync(idTheme);

            if (themeModel == null) return NotFound();

            return Ok(themeModel);
        }

        [HttpGet()]
        public async Task<ActionResult> GetThemeByDescriptionAsync([FromRoute] string descriptionTheme)
        {
            List<ThemeModel> themes = await _repository.GetThemesByDescriptionAsync(descriptionTheme);

            if (themes.Count < 1) return NoContent();

            return Ok(themes);
        }

        [HttpPost]
        public async Task<ActionResult> NewThemeAsync([FromBody] NewThemeDTO themeDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewThemeAsync(themeDTO);
            return Created($"api/Themes/{themeDTO.Description}", themeDTO.Description);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateThemeAsync([FromBody] UpdateThemeDTO themeDTO)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdateThemeAsync(themeDTO);
            return Ok(themeDTO);
        }

        [HttpDelete("delete/{idTheme}")]
        public async Task<ActionResult> DeleteThemeAsync([FromRoute] int idTheme)
        {
            await _repository.DeleteThemeAsync(idTheme);
            return NoContent();
        }
        #endregion

    }
}
