using System;
using System.Windows.Input;

namespace prjBTLDemo.NET.Utilities // Sử dụng một namespace duy nhất
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute; // Delegate để thực thi hành động
        private readonly Predicate<object> _canExecute; // Delegate để kiểm tra điều kiện có thể thực thi

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}