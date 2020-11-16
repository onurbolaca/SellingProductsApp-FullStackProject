namespace SatisSimilasyon.Web.Models
{
  public class SimulationLineModel
  {
    public string CustomerReferenceCode { get; set; }
    public string ReferenceCode { get; set; }
    public string ProductType { get; set; }
    public string Price { get; set; }
    public string NewPrice { get; set; }
    public string SalesQuantity { get; set; }
    public string SalesPriceDifference { get; set; }
    public string TurnoverDifference { get; set; }
  }
}