using Microsoft.Playwright;

namespace AutomationExerciseTests.Pages;

public class HomePage : BasePage
{
    public HomePage(IPage page) : base(page) { }

    public async Task GoToAsync() => await _page.GotoAsync("https://automationexercise.com");

    public async Task ClickSignupLoginAsync() => await _page.ClickAsync("a[href='/login']");
    public async Task SearchProductAsync(string searchTerm)
    {
        await _page.ClickAsync("a[href='/products']");
        await _page.FillAsync("input[name='search']", searchTerm);
        await _page.ClickAsync("button[type='button']");
    }
}
