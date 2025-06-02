

# AutomationExerciseTests

End-to-End (E2E) and API test automation framework built using **Playwright for .NET**, targeting `https://automationexercise.com`. The framework supports UI tests, API tests, performance metrics, dynamic data loading, and visual regression validation.

---

## 📦 Tech Stack

- **Language**: C# (.NET 8+)
- **Automation Tool**: [Microsoft.Playwright](https://playwright.dev/dotnet/)
- **Test Framework**: MSTest
- **JSON**: For parameterized test data
- **ImageSharp**: For pixel-by-pixel screenshot comparison
- **VS Code**: IDE of choice

---

## 🛠️ Setup Instructions

### 1. Clone the Repo

```bash
git clone https://github.com/your-username/george-automationexercise.git
cd george-automationexercise
````

### 2. Install Dependencies

```bash
dotnet restore
```

### 3. Install Playwright Browsers

```bash
playwright install
```

(If using Playwright CLI with .NET, install using Node first or run via C# initialization logic.)

---

## ▶️ Test Execution

### Run All Tests

```bash
dotnet test
```

### Run Specific Test by Name

```bash
dotnet test --filter "TestName"
```

---

## 🧪 Test Architecture

### ➤ Project Layout

```
AutomationExerciseTests/
│
├── Models/           # Data model classes for parameterized test data
├── Pages/            # Page Object Model (POM) layer
├── TestData/         # JSON files for dynamic test input
├── Tests/            # MSTest-based UI and API test files
├── Utilities/        # Reusable helpers: performance, screenshots, data loader
```

---

### ➤ Design Patterns Used

* **Page Object Model (POM)**: All page interactions are abstracted in classes under `Pages/` (e.g., `HomePage.cs`, `CartPage.cs`).
* **Data-Driven Testing**: Uses `[DynamicData]` attribute and external JSON to drive inputs (`registrationData.json`, `cartOperationsData.json`).
* **Modular Utilities**: Utilities are separated cleanly — no test logic mixed with screenshot, performance or test setup logic.
* **Separation of Concerns**:

  * `Tests/` contains actual test cases
  * `Models/` holds typed classes to deserialize JSON
  * `Utilities/` provides cross-cutting concerns (e.g., loading data, timing, image comparison)

---

## 📌 Key Implementation Decisions

### 🔹 Dynamic Data Loading

Rather than hardcoding test values, test data is loaded dynamically via JSON. This makes the tests:

* More maintainable
* Easily scalable
* Cleaner for CI/CD integration

Example: `LoadRegistrationData()` and `LoadCartOperationData()` use `System.Text.Json` and are defined in `TestDataLoader.cs`.

---

### 🔹 Screenshot Comparison for Visual Testing

Visual regression testing is implemented using:

```csharp
File.ReadAllBytes(baselinePath).SequenceEqual(File.ReadAllBytes(currentPath));
```

This is a **byte-to-byte match**, which can lead to false positives. Planned to migrate to pixel diffing via **ImageSharp** or **pixelmatch** with tolerance.

---

### 🔹 Performance Timing

`PerformanceHelper.cs` is used to time page loads using:

```csharp
var stopwatch = Stopwatch.StartNew();
await page.GotoAsync(url);
stopwatch.Stop();
```

This allows you to profile the user experience during major flows like registration or checkout.

---

## 🧗 Challenges Encountered

### 🔸 Relative File Paths in .NET

* **Issue**: `Path.Combine("TestData", "file.json")` failed when running tests from different directories.
* **Solution**: Used `AppContext.BaseDirectory` to make path resolution reliable.

### 🔸 Screenshot Comparison Always Failing

* **Issue**: Simple byte comparison always failed due to rendering time, fonts, anti-aliasing.
* **Solution**: Plan to integrate ImageSharp with pixel tolerance to solve that.

### 🔸 Quantity Update Not Working in Cart

* **Issue**: Input field for quantity was disabled.
* **Solution**: Skipped quantity update validation or made it configurable via test data.

---

## 🚀 How I’d Improve This Framework with More Time

1. **Advanced Visual Regression**:

   * Use a diff engine like [ImageSharp](https://github.com/SixLabors/ImageSharp) or [Resemble.js](https://rsmbl.github.io/Resemble.js/)
   * Allow setting pixel tolerance and bounding box exclusions.

2. **Reporting Integration**:

   * Integrate **Allure** or **Extent Reports** for HTML summaries of test runs.

3. **Parallel Test Execution**:

   * Configure `MSTest` or migrate to `xUnit` or `NUnit` for better parallelism support.

4. **Test Tags and Categories**:

   * Enable filtering via test categories like `[TestCategory("Regression")]`.

5. **Dockerized Test Runner**:

   * Enable container-based test execution for clean, isolated environments.

6. **CI/CD Integration**:

   * Add GitHub Actions workflow or Azure Pipeline for automated test runs.

7. **Network Stubbing / Mocking**:

   * Use Playwright’s `RouteAsync` for mocking API responses in certain flows.

---

## 💡 Example Test Cases

### ✅ UI: RecordUserRegistrationPerformance

Measures performance and ensures registration success.

### ✅ API: ValidateProductListFromApi

Verifies `/api/productsList` endpoint against dynamic data.

### ✅ Visual: ScreenshotHelper.CompareScreenshots

Fails if UI changes unintentionally.

---

## 🔧 Cleaning Build Artifacts

If `dotnet clean` fails to remove `bin/` and `obj/`, use:

```bash
rm -rf bin obj
```

You can also create a script:

```bash
#!/bin/bash
dotnet clean
rm -rf bin obj TestResults
```

---
