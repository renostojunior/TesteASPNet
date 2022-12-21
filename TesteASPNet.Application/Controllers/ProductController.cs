using TesteASPNet.Application.Model;
using TesteASPNet.Domain.Entity;
using TesteASPNet.Domain.Interfaces;
using TesteASPNet.Service.Validators;
using AutoMapper;
using Canducci.Pagination;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace TesteASPNet.Application.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IBaseService<Product> _productService;
        private readonly IMapper _mapper;
        private readonly ProductValidator _validator;



        public ProductController(IBaseService<Product> productService,
                                 IMapper mapper,
                                 ProductValidator validator)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpGet("{page}/{onlyactive}")]
        public async Task<ActionResult<PaginatedRest<ProductModel>>> FindByPage(int page, bool onlyactive = true)
        {
            if (page == 0)
                page = 1;

            Expression<Func<Product, bool>> where = null;

            PaginatedRest<Product> paginatedRest;

            if (onlyactive)
                where = (p => p.Status == Product.StatusSituation.Active);

            if (where == null)
                paginatedRest = await _productService.GetWithPagination(page);
            else
                paginatedRest = await _productService.GetWithPagination(page, where);

            if (paginatedRest != null)
                return Ok(paginatedRest);

            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<ProductModel>> FindById(int id)
        {
            var product = await _productService.GetById<Product>(id);

            if (product != null)
                return Ok(_mapper.Map<ProductModel>(product));

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> Create(ProductModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var product = _mapper.Map<Product>(model);

            if (product != null)
            {
                var validatorResult = _validator.Validate(product);

                if (!validatorResult.IsValid)
                    return BadRequest(validatorResult.Errors);

                var response = await _productService.Add<Product, ProductModel>(product);
                return Ok(response);
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<ProductModel>> Update(ProductModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var product = _mapper.Map<Product>(model);
            if (product != null)
            {
                var validatorResult = _validator.Validate(product);

                if (!validatorResult.IsValid)
                    return BadRequest(validatorResult.Errors);

                var response = await _productService.Update<Product, ProductModel>(product);
                return Ok(response);
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int productId)
        {
            var product = await _productService.GetById<Product>(productId);
            if (product != null)
            {
                product.Status = Product.StatusSituation.Inactive;

                var result = await _productService.Update<Product>(product);

                if (!result)
                    return BadRequest();

                return Ok(true);
            }

            return NotFound();
        }
    }
}
