using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using MVVM;
using MVVM.Arguments;
using QueryConverter.Utilities;

namespace QueryConverter.ViewModels;

public class ExecuteSqlProcedureConverterViewModel : DataContext
{
    public string _unConvertedText;
    public string _message;

    public string UnConvertedText
    {
        get => _unConvertedText;
        set
        {
            if (_unConvertedText == value) return;
            _unConvertedText = value;
            OnPropertyChanged();
        }
    }

    public string Message
    {
        get => _message;
        set
        {
            if (_message == value) return;
            _message = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsOpen));
        }
    }

    public bool IsOpen
    {
        get => string.IsNullOrEmpty(_message);
    }
    
    public Command Convert { get; }

    public ExecuteSqlProcedureConverterViewModel()
    {
        Convert = new Command();
        Convert.ExecuteActions += ConvertOnExecuteActions;
    }

    private void ConvertOnExecuteActions(object parameters, ExecuteCommandArguments arg)
    {
        try
        {
            Clipboard.SetText(new ExecuteSqlProcedureConverter(UnConvertedText).ConvertedText);
            CreateMessage("Copied to Clipboard");
        }
        catch (Exception e)
        {
            Clipboard.SetText(e.Message);
            CreateMessage(e.Message);
        }
    }

    private void CreateMessage(string message)
    {
        // Message = message;
        // var task = new Task(() =>
        // {
        //     Task.Delay(3000);
        //     if (message == Message)
        //         Message = "";
        // });
        // task.Start();
    }
}