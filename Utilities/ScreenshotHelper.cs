using Microsoft.Playwright;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace AutomationExerciseTests.Utilities
{
    public static class ScreenshotHelper
    {
        public static async Task<bool> CompareScreenshotAsync(IPage page, string screenshotName)
        {
            string baselinePath = Path.Combine("VisualBaselines", $"{screenshotName}.png");
            string currentPath = Path.Combine("VisualResults", $"{screenshotName}.png");

            Directory.CreateDirectory("VisualBaselines");
            Directory.CreateDirectory("VisualResults");

            await page.ScreenshotAsync(new PageScreenshotOptions { Path = currentPath, FullPage = true });

            if (!File.Exists(baselinePath))
            {
                File.Copy(currentPath, baselinePath); // First-time save as baseline
                return true;
            }

            return File.ReadAllBytes(baselinePath).SequenceEqual(File.ReadAllBytes(currentPath));
        }
    }

    
}
