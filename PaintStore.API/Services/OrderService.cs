using System;
using PaintStore.API.Repositories;
using PaintStore.Model.Models;

namespace PaintStore.API.Services;

public class OrderService
{
    private readonly OrderRepository _orderRepo;
    private readonly PaintProductRepository _paintRepo;
    private readonly UserRepository _userRepo;
    private readonly ILogger<OrderService> _logger;

    public OrderService(OrderRepository orderRepository, PaintProductRepository paintProductRepository, UserRepository userRepository, ILogger<OrderService> logger)
    {
        _orderRepo = orderRepository;
        _paintRepo = paintProductRepository;
        _userRepo = userRepository;
        _logger = logger;
    }


    public List<Order> GetAllOrders()
    {
        return _orderRepo.GetAllOrders();
    }

    public Order GetOrderById(int id)
    {
        return _orderRepo.GetOrderById(id);
    }

    public async Task<Order> CreateOrder(List<PaintsOrder> paintsOrders, int userId)
    {
        var order = new Order();
        var newPaintOrder = new List<PaintsOrder>();
        var user = await _userRepo.GetUserById(userId);
        order.UserId = user.Id;

        if (paintsOrders.Count > 0)
        {
            if (paintsOrders.Any(p => p.Quantity <= 0))
            {
                _logger.LogWarning("CreateOrder rejected: a product quantity is not greater than 0");
                throw new Exception("The quantity of product must larger than 0");
            }
            else
            {
                foreach (var item in paintsOrders)
                {
                    var newPaint = _paintRepo.GetPaintById(item.PaintProductId);
                    if (newPaint != null)
                    {
                        item.PaintProduct = newPaint;
                        newPaintOrder.Add(item);
                    }
                }
            }
        }
        else
        {
            _logger.LogWarning("CreateOrder rejected: no paint products in the order");
            throw new Exception("The Order do not include any paint product");
        }
        order.PaintsOrders = newPaintOrder;
        var createdOrder = await _orderRepo.CreateOrder(order);
        _logger.LogInformation("Order {Id} created successfully for user {UserId}", createdOrder.Id, createdOrder.UserId);
        return createdOrder;
    }

    public async Task UpdateOrder(int orderid, int userId, List<PaintsOrder> paintsOrders)
    {
        var currentOrder = _orderRepo.GetOrderById(orderid);
        var newUser = await _userRepo.GetUserById(userId);
        var newPaintOrder = new List<PaintsOrder>();

        currentOrder.UserId = userId;
        if (paintsOrders.Count > 0)
        {
            if (paintsOrders.Any(p => p.Quantity <= 0))
            {
                throw new Exception("The quantity of product must larger than 0");
            }
            else
            {
                foreach (var item in paintsOrders)
                {
                    var newPaint = _paintRepo.GetPaintById(item.PaintProductId);
                    if (newPaint != null)
                    {
                        item.PaintProduct = newPaint;
                        newPaintOrder.Add(item);
                    }
                }
            }
        }
        else
        {
            throw new Exception("The Order do not include any paint product");
        }
        currentOrder.PaintsOrders = newPaintOrder;
        _orderRepo.UpdateOrder(currentOrder);
    }

    public void DeleteOrder(int id)
    {
        _orderRepo.DeleteOrder(id);
    }

    public List<Order> GetOrdersByUserId(int userid)
    {
        return _orderRepo.GetOrdersByUserId(userid);
    }

}
