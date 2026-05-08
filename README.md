# Demo Web Shop - C# SpecFlow Automation Framework

End-to-end BDD test automation for [Demo Web Shop](https://demowebshop.tricentis.com) using C#, Playwright, SpecFlow, and NUnit.

## Scenario Automated

1. Login to the shopping cart application
2. Navigate to Computers → Desktops
3. Select "Build your own cheap computer"
4. Configure and add the product to the cart
5. Accept Terms of Service and proceed to checkout
6. Complete Billing and Shipping details
7. Select Cash on Delivery as payment method
8. Confirm the order
9. Capture and print the order number

---

## Prerequisites

| Requirement | Version |
|---|---|
| [.NET SDK](https://dotnet.microsoft.com/download) | 8.0 or later |
| [Node.js](https://nodejs.org) | 18+ (for Playwright) |

---

## Project Structure

```
DemoWebShop.Automation/
├── Config/
│   └── appsettings.json          ← Test data & configuration
├── Features/
│   └── DemoWebShopShopping.feature
├── StepDefinitions/
│   ├── LoginSteps.cs
│   ├── NavigationSteps.cs
│   ├── ProductSteps.cs
│   └── CheckoutSteps.cs
├── Pages/                         ← Page Object Model
│   ├── BasePage.cs
│   ├── LoginPage.cs
│   ├── HomePage.cs
│   ├── DesktopsPage.cs
│   ├── ProductDetailPage.cs
│   ├── ShoppingCartPage.cs
│   ├── CheckoutPage.cs
│   └── OrderConfirmationPage.cs
├── Hooks/
│   └── TestHooks.cs               ← Browser lifecycle & reporting
├── Support/
│   ├── PlaywrightContext.cs        ← Shared browser/page context
│   ├── ConfigReader.cs
│   └── TestSettings.cs
├── Utilities/
│   ├── ScreenshotHelper.cs        ← Failure screenshots
│   └── ReportHelper.cs            ← ExtentReports HTML report
└── TestRunner/
    └── AssemblySetup.cs           ← Playwright browser install
```

---

## Quick Start

### Step 1 — Configure test credentials

Open `Config/appsettings.json` and update:

```json
{
  "TestSettings": {
    "BaseUrl": "https://demowebshop.tricentis.com",
    "Email": "YOUR_REGISTERED_EMAIL",
    "Password": "YOUR_PASSWORD",
    "Browser": "chromium",
    "Headless": false,
    "SlowMotion": 50
  },
  "BillingDetails": {
    "FirstName": "John",
    "LastName": "Doe",
    "Country": "United States",
    "State": "Illinois",
    "City": "Chicago",
    "Address1": "1234 Test Street",
    "ZipCode": "60601",
    "PhoneNumber": "1234567890"
  }
}
```

> **Note:** You must have a registered account on demowebshop.tricentis.com.  
> Register at: https://demowebshop.tricentis.com/register

### Step 2 — Restore packages

```bash
cd DemoWebShop.Automation
dotnet restore
```

### Step 3 — Install Playwright browsers

```bash
dotnet build
pwsh bin/Debug/net8.0/playwright.ps1 install
```

On macOS/Linux if you don't have PowerShell:
```bash
dotnet tool install --global Microsoft.Playwright.CLI
playwright install
```

### Step 4 — Run the tests

```bash
dotnet test
```

Or with verbose output:
```bash
dotnet test --logger "console;verbosity=detailed"
```

---

## Configuration Options

| Setting | Description | Default |
|---|---|---|
| `Browser` | `chromium`, `firefox`, `webkit` | `chromium` |
| `Headless` | Run browser headless | `false` |
| `SlowMotion` | Delay in ms between actions | `50` |
| `DefaultTimeout` | Element wait timeout in ms | `30000` |

---

## Test Reports

After each run:
- **HTML Report**: `TestResults/Reports/ExtentReport_<timestamp>.html`
- **Screenshots** (on failure): `TestResults/Screenshots/`

Open the HTML report in any browser for a full execution summary.

---

## Troubleshooting

| Issue | Fix |
|---|---|
| `Browser not found` | Run `playwright install` |
| `Login failed` | Verify credentials in appsettings.json |
| `Element not found` | Increase `DefaultTimeout` in appsettings.json |
| `State dropdown empty` | Ensure Country = "United States" is selected |
| `SpecFlow binding errors` | Run `dotnet restore` and rebuild |

---

## Technology Stack

- **Language**: C# (.NET 8)
- **BDD Framework**: SpecFlow 3.9 (Cucumber)
- **Browser Automation**: Microsoft Playwright
- **Test Runner**: NUnit 3
- **Reporting**: ExtentReports 5
- **Config**: Microsoft.Extensions.Configuration
- **Pattern**: Page Object Model
