using Microsoft.Playwright;
using NUnit.Framework;

namespace DemoWebShop.Automation.Pages
{
    public class ShoppingCartPage : BasePage
    {
        private const string CartPageTitle = ".page-title h1";
        private const string TermsOfServiceCheckbox = "#termsofservice";
        private const string CheckoutButton = "#checkout";
        private const string CartItems = ".cart-item-row";
        private const string ShoppingCartLink = ".cart-qty";

        public ShoppingCartPage(IPage page) : base(page) { }

        public async Task NavigateToCartAsync(string baseUrl)
        {
            await Page.GotoAsync($"{baseUrl}/cart");
            await WaitForPageLoadAsync();
        }

        public async Task AssertCartHasItemsAsync()
        {
            await WaitForSelectorAsync(CartItems);
            var itemCount = await Page.Locator(CartItems).CountAsync();
            Assert.That(itemCount, Is.GreaterThan(0), "Shopping cart is empty.");
        }

        public async Task AcceptTermsOfServiceAsync()
        {
            var checkbox = Page.Locator(TermsOfServiceCheckbox);
            await checkbox.WaitForAsync(new() { State = WaitForSelectorState.Visible, Timeout = Timeout });
            var isChecked = await checkbox.IsCheckedAsync();
            if (!isChecked)
            {
                await checkbox.CheckAsync();
            }
            Assert.That(await checkbox.IsCheckedAsync(), Is.True, "Terms of service checkbox not checked.");
        }

        public async Task ProceedToCheckoutAsync()
        {
            await ClickAsync(CheckoutButton);
            await WaitForPageLoadAsync();
        }
    }
}
