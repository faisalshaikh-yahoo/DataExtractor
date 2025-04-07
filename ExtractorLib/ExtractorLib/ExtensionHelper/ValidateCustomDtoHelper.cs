using ExtractorLib.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractorLib.Helper
{
    public class ValidateCustomDtoHelper
    {

        public static List<T> ValidateFileData<T>(List<T> records) where T : BaseInputFormatDto
        {

            var errorsList = new List<string>();
            try
            {

                int rowNumber = 1;
                foreach (var record in records)
                {
                    if (!record.IsValid(out var errors))
                    {
                        foreach (var error in errors)
                        {
                            errorsList.Add($"Row {rowNumber}: {error}");
                        }
                    }

                    rowNumber++;
                }
            }
            catch (Exception ex)
            {
                errorsList.Add($"Parsing error: {ex.Message}");
            }

            // Display or log errors
            if (errorsList.Any())
            {
                Console.WriteLine("Validation errors:");
                foreach (var error in errorsList)
                {
                    Console.WriteLine(error);
                }
                return new List<T>();
            }
            return records.ToList();

        }
    }
}
