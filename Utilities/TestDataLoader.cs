using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AutomationExerciseTests.Models;

namespace AutomationExerciseTests.Utilities
{
    public static class TestDataLoader
    {
        public static IEnumerable<object[]> LoadRegistrationData()
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "TestData", "registrationData.json");
                var json = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<List<RegistrationData>>(json);
                return data.Select(d => new object[] { d.Name, d.EmailPrefix, d.Password });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load test data: " + ex.Message);
                throw;
            }
        }

        public static IEnumerable<object[]> LoadSearchFilterData()
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "TestData", "searchFilterData.json");
                var json = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<List<SearchFilterData>>(json);
                return data.Select(d => new object[] { d });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load search filter data: " + ex.Message);
                throw;

            }

        }

        public static IEnumerable<object[]> LoadCartOperationData()
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "TestData", "cartOperationsData.json");
                var json = File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<List<CartOperationData>>(json);
                return data.Select(d => new object[] { d });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load cart operation data: " + ex.Message);
                throw;

            }

        }

        public static IEnumerable<object[]> LoadProductListAssertions()
        {
            try
            {
                var path = Path.Combine(AppContext.BaseDirectory, "TestData", "productListAssertions.json");
                var json = File.ReadAllText(path);
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductListTestData>>(json);
                return data.Select(d => new object[] { d });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to load product list data: " + ex.Message);
                throw;
            }
        }

        
        
    }
}
