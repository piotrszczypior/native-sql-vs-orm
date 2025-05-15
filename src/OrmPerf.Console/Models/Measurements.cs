namespace OrmPerf.Console.Models;

public class Measurements
{
    public string IterationMode { get; set; }
    public string IterationStage { get; set; }
    public int LaunchIndex { get; set; }
    public int IterationIndex { get; set; }
    public int Operations { get; set; }
    public int Nanoseconds { get; set; }
}