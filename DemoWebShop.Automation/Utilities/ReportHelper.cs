using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace DemoWebShop.Automation.Utilities
{
    public static class ReportHelper
    {
        private static ExtentReports? _extentReports;
        private static ExtentTest? _currentTest;
        private static readonly object Lock = new();

        public static void InitializeReport()
        {
            var reportDir = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "..", "..", "..",
                "TestResults", "Reports");

            Directory.CreateDirectory(reportDir);

            var reportPath = Path.Combine(reportDir, $"ExtentReport_{DateTime.Now:yyyyMMdd_HHmmss}.html");
            reportPath = Path.GetFullPath(reportPath);

            var sparkReporter = new ExtentSparkReporter(reportPath);
            sparkReporter.Config.DocumentTitle = "Demo Web Shop - Test Execution Report";
            sparkReporter.Config.ReportName = "BDD Automation Test Results";
            sparkReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(sparkReporter);
            _extentReports.AddSystemInfo("Application", "Demo Web Shop");
            _extentReports.AddSystemInfo("Environment", "QA");
            _extentReports.AddSystemInfo("URL", "https://demowebshop.tricentis.com");

            Console.WriteLine($"Report will be generated at: {reportPath}");
        }

        public static void CreateTest(string testName)
        {
            lock (Lock)
            {
                _currentTest = _extentReports?.CreateTest(testName);
            }
        }

        public static void LogStep(string message)
        {
            lock (Lock)
            {
                _currentTest?.Log(Status.Pass, message);
            }
        }

        public static void LogInfo(string message)
        {
            lock (Lock)
            {
                _currentTest?.Log(Status.Info, message);
            }
        }

        public static void LogSuccess(string message)
        {
            lock (Lock)
            {
                _currentTest?.Log(Status.Pass, message);
            }
        }

        public static void LogFailure(string message, string? screenshotPath = null)
        {
            lock (Lock)
            {
                _currentTest?.Log(Status.Fail, message);
                if (!string.IsNullOrEmpty(screenshotPath) && File.Exists(screenshotPath))
                {
                    _currentTest?.AddScreenCaptureFromPath(screenshotPath);
                }
            }
        }

        public static void FlushReport()
        {
            _extentReports?.Flush();
        }
    }
}
