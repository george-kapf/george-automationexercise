using Microsoft.Playwright;

namespace AutomationExerciseTests.Pages;

public class BasePage
{
    protected readonly IPage _page;
    public BasePage(IPage page) => _page = page;
}
