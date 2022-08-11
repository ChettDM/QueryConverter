using System;
using System.Linq;
using System.Text;

namespace QueryConverter.Utilities;

public class ExecuteSqlProcedureConverter
{
    private readonly string _textToConvert;
    
    public string ConvertedText => ConvertQuery(_textToConvert);
    
    public ExecuteSqlProcedureConverter(string textToConvert)
    {
        _textToConvert = textToConvert;
    }

    private string ConvertQuery(string textToConvert)
    {
        if (string.IsNullOrEmpty(textToConvert)) throw new Exception("No Text Provided");
        
        var textToConvertSections = textToConvert.Split("',N'");

        if (textToConvertSections.Length != 2) throw new Exception("More than one Split");
        
        var textToConvertFirstPart = textToConvertSections[0];
        var textToConvertSecondPart = textToConvertSections[1];

        textToConvertFirstPart = textToConvertFirstPart[21..];

        var textToConvertSecondPartSections = textToConvertSecondPart.Split(',');

        var convertedStringBuilder = new StringBuilder();
        convertedStringBuilder.AppendLine("-- Declarations");
        convertedStringBuilder.Append("DECLARE ");

        var index = 0;
        for (; index < textToConvertSecondPartSections.Length; index++)
        {
            var item = textToConvertSecondPartSections[index];
            if (!item.Contains('='))
                convertedStringBuilder.Append(item + ", ");
            else break;
        }
        convertedStringBuilder.Remove(convertedStringBuilder.Length - 3, 3);

        convertedStringBuilder.AppendLine(";");
        convertedStringBuilder.AppendLine();
        convertedStringBuilder.AppendLine("-- Sets");
        for (; index < textToConvertSecondPartSections.Length; index++)
        {
            convertedStringBuilder.AppendLine(
                "SET " + 
                textToConvertSecondPartSections[index] + 
                ";");
        }

        convertedStringBuilder.AppendLine();
        convertedStringBuilder.AppendLine("-- Query");
        convertedStringBuilder.AppendLine(textToConvertFirstPart);

        return convertedStringBuilder.ToString();
    }
}