using System;
using System.ComponentModel.DataAnnotations;

namespace PaintManagementSystem.Models;

public class User
{
    public string UserName{ get;  set; }
    public List<Order> HistoricalOrder { get; set; }

    public List<Payment> HistoricalPayment { get; set; }

    public string GetMostExpensiveOrder(){
    Order ? MostExpensive  =  HistoricalOrder.OrderByDescending(p => p.TotalPrice).FirstOrDefault();
    return MostExpensive?.DisplayOrder();
    }

    public string GetLatestOrder(){
    Order ? LatestOrder = HistoricalOrder.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
    return LatestOrder?.DisplayOrder();
    }

    public string GetCheapestRecords(){
    Payment ? CheapestRecord = HistoricalPayment.OrderBy(p => p.paymentAmount).FirstOrDefault();
    return CheapestRecord?.PaymentInfor();
    }    

    public string GetLatestPayment(){
    Payment ? LatestPayment = HistoricalPayment.OrderByDescending(p => p.CreatedAt).FirstOrDefault();
    return LatestPayment?.PaymentInfor();
    }

    public List<Payment> GetTotalRecords(){
    List<Payment> TotalRecords = HistoricalPayment.FindAll(p => p.paymentAmount > 10);
    return TotalRecords;
    }

}
