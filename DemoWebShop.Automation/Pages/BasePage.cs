using Microsoft.Playwright;

namespace DemoWebShop.Automation.Pages
{
    public abstract class BasePage
    {
        protected readonly IPage Page;
        protected readonly int Timeout;

        protected BasePage(IPage page, int timeout = 30000)
        {
            Page = page;
            Timeout = timeout;
        }

        protected async Task WaitForPageLoadAsync()
        {
            await Page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle, new() { Timeout = Timeout });
        }

        protected async Task ClickAsync(string selector)
        {
            var locator = Page.Locator(selector);
            await locator.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = Timeout });
            await locator.ClickAsync();
        }

        protected async Task FillAsync(string selector, string value)
        {
            var locator = Page.Locator(selector);
            await locator.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = Timeout });
            await locator.ClearAsync();
            await locator.FillAsync(value);
        }

        protected async Task SelectByValueAsync(string selector, string value)
        {
            var locator = Page.Locator(selector);
            await locator.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = Timeout });
            await locator.SelectOptionAsync(new SelectOptionValue { Value = value });
        }

        protected async Task SelectByLabelAsync(string selector, string label)
        {
            var locator = Page.Locator(selector);
            await locator.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = Timeout });
            await locator.SelectOptionAsync(new SelectOptionValue { Label = label });
        }

        protected async Task<string> GetTextAsync(string selector)
        {
            var locator = Page.Locator(selector);
            await locator.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = Timeout });
            return (await locator.InnerTextAsync()).Trim();
        }

        protected async Task<bool> IsVisibleAsync(string selector)
        {
            try
            {
                await Page.Locator(selector).WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = 5000 });
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected async Task WaitForSelectorAsync(string selector)
        {
            await Page.WaitForSelectorAsync(selector, new() { Timeout = Timeout });
        }
    }
}
