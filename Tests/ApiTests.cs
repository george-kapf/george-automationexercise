using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomationExerciseTests.Models;
using AutomationExerciseTests.Utilities;
using Newtonsoft.Json.Linq;

namespace AutomationExerciseTests.Tests
{
    [TestClass]
    public class ApiTests
    {
        private static readonly HttpClient _httpClient = new();

        [DataTestMethod]
        [DynamicData(nameof(TestDataLoader.LoadProductListAssertions), typeof(TestDataLoader), DynamicDataSourceType.Method)]
        public async Task TestProductListApi(ProductListTestData testData)
        {
            var response = await _httpClient.GetAsync("https://automationexercise.com/api/productsList");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            var products = json["products"];

            bool categoryMatch = products.Any(p =>
                p["category"]?.ToString().Contains(testData.ExpectedCategory, StringComparison.OrdinalIgnoreCase) == true);

            bool productNameMatch = products.Any(p =>
                p["name"]?.ToString().Equals(testData.ExpectedProductName, StringComparison.OrdinalIgnoreCase) == true);

            Assert.IsTrue(categoryMatch, $"Expected category '{testData.ExpectedCategory}' not found.");
            Assert.IsTrue(productNameMatch, $"Expected product '{testData.ExpectedProductName}' not found.");
        }
    }
}
