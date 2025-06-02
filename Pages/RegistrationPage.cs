using Microsoft.Playwright;

namespace AutomationExerciseTests.Pages;

public class RegistrationPage : BasePage
{
    public RegistrationPage(IPage page) : base(page) { }

    public async Task RegisterAsync(string name, string email, string password)
    {
        await _page.FillAsync("input[data-qa='signup-name']", name);
        await _page.FillAsync("input[data-qa='signup-email']", email);
        await _page.ClickAsync("button[data-qa='signup-button']");
        await _page.CheckAsync("#id_gender1");
        await _page.FillAsync("input[data-qa='password']", password);
        await _page.SelectOptionAsync("#days", "10");
        await _page.SelectOptionAsync("#months", "5");
        await _page.SelectOptionAsync("#years", "1990");
        await _page.FillAsync("input[data-qa='first_name']", "John");
        await _page.FillAsync("input[data-qa='last_name']", "Doe");
        await _page.FillAsync("input[data-qa='company']", "Test Company");
        await _page.FillAsync("input[data-qa='address']", "123 Test St");
        await _page.FillAsync("input[data-qa='address2']", "Apt 456");
        await _page.SelectOptionAsync("select[data-qa='country']", "United States");
        await _page.FillAsync("input[data-qa='state']", "California");
        await _page.FillAsync("input[data-qa='city']", "Los Angeles");
        await _page.FillAsync("input[data-qa='zipcode']", "90001");
        await _page.FillAsync("input[data-qa='mobile_number']", "555-555-5555");

        await _page.ClickAsync("button[data-qa='create-account']");
    }

    public async Task<bool> IsAccountCreatedAsync() =>
        await _page.IsVisibleAsync("h2[data-qa='account-created']");
}
