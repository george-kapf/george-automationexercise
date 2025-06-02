using Microsoft.Playwright;

namespace AutomationExerciseTests.Pages;

public class CartPage : BasePage
{
    public CartPage(IPage page) : base(page) { }

    private ILocator ProductWrappers => _page.Locator(".features_items .product-image-wrapper");

    public async Task AddProductToCart(int index)
    {
        // Hover to reveal the overlay
        await HoverOverProductByIndexAsync(index);

        // Locate all "Add to cart" buttons after hover (each within .product-overlay)
        var addToCartButtons = _page.Locator(".features_items .product-overlay .add-to-cart");

        await addToCartButtons.Nth(index).ClickAsync();
        await _page.ClickAsync("button[data-dismiss='modal']");
    }

    public async Task HoverOverProductByIndexAsync(int index)
    {
        var products = await ProductWrappers.ElementHandlesAsync();
        await products[index].HoverAsync();
    }

    public async Task GoToCartAsync() => await _page.ClickAsync("a[href='/view_cart']");

    public async Task UpdateQuantity(int index, int quantity)
    {
        await _page.FillAsync($"tr:nth-child({index}) input.cart_quantity_input", quantity.ToString());
    }

    public async Task RemoveProduct(int index)
    {
        await _page.ClickAsync($"tr:nth-child({index}) .cart_delete a");
    }

    public async Task<bool> IsCartUpdatedAsync() =>
        await _page.Locator("#cart_info").IsVisibleAsync();
}
