using NUnit.Framework;

namespace DemoWebShop.Automation.TestRunner
{
    [SetUpFixture]
    public class AssemblySetup
    {
        [OneTimeSetUp]
        public void InstallPlaywrightBrowsers()
        {
            // Install Playwright browsers before tests run (no --with-deps to avoid sudo requirement on macOS)
            var exitCode = Microsoft.Playwright.Program.Main(new[] { "install", "chromium" });
            if (exitCode != 0)
            {
                Console.WriteLine($"Playwright browser install returned exit code {exitCode} — browsers may already be installed.");
            }
        }
    }
}
