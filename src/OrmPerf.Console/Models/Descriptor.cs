namespace OrmPerf.Console.Models;

public class Descriptor
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Legend { get; set; }
    public string NumberFormat { get; set; }
    public int UnitType { get; set; }
    public string Unit { get; set; }
    public bool TheGreaterTheBetter { get; set; }
    public int PriorityInCategory { get; set; }
}