using DemoWebShop.Automation.Pages;
using DemoWebShop.Automation.Support;
using TechTalk.SpecFlow;

namespace DemoWebShop.Automation.StepDefinitions
{
    [Binding]
    public class NavigationSteps
    {
        private readonly PlaywrightContext _context;

        public NavigationSteps(PlaywrightContext context)
        {
            _context = context;
        }

        [When(@"I navigate to the Computers menu and select Desktops")]
        public async Task WhenINavigateToTheComputersMenuAndSelectDesktops()
        {
            var homePage = new HomePage(_context.Page);
            await homePage.NavigateToDesktopsAsync();

            var desktopsPage = new DesktopsPage(_context.Page);
            await desktopsPage.AssertOnDesktopsPageAsync();
        }
    }
}
