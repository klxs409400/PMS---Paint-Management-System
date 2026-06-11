using System;

namespace PaintManagementSystem.Models;

public class Brand
{
    public String BrandName { get; set; }

    public Brand(String name){
        BrandName = name;
    }
}
