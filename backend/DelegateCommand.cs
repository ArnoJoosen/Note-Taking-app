using System.Windows.Input;

namespace Backend;

class DelegateCommand : ICommand
{
    private Action<object> _executeMethod;

    public DelegateCommand(Action<object> executeMethod)
    {
        _executeMethod = executeMethod;
    }

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        _executeMethod(parameter);
    }

    public event EventHandler CanExecuteChanged;
}
