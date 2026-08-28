using System;
using Microsoft.EntityFrameworkCore;
using PaintStore.API.Database;
using PaintStore.Model.Models;

namespace PaintStore.API.Repositories;

public class OrderRepository
{
    private readonly PaintStoreDbContext _Dbcontext;

    public OrderRepository(PaintStoreDbContext dbcontext)
    {
        _Dbcontext = dbcontext;
    }

    public List<Order> GetAllOrders()
    {
        return _Dbcontext.Orders.Include(o => o.PaintsOrders).ThenInclude(po => po.PaintProduct).ToList();
    }

    public Order GetOrderById(int id)
    {
        var order = _Dbcontext.Orders.Include(o => o.PaintsOrders).ThenInclude(po => po.PaintProduct).FirstOrDefault(p => p.Id == id);
        if (order != null)
        {
            return order;
        }
        throw new Exception("The Order id is invalid!");
    }

    public async Task<Order> CreateOrder(Order order)
    {
        var newOrder = new Order();
        newOrder.CreatedDate = DateTime.Now;
        newOrder.PaintsOrders = order.PaintsOrders;
        newOrder.UserId = order.UserId;
        newOrder.totalPrice = order.GetTotalPrice();

        await _Dbcontext.AddAsync(newOrder); //如果不await，就会直接跑下一句话
        await _Dbcontext.SaveChangesAsync();
        return newOrder;
    }

    public void UpdateOrder(Order order)
    {
        var currentOrder = _Dbcontext.Orders.Include(o => o.PaintsOrders).FirstOrDefault(p => p.Id == order.Id);
        if (currentOrder != null)
        {
            currentOrder.UserId = order.UserId;
            _Dbcontext.RemoveRange(currentOrder.PaintsOrders);
            currentOrder.PaintsOrders = order.PaintsOrders;
            currentOrder.totalPrice = order.GetTotalPrice();
            _Dbcontext.SaveChanges();
        }
        else { throw new Exception("The Order id is invalid"); }

    }

    public void DeleteOrder(int id)
    {
        var deleteOrder = _Dbcontext.Orders.FirstOrDefault(p => p.Id == id);
        if (deleteOrder != null)
        {
            _Dbcontext.Remove(deleteOrder);
            _Dbcontext.SaveChanges();
        }
        else { throw new Exception("The Order id is invalid"); }
    }

    public List<Order> GetOrdersByUserId(int userid)
    {
        var orders = _Dbcontext.Orders.Include(o => o.PaintsOrders).ThenInclude(po => po.PaintProduct).Where(p => p.UserId == userid).ToList();
        return orders;
    }
}
