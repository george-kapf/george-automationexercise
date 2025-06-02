using Microsoft.Playwright;

namespace AutomationExerciseTests.Pages;

public class ProductPage : BasePage
{
    public ProductPage(IPage page) : base(page) { }

    public async Task ApplyFiltersAsync(string category, string subCategory)
    {
        // Click the category link
        await _page.ClickAsync($"a[href='#{category}']");

        // Wait for subcategories to be visible
        await _page.WaitForSelectorAsync(".panel-body ul li");

        // Click the subcategory link (case-insensitive match)
        var links = await _page.QuerySelectorAllAsync(".panel-body ul li a");
        foreach (var link in links)
        {
            var text = await link.InnerTextAsync();
            if (text.Trim().Equals(subCategory, StringComparison.OrdinalIgnoreCase))
            {
                await link.ClickAsync();
                break;
            }
        }

        // Optionally: Wait for products to load or validate element visibility
        await _page.WaitForSelectorAsync(".features_items .product-image-wrapper");
    }

    public async Task<bool> AreFilteredResultsVisibleAsync() =>
        await _page.Locator(".productinfo").CountAsync() > 0;
}
