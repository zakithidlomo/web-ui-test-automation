namespace DemoWebShop.Automation.Support
{
    public class AppSettings
    {
        public TestSettings TestSettings { get; set; } = new();
        public BillingDetails BillingDetails { get; set; } = new();
    }

    public class TestSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Browser { get; set; } = "chromium";
        public bool Headless { get; set; } = false;
        public float SlowMotion { get; set; } = 0;
        public int DefaultTimeout { get; set; } = 30000;
    }

    public class BillingDetails
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
