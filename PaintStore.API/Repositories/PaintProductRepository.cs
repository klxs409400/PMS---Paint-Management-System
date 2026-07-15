using System;
using Microsoft.VisualBasic;
using PaintStore.API.Database;
using PaintStore.Model.Models;

namespace PaintStore.API.Repositories;

public class PaintProductRepository
{
    private readonly PaintStoreDbContext _Dbcontext;
    public PaintProductRepository(PaintStoreDbContext dbContext)
    {
        _Dbcontext = dbContext;
    }

    public List<PaintProduct> GetAllPaintProducts()
    {
        return _Dbcontext.PaintProducts.ToList();
    }

    public PaintProduct GetPaintById(int id)
    {
        var findPaint = _Dbcontext.PaintProducts.FirstOrDefault(p => p.Id == id);
        if (findPaint != null)
        {
            return findPaint;
        }
        throw new Exception("The Paint id is invalid!");

    }

    public PaintProduct CreatePaintProduct(PaintProduct paintProduct)
    {
        var newProduct = new PaintProduct();
        newProduct.Name = paintProduct.Name;
        newProduct.Type = paintProduct.Type;
        // newProduct.Brand = paintProduct.Brand;
        newProduct.CreatedDate = DateTime.Now;
        // newProduct.Specification = paintProduct.Specification;
        newProduct.Price = paintProduct.Price;
        newProduct.Stock = paintProduct.Stock;
        newProduct.TaxRate = paintProduct.TaxRate;
        _Dbcontext.Add(newProduct);
        _Dbcontext.SaveChanges();
        return newProduct;
    }

    public void UpdatePaintProduct(PaintProduct paintProduct)
    {
        var currentProduct = _Dbcontext.PaintProducts.FirstOrDefault(p => p.Id == paintProduct.Id);
        if (currentProduct != null)
        {
            currentProduct.Name = paintProduct.Name;
            currentProduct.Price = paintProduct.Price;
            currentProduct.Stock = paintProduct.Stock;
            currentProduct.Type = paintProduct.Type;
            _Dbcontext.SaveChanges();
        }
        else { throw new Exception("The product id is invalid"); }
    }

    public void DeletePaintProduct(int id)
    {
        var deleteProduct = _Dbcontext.PaintProducts.FirstOrDefault(p => p.Id == id);
        if (deleteProduct != null)
        {
            _Dbcontext.Remove(deleteProduct);
            _Dbcontext.SaveChanges();
        }
        else { throw new Exception("The product id is invalid"); }
    }

    public List<PaintProduct> SearchProductByPrice(decimal minPrice, decimal maxPrice)
    {
        var findProduct = _Dbcontext.PaintProducts.Where(p => p.Price > minPrice && p.Price < maxPrice).ToList();
        return findProduct;
    }
}
