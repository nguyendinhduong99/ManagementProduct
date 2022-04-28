using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PM.Data.Models.Dtos;
using PM.Repository.Repository.IRepository;
using System.Linq;

namespace ParkyAPI.Controllers
{

    [Route("api/v{version:apiVersion}/products/")]
    [ApiController]
    [ApiVersion("2.0")]

    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public class ProductV2sController : ControllerBase
    {
        private readonly IProductRepository _pro;
        private readonly IMapper _mapper;

        public ProductV2sController(IProductRepository pro, IMapper mapper)
        {
            _pro = pro;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all list products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        public IActionResult GetFirstProduct(string sortBy, string searchString, int? pageNumber)
        {
            var obj = _pro.GetProducts( sortBy,  searchString, pageNumber).FirstOrDefault();
            return Ok(_mapper.Map<ProductDto>(obj));
        }
        
    }
}
