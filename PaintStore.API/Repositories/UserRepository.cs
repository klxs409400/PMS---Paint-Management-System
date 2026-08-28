using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using PaintStore.API.Database;
using PaintStore.Model.Models;

namespace PaintStore.API.Repositories;

public class UserRepository
{
    private readonly PaintStoreDbContext _Dbcontext;

    public UserRepository(PaintStoreDbContext dbContext)
    {
        _Dbcontext = dbContext;
    }

    public List<User> GetAllUsers()
    {
        return _Dbcontext.Users.ToList();
    }

    public async Task<User> GetUserById(int id)
    {
        var user = await _Dbcontext.Users.FirstOrDefaultAsync(p => p.Id == id);
        // await 能帮我提取到FirstOrDefaultAsync这个返回值真正的结果--Task<User>,且它有很大概率会开启一个新的thread来跑这一步，不会阻塞我们的程序
        if (user != null)
        {
            return user;
        }
        throw new Exception("The user id is invalid!");
    }

    public void UpdateUser(User user)
    {
        var currentUser = _Dbcontext.Users.FirstOrDefault(p => p.Id == user.Id);
        if (currentUser != null)
        {
            currentUser.Name = user.Name;
            currentUser.Email = user.Email;
            currentUser.Phone = user.Phone;
            // currentUser.HistoricalOrder = user.HistoricalOrder;
            // currentUser.HistoricalPayment = user.HistoricalPayment;
            _Dbcontext.SaveChanges();

        }
        else { throw new Exception("The user id is invalid!"); }
    }

    public void DeleteUser(int id)
    {
        var deleteUser = _Dbcontext.Users.FirstOrDefault(p => p.Id == id);
        if (deleteUser != null)
        {
            _Dbcontext.Remove(deleteUser);
            _Dbcontext.SaveChanges();

        }
        else { throw new Exception("The user id is invalid!"); }
    }

    public User CreateUser(User user)
    {
        var newUser = new User();
        newUser.Email = user.Email;
        newUser.Name = user.Name;
        newUser.Phone = user.Phone;
        newUser.CreatedDate = DateTime.Now;
        newUser.HistoricalOrder = new List<Order>();
        newUser.HistoricalPayment = new List<Payment>();
        _Dbcontext.Add(newUser);
        _Dbcontext.SaveChanges();
        return newUser;

    }
}
