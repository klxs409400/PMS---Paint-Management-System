using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaintStore.Model;

namespace PaintManagementSystem.PaintStore.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private List<Order> orders;

        public OrderController()
        {
            orders = new List<Order>();
        }
        
        [HttpGet("Order/GetAllOrders")]
        public ActionResult GetAllOrders([FromQuery] int? PageNumber, [FromQuery] int? PageSize )
        {   var products = orders;
            
            if(PageNumber.HasValue && PageSize.HasValue)
            {
                products = orders.OrderBy(p => p.Id).Skip((PageNumber.Value-1) * PageSize.Value).Take(PageSize.Value).ToList();
                return Ok(products);
            }
            
            return Ok(orders);
            
        }

        [HttpGet("Order/GetOrderByPriceRange")]
        public ActionResult GetOrderByPriceRange([FromQuery] decimal? maxPrice,[FromQuery] decimal? minPrice)
        {   
            if(maxPrice.HasValue && minPrice.HasValue)
            {
                var products = orders.Where(p => p.GetTotalPrice() > minPrice && p.GetTotalPrice() < maxPrice).ToList();
                return Ok(products);
            }
            return NotFound();
        }

        [HttpGet("Order/GetOrdersByPaintId")]
        public ActionResult GetOrdersByPaintId([FromQuery]int Paintid)
        {   
            var products = orders.Where(p => p.PaintProducts.Any(o => o.Id == Paintid)).ToList();
            if(products.Count > 0)
            {
                return Ok(products);
            }
            return NotFound();
        }

         [HttpGet("Order/GetOrdersByUserId")]
        public ActionResult GetOrdersByUserId([FromQuery]int Userid)
        {   
            if(orders.Any(p => p.UserId == Userid))
            {
                 var products = orders.Where(p => p.UserId == Userid).ToList();
                return Ok(products);
            }
            return NotFound();
        }

        [HttpGet("Order/GetLastMonthOrders")]
        public ActionResult GetLastMonthOrders()
        {
            DateTime time = DateTime.Now;
            DateTime lastmonth = time.AddMonths(-1);
            var products = orders.Where(p => p.CreatedDate >= lastmonth && p.CreatedDate <= time).ToList();
            if(products.Count > 0)
            {
                return Ok(products);
            }
            return NotFound();
        }


        [HttpGet("Order/GetOrdersByDate")]
        public ActionResult GetOrdersByDate([FromQuery] DateTime date)
        {
            if(orders.Any(p => p.CreatedDate.Date == date)){
                var products = orders.Where(p => p.CreatedDate.Date == date).ToList();
                return Ok(products);
            }
            return NotFound();
        }
    }
}
