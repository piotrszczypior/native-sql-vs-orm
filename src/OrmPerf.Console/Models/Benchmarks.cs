namespace OrmPerf.Console.Models;

public class Benchmarks
{
    public string DisplayInfo { get; set; }
    public string Namespace { get; set; }
    public string Type { get; set; }
    public string Method { get; set; }
    public string MethodTitle { get; set; }
    public string Parameters { get; set; }
    public string FullName { get; set; }
    public string HardwareIntrinsics { get; set; }
    public Statistics Statistics { get; set; }
    public Memory Memory { get; set; }
    public Measurements[] Measurements { get; set; }
    public Metrics[] Metrics { get; set; }
}