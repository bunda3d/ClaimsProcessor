# Insurance Claims Processor

A C# .NET 9.0 solution for processing insurance claims based on policy rules.

## 🚀 How to Run

You can run this application using Visual Studio or the .NET CLI (Command Line).

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

### Option 1: Visual Studio
1. Open the solution in Visual Studio.
2. Set **`ClaimsProcessor.App`** as the Startup Project.
3. Press **F5** or run the application.
4. The console will display policy details and evaluate multiple claim scenarios.

### Option 2: Command Line (CLI)
Navigate to the root solution folder in your terminal and run:
```bash
dotnet run --project ClaimsProcessor.App
```

## 🧪 Running Tests
This solution uses **xUnit** for unit testing.

### Via Visual Studio
1. Open the **Test Explorer** (Test > Test Explorer).
2. Click the green "Run All" button.

### Via Command Line
To run all tests and see a localized summary:
```bash
dotnet test
```
### 🖥️ Console Output
This is what you should see when running the application:
![Console Application Demo](assets/console-demo.png)

