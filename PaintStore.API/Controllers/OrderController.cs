using Microsoft.AspNetCore.Mvc;
using PaintStore.API.Dto;
using PaintStore.API.Services;
using PaintStore.Model.Models;

namespace PaintStore.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(OrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult GetAllOrders()
        {
            var Allorders = _orderService.GetAllOrders();
            List<OrderResponseDto> resultOrders = new List<OrderResponseDto>();
            foreach (var o in Allorders)
            {
                var resultOrder = new OrderResponseDto();
                var resultPaintsorders = new List<OrderItemResponseDto>();
                foreach (var po in o.PaintsOrders)
                {
                    var resultPaintsorder = new OrderItemResponseDto();
                    resultPaintsorder.Quantity = po.Quantity;
                    resultPaintsorder.PaintType = po.PaintProduct.Type;
                    resultPaintsorder.PaintName = po.PaintProduct.Name;
                    resultPaintsorder.PaintProductId = po.PaintProductId;
                    resultPaintsorders.Add(resultPaintsorder);
                }
                resultOrder.TotalPrice = o.totalPrice;
                resultOrder.UserId = o.UserId;
                resultOrder.Id = o.Id;
                resultOrder.CreatedDate = o.CreatedDate;
                resultOrder.PaintsOrders = resultPaintsorders;
                resultOrders.Add(resultOrder);

            }
            return Ok(resultOrders);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            _logger.LogInformation("Received request to delete order with id {Id}", id);
            _orderService.DeleteOrder(id);
            _logger.LogInformation("Deleted order with id {Id}", id);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult GetOrderById(int id)
        {
            var getOrder = _orderService.GetOrderById(id);
            var resultOrder = new OrderResponseDto();
            var resultPaintsOrders = new List<OrderItemResponseDto>();
            resultOrder.UserId = getOrder.UserId;
            resultOrder.TotalPrice = getOrder.totalPrice;
            resultOrder.CreatedDate = getOrder.CreatedDate;
            resultOrder.Id = getOrder.Id;
            foreach (var o in getOrder.PaintsOrders)
            {
                var resultPaintsOrder = new OrderItemResponseDto();
                resultPaintsOrder.Quantity = o.Quantity;
                resultPaintsOrder.PaintType = o.PaintProduct.Type;
                resultPaintsOrder.PaintName = o.PaintProduct.Name;
                resultPaintsOrder.PaintProductId = o.PaintProductId;
                resultPaintsOrders.Add(resultPaintsOrder);

            }
            resultOrder.PaintsOrders = resultPaintsOrders;
            return Ok(resultOrder);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateRequestDto orderCreateRequestDto) //ActionResult代表的是http的返回值
        { //当这个方法是异步是，返回值只能说task/void
            _logger.LogInformation("Received request to create order for user {UserId}", orderCreateRequestDto.UserId);

            var CreateOrder = new OrderResponseDto();
            var resultCreatePaintOrder = new List<PaintsOrder>();
            foreach (var oitem in orderCreateRequestDto.PaintsOrders)
            {
                var paintsOrder = new PaintsOrder();
                paintsOrder.Quantity = oitem.Quantity;
                paintsOrder.PaintProductId = oitem.PaintProductId;
                resultCreatePaintOrder.Add(paintsOrder);
            }
            var returnOrder = await _orderService.CreateOrder(resultCreatePaintOrder, orderCreateRequestDto.UserId);
            // 只要是是long processing的操作，都必须是await的，且await后面一定是异步操作/或者说它的返回值一定是异步操作
            // await 后面必须是一个 "awaitable" 表达式——最常见的就是一个异步方法的调用，它的返回类型是 Task 或 Task<T>
            CreateOrder.CreatedDate = returnOrder.CreatedDate;
            CreateOrder.UserId = returnOrder.UserId;
            CreateOrder.Id = returnOrder.Id;
            CreateOrder.TotalPrice = returnOrder.totalPrice;

            var resultPaintsOrder = new List<OrderItemResponseDto>();
            foreach (var rpo in returnOrder.PaintsOrders)
            {
                var resultPaintOrder = new OrderItemResponseDto();
                resultPaintOrder.Quantity = rpo.Quantity;
                resultPaintOrder.PaintProductId = rpo.PaintProductId;
                resultPaintOrder.PaintName = rpo.PaintProduct.Name;
                resultPaintOrder.PaintType = rpo.PaintProduct.Type;
                resultPaintsOrder.Add(resultPaintOrder);
            }
            CreateOrder.PaintsOrders = resultPaintsOrder;
            _logger.LogInformation("Created order with id {Id}", CreateOrder.Id);
            // Service/Repository 都改成 async Task<T> + await 之后，这里能这样一路 await 传导下去
            // 方法本身要加 async 修饰符（配合方法体里出现的 await，两者要配套，不然编译报错）
            // Created() 本身返回的是 IActionResult，但因为整个方法是 async Task<IActionResult>，
            // 编译器会自动把这个返回值包装成 Task<IActionResult>，不需要手动 Task.FromResult(...)
            return Created($"/api/orders/{CreateOrder.Id}", CreateOrder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder(int id, [FromBody] OrderUpdateRequestDto orderUpdateRequestDto)
        {
            _logger.LogInformation("Received request to update order with id {Id}", id);

            var resultUpdatePaintOrder = new List<PaintsOrder>();
            foreach (var po in orderUpdateRequestDto.PaintsOrders)
            {
                var UpdatepaintsOrder = new PaintsOrder();
                UpdatepaintsOrder.PaintProductId = po.PaintProductId;
                UpdatepaintsOrder.Quantity = po.Quantity;
                resultUpdatePaintOrder.Add(UpdatepaintsOrder);
            }
            await _orderService.UpdateOrder(id, orderUpdateRequestDto.UserId, resultUpdatePaintOrder);
            _logger.LogInformation("Updated order with id {Id}", id);
            return Ok();
        }

        [HttpGet("user/{userId}")]
        public ActionResult GetOrderByUserId(int userid)
        {
            var getOrders = _orderService.GetOrdersByUserId(userid);
            var resultOrders = new List<OrderResponseDto>();
            foreach (var o in getOrders)
            {
                var resultOrder = new OrderResponseDto();
                resultOrder.Id = o.Id;
                resultOrder.UserId = o.UserId;
                resultOrder.CreatedDate = o.CreatedDate;
                resultOrder.TotalPrice = o.totalPrice;

                var resultPaintsOrders = new List<OrderItemResponseDto>();
                foreach (var po in o.PaintsOrders)
                {
                    var resultPaintsOrder = new OrderItemResponseDto();
                    resultPaintsOrder.Quantity = po.Quantity;
                    resultPaintsOrder.PaintProductId = po.PaintProductId;
                    resultPaintsOrder.PaintName = po.PaintProduct.Name;
                    resultPaintsOrder.PaintType = po.PaintProduct.Type;
                    resultPaintsOrders.Add(resultPaintsOrder);
                }
                resultOrder.PaintsOrders = resultPaintsOrders;
                resultOrders.Add(resultOrder);
            }
            return Ok(resultOrders);
        }


    }
}
