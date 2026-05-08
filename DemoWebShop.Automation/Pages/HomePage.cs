using Microsoft.Playwright;

namespace DemoWebShop.Automation.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IPage page) : base(page) { }

        public async Task NavigateToDesktopsAsync()
        {
            // Hover over Computers menu item to reveal sub-menu
            var computersMenu = Page.Locator(".top-menu > li").Filter(new() { HasText = "Computers" });
            await computersMenu.HoverAsync();
            await Page.WaitForTimeoutAsync(600);

            // Click on Desktops sub-menu item
            var desktopsLink = Page.Locator(".top-menu a[href='/desktops']");
            await desktopsLink.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = Timeout });
            await desktopsLink.ClickAsync();
            await WaitForPageLoadAsync();
        }
    }
}
