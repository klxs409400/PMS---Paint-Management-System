using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintStore.Model;

namespace PaintManagementSystem.PaintStore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaintStoreController : ControllerBase
    {
        private List<PaintProduct> products;
        public PaintStoreController()
        {
            products = new List<PaintProduct>();
        }

        [HttpGet("PaintStore/GetAllPaintProducts")]
        public ActionResult GetAllPaintProducts([FromQuery] int? PageNumber, [FromQuery] int? PageSize)
        {
            if(PageNumber.HasValue && PageSize.HasValue)
            {
                var product = products.OrderBy(p => p.Id).Skip((PageNumber.Value-1*PageSize.Value)).Take(PageSize.Value).ToList();
                return Ok(product);
            }
            return Ok(products);
        }

        [HttpGet("PaitStore/GetProductsByPriceRange")]
        public ActionResult GetProductsByPriceRange([FromQuery]decimal maxPrice, [FromQuery] decimal minPrice)
        {
            var product = products.Where(p => p.Price > minPrice && p.Price< maxPrice).ToList();
            if(product.Count > 0)
            {
                return Ok(product);
            }
            return NotFound();
        }

        [HttpGet("PaintStore/GetPaintProductsByPaintId")]

        public ActionResult GetPaintProductsByPaintId([FromQuery] int id)
        {
           if(products.Any(p => p.Id == id))
            {
                return Ok(products.Where(p =>p.Id ==id));
            } 
            return NotFound();
        }

    }

}
