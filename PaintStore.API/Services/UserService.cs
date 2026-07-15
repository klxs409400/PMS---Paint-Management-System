using System;
using PaintStore.API.Repositories;
using PaintStore.Model.Models;
using System.Net.Mail;

namespace PaintStore.API.Services;

public class UserService
{
    private readonly UserRepository _userRepo;
    private readonly ILogger<UserService> _logger;

    public UserService(UserRepository userRepository, ILogger<UserService> logger)
    {
        _userRepo = userRepository;
        _logger = logger;
    }

    public User CreateUser(string name, string email, string phone)
    {
        var user = new User();
        if (!string.IsNullOrEmpty(name))
        {
            user.Name = name;
        }
        else
        {
            _logger.LogWarning("CreateUser rejected: name is empty");
            throw new Exception("The Name is invalid");
        }

        if (string.IsNullOrEmpty(email))
        {
            _logger.LogWarning("CreateUser rejected: email is empty");
            throw new Exception("The Email is invalid");
        }

        bool isValidEmail;
        try
        {
            var mailAddress = new MailAddress(email);
            isValidEmail = true;
        }
        catch (FormatException)
        {
            isValidEmail = false;
        }

        if (!isValidEmail)
        {
            _logger.LogWarning("CreateUser rejected: email {Email} has invalid format", email);
            throw new Exception("The email format is invalid");
        }

        var allUsers = _userRepo.GetAllUsers();
        if (allUsers.Any(u => u.Email == email))
        {
            _logger.LogWarning("CreateUser rejected: email {Email} is already in use", email);
            throw new Exception("The email cannot be used!");
        }

        user.Email = email;
        user.Phone = phone;
        var createdUser = _userRepo.CreateUser(user);
        _logger.LogInformation("User {Id} created successfully", createdUser.Id);
        return createdUser;
    }

    public List<User> GetAllUsers()
    {
        return _userRepo.GetAllUsers();
    }

    public User GetUserById(int id)
    {
        return _userRepo.GetUserById(id);
    }

    public void UpdateUser(int id, string name, string email, string phone)
    {
        var updateuser = _userRepo.GetUserById(id);
        if (!string.IsNullOrEmpty(name))
        {
            updateuser.Name = name;
        }
        else { throw new Exception("The Name is invalid"); }

        if (string.IsNullOrEmpty(email))
        {
            throw new Exception("The Email is invalid");
        }

        bool isValidEmail;
        try
        {
            var mailAddress = new MailAddress(email);
            isValidEmail = true;
        }
        catch (FormatException)
        {
            isValidEmail = false;
        }

        if (!isValidEmail)
        {
            throw new Exception("The email format is invalid");
        }

        var allUsers = _userRepo.GetAllUsers();
        if (allUsers.Any(u => u.Email == email && u.Id != id))
        {
            throw new Exception("The email cannot be used!");
        }

        updateuser.Email = email;
        updateuser.Phone = phone;
        _userRepo.UpdateUser(updateuser);

    }

    public void DeleteUser(int id)
    {
        _userRepo.DeleteUser(id);
    }
}
