using eShopSolution.Application.Catolog.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(string languageId)
        {
            var products = await _categoryService.GetAll(languageId);
            return Ok(products);
        }

        [HttpGet("{id}/{languageId}")]
        public async Task<ActionResult> GeById(int id, string languageId)
        {
            var products = await _categoryService.GetCategoryById(id, languageId);
            return Ok(products);
        }
    }
}