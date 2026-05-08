using Microsoft.Playwright;
using NUnit.Framework;

namespace DemoWebShop.Automation.Pages
{
    public class ProductDetailPage : BasePage
    {
        private const string ProductTitle = ".product-name h1";
        private const string AddToCartButton = "input[value='Add to cart']";
        private const string CartNotification = "#bar-notification .content";
        private const string QuantityField = ".qty-input";

        public ProductDetailPage(IPage page) : base(page) { }

        public async Task AssertProductTitleAsync(string expectedTitle)
        {
            await WaitForSelectorAsync(ProductTitle);
            var title = await GetTextAsync(ProductTitle);
            Assert.That(title, Does.Contain(expectedTitle), $"Product title mismatch. Expected: {expectedTitle}, Got: {title}");
        }

        public async Task SelectRadioOptionByLabelAsync(string labelText)
        {
            // Click on the label to select the associated radio button
            var label = Page.Locator($".product-variant-line label").Filter(new() { HasText = labelText }).First;
            await label.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = Timeout });
            await label.ClickAsync();
            await Page.WaitForTimeoutAsync(300);
        }

        public async Task CheckSoftwareOptionAsync(string labelText)
        {
            var label = Page.Locator($".product-variant-line label").Filter(new() { HasText = labelText }).First;
            await label.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = Timeout });
            await label.ClickAsync();
            await Page.WaitForTimeoutAsync(300);
        }

        public async Task ConfigureDefaultOptionsAsync()
        {
            // Select first available option for each attribute group using radio buttons
            // Processor - first option (2.2 GHz)
            var processorOptions = Page.Locator("input[type='radio']");
            var count = await processorOptions.CountAsync();
            if (count > 0)
            {
                // Select radio buttons for Processor, RAM, HDD, OS (pick first of each group)
                var radioGroups = await Page.Locator("input[type='radio']").AllAsync();
                var selectedGroups = new HashSet<string>();

                foreach (var radio in radioGroups)
                {
                    var name = await radio.GetAttributeAsync("name") ?? string.Empty;
                    if (!selectedGroups.Contains(name))
                    {
                        await radio.CheckAsync();
                        selectedGroups.Add(name);
                        await Page.WaitForTimeoutAsync(200);
                    }
                }
            }
        }

        public async Task SetQuantityAsync(int quantity)
        {
            if (await IsVisibleAsync(QuantityField))
            {
                await FillAsync(QuantityField, quantity.ToString());
            }
        }

        public async Task AddToCartAsync()
        {
            await ClickAsync(AddToCartButton);
            // Wait for notification bar
            await WaitForSelectorAsync(CartNotification);
            await Page.WaitForTimeoutAsync(1500);
        }

        public async Task AssertCartNotificationAsync()
        {
            var isVisible = await IsVisibleAsync(CartNotification);
            Assert.That(isVisible, Is.True, "Cart notification not displayed after adding product.");
        }
    }
}
