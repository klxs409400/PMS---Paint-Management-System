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
        public ActionResult CreateOrder([FromBody] OrderCreateRequestDto orderCreateRequestDto)
        {
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
            var returnOrder = _orderService.CreateOrder(resultCreatePaintOrder, orderCreateRequestDto.UserId);
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
            return Created($"/api/orders/{CreateOrder.Id}", CreateOrder);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateOrder(int id, [FromBody] OrderUpdateRequestDto orderUpdateRequestDto)
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
            _orderService.UpdateOrder(id, orderUpdateRequestDto.UserId, resultUpdatePaintOrder);
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
