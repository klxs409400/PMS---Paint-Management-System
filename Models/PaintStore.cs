using System;
using System.Net.Http.Headers;

namespace PaintManagementSystem.Models;

public class PaintStore
{
    public PaintProduct[] Products {get; set; }

    public PaintStore(PaintProduct[] products){
        Products = products;
    }

    public string PaintInfor(){
        string information ="";
        for(int i = 0; i < Products.Length; i++){
            information += Products[i].DisplayInfo();
        }
        return information;
    }

}
