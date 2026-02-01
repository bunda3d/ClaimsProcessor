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

---

## 🏛️ Architecture & Design Decisions

### 1. Domain-Centric Design
I separated the core logic (`ClaimsProcessor.Core`) from the operational interface (`ClaimsProcessor.App`).
- **Benefit**: The business logic is pure, checking only rules against data. It has no dependencies on the UI (Console).
- **Decoupling**: This allows us to replace the UI entirely (e.g., swapping the Console App for a Web API or changing a frontend from Vue to Angular) without touching the core business logic.

### 2. Test-Driven Development (TDD)
I followed a Red-Green-Refactor cycle.
- **Process**: Each business rule (e.g., *Policy must be active*) was defined as a failing test before implementation.
- **Outcome**: This ensured 100% meaningful code coverage and prevented regression as new rules (like *Zero Payout logic*) were added.

### 3. Immutability
I used C# `record` types for `Policy` and `Claim`.
- **Reasoning**: Insurance data represents historical facts/transactions. A claim amount shouldn't change via side effects after submission.
- **Comparison**: Unlike standard classes (POCOs), `records` are immutable by default and use value-based equality. This makes them safer for passing data between services and easier to test.

### 4. Explicit Result Types
Instead of throwing exceptions for business failures (like "Policy Inactive"), I returned a `ClaimResult` object.
- **Reasoning**: A valid policy rejection is a normal business flow, not a system error or crash. This avoids the performance penalty and complexity of using Exceptions for control flow.
- **Predictability**: This makes failure paths explicit in the method signature, ensuring the caller handles outcomes responsibly rather than relying on `try/catch` blocks for returning thrown messages.

## 🔮 Future Improvements
With more time, I would implement:
- **Repository Pattern**: Abstract and data operations (CRUD) to load Policies/Claims from a real database and record outcomes. 
  - Moving CRUD ops out of functions decouples the data access layer, allowing better separation of concerns and easier testing/mocking.
  - It also makes migrating to other DB types or Object-Relational Mappers practically possible.
- **Web API**: Expose claim evaluation services via RESTful API.
- **UI Enhancements**: Build a web front-end for users to submit claims and view results.
- **Validation Pipeline**: Use tested tools like `FluentValidation` for basic data checking, to have great "input hygiene", and so the core business rule logic can be more focused.
- **Currency Handling**: Overkill for this exercise, but large financial systems should avoid `decimal` primitives for money.
  - A structured `record Money(decimal Amount, string Currency)` could prevent accounting errors if pricing serves multiple markets (e.g., $CAD, $USD, $EUR).

