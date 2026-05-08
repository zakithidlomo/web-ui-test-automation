using Microsoft.Playwright;

namespace DemoWebShop.Automation.Utilities
{
    public static class ScreenshotHelper
    {
        public static async Task<string> TakeScreenshotAsync(IPage page, string scenarioName)
        {
            var screenshotDir = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "..", "..", "..",
                "TestResults", "Screenshots");

            Directory.CreateDirectory(screenshotDir);

            var sanitized = string.Concat(scenarioName.Split(Path.GetInvalidFileNameChars()));
            var fileName = $"{sanitized}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            var fullPath = Path.GetFullPath(Path.Combine(screenshotDir, fileName));

            try
            {
                await page.ScreenshotAsync(new PageScreenshotOptions
                {
                    Path = fullPath,
                    FullPage = true
                });
                Console.WriteLine($"Screenshot saved: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to capture screenshot: {ex.Message}");
            }

            return fullPath;
        }
    }
}
