using AutomationExerciseTests.Pages;
using AutomationExerciseTests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomationExerciseTests.Models;

namespace AutomationExerciseTests.Tests;

[DoNotParallelize]
[TestClass]
public class E2ETests : TestBase
{

    [TestMethod]
    [DynamicData(nameof(TestDataLoader.LoadRegistrationData), typeof(TestDataLoader), DynamicDataSourceType.Method)]
    public async Task TestUserRegistration(string name, string emailPrefix, string password)
    {
        var home = new HomePage(_page);
        var register = new RegistrationPage(_page);

        await home.GoToAsync();
        await home.ClickSignupLoginAsync();

        string email = $"{emailPrefix}{Guid.NewGuid():N}@example.com";
        await register.RegisterAsync(name, email, password);

        Assert.IsTrue(await register.IsAccountCreatedAsync());
    }

    [TestMethod]
    [DynamicData(nameof(TestDataLoader.LoadSearchFilterData), typeof(TestDataLoader), DynamicDataSourceType.Method)]
    public async Task TestProductSearchAndFilter(SearchFilterData data)
    {
        var home = new HomePage(_page);
        var product = new ProductPage(_page);

        await home.GoToAsync();
        await home.SearchProductAsync(data.SearchTerm);
        await product.ApplyFiltersAsync(data.Category, data.SubCategory);

        Assert.IsTrue(await product.AreFilteredResultsVisibleAsync());
    }

    [TestMethod]
    [DynamicData(nameof(TestDataLoader.LoadCartOperationData), typeof(TestDataLoader), DynamicDataSourceType.Method)]
    public async Task TestShoppingCartFunctionality(CartOperationData cartData)
    {
        var home = new HomePage(_page);
        var cart = new CartPage(_page);

        await home.GoToAsync();
        foreach (var index in cartData.ProductIndices)
        {
            await cart.AddProductToCart(index);
        }
        await cart.GoToCartAsync();
        //await cart.UpdateQuantity(1, 3); // Not able to update quantity because quantity input is disabled.
        await cart.RemoveProduct(2);

        Assert.IsTrue(await cart.IsCartUpdatedAsync());
    }

    [TestMethod]
    public async Task RecordUserRegistrationPerformance()
    {
        var home = new HomePage(_page);
        var register = new RegistrationPage(_page);

        await home.GoToAsync();
        double homeLoadTime = await PerformanceHelper.GetPageLoadTimeAsync(_page);
        Console.WriteLine($"Home Page Load Time: {homeLoadTime} seconds");

        await home.ClickSignupLoginAsync();
        await register.RegisterAsync("TestUser", $"test{Guid.NewGuid()}@example.com", "Test1234");
        double signupLoadTime = await PerformanceHelper.GetPageLoadTimeAsync(_page);
        Console.WriteLine($"Signup Page Load Time: {signupLoadTime} seconds");

        Assert.IsTrue(await register.IsAccountCreatedAsync());
    }
    
    [TestMethod]
    public async Task VerifyHomePageVisualRegression()
    {
        var home = new HomePage(_page);
        await home.GoToAsync();

        bool match = await ScreenshotHelper.CompareScreenshotAsync(_page, "HomePage");

        Assert.IsTrue(match, "Visual regression detected on Home Page!");
    }

}
