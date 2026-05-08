using Microsoft.Playwright;
using NUnit.Framework;

namespace DemoWebShop.Automation.Pages
{
    public class OrderConfirmationPage : BasePage
    {
        private const string SuccessMessage = ".section.order-completed .title strong";
        private const string OrderNumberSelector = ".section.order-completed ul.details li.order-number strong";

        public OrderConfirmationPage(IPage page) : base(page) { }

        public async Task AssertOrderPlacedSuccessfullyAsync()
        {
            await WaitForSelectorAsync(SuccessMessage);
            var message = await GetTextAsync(SuccessMessage);
            Assert.That(message, Does.Contain("successfully"),
                $"Order success message not found. Got: {message}");
        }

        public async Task<string> GetOrderNumberAsync()
        {
            await WaitForSelectorAsync(OrderNumberSelector);
            var rawText = await GetTextAsync(OrderNumberSelector);
            return rawText.Trim();
        }

        public async Task PrintOrderNumberAsync()
        {
            var orderNumber = await GetOrderNumberAsync();
            Console.WriteLine("==============================================");
            Console.WriteLine($"  ORDER PLACED SUCCESSFULLY");
            Console.WriteLine($"  Order Number: {orderNumber}");
            Console.WriteLine($"  Timestamp   : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine("==============================================");
            TestContext.Out.WriteLine($"Order Number: {orderNumber}");
        }
    }
}
