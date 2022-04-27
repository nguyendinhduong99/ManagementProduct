using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM.Data.Models;
using PM.Data.Models.Dtos;
using PM.Repository.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/producttypes/")]
    [ApiController]

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class ProductTypesController : ControllerBase
    {
        private readonly IProductTypeRepository _pt;
        private readonly IMapper _mapper;

        public ProductTypesController(IProductTypeRepository pt, IMapper mapper)
        {
            _pt = pt;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all list producttype
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ProductTypeDto>))]
        [ProducesResponseType(400)]

        public IActionResult GetProductTypes()
        {
            var objList = _pt.GetProductTypes();
            //return Ok(objList);

            var objListDto = new List<ProductTypeDto>();

            foreach (var item in objList)
            {
                objListDto.Add(_mapper.Map<ProductTypeDto>(item));
            }

            return Ok(objListDto);
        }




        /// <summary>
        /// Get product type by id
        /// </summary>
        /// <param name="id">The id of product type</param>
        /// <returns></returns>
        [HttpGet("get-product-type-by-id/{id}", Name = "GetProductTypeById")]
        [ProducesResponseType(200, Type = typeof(ProductTypeDto))]
        [ProducesResponseType(400)]
        [ProducesDefaultResponseType]
        public IActionResult GetProductTypeById(int id)
        {
            var obj = _pt.GetProductTypeById(id);
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                var objDto = _mapper.Map<ProductTypeDto>(obj);
                return Ok(objDto);
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CreateProductTypeDto))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateProductType([FromBody] CreateProductTypeDto trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_pt.ProductTypeExist(trailDto.Name))
            {
                ModelState.AddModelError("", "National park exist");
                return StatusCode(404, ModelState);
            }

            var ptObj = _mapper.Map<ProductType>(trailDto);
            if (!_pt.CreateProductType(ptObj))
            {
                ModelState.AddModelError("", $"Something went  wrorng when saving the record {ptObj.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetProductTypeById", new { version = HttpContext.GetRequestedApiVersion().ToString() ,id = ptObj.Id }, ptObj);
        }

        [HttpPatch("{id}", Name = "UpdateProductType")]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int id, [FromBody] ProductTypeDto ptDto)
        {
            if (ptDto == null || ptDto.Id != id)
            {
                return BadRequest(ModelState);
            }


            var ptObj = _mapper.Map<ProductType>(ptDto);
            if (!_pt.UpdateProductType(ptObj))
            {
                ModelState.AddModelError("", $"Something went  wrorng when update the record {ptObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteProductType")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteProductType(int id)
        {
            if (!_pt.ProductTypeExist(id))
            {
                return NotFound();
            }


            var ptObj = _pt.GetProductTypeById(id);
            if (!_pt.DeleteProductType(ptObj))
            {
                ModelState.AddModelError("", $"Something went  wrorng when delete the record {ptObj.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
