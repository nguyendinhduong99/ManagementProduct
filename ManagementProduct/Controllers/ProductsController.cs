using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PM.Data.Models;
using PM.Data.Models.Dtos;
using PM.Repository.Repository.IRepository;
using System;
using System.Collections.Generic;

namespace ParkyAPI.Controllers
{

    [Route("api/v{version:apiVersion}/products/")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _pro;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository pro, IMapper mapper, ILogger<ProductsController> logger)
        {
            _pro = pro;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Get all list products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ProductDto>))]
        [ProducesResponseType(400)]
        //[Authorize(Roles = "DEV")]
        public IActionResult GetProducts(string sortBy, string searchString, int? pageNumber)
        {
            _logger.LogWarning("THIS IS A CUSTOM MESSAGE");
            try
            {
                throw new NotImplementedException();
            }
            catch (NotImplementedException ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            var objList = _pro.GetProducts( sortBy,  searchString, pageNumber);
            //return Ok(objList);

            var objListDto = new List<ProductDto>();

            foreach (var item in objList)
            {
                objListDto.Add(_mapper.Map<ProductDto>(item));
            }

            return Ok(objListDto);
        }
        /// <summary>
        /// Get national product by id
        /// </summary>
        /// <param name="id">The id of product</param>
        /// <returns></returns>
        [HttpGet("get-product-by-id/{id}", Name = "GetProductById")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        public IActionResult GetProductById(int id)
        {
            var obj = _pro.GetProductById(id);
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                var objDto = _mapper.Map<ProductDto>(obj);
                return Ok(objDto);
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_pro.ProductExist(productDto.Name))
            {
                ModelState.AddModelError("", "Product park exist");
                return StatusCode(404, ModelState);
            }

            var proObj = _mapper.Map<Product>(productDto);
            if (!_pro.CreateProduct(proObj))
            {
                ModelState.AddModelError("", $"Something went  wrorng when saving the record {proObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetProductById", new { version = HttpContext.GetRequestedApiVersion().ToString(), id = proObj.Id }, proObj);
        }

        [HttpPatch("{id}", Name = "UpdateProduct")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDto nationalDto)
        {
            if (nationalDto == null || nationalDto.Id != id)
            {
                return BadRequest(ModelState);
            }


            var proObj = _mapper.Map<Product>(nationalDto);
            if (!_pro.UpdateProduct(proObj))
            {
                ModelState.AddModelError("", $"Something went  wrorng when update the record {proObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProduct(int id)
        {
            if (!_pro.ProductExist(id))
            {
                return NotFound();
            }


            var proObj = _pro.GetProductById(id);
            if (!_pro.DeleteProduct(proObj))
            {
                ModelState.AddModelError("", $"Something went  wrorng when delete the record {proObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
