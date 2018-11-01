using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Warehouse.Common
{
    class RelayCommand
    {
        #region Constructors
        /// <summary>
        /// RelayCommand constructor
        /// </summary>
        /// <param name="executeFunction">function to be executed</param>
        public RelayCommand(Action<object> executeFunction)
        {
            _ExecuteFunction = executeFunction;
        }

        /// <summary>
        /// RelayCommand constructor
        /// </summary>
        /// <param name="executeFunction">function to be executed</param>
        /// <param name="canExecuteFunction">function witch checks that command can be executed</param>
        public RelayCommand(Action<object> executeFunction, Func<object, bool> canExecuteFunction)
        {
            _ExecuteFunction = executeFunction;
            _CanExecuteFunction = canExecuteFunction;
        }

        #endregion

        private bool? _canExecute;
        public void ChangeCanExecute(bool newValue)
        {
            _canExecute = newValue;
            RaiseCanExecuteChanged();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "Das soll nicht statisch bleiben, damit es bei Bedarf wieder ohne CommandManager.RequerySuggested funktioniert")]
        public void ChangeCanExecute()
        {
            RaiseCanExecuteChanged();
        }

        private readonly Func<object, bool> _CanExecuteFunction;
        public bool CanExecute(object parameter)
        {
            if (_canExecute.HasValue)
            {
                return _canExecute.Value;
            }
            if (_CanExecuteFunction != null)
            {
                return _CanExecuteFunction(parameter);
            }
            return true;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }


        private static void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        private readonly Action<object> _ExecuteFunction;
        public void Execute(object parameter)
        {
            if (_ExecuteFunction != null)
            {
                _ExecuteFunction(parameter);
            }
        }

        public static void UpdateBindings()
        {
            TextBox focusedTextBox = System.Windows.Input.Keyboard.FocusedElement as TextBox;
            if (focusedTextBox != null)
            {
                BindingExpression textBindingExpr = focusedTextBox.GetBindingExpression(TextBox.TextProperty);
                if (textBindingExpr != null)
                {
                    textBindingExpr.UpdateSource();
                }
            }
            else
            {
                ComboBox focusedComboBox = System.Windows.Input.Keyboard.FocusedElement as ComboBox;
                if (focusedComboBox != null)
                {
                    BindingExpression selectedValueBindingExpr = focusedComboBox.GetBindingExpression(Selector.SelectedValueProperty);
                    if (selectedValueBindingExpr != null)
                    {
                        selectedValueBindingExpr.UpdateSource();
                    }
                    BindingExpression selectedIndexBindingExpr = focusedComboBox.GetBindingExpression(Selector.SelectedIndexProperty);
                    if (selectedIndexBindingExpr != null)
                    {
                        selectedIndexBindingExpr.UpdateSource();
                    }
                    BindingExpression selectedItemBindingExpr = focusedComboBox.GetBindingExpression(Selector.SelectedItemProperty);
                    if (selectedItemBindingExpr != null)
                    {
                        selectedItemBindingExpr.UpdateSource();
                    }
                }

            }

        }
    }
}
