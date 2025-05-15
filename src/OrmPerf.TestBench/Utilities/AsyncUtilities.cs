using System.Globalization;

namespace OrmPerf.TestBench.Utilities;

internal static class AsyncUtilities
{
    private static readonly TaskFactory _taskFactory = new(
        CancellationToken.None,
        TaskCreationOptions.None, 
        TaskContinuationOptions.None, 
        TaskScheduler.Default);

    public static TResult RunSync<TResult>(Func<Task<TResult>> func)
    {
        var cultureUi = CultureInfo.CurrentUICulture;
        var culture = CultureInfo.CurrentCulture;
        return _taskFactory.StartNew(() =>
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = cultureUi;
            return func();
        }).Unwrap().GetAwaiter().GetResult();
    }

    public static void RunSync(Func<Task> func)
    {
        var cultureUi = CultureInfo.CurrentUICulture;
        var culture = CultureInfo.CurrentCulture;
        _taskFactory.StartNew(() =>
        {
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = cultureUi;
            return func();
        }).Unwrap().GetAwaiter().GetResult();
    }
}