using Microsoft.Playwright;
using System.Threading.Tasks;

namespace AutomationExerciseTests.Utilities
{
    public static class PerformanceHelper
    {
        public static async Task<double> GetPageLoadTimeAsync(IPage page)
        {
            var timing = await page.EvaluateAsync<PerformanceTiming>(@"
                () => {
                    const timing = window.performance.timing;
                    return {
                        navigationStart: timing.navigationStart,
                        loadEventEnd: timing.loadEventEnd
                    };
                }");

            return (timing.LoadEventEnd - timing.NavigationStart) / 1000.0; // in seconds
        }
    }

    public class PerformanceTiming
    {
        public long NavigationStart { get; set; }
        public long LoadEventEnd { get; set; }
    }
}
