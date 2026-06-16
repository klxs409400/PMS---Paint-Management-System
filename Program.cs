using System.Reflection.Metadata;
using PaintManagementSystem.Enums;
using PaintManagementSystem.Models;
using PaintManagementSystem.Practice;




PaintSpecification specification1 = new PaintSpecification("blue", 5, PaintType.BaseCoat);
PaintSpecification specification2 = new PaintSpecification("red", 6, PaintType.Matte);
PaintSpecification specification3 = new PaintSpecification("green", 4, PaintType.Glossy);

Brand brand1 = new Brand("BrandA");
Brand brand2 = new Brand("BrandB");
Brand brand3 = new Brand("BrandC");


PaintProduct product1 = new PaintProduct("PaintA", PaintType.BaseCoat, specification1, 67, brand1, 4);
PaintProduct product2 = new PaintProduct("PaintB", PaintType.Matte, specification2, 54, brand2, 3);
PaintProduct product3 = new PaintProduct("PaintC", PaintType.Glossy, specification3, 45, brand3, 5);

List<PaintProduct> products = new List<PaintProduct> { product1, product2, product3};

// System.Console.WriteLine(product1.DisplayInfo());
// System.Console.WriteLine(product2.DisplayInfo());
// System.Console.WriteLine(product3.DisplayInfo());

Order order1 = new Order(products, [2,3,4],1);

// System.Console.WriteLine(order1.DisplayOrder());
// System.Console.WriteLine(order1.GetTotalPrice());

PaintStore paintStore = new PaintStore(products);

// System.Console.WriteLine(paintStore.PaintInfor());

List<PaintProduct> productstorage = new List<PaintProduct>{product1,product2,product3};

Storage<PaintProduct> storage1 = new Storage<PaintProduct>(productstorage);

PaintProduct? findProduct = productstorage.Find(p => p.Price > 50);
System.Console.WriteLine(findProduct?.DisplayInfo());

// List<PaintProduct> allproducts = productstorage.FindAll(p => p.Price >50);
// foreach(PaintProduct product in allproducts){
//     System.Console.WriteLine(product.DisplayInfo());
// }

// productstorage.RemoveAll(p => p.Price < 50);
// int Countnumber = productstorage.Count;
// System.Console.WriteLine(Countnumber);

List<PaintProduct> screenProducts = productstorage.Where(p => p.Price > 50).ToList();
foreach(PaintProduct p in screenProducts){
    System.Console.WriteLine(p.DisplayInfo());
}

List<PaintProduct> sorted =  productstorage.OrderBy(p => p.Price).ToList();
foreach(PaintProduct p in sorted){
    System.Console.WriteLine($"The Name of product is {p.Name} and the price is$ {p.Price} ");
}

String name = productstorage.FirstOrDefault( p=> p.Type == PaintType.Matte)?.Name;
System.Console.WriteLine(name);

bool expensive = productstorage.Any(p => p.Price >100);
System.Console.WriteLine(expensive);

order1.GetMostExpensivePaintProduct();
order1.SpecificPaint(100,50);
var totalP = order1.FindTotalPrice(PaintType.BaseCoat);
System.Console.WriteLine(totalP);
