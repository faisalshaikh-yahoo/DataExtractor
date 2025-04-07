// See https://aka.ms/new-console-template for more information
using ExtractorLib;
using ExtractorLib.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


if (args.Length < 2)
{
    Console.WriteLine("Usage: DataExtraction.exe <BankName> <InputFile>.csv");
    return;
}
var bankName = args[0];
var filePath = args[1];

var serviceCollection = new ServiceCollection();
serviceCollection.AddExtractionServices(); // Extension method from library

var serviceProvider = serviceCollection.BuildServiceProvider();

// Resolve and use service
var extractionUtil = serviceProvider.GetRequiredService<IExtractionUtil>();

extractionUtil.ExtractData(bankName, filePath);
