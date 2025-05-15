namespace OrmPerf.Console.Models;

public class Report
{
    public string Title { get; set; }
    public HostEnvironmentInfo HostEnvironmentInfo { get; set; }
    public Benchmarks[] Benchmarks { get; set; }
}