using Microsoft.Playwright;
using NUnit.Framework;

namespace DemoWebShop.Automation.Pages
{
    public class DesktopsPage : BasePage
    {
        private const string PageTitle = ".category-title h1";

        public DesktopsPage(IPage page) : base(page) { }

        public async Task AssertOnDesktopsPageAsync()
        {
            await WaitForSelectorAsync(PageTitle);
            var title = await GetTextAsync(PageTitle);
            Assert.That(title, Does.Contain("Desktops"), $"Expected Desktops page but got: {title}");
        }

        public async Task SelectProductAsync(string productName)
        {
            var productLink = Page.Locator(".product-title a").Filter(new() { HasText = productName });
            await productLink.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = Timeout });
            await productLink.ClickAsync();
            await WaitForPageLoadAsync();
        }
    }
}
