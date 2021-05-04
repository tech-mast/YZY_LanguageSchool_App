﻿using System;
using System.Windows.Input;

namespace YZYLibraryAzure
{
    public class YZYCommand : ICommand
    {
        Action _TargetExecuteMethod;
        Func<bool> _TargetCanExecuteMethod;

        public YZYCommand(Action executeMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = null;
        }

        public YZYCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _TargetExecuteMethod = executeMethod;
            _TargetCanExecuteMethod = canExecuteMethod;
        }

        public bool CanExecute(object parameter)
        {

            if (_TargetCanExecuteMethod != null)
            {
                return _TargetCanExecuteMethod();
            }

            if (_TargetExecuteMethod != null)
            {
                return true;
            }

            return false;
        }

        // Beware - should use weak references if command instance lifetime is longer than lifetime of UI obj

        // Prism commands solve this in their implementation 
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            if (_TargetExecuteMethod != null)
            {
                _TargetExecuteMethod();
            }
        }
    }
}