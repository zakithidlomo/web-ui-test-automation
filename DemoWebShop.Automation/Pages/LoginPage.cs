using Microsoft.Playwright;
using NUnit.Framework;

namespace DemoWebShop.Automation.Pages
{
    public class LoginPage : BasePage
    {
        private const string EmailField = "#Email";
        private const string PasswordField = "#Password";
        private const string LoginButton = "input.login-button";
        private const string AccountLink = ".account";
        private const string ErrorMessages = ".validation-summary-errors";

        public LoginPage(IPage page) : base(page) { }

        public async Task NavigateAsync(string baseUrl)
        {
            await Page.GotoAsync($"{baseUrl}/login");
            await WaitForPageLoadAsync();
        }

        public async Task LoginAsync(string email, string password)
        {
            await FillAsync(EmailField, email);
            await FillAsync(PasswordField, password);
            await ClickAsync(LoginButton);
            await WaitForPageLoadAsync();
        }

        public async Task AssertLoggedInAsync()
        {
            await WaitForSelectorAsync(AccountLink);
            var isVisible = await IsVisibleAsync(AccountLink);
            Assert.That(isVisible, Is.True, "User is not logged in — account link not found.");
        }
    }
}
