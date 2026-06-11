using System.Reflection.Metadata;
using PaintManagementSystem.Enums;
using PaintManagementSystem.Models;


PaintSpecification specification1 = new PaintSpecification("blue", 5, PaintType.BaseCoat);

PaintSpecification specification2 = new PaintSpecification("red", 6, PaintType.Matte);

PaintSpecification specification3 = new PaintSpecification("green", 4, PaintType.Glossy);

PaintProduct product1 = new PaintProduct("PaintA", PaintType.BaseCoat, specification1, 67);

PaintProduct product2 = new PaintProduct("PaintB", PaintType.Matte, specification2, 54);

PaintProduct product3 = new PaintProduct("PaintC", PaintType.Glossy, specification3, 45);

System.Console.WriteLine(product1.DisplayInfo());
System.Console.WriteLine(product2.DisplayInfo());
System.Console.WriteLine(product3.DisplayInfo());

Order order1 = new Order(product1, 25);

System.Console.WriteLine(order1.DisplayOrder());
System.Console.WriteLine(order1.GetTotalPrice());