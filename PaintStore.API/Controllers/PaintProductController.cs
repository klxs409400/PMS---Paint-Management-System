using Microsoft.AspNetCore.Mvc;
using PaintStore.API.Dto;
using PaintStore.API.Services;
using PaintStore.Model.Models;

namespace PaintStore.API.Controllers
{
    [Route("api/paintproducts")]
    [ApiController]
    public class PaintProductController : ControllerBase
    {
        private readonly PaintProductService _paintProductService;
        private readonly ILogger<PaintProductController> _logger;

        public PaintProductController(PaintProductService paintProductService, ILogger<PaintProductController> logger)
        {
            _paintProductService = paintProductService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetAllPaintProduct()
        {
            var AllPaints = _paintProductService.GetAllPaintProduct();
            List<PaintProductResponseDto> resultPaints = new List<PaintProductResponseDto>();
            foreach (var p in AllPaints)
            {
                var resultPaint = new PaintProductResponseDto();
                resultPaint.Id = p.Id;
                resultPaint.Name = p.Name;
                resultPaint.Price = p.Price;
                resultPaint.Type = p.Type;
                resultPaint.Stock = p.Stock;
                resultPaints.Add(resultPaint);
            }
            return Ok(resultPaints);
        }

        [HttpGet("{id}")]
        public ActionResult GetPaintProductById(int id)
        {
            var getPaint = _paintProductService.GetPaintById(id);
            var resultPaint = new PaintProductResponseDto();
            resultPaint.Id = getPaint.Id;
            resultPaint.Name = getPaint.Name;
            resultPaint.Price = getPaint.Price;
            resultPaint.Type = getPaint.Type;
            resultPaint.Stock = getPaint.Stock;
            return Ok(resultPaint);
        }

        [HttpPost]
        public ActionResult CreatePaintProduct([FromBody] PaintProductCreateRequestDto paintProductCreateRequestDto)
        {
            _logger.LogInformation("Received request to create paint product {Name}", paintProductCreateRequestDto.Name);

            var resultPaint = new PaintProductResponseDto();
            var createPaint = _paintProductService.CreatePaintProduct(paintProductCreateRequestDto.Name, paintProductCreateRequestDto.Type, paintProductCreateRequestDto.Price, paintProductCreateRequestDto.Stock);
            resultPaint.Id = createPaint.Id;
            resultPaint.Name = createPaint.Name;
            resultPaint.Type = createPaint.Type;
            resultPaint.Stock = createPaint.Stock;
            resultPaint.Price = createPaint.Price;

            _logger.LogInformation("Created paint product with id {Id}", resultPaint.Id);
            return Created($"api/paintproducts/{resultPaint.Id}", resultPaint);
        }

        [HttpPut("{id}")]
        public ActionResult UpdatePaintProduct(int id, [FromBody] PaintProductUpdateDto paintProductUpdateDto)
        {
            _logger.LogInformation("Received request to update paint product with id {Id}", id);
            _paintProductService.UpdatePaintProduct(id, paintProductUpdateDto.Name, paintProductUpdateDto.Type, paintProductUpdateDto.Price, paintProductUpdateDto.Stock);
            _logger.LogInformation("Updated paint product with id {Id}", id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePaintProduct(int id)
        {
            _logger.LogInformation("Received request to delete paint product with id {Id}", id);
            _paintProductService.DeletePaintProduct(id);
            _logger.LogInformation("Deleted paint product with id {Id}", id);
            return Ok();
        }

        [HttpGet("search")]
        public ActionResult SearchProductByPrice([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            List<PaintProduct> paintProducts = _paintProductService.SearchProductByPrice(minPrice, maxPrice);
            List<PaintProductResponseDto> resultPaints = new List<PaintProductResponseDto>();
            foreach (var p in paintProducts)
            {
                var resultPaint = new PaintProductResponseDto();
                resultPaint.Id = p.Id;
                resultPaint.Name = p.Name;
                resultPaint.Price = p.Price;
                resultPaint.Type = p.Type;
                resultPaint.Stock = p.Stock;
                resultPaints.Add(resultPaint);
            }
            return Ok(resultPaints);
        }

    }
}
