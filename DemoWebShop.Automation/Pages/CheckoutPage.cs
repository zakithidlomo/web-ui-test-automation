using Microsoft.Playwright;
using DemoWebShop.Automation.Support;

namespace DemoWebShop.Automation.Pages
{
    public class CheckoutPage : BasePage
    {
        // Billing selectors
        private const string BillingAddressDropdown = "#billing-address-select";
        private const string BillingFirstName = "#BillingNewAddress_FirstName";
        private const string BillingLastName = "#BillingNewAddress_LastName";
        private const string BillingEmail = "#BillingNewAddress_Email";
        private const string BillingCountry = "#BillingNewAddress_CountryId";
        private const string BillingState = "#BillingNewAddress_StateProvinceId";
        private const string BillingCity = "#BillingNewAddress_City";
        private const string BillingAddress1 = "#BillingNewAddress_Address1";
        private const string BillingZip = "#BillingNewAddress_ZipPostalCode";
        private const string BillingPhone = "#BillingNewAddress_PhoneNumber";
        private const string BillingContinueButton = "#billing-buttons-container .new-address-next-step-button";
        private const string BillingContinueButtonExisting = "#billing-buttons-container .billing-address-next-step-button";

        // Shipping selectors
        private const string ShippingContinueButton = "#shipping-buttons-container .new-address-next-step-button";
        private const string ShippingContinueButtonExisting = "#shipping-buttons-container .shipping-address-next-step-button";

        // Shipping method selectors
        private const string ShippingMethodContinueButton = "#shipping-method-buttons-container input";

        // Payment method selectors
        private const string PaymentMethodContinueButton = "#payment-method-buttons-container input";

        // Payment info
        private const string PaymentInfoContinueButton = "#payment-info-buttons-container input";

        // Confirm
        private const string ConfirmOrderButton = "#confirm-order-buttons-container input";

        public CheckoutPage(IPage page) : base(page) { }

        public async Task FillBillingAddressAsync(BillingDetails billing)
        {
            // If there's a dropdown with existing addresses, select "New Address"
            if (await IsVisibleAsync(BillingAddressDropdown))
            {
                await Page.Locator(BillingAddressDropdown).SelectOptionAsync(new SelectOptionValue { Value = "0" });
                await Page.WaitForTimeoutAsync(800);
            }

            await FillAsync(BillingFirstName, billing.FirstName);
            await FillAsync(BillingLastName, billing.LastName);
            await FillAsync(BillingEmail, billing.Email);
            await SelectByLabelAsync(BillingCountry, billing.Country);
            await Page.WaitForTimeoutAsync(1200); // Wait for state dropdown to populate via AJAX
            await SelectByLabelAsync(BillingState, billing.State);
            await FillAsync(BillingCity, billing.City);
            await FillAsync(BillingAddress1, billing.Address1);
            await FillAsync(BillingZip, billing.ZipCode);
            await FillAsync(BillingPhone, billing.PhoneNumber);
        }

        public async Task ClickBillingContinueAsync()
        {
            if (await IsVisibleAsync(BillingContinueButton))
                await ClickAsync(BillingContinueButton);
            else
                await ClickAsync(BillingContinueButtonExisting);

            await Page.WaitForTimeoutAsync(1500);
        }

        public async Task ClickShippingContinueAsync()
        {
            if (await IsVisibleAsync(ShippingContinueButton))
                await ClickAsync(ShippingContinueButton);
            else if (await IsVisibleAsync(ShippingContinueButtonExisting))
                await ClickAsync(ShippingContinueButtonExisting);

            await Page.WaitForTimeoutAsync(1500);
        }

        public async Task SelectShippingMethodAsync(string method)
        {
            // Select shipping method radio by label text
            var label = Page.Locator("#checkout-step-shipping-method label").Filter(new() { HasText = method });
            if (await label.CountAsync() > 0)
            {
                await label.First.ClickAsync();
            }
            else
            {
                // Fallback: select first available shipping method
                await Page.Locator("#checkout-step-shipping-method input[type='radio']").First.CheckAsync();
            }
            await Page.WaitForTimeoutAsync(500);
            await ClickAsync(ShippingMethodContinueButton);
            await Page.WaitForTimeoutAsync(1500);
        }

        public async Task SelectPaymentMethodAsync(string paymentMethod)
        {
            // Find the payment method radio by label text
            var labels = Page.Locator("#checkout-step-payment-method label");
            var count = await labels.CountAsync();

            for (int i = 0; i < count; i++)
            {
                var text = await labels.Nth(i).InnerTextAsync();
                if (text.Contains(paymentMethod, StringComparison.OrdinalIgnoreCase))
                {
                    await labels.Nth(i).ClickAsync();
                    break;
                }
            }

            await Page.WaitForTimeoutAsync(500);
            await ClickAsync(PaymentMethodContinueButton);
            await Page.WaitForTimeoutAsync(1500);
        }

        public async Task ContinuePaymentInfoAsync()
        {
            if (await IsVisibleAsync(PaymentInfoContinueButton))
            {
                await ClickAsync(PaymentInfoContinueButton);
                await Page.WaitForTimeoutAsync(1500);
            }
        }

        public async Task ConfirmOrderAsync()
        {
            await WaitForSelectorAsync(ConfirmOrderButton);
            await ClickAsync(ConfirmOrderButton);
            await WaitForPageLoadAsync();
        }
    }
}
