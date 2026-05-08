using DemoWebShop.Automation.Support;
using DemoWebShop.Automation.Utilities;
using Microsoft.Playwright;
using TechTalk.SpecFlow;

namespace DemoWebShop.Automation.Hooks
{
    [Binding]
    public class TestHooks
    {
        private readonly PlaywrightContext _context;
        private readonly ScenarioContext _scenarioContext;

        public TestHooks(PlaywrightContext context, ScenarioContext scenarioContext)
        {
            _context = context;
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            ReportHelper.InitializeReport();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            ReportHelper.FlushReport();
        }

        [BeforeScenario(Order = 1)]
        public async Task BeforeScenarioAsync()
        {
            var settings = ConfigReader.GetTestSettings();

            var playwright = await Playwright.CreateAsync();

            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = settings.Headless,
                SlowMo = settings.SlowMotion
            };

            IBrowser browser = settings.Browser.ToLower() switch
            {
                "firefox" => await playwright.Firefox.LaunchAsync(launchOptions),
                "webkit" => await playwright.Webkit.LaunchAsync(launchOptions),
                _ => await playwright.Chromium.LaunchAsync(launchOptions)
            };

            var browserContext = await browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1280, Height = 720 },
                IgnoreHTTPSErrors = true
            });

            var page = await browserContext.NewPageAsync();
            page.SetDefaultTimeout(settings.DefaultTimeout);

            _context.PlaywrightInstance = playwright;
            _context.Browser = browser;
            _context.BrowserContext = browserContext;
            _context.Page = page;

            ReportHelper.CreateTest(_scenarioContext.ScenarioInfo.Title);
            ReportHelper.LogInfo($"Browser: {settings.Browser} | Headless: {settings.Headless}");
        }

        [AfterScenario(Order = 1)]
        public async Task AfterScenarioAsync()
        {
            if (_scenarioContext.TestError != null)
            {
                if (_context.Page != null)
                {
                    var screenshotPath = await ScreenshotHelper.TakeScreenshotAsync(
                        _context.Page,
                        _scenarioContext.ScenarioInfo.Title);
                    ReportHelper.LogFailure(
                        $"FAILED: {_scenarioContext.TestError.Message}",
                        screenshotPath);
                }
                else
                {
                    ReportHelper.LogFailure($"FAILED (browser never launched): {_scenarioContext.TestError.Message}");
                }
            }
            else
            {
                ReportHelper.LogSuccess("Scenario completed successfully.");
            }

            try { if (_context.BrowserContext != null) await _context.BrowserContext.CloseAsync(); } catch { /* already closed */ }
            try { if (_context.Browser != null) await _context.Browser.CloseAsync(); } catch { /* already closed */ }
            try { _context.PlaywrightInstance?.Dispose(); } catch { /* ignore */ }
        }
    }
}
