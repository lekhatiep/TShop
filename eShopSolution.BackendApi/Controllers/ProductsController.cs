using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.Catolog.ProductImages;
using eShopSolution.Application.Catolog.Products;
using eShopSolution.ViewModels.Catalog.Category;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _manageProductService;

        public ProductsController(IProductService manageProductService)
        {
            _manageProductService = manageProductService;
        }

        //http://localhost:port/products?pageIndex=1&pageSize=10&CategoryId=

        [HttpGet("paging")]
        public async Task<ActionResult> GetAllPaging([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _manageProductService.GetAllPaging(request);
            return Ok(products);
        }

        //http:localhost/product/1/vi-Vn
        [HttpGet("{productId}/{languageId}")]
        public async Task<ActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductService.GetById(productId, languageId);
            if (product == null)
            {
                return BadRequest("Cannot find product");
            }
            return Ok(product);
        }

        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _manageProductService.Create(request);
            if (productId == 0)
            {
                return BadRequest();
            }
            var product = await _manageProductService.GetById(productId, request.LanguageId);

            return CreatedAtAction(nameof(GetById), new { id = productId }, product);
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var affectedResult = await _manageProductService.Update(request);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //http:localhost/product/1
        [HttpDelete("{productId}")]
        public async Task<ActionResult> Detele(int productId)
        {
            var affectedResult = await _manageProductService.Delete(productId);
            if (affectedResult == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        //http:localhost/product/price/1/200
        [HttpPatch("{id}/{newPrice}")]
        public async Task<ActionResult> UpdatePrice(int id, decimal newPrice)
        {
            var isSuccessful = await _manageProductService.UpdatePrice(id, newPrice);
            if (isSuccessful)
            {
                return Ok();
            }
            return BadRequest();
        }

        //Images
        [HttpPost("{productId}/images")]
        public async Task<ActionResult> CreateImage(int productId, [FromQuery] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _manageProductService.AddImage(productId, request);
            if (imageId == 0)
            {
                return BadRequest();
            }
            var image = await _manageProductService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<ActionResult> GetImageById(int imageId)
        {
            var image = await _manageProductService.GetImageById(imageId);
            if (image == null)
            {
                return BadRequest("Cannot find image");
            }
            return Ok(image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<ActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var image = await _manageProductService.UpdateImage(imageId, request);
            if (image == 0)
            {
                return BadRequest("Cannot find image");
            }
            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<ActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var image = await _manageProductService.RemoveImage(imageId);
            if (image == 0)
            {
                return BadRequest("Cannot find image");
            }
            return Ok();
        }

        [HttpPut("{id}/categories")]
        public async Task<IActionResult> AssignCategory(int Id, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _manageProductService.AssignCategory(Id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("featured/{languageId}/{take}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetFeauredProduct(string languageId, int take)
        {
            var products = await _manageProductService.GetFeaturedProducts(languageId, take);
            if (products.Count == 0)
            {
                return BadRequest("Can not find product");
            }
            return Ok(products);
        }

        [HttpGet("latest/{languageId}/{take}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetLatestProduct(string languageId, int take)
        {
            var products = await _manageProductService.GetLatestProducts(languageId, take);
            if (products.Count == 0)
            {
                return BadRequest("Can not find product");
            }
            return Ok(products);
        }
    }
}