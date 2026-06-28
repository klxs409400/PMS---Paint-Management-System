using System;
using PaintManagementSystem.Enums;

namespace PaintManagementSystem.Models;

public class PaintSpecification
{
    public string Color { get; set; }

    public int SizeInLiters { get; set; }

    public PaintType PaintType{get; set; }

    public PaintSpecification(string colorSet, int sizeInLitersSet, PaintType paintTypeSet){
        
        Color = colorSet;
        SizeInLiters = sizeInLitersSet;
        PaintType = paintTypeSet;


    }
    
    public String DisplaySpecification(){
        return $"The Color of Paint is {Color}, The Size of Paint is {SizeInLiters} Liters, The type of Paint is {PaintType}";
    }
}
