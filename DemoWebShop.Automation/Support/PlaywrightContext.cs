using Microsoft.Playwright;

namespace DemoWebShop.Automation.Support
{
    public class PlaywrightContext
    {
        public IPlaywright PlaywrightInstance { get; set; } = null!;
        public IBrowser Browser { get; set; } = null!;
        public IBrowserContext BrowserContext { get; set; } = null!;
        public IPage Page { get; set; } = null!;
        public string OrderNumber { get; set; } = string.Empty;
    }
}
