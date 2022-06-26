using CleanArchMvc.Application.DTO;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controller
{
    [Route("api/[controller]")] // Rota da api
    [ApiController] // Permite habilitar outros serviços para api
    public class CategoriesController : ControllerBase // Controller base omite o acesso a views
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetCategories();
            if (categories == null)
                return NotFound("Categories not found");

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
                return BadRequest("Invalid Data");

            await _categoryService.Add(categoryDTO);

            return new CreatedAtRouteResult("GetCategory",
                                             new { id = categoryDTO.Id },
                                             categoryDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
                return BadRequest();

            if (categoryDTO == null)
                return BadRequest();

            await _categoryService.Update(categoryDTO);

            return Ok(categoryDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = _categoryService.GetById(id);

            if (category == null)
                return NotFound("Category not found");

            await _categoryService.Remove(id);

            return Ok(category);
        }
    }
}
