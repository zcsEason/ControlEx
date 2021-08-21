﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ControlExTest
{
    public class RefreshCommand : ICommand
    {

        public event EventHandler CanExecuteChanged;

        public Action<object> action;
        public RefreshCommand(Action<object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action.Invoke(parameter);
        }
    }
}
