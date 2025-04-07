
# DataExtraction

A console application built with .NET Core 8 for extracting and processing bank data files. Designed using OOP principles and common design patterns for scalability and maintainability.

---

## 🧰 Requirements

- [.NET Core 8 SDK](https://dotnet.microsoft.com/en-us/download)

---

## 📁 Project Structure

```
DataExtraction/
│
├── DataExtraction/          # Startup project
│   └── Program.cs
│
├── ExtractionLib/           # Core logic implemented using OOP & Design Patterns
│
├── DataExtraction.Tests/    # Unit tests covering all business logic
│
└── Release/                 # Console app + sample input files
    ├── DataExtraction.exe
    ├── barclays_input.csv
    └── hdfc_input.csv
```

---

## 🚀 How to Run

1. Navigate to the `Release` directory:
   ```bash
   cd Release
   ```

2. Run the application from the command line using:
   ```bash
   DataExtraction.exe <bank name> <input file>
   ```

   ### Example:
   ```bash
   DataExtraction.exe Barclays barclays_input.csv
   ```

---

## ✅ Testing

Unit tests are located in the `DataExtraction.Tests` folder.

To run tests:
```bash
dotnet test DataExtraction.Tests
```

---

## 🧠 Design Approach

- **OOP Principles**: Follows SOLID principles for clean and maintainable code.
- **Design Patterns**: Utilizes patterns such as Strategy, Factory, and Dependency Injection where applicable.

---

## 📌 Notes

- Ensure you have the required .NET runtime installed to run the `.exe`.
- Input files should follow the expected format for each bank.
