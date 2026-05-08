using DemoWebShop.Automation.Pages;
using DemoWebShop.Automation.Support;
using TechTalk.SpecFlow;

namespace DemoWebShop.Automation.StepDefinitions
{
    [Binding]
    public class ProductSteps
    {
        private readonly PlaywrightContext _context;
        private readonly TestSettings _settings;

        public ProductSteps(PlaywrightContext context)
        {
            _context = context;
            _settings = ConfigReader.GetTestSettings();
        }

        [When(@"I select the ""(.*)"" product")]
        public async Task WhenISelectTheProduct(string productName)
        {
            var desktopsPage = new DesktopsPage(_context.Page);
            await desktopsPage.SelectProductAsync(productName);
        }

        [When(@"I configure the product with default options")]
        public async Task WhenIConfigureTheProductWithDefaultOptions()
        {
            var productPage = new ProductDetailPage(_context.Page);
            await productPage.ConfigureDefaultOptionsAsync();
            await productPage.SetQuantityAsync(1);
        }

        [When(@"I add the product to the cart")]
        public async Task WhenIAddTheProductToTheCart()
        {
            var productPage = new ProductDetailPage(_context.Page);
            await productPage.AddToCartAsync();
        }

        [Then(@"I should see a cart notification")]
        public async Task ThenIShouldSeeACartNotification()
        {
            var productPage = new ProductDetailPage(_context.Page);
            await productPage.AssertCartNotificationAsync();
        }

        [When(@"I navigate to the shopping cart")]
        public async Task WhenINavigateToTheShoppingCart()
        {
            var cartPage = new ShoppingCartPage(_context.Page);
            await cartPage.NavigateToCartAsync(_settings.BaseUrl);
            await cartPage.AssertCartHasItemsAsync();
        }

        [When(@"I accept the terms of service")]
        public async Task WhenIAcceptTheTermsOfService()
        {
            var cartPage = new ShoppingCartPage(_context.Page);
            await cartPage.AcceptTermsOfServiceAsync();
        }

        [When(@"I proceed to checkout")]
        public async Task WhenIProceedToCheckout()
        {
            var cartPage = new ShoppingCartPage(_context.Page);
            await cartPage.ProceedToCheckoutAsync();
        }
    }
}
