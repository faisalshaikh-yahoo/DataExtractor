using ExtractorLib.CustomAttribute;
using ExtractorLib.CustomAttribute.Interfaces;
using ExtractorLib.Dto;

namespace ExtractorLib.CustomAttribute.Services
{
    public class PropertyMapService : IPropertyMapService
    {
        private readonly IComplexFieldExtractorFactory _complexFieldExtractFactory;
        public PropertyMapService(IComplexFieldExtractorFactory complexFieldExtractFactory)
        {
            _complexFieldExtractFactory = complexFieldExtractFactory;
        }

        public List<TOutput> MapProperties<TInput,TOutput>(List<TInput> inputRecords) 
            where TInput : BaseInputFormatDto
            where TOutput : BaseOutputFormatDto, new()
        {
            List<TOutput> outputRecords = new List<TOutput>();
            foreach(var input in inputRecords)
            {
                TOutput output = new TOutput();
                var sourceProps = input.GetType().GetProperties();
                var targetProps = output.GetType().GetProperties();
                foreach (var sourceProp in sourceProps)
                {
                    var csvAttr = sourceProp.GetCustomAttributes(typeof(CsvMap), false)
                                            .FirstOrDefault() as CsvMap;

                    if (csvAttr != null && !string.IsNullOrEmpty(csvAttr.ExtractColumnType))
                    {
                        var complexFieldExtractor = _complexFieldExtractFactory.GetFieldExtractor(csvAttr.ExtractColumnType);
                        complexFieldExtractor.ExtractComplexField<TInput, TOutput>(input, output);
                    }
                    else
                    {
                        // Use Same property if attribute is not defined, otherwise fallback to Column
                        var targetPropName = csvAttr == null ? sourceProp.Name : csvAttr.Column;

                        var targetProp = targetProps.FirstOrDefault(p => p.Name == targetPropName
                                                                         && p.PropertyType == sourceProp.PropertyType);

                        if (targetProp != null && targetProp.CanWrite)
                        {
                            var value = sourceProp.GetValue(input);
                            targetProp.SetValue(output, value);
                        }
                    }
                }
                outputRecords.Add(output);

            }
            return outputRecords;
        }

        public void SetProperty<T>(T dto, string propertyName, object value) where T : BaseOutputFormatDto
        {
            var property = typeof(T).GetProperty(propertyName);
            if (property != null && property.CanWrite)
            {
                property.SetValue(dto, value);
            }
        }
        public object? GetProperty<T>(T dto, string propertyName) where T : BaseInputFormatDto
        {
            var property = typeof(T).GetProperty(propertyName);
            return property?.CanRead == true ? property.GetValue(dto) : null;
        }
    }
}
