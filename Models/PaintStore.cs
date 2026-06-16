using System;
using System.Net.Http.Headers;

namespace PaintManagementSystem.Models;

public class PaintStore
{
    public List<PaintProduct> Products {get; set; }

     public PaintStore(List<PaintProduct> products){
        Products = products;
    }

    public PaintStore(){
        Products = new List<PaintProduct>();
    }
    public string PaintInfor(){
        string information ="";
        for(int i = 0; i < Products.Count; i++){
            information += Products[i].DisplayInfo();
        }
        return information;
    }

}
