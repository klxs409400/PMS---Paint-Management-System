using System;
using PaintStore.API.Repositories;
using PaintStore.Model;
using PaintStore.Model.Models;

namespace PaintStore.API.Services;

public class PaintProductService
{
    private readonly PaintProductRepository _paintRepo;
    private readonly ILogger<PaintProductService> _logger;

    public PaintProductService(PaintProductRepository paintProductRepository, ILogger<PaintProductService> logger)
    {
        _paintRepo = paintProductRepository;
        _logger = logger;
    }


    public PaintProduct CreatePaintProduct(string name, PaintType type, decimal price, int stock)
    {
        var newPaint = new PaintProduct();
        if (!string.IsNullOrEmpty(name))
        {
            newPaint.Name = name;
        }
        else
        {
            _logger.LogWarning("CreatePaintProduct rejected: name is empty");
            throw new Exception("The Name is invalid");
        }

        if (price > 0)
        {
            newPaint.Price = price;
        }
        else
        {
            _logger.LogWarning("CreatePaintProduct rejected: price {Price} is not greater than 0", price);
            throw new Exception("The price must greater than 0");
        }

        if (stock >= 0)
        {
            newPaint.Stock = stock;
        }
        else
        {
            _logger.LogWarning("CreatePaintProduct rejected: stock {Stock} is negative", stock);
            throw new Exception("The stock must greater than 0");
        }
        newPaint.Type = type;
        var createdPaint = _paintRepo.CreatePaintProduct(newPaint);
        _logger.LogInformation("Paint product {Id} created successfully", createdPaint.Id);
        return createdPaint;
    }

    public List<PaintProduct> GetAllPaintProduct()
    {
        return _paintRepo.GetAllPaintProducts();
    }

    public PaintProduct GetPaintById(int id)
    {
        return _paintRepo.GetPaintById(id);
    }

    public void UpdatePaintProduct(int id, string name, PaintType type, decimal price, int stock)
    {
        var currentProduct = _paintRepo.GetPaintById(id);
        if (!string.IsNullOrEmpty(name))
        {
            currentProduct.Name = name;
        }
        else { throw new Exception("The Name is invalid"); }

        if (price > 0)
        {
            currentProduct.Price = price;
        }
        else { throw new Exception("The price must greater than 0"); }

        if (stock >= 0)
        {
            currentProduct.Stock = stock;
        }
        else { throw new Exception("The stock must greater than 0"); }
        currentProduct.Type = type;
        _paintRepo.UpdatePaintProduct(currentProduct);
    }
    public void DeletePaintProduct(int id)
    {
        _paintRepo.DeletePaintProduct(id);

    }

    public List<PaintProduct> SearchProductByPrice(decimal minPrice, decimal maxPrice)
    {
        if (maxPrice > minPrice && maxPrice > 0)
        {
            return _paintRepo.SearchProductByPrice(minPrice, maxPrice);
        }
        else { throw new Exception("The max price must larger than min price"); }
    }
}

