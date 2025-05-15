namespace OrmPerf.Console.Models;

public class Memory
{
    public int Gen0Collections { get; set; }
    public int Gen1Collections { get; set; }
    public int Gen2Collections { get; set; }
    public int TotalOperations { get; set; }
    public int BytesAllocatedPerOperation { get; set; }
}