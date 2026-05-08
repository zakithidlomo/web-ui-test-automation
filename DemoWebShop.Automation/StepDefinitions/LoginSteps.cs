using DemoWebShop.Automation.Pages;
using DemoWebShop.Automation.Support;
using TechTalk.SpecFlow;

namespace DemoWebShop.Automation.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly PlaywrightContext _context;
        private readonly TestSettings _settings;
        private LoginPage _loginPage = null!;

        public LoginSteps(PlaywrightContext context)
        {
            _context = context;
            _settings = ConfigReader.GetTestSettings();
        }

        [Given(@"I navigate to the Demo Web Shop website")]
        public async Task GivenINavigateToTheDemoWebShopWebsite()
        {
            await _context.Page.GotoAsync(_settings.BaseUrl);
            await _context.Page.WaitForLoadStateAsync(Microsoft.Playwright.LoadState.DOMContentLoaded);
        }

        [Given(@"I log in with registered credentials")]
        public async Task GivenILogInWithRegisteredCredentials()
        {
            _loginPage = new LoginPage(_context.Page);
            await _loginPage.NavigateAsync(_settings.BaseUrl);
            await _loginPage.LoginAsync(_settings.Email, _settings.Password);
            await _loginPage.AssertLoggedInAsync();
        }
    }
}
