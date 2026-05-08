using DemoWebShop.Automation.Pages;
using DemoWebShop.Automation.Support;
using TechTalk.SpecFlow;

namespace DemoWebShop.Automation.StepDefinitions
{
    [Binding]
    public class CheckoutSteps
    {
        private readonly PlaywrightContext _context;
        private readonly BillingDetails _billing;

        public CheckoutSteps(PlaywrightContext context)
        {
            _context = context;
            _billing = ConfigReader.GetBillingDetails();
        }

        [When(@"I fill in the billing address details")]
        public async Task WhenIFillInTheBillingAddressDetails()
        {
            var checkoutPage = new CheckoutPage(_context.Page);
            await checkoutPage.FillBillingAddressAsync(_billing);
            await checkoutPage.ClickBillingContinueAsync();
        }

        [When(@"I continue through the shipping address step")]
        public async Task WhenIContinueThroughTheShippingAddressStep()
        {
            var checkoutPage = new CheckoutPage(_context.Page);
            await checkoutPage.ClickShippingContinueAsync();
        }

        [When(@"I select ""(.*)"" as the shipping method")]
        public async Task WhenISelectAsTheShippingMethod(string shippingMethod)
        {
            var checkoutPage = new CheckoutPage(_context.Page);
            await checkoutPage.SelectShippingMethodAsync(shippingMethod);
        }

        [When(@"I select ""(.*)"" as the payment method")]
        public async Task WhenISelectAsThePaymentMethod(string paymentMethod)
        {
            var checkoutPage = new CheckoutPage(_context.Page);
            await checkoutPage.SelectPaymentMethodAsync(paymentMethod);
        }

        [When(@"I continue through payment info")]
        public async Task WhenIContinueThroughPaymentInfo()
        {
            var checkoutPage = new CheckoutPage(_context.Page);
            await checkoutPage.ContinuePaymentInfoAsync();
        }

        [When(@"I confirm the order")]
        public async Task WhenIConfirmTheOrder()
        {
            var checkoutPage = new CheckoutPage(_context.Page);
            await checkoutPage.ConfirmOrderAsync();
        }

        [Then(@"the order should be placed successfully")]
        public async Task ThenTheOrderShouldBePlacedSuccessfully()
        {
            var confirmationPage = new OrderConfirmationPage(_context.Page);
            await confirmationPage.AssertOrderPlacedSuccessfullyAsync();
        }

        [Then(@"I capture and print the order number")]
        public async Task ThenICaptureAndPrintTheOrderNumber()
        {
            var confirmationPage = new OrderConfirmationPage(_context.Page);
            var orderNumber = await confirmationPage.GetOrderNumberAsync();
            _context.OrderNumber = orderNumber;
            await confirmationPage.PrintOrderNumberAsync();
        }
    }
}
