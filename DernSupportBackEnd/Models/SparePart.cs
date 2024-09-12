public class SparePart
{
    public int SparePartId { get; set; }
    public string Name { get; set; }
    public int StockLevel { get; set; }
    public decimal Cost { get; set; }

    // Many-to-Many relationship with SupportRequests
    public ICollection<SupportRequestSparePart> SupportRequestSpareParts { get; set; }
}