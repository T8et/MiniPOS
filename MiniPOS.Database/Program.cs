// See https://aka.ms/new-console-template for more information
using MiniPOS.DataHub.Models;

Console.Read();

AppDBContext db = new AppDBContext();
var response = db.BtProductCats.ToList();

Console.WriteLine("Hello, World!");
foreach(var item in response)
{
    Console.WriteLine(item.CatProductCode+item.CatProductDesc);
}
