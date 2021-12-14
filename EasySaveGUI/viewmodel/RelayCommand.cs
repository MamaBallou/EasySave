using System;
using System.Diagnostics;
using System.Windows.Input;

namespace EasySaveGUI.views
{
    public sealed class RelayCommand : ICommand
    {
        #region Fields 
        readonly Action<object> m_execute;
        readonly Predicate<object> m_canExecute;
        #endregion // Fields 
        #region Constructors 
        /// <summary> 
        /// Creates a new command that can always execute. 
        /// </summary> 
        /// <param name="execute">The execution logic.</param> 

        public RelayCommand(Action<object> execute)
            : this(execute, null)
        { }
        /// <summary> 
        /// Creates a new command. 
        /// </summary> 
        /// <param name="execute">The execution logic.</param> 
        /// <param name="canExecute">The execution status logic.</param> 

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            this.m_execute = execute;
            this.m_canExecute = canExecute;
        }
        #endregion // Constructors 
        #region ICommand Members 
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return this.m_canExecute == null ? true : this.m_canExecute(parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public void Execute(object parameter)
        {
            this.m_execute(parameter);
        }
        #endregion // ICommand Members 
    }
}
