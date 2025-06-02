using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutomationExerciseTests.Utilities;

[TestClass]
public class TestBase
{
    protected IPlaywright _playwright = null!;
    protected IBrowser _browser = null!;
    protected IBrowserContext _context = null!;
    protected IPage _page = null!;

    [TestInitialize]
    public async Task TestInitialize()
    {
        _playwright = await Playwright.CreateAsync();

        bool headless = Environment.GetEnvironmentVariable("HEADLESS") != "false";
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = headless
        });

        _context = await _browser.NewContextAsync();
        _page = await _context.NewPageAsync();
    }

    [TestCleanup]
    public async Task TestCleanup()
    {
        await _context?.CloseAsync();
        await _browser?.CloseAsync();
        _playwright?.Dispose();
    }
}
