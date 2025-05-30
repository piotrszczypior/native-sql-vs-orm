namespace OrmPerf.Console.Models;

public class Statistics
{
    public double[] OriginalValues { get; set; }
    public int N { get; set; }
    public double Min { get; set; }
    public double LowerFence { get; set; }
    public double Q1 { get; set; }
    public double Median { get; set; }
    public double Mean { get; set; }
    public double Q3 { get; set; }
    public double UpperFence { get; set; }
    public double Max { get; set; }
    public double InterquartileRange { get; set; }
    public double[] LowerOutliers { get; set; }
    public object[] UpperOutliers { get; set; }
    public double[] AllOutliers { get; set; }
    public double StandardError { get; set; }
    public double Variance { get; set; }
    public double StandardDeviation { get; set; }
    public double Skewness { get; set; }
    public double Kurtosis { get; set; }
    public ConfidenceInterval ConfidenceInterval { get; set; }
    public Percentiles Percentiles { get; set; }
}